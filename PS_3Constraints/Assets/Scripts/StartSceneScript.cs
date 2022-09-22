using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneScript : MonoBehaviour
{
    PlayerControls controls;

    void Awake()
    {
        controls = new PlayerControls();
        controls.Gameplay.NextScene.performed += ctx => GameStart();
    }

    void GameStart()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
