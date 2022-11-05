using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMoveScript : MonoBehaviour
{
    WallState[,] maze;
    [SerializeField] MazeRenderer mazeScript;
    [HideInInspector] public Vector2 startPosition;
    WallState currentCell;
    [SerializeField] float step;
    float originY;
    Vector3 newPos;
    float fraction;
    [SerializeField] float speed;
    bool isMoving = false;

    void Start()
    {
        //step = mazeScript.size;
        originY = transform.position.y;
    }
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.W))
        {
            LeanTween.moveZ(gameObject, +0.75f, speed);
        }
    }
}
