using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TongueTipTrigger : MonoBehaviour
{
    public TongueScript tongueScript;

    public Transform tongueTipPos;

    private void Update()
    {
        transform.position = tongueTipPos.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Fly")
        {
            tongueScript.ResetTonguePos();
        }
    }
}
