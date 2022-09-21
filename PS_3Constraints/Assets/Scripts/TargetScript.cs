using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScript : MonoBehaviour
{

    GameObject currentCollider;

    public bool CheckFly()
    {
        Collider2D col = Physics2D.OverlapPoint(transform.position);
        if (col!=null)
        {
            if (col.gameObject.tag == "Fly" || col.gameObject.tag == "Target")
            {
                Debug.Log(col.gameObject.tag);
                return true;
            }

            return false;
        }
         return false;
    }
}
