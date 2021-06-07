using UnityEngine;
using UnityEditor;
using NetWork;

[CustomEditor(typeof(ActionInTheBoard))]
public class ActionInTheBoardEditor : Editor
{

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        //si on est en play
        if(EditorApplication.isPlaying)
        {
            //on fait un espace
            EditorGUILayout.Space();
            //on target action in the board
            ActionInTheBoard actionInTheBoard = target as ActionInTheBoard;
            //si il est different egale de null
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
