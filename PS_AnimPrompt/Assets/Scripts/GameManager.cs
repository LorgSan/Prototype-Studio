using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{   
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

    [Header("Scripts Setup")]
    [SerializeField] MazeRenderer mazeRenderer;
    [SerializeField] TiledMove player;

    [Header("Variables Setup")]
    [SerializeField] Text scoreText;
    [HideInInspector] public int score;

    void Start()
    {
        UpdateScore(score);
    }

    public void Restart()
    {
        LeanTween.cancelAll(true);
        player.transform.position = player.startPos;
    }

    public void NewMaze()
    {
        mazeRenderer.Generate();
        LeanTween.cancelAll(true);
        player.transform.position = player.startPos;
    }

    public void NewGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void EndGame()
    {
        SceneManager.LoadScene("EndScene");
    }

    public void UpdateScore(int sum)
    {
        score = sum;
        scoreText.text = score.ToString();
    }
}
