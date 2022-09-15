using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CornScript : MonoBehaviour
{   
    GameManager myManager;
    PanScript myPan;
    Transform child;
    float[] readiness = new float[4] {0, 0, 0, 0};
    [SerializeField] Color rawCorn;
    [SerializeField] Color cookedCorn;
    float[] colorSteps;
    public bool isPlaced = false;
    Color currentColor;
    Material[] mats;
    public int sidesReady = 0;
    [HideInInspector] public AudioSource audioController;
    [SerializeField] AudioClip turnSound;

    #region StateMachine
    public enum Side
    {
        Top,
        Side1,
        Bottom,
        Side2
    }
    private Side _currentSide; //this is our protection level that also states runs the "one-time" state switcher
    public Side CurrentSide
    {
        get
        {
            return _currentSide;
        }
        set
        {
            _currentSide = value;
        }
    }
    #endregion
    void Start()
    {   
        myManager = GameManager.FindInstance();
        myPan = PanScript.FindInstance();
        child = transform.GetChild(0);
        mats = child.GetComponent<MeshRenderer>().materials;
        audioController = GetComponent<AudioSource>();
        StepCalculator(rawCorn, cookedCorn, myManager.cookTime);
    }
    void Update()
    {
        if (isPlaced==true)
        {
            RunStates();
        }
    }

    private void RunStates()
    {
        switch (CurrentSide)
        {
            case Side.Top:
            if (readiness[0] <= myManager.cookTime)
            {
                readiness[0] += Time.deltaTime;
            }
            Color topColor = Color.Lerp(rawCorn, cookedCorn, readiness[0]/myManager.cookTime);
            mats[1].color = topColor;
                break;
            case Side.Side1:
            if (readiness[1] <= myManager.cookTime)
            {
                readiness[1] += Time.deltaTime;
            }
            Color Side1Color = Color.Lerp(rawCorn, cookedCorn, readiness[1]/myManager.cookTime);
            mats[0].color = Side1Color;
                break;
            case Side.Bottom:
            if (readiness[2] <= myManager.cookTime)
            {
                readiness[2] += Time.deltaTime;
            }
            Color BottomColor = Color.Lerp(rawCorn, cookedCorn, readiness[2]/myManager.cookTime);
            mats[3].color = BottomColor;
                break;
            case Side.Side2:
            if (readiness[3] <= myManager.cookTime)
            {
                readiness[3] += Time.deltaTime;
            }
            Color Side2Color = Color.Lerp(rawCorn, cookedCorn, readiness[3]/myManager.cookTime);
            mats[2].color = Side2Color;
                break;
        }
    }

    float[] StepCalculator(Color color1, Color color2, float time)
    {
        float rStep = (color1.r - color2.r)/time;
        float gStep = (color1.g - color2.g)/time;
        float bStep = (color1.b - color2.b)/time;
        var colorSteps = new float[] { rStep, gStep, bStep };
        return colorSteps;
    }

    void CheckCob()
    {
        sidesReady = 0;
        for(int i = 0; i < readiness.Length; i++)
        {
            if (readiness[i] > myManager.cookTime)
            {
                sidesReady++;
                
            }
        }

        if (sidesReady >= 4)
        {
            myPan.DestroyCob();
        }
    }

    public void CobTurn()
    {
        audioController.PlayOneShot(turnSound);
        Vector3 childRot = child.rotation.eulerAngles;
        child.rotation = Quaternion.Euler(
            childRot.x, 
            childRot.y, 
            childRot.z+90f);
        if (childRot.z >= 360)
        {
            child.rotation = Quaternion.Euler(childRot.x, childRot.y, 0);
        }
        switch (CurrentSide)
        {
            case Side.Top:
                CurrentSide = Side.Side1;
                break;
            case Side.Side1:
                CurrentSide = Side.Bottom;
                break;
            case Side.Bottom:
                CurrentSide = Side.Side2;
                break;
            case Side.Side2:
                CurrentSide = Side.Top;
                break;
        }
        CheckCob();
    }
}
