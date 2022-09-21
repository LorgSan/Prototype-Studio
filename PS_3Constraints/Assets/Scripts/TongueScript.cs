using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TongueScript : MonoBehaviour
{
    public Transform target, tongueTipPos, tongueChildObj;

    public float speed = 300f;

    private Vector3 initialScale;
    
    // Start is called before the first frame update
    void Start()
    {
        //Saving the initial scale of the tongueChildObj for later use
        initialScale = tongueChildObj.transform.localScale;
    }

    void Update()
    {
        //Checking if the target is not null (Is present in the scene and not destroyed)
        if(target != null){

            //Here calulating the angle and rotating the tongue
            float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x -transform.position.x ) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, speed * Time.deltaTime);

            //if the target is in the range of the tongue
            if ((target.position.x > -6.5f && target.position.x < 6.5f) && (target.position.y > -1.5f && target.position.y < 3.6f))
            {
                ScaleTongue();
            }
        }
    }

    void ScaleTongue()
    {
        //Calculating the distance between the tongue tip and the target
        float distance = Vector3.Distance(tongueTipPos.position, target.position);
        Debug.Log(distance);

        //Scaling the tongue till the distance is greater than 0.2f
        if (distance > 0.2f)
        {
            tongueChildObj.transform.localScale += new Vector3(0, 0.1f, 0);
        }
        else
        {
            //Destroy the target and reset the tongue
            GameObject.Destroy(target.gameObject);
            tongueChildObj.transform.localScale = initialScale;
        }
    }
}
