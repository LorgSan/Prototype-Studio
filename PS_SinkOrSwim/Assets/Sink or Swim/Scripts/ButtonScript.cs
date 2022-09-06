using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    [SerializeField] Transform target;
    StepScript stepScript;
    public bool isRunning;
    Transform locator;
    Vector3 locatorScale;
    SpriteRenderer locatorSR;
    GameObject button;
    // Start is called before the first frame update
    void Start()
    {
        var buttonParent = transform.parent.gameObject;
        stepScript = buttonParent.transform.parent.GetComponent<StepScript>();
        button = transform.GetChild(0).gameObject;
        locator = button.transform.GetChild(1).transform;
        locatorSR = locator.GetComponent<SpriteRenderer>();
        locatorScale = locator.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target);
        if (isRunning == true)
        {   
            ButtonStarter();
        }
        
    }
    public void ButtonReset()
    {
        locator.localScale = locatorScale;
        button.SetActive(true);
    }

    public void ButtonStarter()
    {
        Vector3 newScale = new Vector3 (-0.00065f, -0.0003f, locator.localScale.z);
        locator.localScale = Vector3.MoveTowards(locator.localScale, newScale, 0.1f);
    }
}
