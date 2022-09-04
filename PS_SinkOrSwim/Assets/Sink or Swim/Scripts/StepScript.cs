using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StepScript : MonoBehaviour
{
    #region VarDeclaration
    float timeLeft;
    [SerializeField] float timeTooSoon = 2;
    [SerializeField] float timeToWait = 4;
    [SerializeField] Text debugText;
    bool isRunning = false;
    #endregion
    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void Start()
    {
        CurrentState = StepState.Start;
    }
    void Update()
    {
        InputCheck();
        //Debug.Log(timeLeft);
        //float timeInSec = timeLeft%60;
        debugText.text = timeLeft.ToString();
        //Debug.Log(Mathf.RoundToInt(timeLeft));
    }

    #region StepStateMachine
    public enum StepState
    {
        Start,
        LeftStep,
        RightStep,
        Fall
    }

    private StepState currentState;
    public StepState CurrentState
    {
        get
        {
            return currentState;
        }
        set
        {
            currentState = value;
            TransitionStepState(value);
        }
    }

    public void TransitionStepState (StepState newState)
    {
        currentState = newState;
        switch (newState)
        {
            case StepState.LeftStep:
                Debug.Log("Left step done");
                break;
            case StepState.RightStep:
                Debug.Log("Right step done");
                break;
            case StepState.Fall:
                Debug.Log("You fell!");
                break;
            default:
                //Debug.Log("this state doesn't exist");
                break;
        }
    }
    #endregion

    void InputCheck()
    {
        if (Input.GetKey(KeyCode.R))
        {
            RestartScene();
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (CurrentState == StepState.RightStep || CurrentState == StepState.Start)
            {
                var isOnTime = CoroutineChecker();
                if (isOnTime == true)
                {
                    CurrentState = StepState.LeftStep;
                    if (isRunning == true)
                        {
                            StopCoroutine("StepDelay");
                        }
                    StartCoroutine(StepDelay(timeToWait));
                }
                else CurrentState = StepState.Fall;
            } else CurrentState = StepState.Fall;
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (CurrentState == StepState.LeftStep || CurrentState == StepState.Start)
            {
                var isOnTime = CoroutineChecker();
                if (isOnTime == true)
                {
                    CurrentState = StepState.RightStep;
                    if (isRunning == true)
                        {
                            StopCoroutine("StepDelay");
                        }
                    StartCoroutine(StepDelay(timeToWait));
                }
                else CurrentState = StepState.Fall;
            } else CurrentState = StepState.Fall;
        }
    }

    bool CoroutineChecker()
    {
        bool isOntime;
        if (timeLeft >= timeTooSoon)
        {
            isOntime = false;
        } else
        isOntime = true;
        return isOntime;
    }
    
    IEnumerator StepDelay (float timeToWait)
    {
        isRunning = true;
        for(timeLeft = timeToWait; timeLeft > 0; timeLeft -= Time.deltaTime)
        yield return null;
        CurrentState = StepState.Fall;
        Debug.Log("coroutine ended");
        isRunning = false;
    }
}
