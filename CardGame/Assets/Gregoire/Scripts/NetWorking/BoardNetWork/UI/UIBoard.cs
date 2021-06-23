using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;

namespace NetWork
{
    public class UIBoard : MonoBehaviour
    {
        [SerializeField]
        private SlotInfo[] m_SlotsTransforms = new SlotInfo[5];
        
        [SerializeField]
        private SlotInfo[] m_SlotInfoOtherSide = new SlotInfo[5];

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
            Debug.Log("ListenBoard");
            m_SoBoard.ApplyCardOnSlotEvent.AddListener(ApplyCardOnSlot);
            m_SoBoard.AddCardHandEvent.AddListener(DrawUICardStartGame);
            m_SoBoard.InstantiateCardEvent.AddListener(DrawUICardStartGame);
            m_SoBoard.ApplyCardOnSlotOtherSidevent.AddListener(SetCardUI);
        }

        //je recupère l'instance de mon board qui a été crée au moment de la connexion de mon joueur.
        private void GetInstanceOfTheBoard()
        {
            Debug.Log("coucu");
            if (m_SoBoard != null)
            {
                Debug.Log("coucurrr");
                if (FindObjectOfType<SO_Board>())
                {
                    Debug.Log("coucu je sui find");
                    m_SoBoard = FindObjectOfType<ActionInTheBoard>().BoardInstance;
                    Debug.Log("couculllll");
                    ListenBoard();
                    Debug.Log("coucu je sui null board");
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
                TakeCardDeck();

                //on instancie la prefab a la position de la main qui est GO_Vide in scene
                GameObject l_Card = Instantiate(m_Card, new Vector3(m_HandOtherSide.position.x + (m_Space * (i % m_SoBoard.Side2.m_Hand.Count)), 0.1f, m_HandOtherSide.position.z), transform.rotation);

                //on met la card en enfant de la main
                l_Card.transform.SetParent(m_HandOtherSide);
                //on recupere le compenent dataCard que a la prefab et on donne le so_card 
                //situer a l'emplacement zero du deck qui est dans le side lui même dans le so_board
                l_Card.GetComponent<DataCard>().Card = m_SoBoard.Side2.m_Hand[i];
                //on instancie la prefab a la position de la main qui est GO_Vide in scene

            }
        }

        public void TakeCardDeck()
        {
            //animation de pioche
        }

        //je récupère la position de ma carte une fois posé pour l'appliquer a mon Board
        public void ApplyCardOnSlot()
        {
            for (int j = 0; j < m_SlotsTransforms.Length; j++)
            {
                //je récupère ma carte qui est en enfant de ma zone
                if (m_SlotsTransforms[j].gameObject.transform.childCount != 0)
                {
                    //je recupere le transform de ma card se trouvant en enfant 
                    Transform l_Trs = m_SlotsTransforms[j].GetComponentInChildren<DataCard>().transform;
                    SO_CardData l_Card = l_Trs.GetComponent<DataCard>().Card;
                   
                    //je rempli le so_card du dataCArd avec le so_card de mon slot info sur mon board
                    m_SlotsTransforms[j].GetComponent<SlotInfo>().m_Card = l_Card;
                }
            }
            return;
        }
       
        public void SetCardUI()
        {
            //je veux afficher la carte de mon adversaire sur son slot ou elle a été posé sur mon instance.
            for (int i = 0; i < m_SoBoard.Side2.m_Slot.Length; i++)
            {
                //si je trouve un So_Card
                if(m_SoBoard.Side2.m_Slot[i].Card)
                {
                    //alor pour chaque slot info
                    for (int j = 0; j < m_SlotInfoOtherSide.Length; j++)
                    {
                        //si l'element j du slot a info ne contient de data card
                        if (m_SlotInfoOtherSide[j].GetComponent<SlotInfo>().m_Card == null)
                        {
                            
                            if(i == j)
                            {
                                //alors mon dataCard de mon slot info rempli  mon slot sur mon board a son element i par un dataCArd
                                m_SlotInfoOtherSide[j].GetComponent<SlotInfo>().m_Card = m_SoBoard.Side2.m_Slot[i].Card;
                                // j'instancie ma card adverse sur sa position et rotation
                                GameObject l_Obj = Instantiate(m_Card, m_SlotInfoOtherSide[j].transform.position + new Vector3(0, .1f, 0), m_SlotInfoOtherSide[j].transform.rotation); 
                                //je mets ma card en enfant de la zone droper 
                                l_Obj.transform.parent = m_SlotInfoOtherSide[j].transform;
                                //je vais recup mon datacard pour aller chercher mon so_card, pour remplir avec le so_Card du slot info
                                l_Obj.GetComponent<DataCard>().Card = m_SlotInfoOtherSide[j].GetComponent<SlotInfo>().m_Card;
                                //j'appel la fonction creat asset pour pouvoir mettre les info text.
                                l_Obj.GetComponent<DataCard>().CreateAssetCad();
                                //je ne parcours pas toute la boucle
                                return;
                            }
                        }
                    }
                }
            }
        }
    }
}
