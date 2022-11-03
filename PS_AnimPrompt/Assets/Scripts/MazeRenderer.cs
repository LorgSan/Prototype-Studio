using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeRenderer : MonoBehaviour
{

    [SerializeField]
    [Range(1, 50)]
    private int width = 10;

    [SerializeField]
    [Range(1, 50)]
    private int height = 10;

    [SerializeField]
    private float size = 1f;

    [SerializeField]
    private Transform wallPrefab = null;

    [SerializeField]
    private Transform floorPrefab = null;
    [SerializeField] MazeRotator mazeRotator;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in transform) 
        {
            GameObject.Destroy(child.gameObject);
        }
        mazeRotator.innerWalls.Clear();
        var maze = MazeGenerator.Generate(width, height);
        Draw(maze);
    }

    public void Generate()
    {
        foreach (Transform child in transform) 
        {
            GameObject.Destroy(child.gameObject);
        }
        mazeRotator.innerWalls.Clear();
        var maze = MazeGenerator.Generate(width, height);
        Draw(maze);
    }

    private void Draw(WallState[,] maze)
    {

        var floor = Instantiate(floorPrefab, transform);
        floor.localScale = new Vector3(width, 1, height);

        for (int i = 0; i < width; ++i)
        {
            for (int j = 0; j < height; ++j)
            {
                var cell = maze[i, j];
                var position = new Vector3((-width / 2 + i) * size, 0, (-height / 2 + j) * size);

                if (cell.HasFlag(WallState.UP))
                {
                    var topWall = Instantiate(wallPrefab, transform) as Transform;
                    topWall.localPosition = position + new Vector3(0, 0, size);
                    topWall.localScale = new Vector3(size, topWall.localScale.y, topWall.localScale.z);
                    if (!IsOuterWall(i, j))
                    {
                        mazeRotator.innerWalls.Add(topWall);
                    }
                }

                if (cell.HasFlag(WallState.LEFT))
                {
                    var leftWall = Instantiate(wallPrefab, transform) as Transform;
                    leftWall.localPosition = position + new Vector3(-size, 0, 0);
                    leftWall.localScale = new Vector3(size, leftWall.localScale.y, leftWall.localScale.z);
                    leftWall.eulerAngles = new Vector3(0, 90, 0);
                    if (!IsOuterWall(i, j))
                    {
                        mazeRotator.innerWalls.Add(leftWall);
                    }
                }

                if (i == width - 1)
                {
                    if (cell.HasFlag(WallState.RIGHT))
                    {
                        var rightWall = Instantiate(wallPrefab, transform) as Transform;
                        rightWall.localPosition = position;
                        rightWall.localScale = new Vector3(size, rightWall.localScale.y, rightWall.localScale.z);
                        rightWall.eulerAngles = new Vector3(0, 90, 0);
                        if (!IsOuterWall(i, j))
                        {
                            mazeRotator.innerWalls.Add(rightWall);
                        }
                    }
                }

                if (j == 0)
                {
                    if (cell.HasFlag(WallState.DOWN))
                    {
                        var bottomWall = Instantiate(wallPrefab, transform) as Transform;
                        bottomWall.localPosition = position;
                        bottomWall.localScale = new Vector3(size, bottomWall.localScale.y, bottomWall.localScale.z);
                        if (!IsOuterWall(i, j))
                        {
                            mazeRotator.innerWalls.Add(bottomWall);
                        }
                    }
                }
            }

        }

    }

    bool IsOuterWall(int i, int j)
    {
        if (i == 0)
        {
            return true;
        }
        if (i == width-1)
        {
            return true;
        }
        if (j == 0)
        {
            return true;
        }
        if (j == height-1)
        {
            return true;
        }
        else return false;
    }
}
