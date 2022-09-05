using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseOverScript : MonoBehaviour
{
    StepScript parent;
    void Start()
    {
        parent = transform.parent.gameObject.GetComponent<StepScript>();
    }
    private void OnMouseOver()
    {
        parent.hitObject = gameObject;
        Debug.Log(gameObject.name);
    }
}
