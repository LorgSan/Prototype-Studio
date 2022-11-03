using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRotate : MonoBehaviour
{

    IEnumerator RotateMe(Vector3 byAngles, float inTime) 
    {    
        var fromAngle = transform.rotation;
        var toAngle = Quaternion.Euler(transform.eulerAngles + byAngles);
        for(var t = 0f; t <= 1; t += Time.deltaTime/inTime) 
        {
            transform.rotation = Quaternion.Slerp(fromAngle, toAngle, t);
            yield return null;
        }
        transform.rotation = toAngle;
    }
    public void Rotate (bool goingBack)
    {
        if (goingBack)
        {
            StartCoroutine(RotateMe(Vector3.up * -90, 0.8f));
        } else StartCoroutine(RotateMe(Vector3.up * 90, 0.8f));
   }
 }
