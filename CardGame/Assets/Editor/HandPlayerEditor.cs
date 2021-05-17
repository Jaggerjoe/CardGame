using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(HandPlayer))]
public class HandPlayerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        HandPlayer l_HandPlayer = target as HandPlayer;
        base.OnInspectorGUI();
        using (new EditorGUILayout.VerticalScope())
        {
            EditorGUILayout.Space(20);
            EditorGUILayout.LabelField("EditorButton");
            EditorGUILayout.Space(5);
            if (GUILayout.Button("Shuffle"))
            {
                l_HandPlayer.Shuffle();
            }
        }
    }
}
