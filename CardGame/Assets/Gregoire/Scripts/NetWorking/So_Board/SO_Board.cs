using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.Events;

namespace NetWork
{
    [CreateAssetMenu(fileName = "DataBoard", menuName = "GamesBoard")]
    public class SO_Board : ScriptableObject
    {
        //va avoir 2 instances de side
        private SideBoard m_Side = new SideBoard();
        private SideBoard m_OtherSide = new SideBoard();

        private SlotBoard m_Slot = new SlotBoard();

#region Events
        //cette event va nous servir à si on peut poser une carte
        [SerializeField]
        private UnityEvent m_DropCard = new UnityEvent();

        //cette event va nous servir a compter les points une fois la carte poser
        [SerializeField]
        private UnityEvent m_Point = new UnityEvent();

        //cette event va nous servir a jouer l'effet
        [SerializeField]
        private UnityEvent m_Effect = new UnityEvent();

        //cette event va nous servirsavoir si on ajoute des points combot ou si on enleve
        [SerializeField]
        private UnityEvent m_CombotSigne = new UnityEvent();

        //cet event va nous servir a savoir si on ajoute des points combot ou si on enleve
        [SerializeField]
        private UnityEvent m_CombotPoint = new UnityEvent();

        //cet event va nous servir a savoir quand/si on defausse
        [SerializeField]
        private UnityEvent m_Defausse = new UnityEvent();

        //cet event va nous servir a nous dire quand il faut piocher
        [SerializeField]
        private UnityEvent m_Pioche = new UnityEvent();
        #endregion

        //Toute les regle du jeu
        public void PutCardOnSlot()
        {
            //action que l'on va faire lors d'un tour
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

        public UnityEvent DropCardEvent
        {
            get { return m_DropCard; }
            set { m_DropCard = value; }
        }

        #endregion
    }
}
