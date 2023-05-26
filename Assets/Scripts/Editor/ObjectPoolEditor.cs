using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ObjectPool))]
public class ObjectPoolEditor : Editor
{
    SerializedProperty prefab;
    SerializedProperty limit;


    void OnEnable()
    {
        prefab = serializedObject.FindProperty("prefab");
        limit = serializedObject.FindProperty("limit");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(prefab);
        EditorGUILayout.PropertyField(limit);

        serializedObject.ApplyModifiedProperties();

        if (GUILayout.Button("Remove Children"))
            Selection.activeGameObject.SendMessage("RemoveAll");
    }
}