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
        private SideBoard m_SideChoice = new SideBoard();
        [SerializeField]
        private SideBoard m_OtherSide = new SideBoard();

        [SerializeField]
        private SlotBoard m_Slot = new SlotBoard();

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
        public void DrawCardBoard(ulong p_IDSide)//,int p_IDCard)
        {
            GetPlayerSide(p_IDSide);
            SwitchCardDeckToDrow();//p_IDCard);
            //Invoke le  fait de piocher
            // m_RemoveCardDeck.Invoke();

            //m_AddCardHand.Invoke();
            Debug.Log("je suis le side appeler " + p_IDSide);
           // Debug.Log("je suis la list appeler " + p_IDCard);

            //apres avoir piocher on instancie l'ui de la card 
            //m_InstantiateCard.Invoke();
        }


        //ajouter une carte dans la main
        private void AddDrawCard()
        {
            m_SideChoice.m_Hand.Add(m_CardDeckToHand);
            //Debug.Log(m_CardDeckToHand);
            Debug.Log("longueur de la main = " +m_SideChoice.m_Hand.Count);
            //p_IDCard = Side.m_Deck[0].m_Index;
        }

       
        //retirer une carte du deck
        private void SwitchCardDeckToDrow()//int p_IDCard)
        {
          //on a une boucle de taille de 6
            for (int i = 0; i < 6; i++)
            {
                //Debug.Log("coucou " + m_SideChoice.m_Deck[0]);
                //on dit que notre asset est egale a un coter a la postion 0
                m_CardDeckToHand = m_SideChoice.m_Deck[0];
               
                //le p_idCard est egale a un coter d'un deck a la position 0 sur l'index
               // p_IDCard = m_SideChoice.m_Deck[0].m_Index;
                //Debug.Log("je suis indxe de card " + p_IDCard);

                //on eneleve sur un coter du ceck la carte set
                m_SideChoice.m_Deck.RemoveAt(0);

                //on l'ajoute a la main
                AddDrawCard();
            }
        }
        #endregion

        #region poser card on the slot
        //Toute les regle du jeu appeler dans l'action in the board dans le placementCard
        public void PutCardOnSlot(ulong p_SideID,int p_SlotId,int p_CardID)
        {

            GetPlayerSide(p_SideID);
            //On recupere l'id de la card que l'on a dans la main
            p_CardID = GetIDCard(p_CardID);
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
        #endregion

        //fonction qui sera appeler dans le fonction qui ont besoin de savoir l'id
        //appeler dans putCardOnSlot
        private void GetPlayerSide(ulong p_PlayerNetworkID)
        {
            //va chercher le networkManager pour recup le client ID
            // sa prendrais cette forme = NetworkManager.Singleton.OwnerClientID; dans l'idee
           p_PlayerNetworkID = FindObjectOfType<ActionInTheBoard>().OwnerClientId;

            //si l'id tu paramettre est la meme que celle du networkManager alors c'est le local et on faite aciton side
            if (p_PlayerNetworkID == NetworkManager.Singleton.LocalClientId)
            {
                m_SideChoice = m_Side;
                Debug.Log("je suis tsa mere" + m_SideChoice);
                //tout ce qui se passe sur un sid
            }
            else
            {
                m_SideChoice = m_OtherSide;
                Debug.Log("je suis ton pere" + m_SideChoice);

            }
            //sinon c'est l'autre
            //en gros, p_PlayerNetworkId doit = ou non a NetworkManager.Singleton.OwnerClientID
        }

        /*public void SetDeck(SO_CardData p_Card,ulong p_IDside)
        {
            GetPlayerSide(p_IDside);
            m_SideChoice.m_Deck.Add(p_Card);
        }*/

        //je recupere l'id de ma card que je revois apres
        //appeler dans putCardOnTheSlot
        private int GetIDCard(int p_Side)
        {
            int l_IdCard;
            l_IdCard = m_Side.m_Hand[0].m_Index;
            Debug.Log("HAHAHA je suis index de side dans so board get id card  " + Side.m_Hand[0].m_Index);
            //Debug.Log($"je récupère l'ID de ma carte qui est {l_IdCard}");
            return l_IdCard;
        }


        ////appeler dans l'action in the board dans le networkStart
      /*  public void Shuffle(SideBoard p_side)
        {
            for (int i = 0; i < p_side.m_Deck.Count; i++)
            {
                int j = Random.Range(i, p_side.m_Deck.Count);
                SO_CardData l_Temp = p_side.m_Deck[i];
                p_side.m_Deck[i] = p_side.m_Deck[j];
                p_side.m_Deck[j] = l_Temp;
            }
        }*/





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
