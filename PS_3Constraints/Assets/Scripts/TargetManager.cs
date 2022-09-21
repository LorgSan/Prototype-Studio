using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    #region SingletonDeclaration 
    private static TargetManager instance; 
    public static TargetManager FindInstance()
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
    public static int ColliderCount = 0;

    public void Count()
    {
        ColliderCount++;
        if (ColliderCount == 2)
        {
            Debug.Log("two collisions hit");
        }
    }
}
