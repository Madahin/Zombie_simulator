using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(ProceduralMapGeneration))]
public class ProceduralMapGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        ProceduralMapGeneration myScript = (ProceduralMapGeneration)target;
        if (GUILayout.Button("Build city"))
        {
            myScript.BuildObject();
        }
        if (GUILayout.Button("Reset"))
        {
            myScript.Reset();
        }
    }
}