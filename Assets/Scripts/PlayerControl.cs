using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private Rigidbody rb;
    private Animator anim;
    public Transform ground;
    
    public float walkSpeed;
    public float rollSpeed;
    private bool hasControl;
    private bool cubeContact;
    private bool cubeIsNormal;
    private Vector3 cubeSurface;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        hasControl = true;
        cubeContact = false;
    }
    
    void Update()
    {
        CheckAnimation();
    }

    void FixedUpdate()
    {
        if (hasControl)
            CheckMovement();
    }

    void OnCollisionEnter(Collision collision)
    {
        hasControl = true;
        if (collision.collider.tag == "Cube")
        {
            cubeContact = CheckContacts(collision);
            rb.useGravity = !cubeContact;
        }
    }

    void OnCollisionStay(Collision collision)
    {
        hasControl = true;
        if (collision.collider.tag == "Cube")
        {
            cubeContact = CheckContacts(collision);
            rb.useGravity = !cubeContact;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        hasControl = false;
        if (collision.collider.tag == "Cube")
        {
            cubeContact = false;
            rb.useGravity = true;
        }
    }

    bool CheckContacts(Collision collision)
    {
        for (int i = 0; i < collision.contactCount; ++i)
        {
            Vector3 normal = collision.GetContact(i).normal;
            Collider other = collision.GetContact(i).otherCollider;
            if (Vector3.Angle(normal, Vector3.up) > 45 &&
                Vector3.Angle(normal, transform.forward) > 90)
            {
                cubeIsNormal = other.attachedRigidbody.mass == 1;
                cubeSurface = Vector3.Cross(ground.forward, normal);
                if (cubeSurface.y < 0)
                    cubeSurface = -cubeSurface;
                Debug.Log(cubeIsNormal + " " + cubeSurface.ToString());
                return true;
            }
        }
        return false;
    }

    void CheckMovement()
    {
        float xAxis = Input.GetAxis("Horizontal");
        bool mode = Input.GetKey(KeyCode.LeftShift);
        if (xAxis != 0)
        {
            float speed = mode ? rollSpeed : walkSpeed;
            Quaternion rot = Quaternion.LookRotation(
                xAxis > 0 ? ground.right : -ground.right);
            rb.MoveRotation(rot);
            Vector3 dir = Vector3.zero;
            if (!cubeContact) dir = transform.forward;
            else if (mode) dir = cubeSurface;
            else if (!cubeIsNormal) dir = transform.forward;
            Vector3 step = dir * Mathf.Abs(xAxis);
            Vector3 newPos = transform.position +
                step * speed * Time.fixedDeltaTime;
            rb.MovePosition(newPos);
        }
        else
        {
            rb.MoveRotation(ground.rotation);
        }
    }

    void CheckAnimation()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            anim.SetBool("Walk_Anim", true);
            if (Input.GetKey(KeyCode.LeftShift))
            {
                anim.SetBool("Roll_Anim", true);
                anim.SetBool("Walk_Anim", false);
            }
            else if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                anim.SetBool("Roll_Anim", false);
            }
        }
        else if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            anim.SetBool("Walk_Anim", false);
            anim.SetBool("Roll_Anim", false);
        }
        
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (!anim.GetBool("Open_Anim"))
            {
                anim.SetBool("Open_Anim", true);
            }
            else
            {
                anim.SetBool("Open_Anim", false);
            }
        }
    }

}
