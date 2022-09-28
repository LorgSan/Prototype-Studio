using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public float anger;
    public float step;
    public float angerDecrease;
    float angerValue;
    public Color angerColor;
    Color normalColor;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioSource ambient;
    [SerializeField] AudioSource scream;
    [SerializeField] AudioSource ghostWhisper;
    [SerializeField] SpriteRenderer screamSprite;
    [SerializeField] Sprite[] ghosts;
    bool lesson = false;
    bool red;
    void Start()
    {
        normalColor = Camera.main.backgroundColor;
        GhostStarter();
    }
   
    // Update is called once per frame
    void Update()
    {
        BackColorManager();
        //GhostDestroy();
    }

    void BackColorManager()
    {
            if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
            {
                angerValue = angerValue+step;
                //Debug.Log("Mouse moving");
            } 
            if (lesson != true)
            {
                angerValue = angerValue-angerDecrease;
            }
            
            anger = Mathf.Clamp(angerValue, 0, 1);
            Color lerpedColor = Color.Lerp(normalColor, angerColor, anger);
            Camera.main.backgroundColor = lerpedColor;

            if (anger != 0)
            {
                if (audioSource.isPlaying == false)
                {
                    audioSource.Play();
                }
                audioSource.volume = anger;
                Color alphaColor = new Color (1, 1, 1, anger);
                screamSprite.color = alphaColor;
            } else audioSource.Stop();

            if (anger == 1)
            {   
                if (scream.isPlaying == false)
                {
                    CancelInvoke();
                    ghostWhisper.Stop();
                    scream.Play();
                    lesson = true;
                    StartCoroutine(Lesson(5f));
                }
            } else scream.Stop();
            
        }
    IEnumerator Lesson(float WaitForSeconds)
    {
        yield return new WaitForSeconds(WaitForSeconds);
        lesson = false; 
        yield return new WaitForSeconds (3f);
        GhostStarter();
    }

    void GhostStarter()
    {
        InvokeRepeating("GhostSpawner", 0f, 0.2f);
        ghostWhisper.Play();
    }

    void GhostSpawner()
    {
        GameObject ghost = Instantiate(Resources.Load("Ghost") as GameObject);
        ghost.GetComponent<SpriteRenderer>().sprite = ghosts[Random.Range(0,4)];
        ghost.transform.position = new Vector2 (Random.Range(-5.5f, 5.5f), Random.Range(1.6f, -1.8f));
        ghost.transform.parent = transform;
    }

}
