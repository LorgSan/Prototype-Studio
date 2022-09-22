using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : GenericSingletonClass<GameManager>
{
    public float highScore = 0;
    [SerializeField] float timer;
    [SerializeField] Text timerText;
    bool newScene = false;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (newScene == false)
        {
            timer -= Time.deltaTime;
            timerText.text = timer.ToString();
            if (timer <= 0.0f)
            {
                timerEnded();
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
 
    void timerEnded()
    {
        newScene = true;
        SceneManager.LoadScene("EndScene");
    }
}
