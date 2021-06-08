using UnityEngine;
using MLAPI;
using UnityEngine.Events;
using System.Collections.Generic;
using System.Collections;

namespace NetWork
{

    [CreateAssetMenu(fileName = "DataBoard", menuName = "GamesBoard")]
    public class SO_Board : ScriptableObject
    {
        //va avoir 2 instances de side(coter)
        [SerializeField]
        private SideBoard m_Side = new SideBoard();

        [SerializeField]
        private SideBoard m_OtherSide = new SideBoard();

        private SO_CardData m_CardDeckToHand = null;

        //liste d'asset card qui n'est jamais changer
        [SerializeField]
        private List<SO_CardData> m_AllCardsInstance =  new List<SO_CardData>();

        
        #region Events
        //cette event va nous servir � si on peut poser une carte
        
        [Space(10)]
        private UnityEvent m_RemoveCardDeck = new UnityEvent();

        //cet event va nous servir a nous dire quand il faut piocher

        private UnityEvent m_AddCardHand = new UnityEvent();

        private UnityEvent m_InstantiateCard = new UnityEvent();

       
        private UnityEvent m_DropCard = new UnityEvent();

        //cette event va nous servir a compter les points une fois la carte poser
       
        private UnityEvent m_Point = new UnityEvent();

        //cette event va nous servir a jouer l'effet
        
        private UnityEvent m_Effect = new UnityEvent();

        //cette event va nous servirsavoir si on ajoute des points combot ou si on enleve
        
        private UnityEvent m_CombotSigne = new UnityEvent();

        //cet event va nous servir a savoir si on ajoute des points combot ou si on enleve
        
        private UnityEvent m_CombotPoint = new UnityEvent();

        //cet event va nous servir a savoir quand/si on defausse
        
        private UnityEvent m_Defausse = new UnityEvent();


        #endregion

        //on donne le deck side et selui de l'other side
        public void SetDeckSideAndOtherSide()
        {
            //boucle comme un for
            //on recupere la liste dans l'ordre 
            Side.m_Deck.AddRange(m_AllCardsInstance);
            Side2.m_Deck.AddRange(m_AllCardsInstance);
        }

        #region pioche 

        //appeler de l'action in the board
        public void DrawCardBoard(ulong p_IDSide)
        {
            //Invoke le  fait de piocher
            m_AddCardHand.Invoke();
        }

       
        //retirer une carte du deck
        public void SwitchCardDeckToDrow(SideBoard p_side)//int p_IDCard)
        {
          //on a une boucle de taille de 6
            for (int i = 0; i < 6; i++)
            {
                p_side.m_Hand.Add(p_side.m_Deck[0]);
                //on eneleve sur un coter du ceck la carte set
                p_side.m_Deck.RemoveAt(0);
            }
        }
        #endregion

        #region poser card on the slot
        //Toute les regle du jeu appeler dans l'action in the board dans le placementCard
        public void PutCardOnSlot()
        {

            //GetPlayerSide(p_SideID);
            //On recupere l'id de la card que l'on a dans la main
            //p_CardID = GetIDCard(p_CardID);

            //Récupéré la carte
            //m_DropCard.Invoke();
            //Ajouter la carte sur le slot 

            //On va regarder de quel side on est

            //on va poser nos card sur les slots, 
            //si il n'y a pas de card deja poser sur un slot alors on peut poser notre card

            //on va check si la card et le slot ont la meme zone

            //on va jouer l'effet

            //on va compter les points

            //on va compter les combot signe (retire ou ajouter des points)

            //on va compter les combot point (retire ou ajouter des points)

            //On va piocher jusqu'a avoir 6 card
        }
        #endregion
        //j'ajoute ma carte a mon slot et je la retire de ma main 
        public void SetCardOnSlotAndRemoveCardOnHand(int p_CardNumber, int p_SlotIndex)
        {
            for (int i = 0; i < Side.m_Hand.Count; i++)
            {
                if(p_CardNumber == Side.m_Hand[i].m_Index)
                {
                    for (int j = 0; j < Side.m_Slot.Length; j++)
                    {
                        if(j == p_SlotIndex)
                        {
                            Side.m_Slot[j].Card = Side.m_Hand[i];
                            Side.m_Hand.RemoveAt(i);
                        }
                    }
                }
            }
        }

        #region Accesseur
        public SideBoard Side
        {
            get { return m_Side; }
            set { m_Side = value; }
        }
        public SideBoard Side2
        {
            get { return m_OtherSide; }
            set { m_OtherSide = value; }
        }

        public List<SO_CardData> AllCards
        {
            get { return m_AllCardsInstance; }
          //  set { m_AllCardsInstance = value; }
        }
        public UnityEvent InstantiateCardEvent
        {
            get { return m_InstantiateCard; }
            set { m_InstantiateCard = value; }
        }
        public UnityEvent RemoveCardDeckEvent
        {
            get { return m_RemoveCardDeck; }
            set { m_RemoveCardDeck = value; }
        }
        public UnityEvent AddCardHandEvent
        {
            get { return m_AddCardHand; }
            set { m_AddCardHand = value; }
        }
        public UnityEvent DropCardEvent
        {
            get { return m_DropCard; }
            set { m_DropCard = value; }
        }

        #endregion
    }
}
