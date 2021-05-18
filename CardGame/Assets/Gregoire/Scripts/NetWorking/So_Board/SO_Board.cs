using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.LookDev;
using UnityEngine;

namespace NetWork
{
    [CreateAssetMenu(fileName = "DataBoard", menuName = "GamesBoard")]
    public class SO_Board : ScriptableObject
    {
        //va avoir 2 instances de side(coter)
        [SerializeField]
        private SideBoard m_Side = new SideBoard();
        private SideBoard m_Side2 = new SideBoard();

        //private SlotBoard m_Slot = new SlotBoard();

        //Reçoit les RPC

        #region Accesseur

        public SideBoard Side
        {
            get { return m_Side; }
            set { m_Side = value; }
        }
        public SideBoard Side2
        {
            get { return m_Side2; }
            set { m_Side2 = value; }
        }

        //public SlotBoard Slot
        //{
        //    get { return m_Slot; }
        //    set { m_Slot = value; }
        //}

        #endregion

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
    }
}
