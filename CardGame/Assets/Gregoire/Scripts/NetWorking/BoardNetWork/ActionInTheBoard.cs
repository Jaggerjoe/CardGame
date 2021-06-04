using UnityEngine;
using MLAPI;
using MLAPI.Messaging;
using MLAPI.NetworkVariable.Collections;
using MLAPI.NetworkVariable;
using MLAPI.Connection;
using UnityEngine.Networking.Types;
using UnityEditor.PackageManager;
using System;
using System.Collections.Generic;

namespace NetWork
{
    // player
    public class ActionInTheBoard : NetworkBehaviour
    {
        private static SO_Board s_LocalInstance = null;

        [SerializeField]
        private SO_Board m_BoardReference;

  
        public override void NetworkStart()
        {
            if (IsLocalPlayer)
            {
                s_LocalInstance = Instantiate(m_BoardReference);
                if (IsHost)
                {
                    //s_LocalInstance.Side.m_Deck.Shuffle();
                    s_LocalInstance.SetDeckSideAndOtherSide();
                    s_LocalInstance.Side.m_Deck.Shuffle();
                    s_LocalInstance.Side2.m_Deck.Shuffle();
                    Debug.Log("isserver");
                }
                else
                {
                    //appele de la fonction server rpc pour recup les info a envoyer au client
                    Debug.Log("client");
                    RequestServerRpc();
                }
            }
        }


       

        #region get index du deck 2 
 
        //  Utilisé par les clients pour indiquer au serveur qu'un message doit être diffusé.
        //parcousla totaliter du deck et pour chaque card il va prrendre le setindexCard et on  va recup 
        [ServerRpc]
        private void RequestServerRpc()
        {
            //récupère les cartes a envoyer * 22 donc for ici 
            //boucle pour parcourir le deck du side 2
            for (int i = 0; i < s_LocalInstance.Side2.m_Deck.Count; i++)
            {
                AddCardToDeckClientRpc(s_LocalInstance.Side2.m_Deck[i].m_Index);
            }

            Debug.Log("je suis dans la server Rpc" + s_LocalInstance.Side2.m_Deck[0].m_Index);
            //DebugFirstDeckCardNumbeClientRpc();
        }

        [ClientRpc]
        private void DebugFirstDeckCardNumbeClientRpc()
        {
            if(IsHost)
            {
                Debug.Log("Je suis le host, rpc magique : " + s_LocalInstance.Side2.m_Deck[0].m_Index);
            }
            else
            {
                Debug.Log("Je suis le client, rpc magique : " + s_LocalInstance.Side2.m_Deck[0].m_Index);
            }
        }


        //Utilisé par le serveur pour diffuser un message à tous les clients.
        [ClientRpc]
        private void AddCardToDeckClientRpc(int p_IDCard)
        {
            for (int i = 0; i < s_LocalInstance.AllCards.Count; i++)
            {
                if(p_IDCard == s_LocalInstance.AllCards[i].m_Index)
                {
                    if(s_LocalInstance.Side.m_Deck.Count >= 22)
                    {
                        return;
                    }
                    else
                    {
                        if(OwnerClientId == NetworkManager.Singleton.LocalClientId)
                        {
                            s_LocalInstance.Side.m_Deck.Add(s_LocalInstance.AllCards[i]);
                            Debug.Log("j'ai le même ID ");
                        }
                        else
                        {
                            Debug.Log("je n'ai pas le même ID ");
                            s_LocalInstance.Side2.m_Deck.Add(s_LocalInstance.AllCards[i]);
                        }
                        //s_LocalInstance.SetDeck(s_LocalInstance.AllCards[i],OwnerClientId);
                    }
                    break;
                }
                else
                {
                    Debug.LogError("je ne l'ai pas trouvé");
                }
            }
            //on met le other side du host dans le side du client et inversement
         
            //Debug.Log($"l'ID de ma carte client RPC est {p_IDCard}");

        }
        #endregion

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
            s_LocalInstance.PutCardOnSlot(p_SideID, p_IDSlot, p_IDCard);
            //Debug.Log("youyu");
            Debug.Log($"j'ai r�cup�r� la carte dans placementcArd: {p_IDCard}");
        }

        #endregion

        #region Pioche Card
        public void DrawCard(ulong p_PlayerNetworkID, int p_IDCard)
        {
            if (IsServer)
            {
                DrawCardClientRpc(p_PlayerNetworkID, p_IDCard);
            }
            else
            {
                DrawCardServerRpc(p_PlayerNetworkID, p_IDCard);
            }
        }

        [ServerRpc]
        public void DrawCardServerRpc(ulong p_PlayerNetworkID, int p_IDCard)
        {
            DrawCardClientRpc(p_PlayerNetworkID, p_IDCard);
            Debug.Log("ta mere le cookie");
        }
        [ClientRpc]
        public void DrawCardClientRpc(ulong p_PlayerNetworkID, int p_IDCard)
        {

            s_LocalInstance.DrawCardBoard(p_PlayerNetworkID, p_IDCard);
            Debug.Log("je suis la coucou");
        }
        #endregion

        public SO_Board BoardInstance
        {
            get { return s_LocalInstance; }
            set { s_LocalInstance = value; }
        }

    }
}
