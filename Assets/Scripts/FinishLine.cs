using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            Rigidbody rb = collider.attachedRigidbody;
            rb.useGravity = false;
            rb.AddRelativeForce(Vector3.forward, ForceMode.Impulse);
        }
    }
}
