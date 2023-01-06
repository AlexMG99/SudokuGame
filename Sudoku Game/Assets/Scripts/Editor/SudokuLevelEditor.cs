using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SudokuLevelSO))]
public class SudokuLevelEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var level = (SudokuLevelSO)target;

        GUILayout.Space(20);
        if (GUILayout.Button("Hide numbers level", GUILayout.Height(40)))
        {
            level.HideNumbersByDifficulty();
            Debug.Log("Button Clicked!");
        }

    }
}

