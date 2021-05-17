using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


namespace NetWork
{
    //singleton Manager
    public class BoardManager_Instance : MonoBehaviour
    {
        private static BoardManager_Instance s_InstanceBoard = null;

        //instance si il est creer on le revoit sinon on le creer
        public static BoardManager_Instance InstanceBoard
        {
            get
            {
                if (s_InstanceBoard == null)
                    s_InstanceBoard = FindObjectOfType<BoardManager_Instance>();
                if (s_InstanceBoard == null)
                {
                    GameObject obj = new GameObject(" Board Manager");
                    s_InstanceBoard = obj.AddComponent<BoardManager_Instance>();
                }
                return s_InstanceBoard;
            }
        }
    }
}
