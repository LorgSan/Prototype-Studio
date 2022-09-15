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
    [SerializeField] float[] cobRot;
    List<CornScript> cobs = new List<CornScript>();
    [SerializeField] AudioClip readySound;

    bool cobWasCooked = false;

    void Update()
    {
        for(int x = 0; x < cobs.Count; x++)
        {
            CornScript currentCob = cobs[x];
            if (currentCob.sidesReady >= 4)
            {
                int index = cobs.IndexOf(currentCob);
                cobs[index] = null;
                Destroy(currentCob.gameObject);
                cobWasCooked=true;
                GetComponent<AudioSource>().PlayOneShot(readySound);
            }
        }
    }

    public void CobPlace(GameObject cob)
    {
        if (cobs.Count <= cobPos.Length)
        {
            CornScript cornScript = cob.GetComponent<CornScript>();

            if (cobWasCooked==true)
            {
                for(int x = 0; x < cobs.Count; x++)
                {
                    if (cobs[x] == null)
                    {
                        cobs[x] = cornScript;
                    }
                    else cobs.Add(cornScript);
                }
            } else cobs.Add(cornScript);

            int position = cobs.IndexOf(cornScript);
            cornScript.CurrentSide = CornScript.Side.Top;
            cornScript.audioController.Play();
            cornScript.isPlaced = true;
            Transform cobTransform = cob.transform;
            cobTransform.position = cobPos[position];
            cobTransform.rotation = Quaternion.Euler(0f,0f,cobRot[position]);
            cob.GetComponent<PolygonCollider2D>().enabled = true;
            cobTransform.parent = this.transform;

        } else 
        
        Destroy(cob);
    }

    void AvailabilityCheck()
    {
    
    }
    public void ButterPlace()
    {
        Debug.Log("butter placed");
    }

}
