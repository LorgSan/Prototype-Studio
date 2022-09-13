using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanScript : MonoBehaviour
{
    #region SingletonDeclaration 
    private static PanScript instance; 
    public static PanScript FindInstance()
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

    [SerializeField] Vector3[] cobPos;
    List<GameObject> cobs = new List<GameObject>();

    void Start()
    {

    }

    public void CobPlace(GameObject cob)
    {
        if (cobs.Count <= cobPos.Length)
        {
            cobs.Add(cob);
            int position = cobs.IndexOf(cob);
            cob.transform.parent = this.transform;
            cob.transform.position = cobPos[position];
            cob.transform.localScale = new Vector3 (0.1521074f,0.1521074f,0.1521074f);
            if (UtilScript.IsEven(position))
            {
                cob.transform.rotation = Quaternion.Euler(0f,0f,-90f);
            }
        } else Debug.Log("no space");
    }
    public void ButterPlace()
    {
        Debug.Log("butter placed");
    }

}
