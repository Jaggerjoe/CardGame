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
        private  SO_Board m_BoardReference;

        private SO_Board m_BoardInstance = null;

        [SerializeField]
        private ulong m_ClientID = 0;

        [SerializeField]
        private int[] m_IDSlot = { };

        [SerializeField]
        private int m_IDCard = 0;

        public override void NetworkStart()
        {
            m_BoardInstance = Instantiate(m_BoardReference);
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
            //ici on va appeller la fonction de nos event de l'instance ce notre so_Board (m_BoardInstance)
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

        public SO_Board BoardInstance
        {
            get { return m_BoardInstance; }
            set { m_BoardInstance = value; }
        }
    }
}
