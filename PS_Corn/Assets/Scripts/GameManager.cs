using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    GameObject currentCob;
    PanScript panScript;
    public float cookTime = 5f;

    #region SingletonDeclaration 
    private static GameManager instance; 
    public static GameManager FindInstance()
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

    #region StateDeclaration
    [HideInInspector]
    public enum State //creating an enumeration of all the states we possess
    {
        emptyHands,
        cobDragged,
        butterDragged
    }

    private State _currentState; //this is our protection level that also states runs the "one-time" state switcher
    public State CurrentState
    {
        get
        {
            return _currentState;
        }
        set
        {
            _currentState = value;
            //TransitionStates(value);
        }
    }
    #endregion

    void Start()
    {
        panScript = PanScript.FindInstance();
    }
    void Update()
    {   

        if (Input.GetMouseButtonDown(0))
        {
            Collider2D col = Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            //Debug.Log(col);
            if (col!=null && col.gameObject.tag == "Basket" && currentCob==null) 
            {
                CurrentState = State.cobDragged;
                GameObject cob = Instantiate(Resources.Load("Corn") as GameObject);
                currentCob = cob; 
            }

            if (currentCob!=null && col.gameObject.tag == "Pan")
            {
                switch (CurrentState)
                {
                    case State.cobDragged:
                        panScript.CobPlace(currentCob);
                        currentCob = null;
                        CurrentState = State.emptyHands;
                        break;
                    case State.butterDragged:
                        break;
                }
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            Collider2D col = Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            if (col!=null & col.gameObject.tag == "Corn")
            {
                col.GetComponent<CornScript>().CobTurn();
            }
        }

            if (CurrentState == State.cobDragged)
            {
                UtilScript.MoveWithMouse(currentCob.transform, new Vector3(0,0,0));
            }

    }
}
