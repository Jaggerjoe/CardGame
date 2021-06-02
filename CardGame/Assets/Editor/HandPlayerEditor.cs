using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using NetWork;

[CustomEditor(typeof(SO_Board))]
public class HandPlayerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        SO_Board l_HandPlayer = target as SO_Board;
        base.OnInspectorGUI();
        using (new EditorGUILayout.VerticalScope())
        {
            EditorGUILayout.Space(20);
            EditorGUILayout.LabelField("EditorButton");
            EditorGUILayout.Space(5);
            if (GUILayout.Button("Shuffle"))
            {
                l_HandPlayer.Side.m_Deck.Shuffle();
                l_HandPlayer.Side2.m_Deck.Shuffle();
            }
        }
    }
}
