using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiledMove : MonoBehaviour
{
    float step;
    bool MovementAllowed = true;
    [HideInInspector] public Vector3 startPos;
    
    [Header("PlayerSettings")]
    [SerializeField] float speed;

    [Header("Health Setup")]
    [Range(1, 5)]public int maxHealth = 3;
    int minHealth = 1;
    int health;
    [SerializeField] Color errorColor;
    Color mainColor;
    SpriteRenderer myRenderer;
    bool isFlickering;

    [Header("Scripts Setup")]
    [SerializeField] MazeRenderer mazeScript;
    [SerializeField] MazeRotator mazeRotator;
    [SerializeField] HealthViz healthVisualizer;
    [SerializeField] GameManager myManager;

    [Header("Audio Setup")]
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip buzz;
    [SerializeField] AudioClip ouch;
    // Start is called before the first frame update
    void Start()
    {
        step = mazeScript.size;
        startPos = transform.position;
        health = maxHealth;
        myRenderer = GetComponent<SpriteRenderer>();
        mainColor = myRenderer.color;
        healthVisualizer.SetupHealth(maxHealth, minHealth);
    }

    // Update is called once per frame
    void Update()
    {
        bool upAllowed = CheckCollision(Vector3.down); //I hate vectors
        bool downAllowed = CheckCollision(Vector3.up);
        bool rightAllowed = CheckCollision(Vector3.right);
        bool leftAllowed = CheckCollision(Vector3.left);

        if (MovementAllowed == true)
        {
            if(Input.GetKeyDown(KeyCode.W))
            {
                if (upAllowed)
                {
                    MovementAllowed = false;
                    mazeRotator.RotateWalls(speed);
                    LeanTween.moveZ(gameObject, transform.position.z + step, speed).setOnComplete(ResetMovement);
                } else audioSource.PlayOneShot(buzz);
            }
            if(Input.GetKeyDown(KeyCode.A))
            {
                if (leftAllowed)
                {
                    MovementAllowed = false;
                    mazeRotator.RotateWalls(speed);
                    LeanTween.moveX(gameObject, transform.position.x - step, speed).setOnComplete(ResetMovement);
                } else audioSource.PlayOneShot(buzz);
            }
            if(Input.GetKeyDown(KeyCode.S))
            {
                if (downAllowed)
                {
                    MovementAllowed = false;
                    mazeRotator.RotateWalls(speed);
                    LeanTween.moveZ(gameObject, transform.position.z - step, speed).setOnComplete(ResetMovement);
                } else audioSource.PlayOneShot(buzz);
            }
            if(Input.GetKeyDown(KeyCode.D))
            {
                if (rightAllowed)
                {
                    MovementAllowed = false;
                    mazeRotator.RotateWalls(speed);
                    LeanTween.moveX(gameObject, transform.position.x + step, speed).setOnComplete(ResetMovement);
                } else audioSource.PlayOneShot(buzz);
            }
        }
    }

    void ResetMovement()
    {
        MovementAllowed = true;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Wall")
        {
            Debug.Log("collided with the wall");
            WallCol();
        }
    }
    
    void WallCol()
    {
        health = health-1;
        if (health == 0)
        {
            myManager.EndGame();
        } else
        audioSource.PlayOneShot(ouch);
        healthVisualizer.UpdateHealth();
        IEnumerator coroutine = ImmuneDelay(1f); 
        StartCoroutine (coroutine);
    }

    IEnumerator ImmuneDelay(float timeToWait) //this coroutine counts time of the whole immune system state, how long should it be
    {
        isFlickering = true;
        IEnumerator flickerCor = Flicker();
        StartCoroutine(flickerCor);
        yield return new WaitForSeconds(timeToWait); //we wait for the given amount of time
        StopCoroutine(flickerCor); 
        isFlickering = false;
        myRenderer.color = mainColor;
    }

    public IEnumerator Flicker() //the flickering effect state!
    {
        while (isFlickering == true) //while this is true we just go between two colors (the immune color has A of 0, so it looks like it kinda disappers)
        {
             myRenderer.color = errorColor;
             yield return new WaitForSeconds(0.08f);
             myRenderer.color = mainColor;
             yield return new WaitForSeconds(0.05f);
        }
    }

    bool CheckCollision(Vector3 direction)
    {
        int layerMask = 1 << 3;
        layerMask = ~layerMask;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(direction), out hit, 1f, layerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(direction) * hit.distance, Color.red);
            //Debug.Log(direction + "" + "collision detected");
            return false;
        } else {
            Debug.DrawRay(transform.position, transform.TransformDirection(direction) * hit.distance, Color.green);
            //Debug.Log(direction + "" + "no collision");
            return true;
        }
    }
}
