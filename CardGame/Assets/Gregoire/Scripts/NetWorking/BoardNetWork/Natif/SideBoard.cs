using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NetWork
{
    [System.Serializable]
    public class SideBoard
    {
        public ulong m_PlayerID;

        public List<SO_CardData> m_Hand = null;

        public List<SO_CardData> m_Deck = null;

        public List<SO_CardData> m_Discard = null;

        public SlotBoard[] m_Slot = new SlotBoard[5] 
        { 
            new SlotBoard(EZoneCard.CardZones.Zone1),
            new SlotBoard(EZoneCard.CardZones.Zone2),
            new SlotBoard(EZoneCard.CardZones.Zone3), 
            new SlotBoard(EZoneCard.CardZones.Zone4),
            new SlotBoard(EZoneCard.CardZones.Zone5)
        };

        public int m_Point = 0;
    }
}
