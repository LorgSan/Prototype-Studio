using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CornScript : MonoBehaviour
{   
    GameManager myManager;
    Transform child;
    float readinessTop;
    float readinessSide1;
    float readinessBottom;
    float readinessSide2;
    float[] readiness = new float[4] {0, 0, 0, 0};

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
            //TransitionStates(value);
        }
    }
    #endregion

    private void RunStates()
    {
        switch (CurrentSide)
        {
            case Side.Top:
            readiness[0] += Time.deltaTime;
                break;
            case Side.Side1:
            readiness[1] += Time.deltaTime;
                break;
            case Side.Bottom:
            readiness[2] += Time.deltaTime;
                break;
            case Side.Side2:
            readiness[3] += Time.deltaTime;
                break;
        }
    }

    void Start()
    {   
        myManager = GameManager.FindInstance();
        child = transform.GetChild(0);
        CurrentSide = Side.Top;
    }
    void Update()
    {
        RunStates();
        CheckCob();
    }

    void CheckCob()
    {
        for(int i = 0; i < readiness.Length; i++)
        {
            if (readiness[i] >= myManager.cookTime)
            {
                Debug.Log("the side is ready");
            }
        }
    }
    

    public void CobTurn()
    {
        child.rotation = Quaternion.Euler(
            child.rotation.eulerAngles.x, 
            child.rotation.eulerAngles.y + 90f, 
            child.rotation.eulerAngles.z);
    }

    Side GetSide()
    {
        float yRot = transform.rotation.eulerAngles.y;
        Side tempSide;
        switch (yRot)
        {
            case float i when i >= 0 && i <= 90:
                Debug.Log(yRot);
                tempSide = Side.Top;
                break; 
            case float i when i > 90 && i <=180:
                Debug.Log(yRot);
                tempSide = Side.Side1;
                break;
            case float i when i > 180 && i <= 270:
                Debug.Log(yRot);
                tempSide = Side.Bottom;
                break;
            case float i when i > 270 && i <= 360:
                Debug.Log(yRot);
                tempSide = Side.Side2;
                break;
            default:
                tempSide = Side.Top;
                break;
        }
        return tempSide;
    }
}
