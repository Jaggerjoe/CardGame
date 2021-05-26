using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;

namespace NetWork
{

    public class UIBoard : MonoBehaviour
    {
        [SerializeField]
        private Transform[] m_CardsData = new Transform[5];

        [SerializeField]
        private Transform m_Hand = null;
        [SerializeField]
        private GameObject m_Card = null;

        [SerializeField]
        SO_Board m_SoBoard = null;

        private int m_SlotID = 0;
        private int m_CardID = 0; 

       // private ActionInTheBoard m_ActionPlayerNetwork = null;

        private void Start()
        {
            //j'ecoute mes events pour pouvoir plus tard les lancer avec mon ActionInTheBoard
        }
        private void Update()
        {
            GetInstanceOfTheBoard();
        }

        //va recuperer les info et les appliquer à l'UI
        public void ListenBoard()
        {
            m_SoBoard.DropCardEvent.AddListener(GetCardOnSlot);
            //m_SoBoard.RemoveCardDeckEvent.AddListener(TakeCardDeck);
            m_SoBoard.AddCardHandEvent.AddListener(DrawUICard);
            m_SoBoard.InstantiateCardEvent.AddListener(DrawUICard);
        }

        //je recupère l'instance de mon board qui a été crée au moment de la connexion de mon joueur.
        private void GetInstanceOfTheBoard()
        {
            if(m_SoBoard == null)
            {
                if (FindObjectOfType<SO_Board>())
                {
                    Debug.Log("coucou j'ai trouvé ta référence");
                    m_SoBoard = FindObjectOfType<ActionInTheBoard>().BoardInstance;
                    ListenBoard();
                }
            }
        }

        private void DrawUICard()
        {
           //m_SoBoard.DrawCardInfo();
           
            //on va creer une boucle qui fera apparaitre 6 fois nos card
            for(int i = 0; i < 6; i++)
            {
                Debug.Log("coucou je suis devant");
                m_SoBoard.SwitchCardDeckToDrow();
                m_SoBoard.DrawCardData();
                TakeCardDeck();
                Debug.Log("coucou je suis derrière");
                //on instancie la prefab a la position de la main qui est GO_Vide in scene
                GameObject l_Card = Instantiate(m_Card, m_Hand);

                //on met la card en enfant de la main
                l_Card.transform.SetParent(m_Hand);
                //on recupere le compenent dataCard que a la prefab et on donne le so_card 
                //situer a l'emplacement zero du deck qui est dans le side lui même dans le so_board
                l_Card.GetComponent<DataCard>().Card = m_SoBoard.Side.m_Hand[i];
                Debug.Log("recuperation carte asset " + m_SoBoard.Side.m_Hand[i]);
            }
        }

        public void TakeCardDeck()
        {
            //animation de pioche
        }

        //je récupère la position de ma carte une fois posé pour l'appliquer a mon Board
        public void GetCardOnSlot()
        {
            for (int j = 0; j < m_CardsData.Length; j++)
            {
                if (m_CardsData[j].gameObject.transform.childCount != 0)
                {
                    Transform l_Trs = m_CardsData[j].GetComponentInChildren<DataCard>().transform;
                    SO_CardData l_Card = l_Trs.GetComponent<DataCard>().Card;
                    m_CardsData[j] = l_Trs;
                    for (int i = 0; i < m_SoBoard.Side.m_Slot.Length; i++)
                    {
                        i = j;
                        i = m_SlotID;
                        m_SoBoard.Side.m_Slot[i].Card = l_Card;
                        Debug.Log($"ma carte est { m_SoBoard.Side.m_Slot[i].Card} a ma position {m_SoBoard.Side.m_Slot[i].ZoneCard}");
                        return;
                    }
                }
            }
        }
    }
}
