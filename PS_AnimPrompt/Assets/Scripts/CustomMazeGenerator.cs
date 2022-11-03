using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MazeRenderer))]
public class CustomMazeGenerator : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        MazeRenderer mazeRenderer = (MazeRenderer)target;
        if(Application.isPlaying)
        {
            if (GUILayout.Button("Generate Maze"))
            {
                mazeRenderer.Generate();
            }
        }
    }
}
