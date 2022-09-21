using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyScript : GenericSingletonClass<FlyScript>
{

    public bool RightEye = false;
    public bool LeftEye = false;
    void OnTriggerEnter2D(Collider2D col) //we just check if we hit the right collider when the ballot is dragged
    {
        if (col.gameObject.tag == "LeftEye")
        {
            LeftEye = true;
        }

        if (col.gameObject.tag == "RightEye")
        {
            RightEye = true;
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "LeftEye")
        {
            LeftEye = false;
        }

        if (col.gameObject.tag == "RightEye")
        {
            RightEye = false;
        }
    }
}
