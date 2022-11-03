using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MazeRotator))]
public class CustomMazeEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        MazeRotator mazeRotator = (MazeRotator)target;
        if(Application.isPlaying) 
        {
            if (GUILayout.Button("Rotate walls"))
            {
                mazeRotator.RotateWalls();
            }
        }
    }
}
