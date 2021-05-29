using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization.Advanced;
using UnityEngine;
using UnityEngine.Events;

namespace NetWork
{

    [CreateAssetMenu(fileName = "DataBoard", menuName = "GamesBoard")]
    public class SO_Board : ScriptableObject
    {
        //va avoir 2 instances de side(coter)
        [SerializeField]
        private SideBoard m_Side = new SideBoard();
        private SideBoard m_OtherSide = new SideBoard();

        [SerializeField]
        private SlotBoard m_Slot = new SlotBoard();

        private SO_CardData m_CardDeckToHand = null;



        
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

       
        public void DrawCard(ulong p_IDSide, int p_IDCard)
        {
            //Invoke le  fait de piocher
            // m_RemoveCardDeck.Invoke();
            m_AddCardHand.Invoke();
            Debug.Log("je remove ma carte");

            //apres avoir piocher on instancie l'ui de la card 
            //m_InstantiateCard.Invoke();
        }

        //Toute les regle du jeu
        public void PutCardOnSlot(int p_SlotId,int p_CardID)
        {
            
            //On recupere l'id de la card que l'on a dans la main
            p_CardID = GetIDCard();
            Debug.Log($"mon baord dans mon so_board a la carte avec l'ID {p_CardID}");

            //Récupéré la carte
            m_DropCard.Invoke();
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

        //ajouter une carte dans la main
        public void AddDrawCard()
        {
            Side.m_Hand.Add(m_CardDeckToHand);
            //p_IDCard = Side.m_Deck[0].m_Index;
        }

        //retirer une carte du deck
        public void SwitchCardDeckToDrow()
        {
            m_CardDeckToHand = Side.m_Deck[0];
            Side.m_Deck.RemoveAt(0);
            //p_IDCard = Side.m_Deck[0].m_Index;
        }

        //fonction qui sera appeler dans le fonction qui ont besoin de savoir l'id
        public void GetPlayerSide(ulong p_PlayerNetworkID)
        {
            //va chercher le networkManager pour recup le client ID
            // sa prendrais cette forme = NetworkManager.Singleton.OwnerClientID; dans l'idee

            //si l'id tu paramettre est la meme que celle du networkManager alors c'est le local
            //sinon c'est l'autre
            //en gros, p_PlayerNetworkId doit = ou non a NetworkManager.Singleton.OwnerClientID
        }

        //je recupere l'id de ma card que je revois apres
        public int GetIDCard()
        {
            int l_IdCard;
            l_IdCard = Side.m_Hand[0].m_Index;
            Debug.Log("HAHAHA je suis index de side dans so board get id card  " + Side.m_Hand[0].m_Index);
            //Debug.Log($"je récupère l'ID de ma carte qui est {l_IdCard}");
            return l_IdCard;
        }
        public void Shuffle()
        {
            for (int i = 0; i < Side.m_Deck.Count; i++)
            {
                int j = Random.Range(i, Side.m_Deck.Count);
                SO_CardData l_Temp = Side.m_Deck[i];
                Side.m_Deck[i] = Side.m_Deck[j];
                Side.m_Deck[j] = l_Temp;
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

        public SlotBoard Slot
        {
            get { return m_Slot; }
            set { m_Slot = value; }
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
