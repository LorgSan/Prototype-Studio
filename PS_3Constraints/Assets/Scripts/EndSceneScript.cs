using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndSceneScript : GenericSingletonClass<EndSceneScript>
{
    public Text headerText;
    public Text score;
    public Text description;
    PlayerControls controls;

    public override void Awake()
    {
        base.Awake();
        controls = new PlayerControls();
        controls.Gameplay.RestartScene.performed += ctx => RestartScene();
    }

    void RestartScene()
    {
        Destroy(GameManager.Instance);
        SceneManager.LoadScene("SampleScene");
    }

    void Start()
    {
        if (GameManager.Instance.highScore >= 10)
        {
            headerText.text = "buba is full\ngood job";
            Instance.description.text = "you helped buba eat";
        }
        else 
        {
            Instance.headerText.text = "buba is sad\nstill hungy:(";
            Instance.description.text = "buba has only eaten";
        }
        Instance.score.text = GameManager.Instance.highScore.ToString();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Destroy(GameManager.Instance);
            SceneManager.LoadScene("SampleScene");
        }
    }
}
