using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using MLAPI;
using MLAPI.NetworkVariable;
using MLAPI.Messaging;

namespace NetWork
{
    public class Board : NetworkBehaviour
    {
        [SerializeField]
        private SO_Board m_BoardInstance;
        public override void NetworkStart()
        {
            m_BoardInstance = ScriptableObject.CreateInstance<SO_Board>();
            string l_AssetPathName = AssetDatabase.GenerateUniqueAssetPath("Assets/Gregoire/Scripts/NetWorking/So_Board/NewBoard.asset");
            AssetDatabase.CreateAsset(m_BoardInstance, l_AssetPathName);
            Debug.Log("coucou je suis l'instance So creer");
        }
        public void ConnectBoard()
        {
            m_BoardInstance = FindObjectOfType<SO_Board>();
            
            Debug.Log(OwnerClientId);
            Debug.Log("alut");

            if (IsServer)
            {
                ReceiveBoardClientRpc();
            }
            else
            {
                ConnectBoardServerRpc();

            }
        }

        [ServerRpc]
        public void ConnectBoardServerRpc()
        {
            ReceiveBoardClientRpc();
            Debug.Log("youyu 2");
        }

        [ClientRpc]
        public void ReceiveBoardClientRpc()
        {
            Debug.Log("youyu");
        }
    }
}
