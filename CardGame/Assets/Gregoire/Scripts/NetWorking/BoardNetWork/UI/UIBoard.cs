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
        private Transform m_HandOtherSide = null;

        [SerializeField]
        private GameObject m_Card = null;

        [SerializeField]
        SO_Board m_SoBoard = null;

        [SerializeField]
        private float m_Space = 2;

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
            m_SoBoard.AddCardHandEvent.AddListener(DrawUICardStartGame);
            m_SoBoard.InstantiateCardEvent.AddListener(DrawUICardStartGame);
        }

        //je recupère l'instance de mon board qui a été crée au moment de la connexion de mon joueur.
        private void GetInstanceOfTheBoard()
        {
            if(m_SoBoard == null)
            {
                if (FindObjectOfType<SO_Board>())
                {
                    m_SoBoard = FindObjectOfType<ActionInTheBoard>().BoardInstance;
                    //Debug.Log("jerecupère mon instance");
                    ListenBoard();
                }
            }
        }

        private void DrawUICardStartGame()
        {
            //m_SoBoard.DrawCardInfo();
            //if(m_board.side.playerID == 0)
            //on va creer une boucle qui fera apparaitre 6 fois nos card

            for (int i = 0; i < m_SoBoard.Side.m_Hand.Count; i++)
            {
                Debug.Log("coucou je suis appeller ");
                TakeCardDeck();

                //on instancie la prefab a la position de la main qui est GO_Vide in scene
                //GameObject l_Card = Instantiate(m_Card, m_Hand);
                GameObject l_Card = Instantiate(m_Card, new Vector3(m_Hand.position.x + (m_Space * (i % m_SoBoard.Side.m_Hand.Count)), 0.1f, m_Hand.position.z), transform.rotation);

                //on met la card en enfant de la main
                l_Card.transform.SetParent(m_Hand);
                //on recupere le compenent dataCard que a la prefab et on donne le so_card 
                //situer a l'emplacement zero du deck qui est dans le side lui même dans le so_board
                l_Card.GetComponent<DataCard>().Card = m_SoBoard.Side.m_Hand[i];
            }

            for (int i = 0; i < m_SoBoard.Side2.m_Hand.Count; i++)
            {
                Debug.Log("coucou je suis appeller ");
                TakeCardDeck();

                //on instancie la prefab a la position de la main qui est GO_Vide in scene
                GameObject l_Card = Instantiate(m_Card, m_HandOtherSide);

                //on met la card en enfant de la main
                l_Card.transform.SetParent(m_HandOtherSide);
                //on recupere le compenent dataCard que a la prefab et on donne le so_card 
                //situer a l'emplacement zero du deck qui est dans le side lui même dans le so_board
                l_Card.GetComponent<DataCard>().Card = m_SoBoard.Side2.m_Hand[i];

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
                    Debug.Log($"la carte est sur le slot {j}");
                    for (int i = 0; i < m_SoBoard.Side.m_Slot.Length; i++)
                    {
                        if(i ==j)
                        {
                            m_SoBoard.SetCardOnSlotAndRemoveCardOnHand(l_Card.m_Index, i);
                            Debug.Log($"ma carte est { m_SoBoard.Side.m_Slot[i].Card} a ma position {m_SoBoard.Side.m_Slot[i].ZoneCard} et {i}");
                        }
                    }
                }
            }
        }
        public void SetCardOnBoardUI()
        {

        }
    }
}
