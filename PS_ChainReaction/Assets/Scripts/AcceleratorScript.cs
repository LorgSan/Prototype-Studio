using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcceleratorScript : MonoBehaviour
{
    public float thrust = 1.0f;
    [HideInInspector] public Rigidbody rb;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(0, 0, thrust, ForceMode.Impulse);
    }
}
