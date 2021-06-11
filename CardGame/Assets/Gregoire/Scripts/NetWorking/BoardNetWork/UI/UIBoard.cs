using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;

namespace NetWork
{
    public class UIBoard : MonoBehaviour
    {
        [SerializeField]
        private Transform[] m_SlotsTransforms = new Transform[5];

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


        private void Update()
        {
            GetInstanceOfTheBoard();
        }

        //va recuperer les info et les appliquer à l'UI
        public void ListenBoard()
        {
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
                    ListenBoard();
                }
            }
        }

        private void DrawUICardStartGame()
        {
            //on va creer une boucle qui fera apparaitre 6 fois nos card

            for (int i = 0; i < m_SoBoard.Side.m_Hand.Count; i++)
            {
                TakeCardDeck();

                //on instancie la prefab a la position de la main qui est GO_Vide in scene
                GameObject l_Card = Instantiate(m_Card, new Vector3(m_Hand.position.x + (m_Space * (i % m_SoBoard.Side.m_Hand.Count)), 0.1f, m_Hand.position.z), transform.rotation);

                //on met la card en enfant de la main
                l_Card.transform.SetParent(m_Hand);
                //on recupere le compenent dataCard que a la prefab et on donne le so_card 
                //situer a l'emplacement zero du deck qui est dans le side lui même dans le so_board
                l_Card.GetComponent<DataCard>().Card = m_SoBoard.Side.m_Hand[i];
            }

            for (int i = 0; i < m_SoBoard.Side2.m_Hand.Count; i++)
            {
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
        public void ApplyCardOnSlot(out SO_CardData p_CardAsset, out int p_SlotIndex)
        {
            for (int j = 0; j < m_SlotsTransforms.Length; j++)
            {
                //je récupère ma carte qui est en enfant de ma zone
                if (m_SlotsTransforms[j].gameObject.transform.childCount != 0)
                {
                    //je recupere le transform de ma card se trouvant en enfant 
                    Transform l_Trs = m_SlotsTransforms[j].GetComponentInChildren<DataCard>().transform;
                    SO_CardData l_Card = l_Trs.GetComponent<DataCard>().Card;
                    //je remplit l'index avec la carte
                    m_SlotsTransforms[j] = l_Trs;

                    //je boucle sur mon side et donc sur mon slot
                    for (int i = 0; i < m_SoBoard.Side.m_Slot.Length; i++)
                    {
                        // si mon slot i est egale a la position de mon enfant card
                        if(i == j)
                        {
                            //alors je donnec ma card au slot et donc la retire de ma main 
                            m_SoBoard.SetCardOnSlotAndRemoveCardFromHand(l_Card.m_Index, i);
                            Debug.Log($"ma carte est { m_SoBoard.Side.m_Slot[i].Card} a ma position {m_SoBoard.Side.m_Slot[i].ZoneCard} et {i}");
                            p_CardAsset = l_Card;
                            p_SlotIndex = i;
                            return;
                        }
                    }
                }
            }

            p_CardAsset = null;
            p_SlotIndex = -1;
            return;
        }
       
        public void SetCardOnBoardUI()
        {

        }
    }
}
