using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] float[] cobRot;
    [SerializeField] List<CornScript> cobs = new List<CornScript>();
    [SerializeField] AudioClip readySound;
    [SerializeField] Text scoreText;
    float score;
    GameManager myManager;
    bool cobWasCooked = false;

    void Start()
    {
        myManager = GameManager.FindInstance();
    }

    public void DestroyCob()
    {
        for(int x = 0; x < cobs.Count; x++)
        {
            if (cobs[x] != null)
            {
                CornScript currentCob = cobs[x];
                if (currentCob.sidesReady >= 4)
                {
                    int index = cobs.IndexOf(currentCob);
                    cobs[index] = null;
                    //CheckCollider();
                    Destroy(currentCob.gameObject);
                    cobWasCooked=true;
                    GetComponent<AudioSource>().PlayOneShot(readySound);
                    score++;
                    scoreText.text = score.ToString();
                }
            }
        }
    }
    public void CobPlace(GameObject cob)
    {
        if (cobs.Count < 4 || anyNull() == true) //if the amount of cobs is not more than 4 and none of them are null
        {
            CornScript cornScript = cob.GetComponent<CornScript>(); //we save its' cornscript

            if (cobWasCooked==true) //if cobs were at some point cooked
            {
                for(int x = 0; x < cobs.Count; x++)
                {
                    if (cobs[x] == null) //we're finding the created null element in the array of elements
                    {
                        cobs[x] = cornScript; //if it is null we save the generated cob as this element in the array
                        CobSetup(cob, cornScript); //and perform it's setup
                        break;
                    } else if (cobs.Count < 4) //otherwise, if none of them a
                    {
                        cobs.Add(cornScript);
                        CobSetup(cob, cornScript);
                    }
                }

            } else if (cobs.Count < 4)
            {
                cobs.Add(cornScript);
                CobSetup(cob, cornScript);
            } else Destroy(cob);

        } else Destroy(cob);
    }
    bool anyNull()
    {
        bool isNull = false;
        for(int x = 0; x < cobs.Count; x++)
        {
            if (cobs[x] == null)
            {
                isNull = true;
            }
        }
        return isNull;
    }
    void CobSetup(GameObject cob, CornScript cornScript)
    {
        int position = cobs.IndexOf(cornScript);
        cornScript.CurrentSide = CornScript.Side.Top;
        cornScript.audioController.Play();
        cornScript.isPlaced = true;
        Transform cobTransform = cob.transform;
        cobTransform.position = cobPos[position];
        cobTransform.rotation = Quaternion.Euler(0f,0f,cobRot[position]);
        cob.GetComponent<PolygonCollider2D>().enabled = true;
        cobTransform.parent = this.transform;
    }
}
