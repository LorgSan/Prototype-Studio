using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcceleratorScript : MonoBehaviour
{
    public float thrust = 1.0f;
    public Rigidbody rb;
    enum Direction
    {
        north,
        west,
        south,
        east
    }

    [SerializeField] Direction direction;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
       Invoke("StartForce", 0.5f);
    }

    void StartForce()
    {
        switch (direction)
        {
            case Direction.north:
            rb.AddForce(0, 0, thrust, ForceMode.Impulse);
            break;
            case Direction.west:
            rb.AddForce(thrust, 0, 0, ForceMode.Impulse);
            break;
            case Direction.south:
            rb.AddForce(0, 0, -thrust, ForceMode.Impulse);
            break;
            case Direction.east:
            rb.AddForce(-thrust, 0, 0, ForceMode.Impulse);
            break;
        }
        
    }

    void Update()
    {
        if (gameObject.name == "Starter")
        {

        }
    }
}
