using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TongueScript : MonoBehaviour
{
    public Transform target, tongueTipPos, tongueChildObj;

    public float speed = 50.0f;

    private Vector3 initialScale;
    
    // Start is called before the first frame update
    void Start()
    {
        initialScale = tongueChildObj.transform.localScale;
    }

    void Update()
    {
        float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x -transform.position.x ) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, speed * Time.deltaTime);

        ScaleTongue();
    }


    void ScaleTongue()
    {
        //Scale the tongueChildObj to the distance between the tongueTipPos and the target
        float distance = Vector3.Distance(tongueTipPos.position, target.position);
        
        Debug.Log(distance);

        //Keep scaling the tongueChildObj until the distance is less than 0.1
        if (distance > 0.1f)
        {
            tongueChildObj.transform.localScale += new Vector3(0, 0.001f, 0);
        }
    }

}
