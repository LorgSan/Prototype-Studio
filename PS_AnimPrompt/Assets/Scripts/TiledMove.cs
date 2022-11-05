using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiledMove : MonoBehaviour
{
    float step;
    bool MovementAllowed = true;
    [SerializeField] float speed;
    [SerializeField] MazeRenderer mazeScript;
    [SerializeField] MazeRotator mazeRotator;
    // Start is called before the first frame update
    void Start()
    {
        step = mazeScript.size;
    }

    // Update is called once per frame
    void Update()
    {
        if (MovementAllowed == true)
        {
            if(Input.GetKeyDown(KeyCode.W))
            {
                MovementAllowed = false;
                mazeRotator.RotateWalls(speed);
                LeanTween.moveZ(gameObject, transform.position.z + step, speed).setOnComplete(ResetMovement);
            }
            if(Input.GetKeyDown(KeyCode.A))
            {
                MovementAllowed = false;
                mazeRotator.RotateWalls(speed);
                LeanTween.moveX(gameObject, transform.position.x - step, speed).setOnComplete(ResetMovement);
            }
            if(Input.GetKeyDown(KeyCode.S))
            {
                MovementAllowed = false;
                mazeRotator.RotateWalls(speed);
                LeanTween.moveZ(gameObject, transform.position.z - step, speed).setOnComplete(ResetMovement);
            }
            if(Input.GetKeyDown(KeyCode.D))
            {
                MovementAllowed = false;
                mazeRotator.RotateWalls(speed);
                LeanTween.moveX(gameObject, transform.position.x + step, speed).setOnComplete(ResetMovement);
            }
        }
    }

    void ResetMovement()
    {
        MovementAllowed = true;
    }
}
