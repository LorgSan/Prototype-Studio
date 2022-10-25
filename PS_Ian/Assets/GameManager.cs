using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] BoxCollider2D col;
    [SerializeField] Text inputField;
    [SerializeField] string replaceKey;
    [SerializeField] Text prompt;
    [SerializeField] Text output;
    int promptCounter = 0;

    string startText;

    bool inField;
    GameObject current;

    void Start()
    {
        startText = inputField.text;
        UpdatePrompt();
    }

    void UpdatePrompt()
    {
        prompt.text = prompts[promptCounter];
        prompt.text = prompt.text.Replace("\\n", "\n");
        inputField.text = startText;
    }
    
    void OnTriggerEnter2D(Collider2D col)
    {
        GameObject go = col.gameObject;
        if (go.tag == "Button")
        {
            current = go;
            inField = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        inField = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0) && inField == true)
        {
            Debug.Log("Collided");
            string value;
            string addition = current.transform.GetChild(1).GetComponent<InputField>().text;
            if (replaceKey == "// Your code goes here")
            {
                value = inputField.text.Replace(replaceKey, addition);
            } else if (replaceKey.Length >= 25)
                {
                    value = inputField.text.Replace(replaceKey, replaceKey + "\n" + addition);
                } else 
                {
                    value = inputField.text.Replace(replaceKey, replaceKey + addition);
                }
            replaceKey = value;
            inputField.text = value;
            inputField.text = inputField.text.Replace("\\n", "\n");
            Destroy(current);
        }

        if (Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().ToString());
        }
    }

    public void Empty()
    {
        inputField.text = startText;
    }

    public bool isEven (int numberToCheck)
    {
        return numberToCheck % 2 == 0;
    }

    public void Run()
    {
        Debug.Log(results[promptCounter] + inputField.text);
        if (inputField.text == results[promptCounter])
        {
            output.text = output.text + outputs[promptCounter];
            output.text = output.text.Replace("\\n", "\n");
            promptCounter++;
            UpdatePrompt();
        } else {
            Debug.Log(errorOutput.Length);
            output.text = output.text + errorOutput[Random.Range(0, errorOutput.Length-1)];
            output.text = output.text.Replace("\\n", "\n");
        }
    }

    public string[] prompts = new string[] {
        "public int sum( int x, int y) {",
        "public int maximum(Integer[] list) {",
        "",
        "",
        ""
    };

    public string[] results = new string[] {
        "return(x+y);",
        ""
    };

    public string[] outputs = new string[] {

    };

    public string[] errorOutput = new string[] {
        "try again, you can do it!",
        "hmm, did you check the semicolon?",
    };
}
