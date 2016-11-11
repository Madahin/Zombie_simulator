using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(ObjectBuilderScript))]
public class ObjectBuilderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        
        if (GUILayout.Button("Build Object"))
        {
            Debug.Log("patate");
        }
    }
}