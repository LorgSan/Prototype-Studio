using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeRotator : MonoBehaviour
{
    [Range(7, 25)][SerializeField] private int difficulty;
    [HideInInspector] public List<Transform> innerWalls;
    [HideInInspector] public List<Transform> rotateWalls;
    float currentRotY;
    float startRotY;
 
    bool rotated;

    public void PresetLevel()
    {
        for (int i = 0; i <= difficulty; i++)
        {
            int randomInd = Random.Range(0, innerWalls.Count-1);
            rotateWalls.Add(innerWalls[randomInd]);
            innerWalls.RemoveAt(randomInd);
        }
    }

    public void RotateWalls()
    {
        if (rotated)
        {
            for (int i = 0; i < rotateWalls.Count; i++)
            {
                rotateWalls[i].GetComponent<WallRotate>().Rotate(true);
            }
            rotated = false;
        } else
        {
            for (int i = 0; i < rotateWalls.Count; i++)
            {
                rotateWalls[i].GetComponent<WallRotate>().Rotate(false);
            }
            rotated = true;
        }
    }
}
