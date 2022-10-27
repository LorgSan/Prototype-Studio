using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    #region SingletonDeclaration 
    private static AudioManager instance; 
    public static AudioManager FindInstance()
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

    public AudioSource audioSource;
    [SerializeField] AudioClip ding;
    [SerializeField] float starterPitch;
    [SerializeField] float step;

    void Start()
    {
        audioSource.pitch = starterPitch;
    }

    public void PlayDing()
    {
        audioSource.pitch += step;
        audioSource.PlayOneShot(ding);
    }
}
