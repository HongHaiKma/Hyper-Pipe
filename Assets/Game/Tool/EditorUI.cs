// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEditor;

// [CustomEditor(typeof(EditorCore))]
// public class EditorUI : Editor
// {
//     private EditorCore data;
//     void OnEnable()
//     {
//         data = target as EditorCore;
//     }

//     public override void OnInspectorGUI()
//     {
//         // data.isSetupInspector = (bool)EditorGUILayout.Toggle("Setup Inspector", data.isSetupInspector);

//         GUILayout.BeginVertical();
//         if (GUILayout.Button("Random Pipe"))
//         {
//             data.ChangePipe();
//         }
//         GUILayout.BeginHorizontal();
//         // level = EditorGUILayout.TextField("Level", level);
//         GUILayout.EndHorizontal();

//         GUILayout.EndVertical();

//         DrawDefaultInspector();
//     }
// }
