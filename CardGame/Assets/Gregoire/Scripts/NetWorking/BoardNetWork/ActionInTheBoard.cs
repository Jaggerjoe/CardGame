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
                    DrawCardServerRpc();
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
            //Debug.Log("je suis dans la server Rpc" + s_LocalInstance.Side2.m_Deck[0].m_Index);

            for (int i = 0; i < s_LocalInstance.Side.m_Deck.Count; i++)
            {
                //on ajoute le other deck au client
                AddCardToOtherDeckClientRpc(s_LocalInstance.Side.m_Deck[i].m_Index);
            }
            //Debug.Log("boucle 2 je suis dans la server Rpc" + s_LocalInstance.Side.m_Deck[0].m_Index); 
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
                        }
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
            if (IsServer)
            {
                DrawCardClientRpc(s_LocalInstance.Side.m_Deck[0].m_Index);
                Debug.Log("in drawn card is server ma indexe clientrpc est " + s_LocalInstance.Side2.m_Deck[0].m_Index);
            }
            else
            {
       
                DrawCardServerRpc();
           
        }

        [ServerRpc]
        public void DrawCardServerRpc()
        {
            //je pioche une carte;
            s_LocalInstance.SwitchCardDeckToDrow(s_LocalInstance.Side);
            //je boucle pour piocher et mettre au bon emplacmeent la card
            //for (int i = 0; i < s_LocalInstance.Side2.m_Deck.Count; i++)
            for (int i = 0; i < s_LocalInstance.Side.m_Hand.Count; i++)
            {
                //j'appele la clientRpc
                DrawCardClientRpc(s_LocalInstance.Side.m_Hand[i].m_Index);
            }

            s_LocalInstance.SwitchCardDeckToDrow(s_LocalInstance.Side2);
            for (int i = 0; i < s_LocalInstance.Side2.m_Hand.Count; i++)
            {
                //j'appele la clientRpc
                DrawCardOtherSideClientRpc(s_LocalInstance.Side2.m_Hand[i].m_Index);
            }

            DrawCardUiUpdateClientRPC();
        }

        [ClientRpc]
        void DrawCardUiUpdateClientRPC()
        {
            s_LocalInstance.DrawCardBoard(OwnerClientId);
        }

        [ClientRpc]
        public void DrawCardClientRpc(int p_IdCard)
        {
            //je pioche une carte;
            //s_LocalInstance.DrawCardBoard(OwnerClientId);
            Debug.Log($"je sui appeller par {NetworkManager.Singleton.LocalClientId}");

            for (int i = 0; i < s_LocalInstance.Side2.m_Deck.Count; i++)
            {
                if (p_IdCard == s_LocalInstance.Side2.m_Deck[i].m_Index)
                {
                    if (s_LocalInstance.Side.m_Hand.Count >= 6)
                    {
                        //on ne fait rien
                        return;
                    }
                    else
                    {
                        if (OwnerClientId == NetworkManager.Singleton.LocalClientId)
                        {
                            //alors on ajoute au deck de ce joueur la liste dans le meme ordre avec i
                            s_LocalInstance.Side2.m_Hand.Add(s_LocalInstance.Side2.m_Deck[i]);
                            s_LocalInstance.Side2.m_Deck.Remove(s_LocalInstance.Side2.m_Deck[i]);
                            //Debug.Log("j'ai le même ID ");
                        }
                        else
                        {
                            //Debug.Log("je n'ai pas le même ID ");
                            //sinon on ajoute au deck de l'autre joueur la liste aussi
                            s_LocalInstance.Side.m_Hand.Add(s_LocalInstance.Side2.m_Deck[i]);
                            s_LocalInstance.Side.m_Deck.Remove(s_LocalInstance.Side2.m_Deck[i]);
                        }
                    }
                }
            }
        } 

        [ClientRpc]
        public void DrawCardOtherSideClientRpc(int p_IdCard)
        {
            //je pioche une carte;
            //s_LocalInstance.DrawCardBoard(OwnerClientId);


            for (int i = 0; i < s_LocalInstance.Side.m_Deck.Count; i++)
            {
                if (p_IdCard == s_LocalInstance.Side.m_Deck[i].m_Index)
                {
                    if (s_LocalInstance.Side.m_Hand.Count >= 6)
                    {
                        //on ne fait rien
                        return;
                    }
                    else
                    {
                        if (OwnerClientId == NetworkManager.Singleton.LocalClientId)
                        {
                            //alors on ajoute au deck de ce joueur la liste dans le meme ordre avec i
                            s_LocalInstance.Side.m_Hand.Add(s_LocalInstance.Side.m_Deck[i]);
                            s_LocalInstance.Side.m_Deck.Remove(s_LocalInstance.Side.m_Deck[i]);
                            //Debug.Log("j'ai le même ID ");
                        }
                        else
                        {
                            //Debug.Log("je n'ai pas le même ID ");
                            //sinon on ajoute au deck de l'autre joueur la liste aussi
                            s_LocalInstance.Side2.m_Hand.Add(s_LocalInstance.Side.m_Deck[i]);
                            s_LocalInstance.Side2.m_Deck.Remove(s_LocalInstance.Side.m_Deck[i]);
                        }
                    }
                }
            }
        }
        #endregion

        #region Placement Card
        //fonctsion qui sera appeler dans pour l'instant le start 
        //car c'est la generic et qu'elle fait le check
        public void PlacementCard()
        {
            if (IsServer)
            {
                for (int i = 0; i < s_LocalInstance.Side.m_Slot.Length; i++)
                {
                    Debug.Log("coucou je suis dans placement card client dans ma boucle for");
                    if (s_LocalInstance.Side.m_Slot[i].m_Card != null)
                    {
                        Debug.Log(s_LocalInstance.Side.m_Slot[i].m_Card.m_Index);
                        PlacementCardClientRpc(s_LocalInstance.Side.m_Slot[i].m_Card.m_Index, i);
                    }
                }
                Debug.Log("je suis server " + IsServer);
            }
            else
            {
                for (int i = 0; i < s_LocalInstance.Side.m_Slot.Length; i++)
                {
                    Debug.Log("coucou je suis dans placement card client dans ma boucle for");
                    if (s_LocalInstance.Side.m_Slot[i].m_Card != null)
                    {
                        Debug.Log(s_LocalInstance.Side.m_Slot[i].m_Card.m_Index);
                        PlacementCardServerRpc(s_LocalInstance.Side.m_Slot[i].m_Card.m_Index, i);
                    }
                }
                Debug.Log("je suis client " + IsClient);
            }
        }
        [ServerRpc]
        public void PlacementCardServerRpc(int p_IDCard,int p_SlotIndex)
        {
            PlacementCardClientRpc(p_IDCard, p_SlotIndex);
            //ici on va appeller la fonction de nos event de l'instance ce notre so_Board (m_BoardInstance)
        }

        [ClientRpc]
        public void PlacementCardClientRpc(int p_IDCard, int p_SlotIndex)
        {
            for (int i = 0; i < s_LocalInstance.Side2.m_Hand.Count; i++)
            {
                if (p_IDCard == s_LocalInstance.Side2.m_Hand[i].m_Index)
                {
                    for (int j = 0; j < s_LocalInstance.Side2.m_Slot.Length; j++)
                    {
                        if(p_SlotIndex == j)
                        {
                            s_LocalInstance.Side2.m_Slot[j].Card = s_LocalInstance.Side2.m_Hand[i];
                            s_LocalInstance.Side2.m_Hand.RemoveAt(i);
                        }
                    }
                }
            }
        }

        #endregion
        public SO_Board BoardInstance
        {
            get { return s_LocalInstance; }
            set { s_LocalInstance = value; }
        }

    }
}
