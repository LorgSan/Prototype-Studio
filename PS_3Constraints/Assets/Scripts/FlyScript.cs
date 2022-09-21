using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyScript : MonoBehaviour
{
    #region SingletonDeclaration 
    private static FlyScript instance; 
    public static FlyScript FindInstance()
    {
        return instance; //that's just a singletone as the region says
    }

    void Awake() //this happens before the game even starts and it's a part of the singletone
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else if (instance == null)
        {
            //DontDestroyOnLoad(this);
            instance = this;
        }
    }
    #endregion

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
