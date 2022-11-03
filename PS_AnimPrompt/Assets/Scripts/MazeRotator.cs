using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeRotator : MonoBehaviour
{
    [SerializeField] private int difficulty;
    public List<Transform> innerWalls;
    float currentRotY;
    float startRotY;
    float endRotY;
    bool rotated;

    public void RotateWalls()
    {
        if (rotated)
        {
            for (int i = 0; i < innerWalls.Count; i++)
            {
                innerWalls[i].GetComponent<WallRotate>().Rotate(true);
            }
            rotated = false;
        } else
        {
            for (int i = 0; i < innerWalls.Count; i++)
            {
                innerWalls[i].GetComponent<WallRotate>().Rotate(true);
            }
            rotated = false;
        }
    }
}
