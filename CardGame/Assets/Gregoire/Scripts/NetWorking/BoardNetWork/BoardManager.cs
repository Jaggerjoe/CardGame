using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


namespace NetWork
{
    //singleton Manager
    public class BoardManager : MonoBehaviour
    {
        private static BoardManager s_InstanceBoard = null;

        //instance si il est creer on le revoit sinon on le creer
        public static BoardManager InstanceBoard
        {
            get
            {
                if (s_InstanceBoard == null)
                    s_InstanceBoard = FindObjectOfType<BoardManager>();
                if (s_InstanceBoard == null)
                {
                    GameObject obj = new GameObject(" Board Manager");
                    s_InstanceBoard = obj.AddComponent<BoardManager>();
                }
                return s_InstanceBoard;
            }
        }
    }
}
