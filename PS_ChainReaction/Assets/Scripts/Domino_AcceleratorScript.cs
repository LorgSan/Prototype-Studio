using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Domino_AcceleratorScript : AcceleratorScript
{
    [HideInInspector] public Collider col;
    bool hasCollided;
    AudioManager am;
    MeshRenderer mr;
    [SerializeField] Material matReveal;

    // Start is called before the first frame update
    protected override void Start()
    {
        am = AudioManager.FindInstance();
        rb = GetComponent<Rigidbody>();
        mr = GetComponent<MeshRenderer>();
        col = GetComponent<BoxCollider>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if(!hasCollided && collision.gameObject.tag != "Floor")
        {
            hasCollided = true;
            rb.AddForce(collision.GetContact(0).normal, ForceMode.Impulse);
            mr.material = matReveal;
            am.PlayDing();
        }
    }

    
}
