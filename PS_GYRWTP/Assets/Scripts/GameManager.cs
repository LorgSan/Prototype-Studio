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
    bool lesson = false;
    void Start()
    {
        normalColor = Camera.main.backgroundColor;
    }
   
    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
        {
            angerValue = angerValue+step;
            Debug.Log(angerValue);
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
        } else audioSource.Stop();

        if (anger == 1)
        {   
            if (scream.isPlaying == false)
            {
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
    }
}
