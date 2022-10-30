using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Domino_AcceleratorScript : AcceleratorScript
{
    [HideInInspector] public Collider col;
    bool hasCollided;
    AudioManager am;
    MeshRenderer mr;
    AudioSource audioSource;
    public Material matReveal;
    [HideInInspector] public bool isTheLast = false;
    [HideInInspector] public string nextSceneName;

    // Start is called before the first frame update
    protected override void Start()
    {
        am = AudioManager.FindInstance();
        rb = GetComponent<Rigidbody>();
        mr = GetComponent<MeshRenderer>();
        col = GetComponent<BoxCollider>();
        audioSource = GetComponent<AudioSource>();
    }

    protected override void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if(!hasCollided && collision.gameObject.tag != "Floor")
        {
            hasCollided = true;
            //collision.gameObject.GetComponent<Rigidbody>().AddForce(collision.GetContact(0).normal, ForceMode.Impulse);
            rb.AddForce(collision.GetContact(0).normal, ForceMode.Impulse);
            mr.material = matReveal;
            am.PlayDing(audioSource);
            if (isTheLast)
            {
                Invoke("NextScene", 3);
            }
        }

        if (collision.gameObject.tag == "Starter")
        {
            //Debug.Log("collided with the starter");
            collision.gameObject.GetComponent<Rigidbody>().mass = 0;
        }
    }

    void NextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }

    
}
