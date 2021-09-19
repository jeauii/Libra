using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMovement : MonoBehaviour
{
    private Rigidbody rb;

    public float speed;
    private bool onGround;
    
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        FindObjectOfType<CameraMovement>().Reset();
    }
    
    void Start()
    {
        onGround = false;
    }
    
    void Update()
    {
        if (!onGround)
        {
            transform.position += 
                Vector3.down * speed * Time.deltaTime;
        }
    }

    void OnCollisionEnter()
    {
        onGround = true;
        rb.useGravity = true;
        rb.isKinematic = false;
    }

    void OnDestroy()
    {
        CameraMovement view = FindObjectOfType<CameraMovement>();
        if (view)
            view.Reset();
    }
}
