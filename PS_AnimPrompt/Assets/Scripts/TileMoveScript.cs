using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMoveScript : MonoBehaviour
{
    WallState[,] maze;
    [SerializeField] MazeRenderer mazeScript;
    [HideInInspector] public Vector2 startPosition;
    WallState currentCell;
    float step;
    float originY;
    Vector3 newPos;
    float fraction;
    [SerializeField] float speed;
    bool isMoving = false;

    void Start()
    {
        step = mazeScript.size;
        originY = transform.position.y;
    }
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.W) && isMoving == false)
        {
            newPos = new Vector3 (transform.position.x, originY, transform.position.y + step);
            isMoving = true;
        }

        if (isMoving == true) 
        {
            fraction += Time.deltaTime * speed;
            Vector3 tempPos = Vector3.MoveTowards(transform.position, newPos, fraction);
            transform.position = tempPos;

            if (transform.position == newPos)
            {
                isMoving = false;
            }
        }
    }
}
