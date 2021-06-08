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
                    //on recupere les deck et la postion de leur indexe
                    s_LocalInstance.SetDeckSideAndOtherSide();
                    //puis on shuffle
                    s_LocalInstance.Side.m_Deck.Shuffle();
                    s_LocalInstance.Side2.m_Deck.Shuffle();
                    Debug.Log("isserver");
                }
                else
                {
                    //appele de la fonction server rpc pour recup les info a envoyer au client
                    Debug.Log("client");
                    RequestServerRpc();
                    DrawCard();
                }
            }
        }


       

        #region get index du other deck
 
        //  Utilisé par les clients pour indiquer au serveur qu'un message doit être diffusé.
        //parcours la totaliter du deck et pour chaque card il va prrendre le setindexCard et on  va recup 
        [ServerRpc]
        private void RequestServerRpc()
        {
            //récupère les cartes a envoyer * 22 donc for ici 
            //boucle pour parcourir le deck du side 2
            for (int i = 0; i < s_LocalInstance.Side2.m_Deck.Count; i++)
            {
                //on ajoute le other deck au client
                AddCardToDeckClientRpc(s_LocalInstance.Side2.m_Deck[i].m_Index);
            }
            Debug.Log("je suis dans la server Rpc" + s_LocalInstance.Side2.m_Deck[0].m_Index);

            for (int i = 0; i < s_LocalInstance.Side.m_Deck.Count; i++)
            {
                //on ajoute le other deck au client
                AddCardToOtherDeckClientRpc(s_LocalInstance.Side.m_Deck[i].m_Index);
            }
            Debug.Log("boucle 2 je suis dans la server Rpc" + s_LocalInstance.Side.m_Deck[0].m_Index); 
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
        //on ajoute les au deck du serveur au client
        [ClientRpc]
        private void AddCardToDeckClientRpc(int p_IDCard)
        {
            //pour chaque ellement de ma liste d'asset card
            for (int i = 0; i < s_LocalInstance.AllCards.Count; i++)
            {
                //si p_idcard est egale a la liste a l'element i de l'index
                if(p_IDCard == s_LocalInstance.AllCards[i].m_Index)
                {
                    //si le side du client est sup ou egale a 22
                    if(s_LocalInstance.Side.m_Deck.Count >= 22)
                    {
                        //on ne fait rien
                        return;
                    }
                    else
                    {
                        //si l'id du joueur est egale au local client id 
                        if(OwnerClientId == NetworkManager.Singleton.LocalClientId)
                        {
                            //alors on ajoute au deck de ce joueur la liste dans le meme ordre avec i
                            s_LocalInstance.Side.m_Deck.Add(s_LocalInstance.AllCards[i]);
                            Debug.Log("j'ai le même ID ");
                        }
                        else
                        {
                            Debug.Log("je n'ai pas le même ID ");
                            //sinon on ajoute au deck de l'autre joueur la liste aussi
                            s_LocalInstance.Side2.m_Deck.Add(s_LocalInstance.AllCards[i]);
                        }
                        //s_LocalInstance.SetDeck(s_LocalInstance.AllCards[i],OwnerClientId);
                    }
                    //puis on sort du if
                    break;
                }
            }
        }

        [ClientRpc]
        private void AddCardToOtherDeckClientRpc(int p_IDCard)
        {
            //pour chaque ellement de ma liste d'asset card
            for (int i = 0; i < s_LocalInstance.AllCards.Count; i++)
            {
                //si p_idcard est egale a la liste a l'element i de l'index
                if (p_IDCard == s_LocalInstance.AllCards[i].m_Index)
                {
                    //si le side du client est sup ou egale a 22
                    if (s_LocalInstance.Side2.m_Deck.Count >= 22)
                    {
                        //on ne fait rien
                        return;
                    }
                    else
                    {
                        //si l'id du joueur est egale au local client id 
                        if (OwnerClientId == NetworkManager.Singleton.LocalClientId)
                        {
                            //alors on ajoute au deck de ce joueur la liste dans le meme ordre avec i
                            s_LocalInstance.Side2.m_Deck.Add(s_LocalInstance.AllCards[i]);
                            Debug.Log("j'ai le même ID ");
                        }
                        else
                        {
                            Debug.Log("je n'ai pas le même ID ");
                            //sinon on ajoute au deck de l'autre joueur la liste aussi
                            s_LocalInstance.Side.m_Deck.Add(s_LocalInstance.AllCards[i]);
                        }
                        //s_LocalInstance.SetDeck(s_LocalInstance.AllCards[i],OwnerClientId);
                    }
                    //puis on sort du if
                    break;
                }
            }
        }
        #endregion

        

        #region Pioche Card
        public void DrawCard()
        {
       
                DrawCardServerRpc();
           
        }

        [ServerRpc]
        public void DrawCardServerRpc()
        {
            //je pioche une carte;
            s_LocalInstance.DrawCardBoard(OwnerClientId);
            Debug.Log("alut");

            //je boucle pour piocher et mettre au bon emplacmeent la card
            //for (int i = 0; i < s_LocalInstance.Side2.m_Deck.Count; i++)
            for (int i = 0; i<s_LocalInstance.Side.m_Hand.Count; i++)
            {
                //j'appele la clientRpc
                DrawCardClientRpc(s_LocalInstance.Side.m_Hand[i].m_Index);
                Debug.Log("draw serverRpc in boucle");
            }
               
            for(int i = 0; i<s_LocalInstance.Side2.m_Hand.Count; i++)
            {
                //j'appele la clientRpc
                DrawCardClientRpc(s_LocalInstance.Side2.m_Hand[i].m_Index);
                Debug.Log("draw serverRpc in boucle2");
            }
             
            Debug.Log("aurevoir");
        }
        [ClientRpc]
        public void DrawCardClientRpc(int p_IdCard)
        {
            //je pioche une carte;
            //s_LocalInstance.DrawCardBoard(OwnerClientId);
            Debug.Log("alut");
            

            Debug.Log("aurevoir");
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
        public SO_Board BoardInstance
        {
            get { return s_LocalInstance; }
            set { s_LocalInstance = value; }
        }

    }
}
