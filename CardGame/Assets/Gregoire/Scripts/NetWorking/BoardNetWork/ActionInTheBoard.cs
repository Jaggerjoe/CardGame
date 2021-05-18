using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using MLAPI;
using MLAPI.NetworkVariable;
using MLAPI.Messaging;

namespace NetWork
{
    // player
    public class ActionInTheBoard : NetworkBehaviour
    {
        [SerializeField]
        private static SO_Board m_BoardInstance;

        [SerializeField]
        private ulong m_ClientID = 0;

        [SerializeField]
        private int[] m_IDSlot = { };

        [SerializeField]
        private int m_IDCard = 0;

        public override void NetworkStart()
        {
            AssetDatabase.CopyAsset("Assets/Gregoire/Scripts/NetWorking/So_Board/DataBoard.asset", "Assets/Gregoire/Scripts/NetWorking/So_Board/NewBoard.asset");
            //SO_Board l_Board = (SO_Board)AssetDatabase.LoadAssetAtPath(l_Path, typeof(SO_Board));
            //Debug.Log(l_Board);
            //l_Board = ScriptableObject.CreateInstance<SO_Board>();
            //Debug.Log(l_Board);
            //string l_AssetPathName = AssetDatabase.GenerateUniqueAssetPath("Assets/Gregoire/Scripts/NetWorking/So_Board/NewBoard.asset");
            ////l_BoardInstance = m_BoardInstance;
            //AssetDatabase.CreateAsset(l_Board, l_AssetPathName);
            //Debug.Log(l_Board);
            //Debug.Log("coucou je suis l'instance So creer");
        }

        public void ConnectBoard()
        {
            m_BoardInstance = FindObjectOfType<SO_Board>();
            
            Debug.Log(OwnerClientId);
            Debug.Log("alut");

            if (IsServer)
            {
                PlacementCardClientRpc(m_ClientID, m_IDSlot, m_IDCard);
            }
            else
            {
                PlacementCardServerRpc(m_ClientID, m_IDSlot, m_IDCard);
            }
        }

        [ServerRpc]
        public void PlacementCardServerRpc(ulong p_ClientID, int[] p_IDSlot, int p_IDCard)
        {
            PlacementCardClientRpc(p_ClientID,p_IDSlot,p_IDCard);
            Debug.Log("youyu 2");
        }

        [ClientRpc]
        public void PlacementCardClientRpc(ulong p_ClientID, int[] p_IDSlot, int p_IDCard)
        {
            Debug.Log("youyu");
            //update le board de facon individuel  et recupere les infos
          //  m_BoardInstance.PutCardOnSlot();

        }

        public void HandCardInTheSlot()
        {

        }
    }
}
