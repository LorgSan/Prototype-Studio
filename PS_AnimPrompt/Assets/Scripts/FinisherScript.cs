using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinisherScript : MonoBehaviour
{
    [SerializeField] GameManager myManager;
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            myManager.UpdateScore(myManager.score + 1);
            myManager.NewMaze();
        }
    }
}
