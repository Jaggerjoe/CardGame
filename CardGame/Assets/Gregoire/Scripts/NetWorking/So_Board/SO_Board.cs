using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NetWork
{
    [CreateAssetMenu(fileName = "DataBoard", menuName = "GamesBoard")]
    public class SO_Board : ScriptableObject
    {
        //va avoir 2 instances de side(coter)
        [SerializeField]
        private Player m_Player = null;

        [SerializeField]
        private int m_Slots = 0;

        [SerializeField]
        private int m_Jauge = 0;

        [SerializeField]
        private int m_Pion = 0;

        //Reçoit les RPC

    }
}
