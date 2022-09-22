using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlsScript : GenericSingletonClass<ControlsScript>
{
    PlayerControls controls;
    Vector2 rightEyeMove;
    Vector2 leftEyeMove;

    public override void Awake() //this happens before the game even starts and it's a part of the singletone
    {
        base.Awake();
        controls = new PlayerControls();
        ControlsSetup();
        DontDestroyOnLoad(this.gameObject);
    }
    void ControlsSetup()
    {
        controls.Gameplay.RestartScene.performed += ctx => RestartScene();
        controls.Gameplay.RightEye.performed += ctx => rightEyeMove = ctx.ReadValue<Vector2>();
        controls.Gameplay.RightEye.canceled += ctx => rightEyeMove = Vector2.zero;
        controls.Gameplay.LeftEye.performed += ctx => leftEyeMove = ctx.ReadValue<Vector2>();
        controls.Gameplay.LeftEye.canceled += ctx => leftEyeMove = Vector2.zero;
        controls.Gameplay.Catch.performed += ctx => CheckFly(); 
        controls.Gameplay.NextScene.performed += ctx => StartGame();
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "SampleScene")
        {
            FrogScript.Instance.rightEyeMove = rightEyeMove;
            FrogScript.Instance.leftEyeMove = leftEyeMove;
        }
    }

    void CheckFly()
    {
        if (SceneManager.GetActiveScene().name == "SampleScene")
        {
            FrogScript.Instance.CheckFly();
        }
        else Debug.Log("check fly failed");
    }

    void RestartScene()
    {
        Destroy(GameManager.Instance.gameObject);
        SceneManager.LoadScene("StartScene");
    }

    void StartGame()
    {
        if (SceneManager.GetActiveScene().name == "StartScene")
        {
            SceneManager.LoadScene("SampleScene");
        }
    }

    void OnEnable()
    {
        controls.Gameplay.Enable();
    }

    void OnDisable()
    {
        //controls.Gameplay.Disable();
    }
}
