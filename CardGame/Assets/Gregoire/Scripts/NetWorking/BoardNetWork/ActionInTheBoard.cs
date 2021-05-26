using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using MLAPI;
using MLAPI.NetworkVariable;
using MLAPI.Messaging;
using MLAPI.Connection;

namespace NetWork
{
    // player
    public class ActionInTheBoard : NetworkBehaviour
    {
        [SerializeField]
        private  SO_Board m_BoardReference;

        private SO_Board m_BoardInstance = null;

        [SerializeField]
        private bool m_IsConnect = false;

        public override void NetworkStart()
        {
            m_IsConnect = true;

            if (m_IsConnect)
            {
                Debug.Log(m_IsConnect);
                m_BoardInstance = Instantiate(m_BoardReference);
                m_BoardInstance.Shuffle();
                DrawCard(0,5);
            //check savoir si on est deja co
            //set les id dans les side
            
            Debug.Log("NOOOOOOOOOOOOOOOOOOOOOOOOOO, je vous baise");
            }
            else
            {
                Debug.Log("TA GRAND MERE EN STRING DE GUERRE");
            }
        }

        #region Placement Card
        //fonctsion qui sera appeler dans pour l'instant le start 
        //car c'est la generic et qu'elle fait le check
        private void PlacementCard(ulong p_SideID, int p_IDSlot, int p_IDCard)
        {
            /* if (IsServer)
             {
                 PlacementCardClientRpc(p_SideID, p_IDSlot, p_IDCard);
                 Debug.Log("je suis server " + IsServer);
             }
             else
             {
                 PlacementCardServerRpc(p_SideID, p_IDSlot, p_IDCard);
                 Debug.Log("je suis client " + IsClient);
             }*/
        }
        [ServerRpc]
        public void PlacementCardServerRpc(ulong p_SideID, int p_IDSlot, int p_IDCard)
        {
            PlacementCardClientRpc(p_SideID, p_IDSlot, p_IDCard);

            //ici on va appeller la fonction de nos event de l'instance ce notre so_Board (m_BoardInstance)
            Debug.Log("youyu 2");
        }

        [ClientRpc]
        public void PlacementCardClientRpc(ulong p_SideID, int p_IDSlot, int p_IDCard)
        {
            //update le board de facon individuel  et recupere les infos
            m_BoardInstance.PutCardOnSlot(p_IDSlot,p_IDCard);
            //Debug.Log("youyu");
            Debug.Log($"j'ai récupéré la carte dans placementcArd: {p_IDCard}");
        }

        #endregion

        #region Pioche Card
        public void DrawCard(ulong p_IDSlot, int p_IDCard)
        {
            if(IsServer)
            {
                DrawCardClientRpc(p_IDSlot, p_IDCard);
            }
            else
            {
                DrawCardServerRpc(p_IDSlot, p_IDCard);
            }
        }

        [ServerRpc]
        public void DrawCardServerRpc(ulong p_IDSlot, int p_IDCard)
        {
            DrawCardClientRpc(p_IDSlot, p_IDCard);
        }
        [ClientRpc]
        public void DrawCardClientRpc(ulong p_IDSlot, int p_IDCard)
        {
            m_BoardInstance.DrawCard(p_IDSlot, p_IDCard);
            Debug.Log("je suis la coucou");
        }
        #endregion

        public SO_Board BoardInstance
        {
            get { return m_BoardInstance; }
            set { m_BoardInstance = value; }
        }

    }
}
