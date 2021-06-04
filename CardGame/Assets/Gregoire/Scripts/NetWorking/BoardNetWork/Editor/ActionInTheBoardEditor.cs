using UnityEngine;
using UnityEditor;
using NetWork;

[CustomEditor(typeof(ActionInTheBoard))]
public class ActionInTheBoardEditor : Editor
{

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if(EditorApplication.isPlaying)
        {
            EditorGUILayout.Space();
            ActionInTheBoard actionInTheBoard = target as ActionInTheBoard;
            if (actionInTheBoard.BoardInstance != null)
            {
                SerializedObject obj = new SerializedObject(actionInTheBoard.BoardInstance);
                GUI.enabled = false;
                EditorGUILayout.ObjectField("Board Instance", actionInTheBoard.BoardInstance, typeof(ActionInTheBoard), false);
                GUI.enabled = true;
            }
        }

    }

}
