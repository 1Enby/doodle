using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(WorldGenerator))]
public class WorldGeneratorEditor : Editor
{
    SerializedProperty tile_countX;
    SerializedProperty tile_countY;

    SerializedProperty num_lakes;
    SerializedProperty min_lake_radius;
    SerializedProperty max_lake_radius;
    SerializedProperty max_view_size;
    SerializedProperty player;
    SerializedProperty radius;

    SerializedProperty debug_text;
    SerializedProperty num_mountains;

    SerializedProperty max_mountain_radius;


    void OnEnable()
    {
        tile_countX = serializedObject.FindProperty("tile_countX");
        tile_countY = serializedObject.FindProperty("tile_countY");
        num_lakes = serializedObject.FindProperty("num_lakes");
        min_lake_radius = serializedObject.FindProperty("min_lake_radius");
        max_lake_radius = serializedObject.FindProperty("max_lake_radius");
        player = serializedObject.FindProperty("player");
        radius = serializedObject.FindProperty("radius");
        debug_text = serializedObject.FindProperty("debug_text");
        num_mountains = serializedObject.FindProperty("num_mountains");
        max_mountain_radius = serializedObject.FindProperty("max_mountain_radius");

    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(tile_countX);
        EditorGUILayout.PropertyField(tile_countY);
        EditorGUILayout.PropertyField(num_lakes);
        EditorGUILayout.PropertyField(min_lake_radius);
        EditorGUILayout.PropertyField(max_lake_radius);
        EditorGUILayout.PropertyField(player);
        EditorGUILayout.PropertyField(radius);
        EditorGUILayout.PropertyField(num_mountains);
        EditorGUILayout.PropertyField(max_mountain_radius);


        serializedObject.ApplyModifiedProperties();

        if (GUILayout.Button("Remove Children"))
            Selection.activeGameObject.SendMessage("RemoveAll");
    }
}