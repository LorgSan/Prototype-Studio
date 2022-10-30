using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineSpawner : MonoBehaviour
{
    LineRenderer line;
    Vector3[] positions;
    [SerializeField] GameObject dominoPrefab;
    [SerializeField] Material toGiveDominoes;
    Quaternion tempRot;

    void Start()
    {
        line = GetComponent<LineRenderer>();
        Vector3[] positions = new Vector3[line.positionCount];
        line.GetPositions(positions);
        for (int i = 0; i < positions.Length; i++)
        {
            GameObject newgo = Instantiate(dominoPrefab);
            newgo.transform.position = positions[i];
            newgo.GetComponent<Domino_AcceleratorScript>().matReveal = toGiveDominoes;
            if (i+1 < positions.Length)
            {
                newgo.transform.LookAt(positions[i+1]);
                if (i+1 == positions.Length-1)
                {
                    Debug.Log("found");
                    tempRot = newgo.transform.rotation;
                }
            } else {
                newgo.transform.rotation = tempRot;
                Debug.Log(tempRot);
            }
        }
    }
}
