using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// 
/// Editor Script for creating game objectives
/// 
/// Created By: Kpable
/// Date Created: 10-16-17
/// 
/// </summary>
[CustomEditor(typeof(Objective))]
public class ObjectiveEditor : Editor {

    private SerializedProperty type;
    private SerializedProperty targetObject;
    private SerializedProperty targetZone;


    private void OnEnable()
    {
        type = serializedObject.FindProperty("type");
        targetObject = serializedObject.FindProperty("targetObject");
        targetZone = serializedObject.FindProperty("targetZone");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.UpdateIfRequiredOrScript();

        EditorGUILayout.PropertyField(type, new GUIContent("Type"));
        EditorGUI.indentLevel++;
        if (type.intValue != (int)ObjectiveType.GoTo &&
           type.intValue != (int)ObjectiveType.Exit )        {
            EditorGUILayout.PropertyField(targetObject, new GUIContent("Object"));
        }
        if (type.intValue == (int)ObjectiveType.GoTo ||
           type.intValue == (int)ObjectiveType.Exit ||
           type.intValue == (int)ObjectiveType.Plant)
        {
            EditorGUILayout.PropertyField(targetZone, new GUIContent("Zone"));
        }

        serializedObject.ApplyModifiedProperties();
    }
}
