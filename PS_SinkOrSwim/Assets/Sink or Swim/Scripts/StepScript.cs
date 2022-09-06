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
    
    public GameObject hitObject;
    bool isRunning = false;
    private Coroutine StepCoroutine;
    Animator anim;
    #endregion
    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void Start()
    {
        anim = GetComponent<Animator>();
        CurrentState = StepState.Start;
    }
    void Update()
    {
        InputCheck();
        debugText.text = timeLeft.ToString();
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
            anim.SetTrigger("Start");
            if (hitObject != null && hitObject.tag == "LeftLeg")
            {
                //Debug.Log("left leg");
                StepChecker (StepState.RightStep, StepState.LeftStep);
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (hitObject != null && hitObject.tag == "RightLeg")
            {
                //Debug.Log("right leg");
                StepChecker(StepState.LeftStep, StepState.RightStep);
            }
        }
    }

    void StepChecker(StepState previousStep, StepState currentStep)
    {
        if (CurrentState == previousStep || CurrentState == StepState.Start)
        {
            var isOnTime = CoroutineChecker();
            if (isOnTime == true)
            {
                anim.SetBool("RightLeftSwitcher", !anim.GetBool("RightLeftSwitcher"));
                CurrentState = currentStep;
                if (isRunning == true)
                    {
                        StopCoroutine(StepCoroutine);
                        isRunning = false;
                    } 
                StepCoroutine = StartCoroutine(StepDelay(timeToWait));
            }
            else CurrentState = StepState.Fall;
        } else CurrentState = StepState.Fall;
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
