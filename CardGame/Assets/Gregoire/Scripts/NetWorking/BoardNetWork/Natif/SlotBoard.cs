using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NetWork
{
    [System.Serializable]
    public class SlotBoard 
    {
        private EZoneCard.CardZones m_Zone = 0;
        private SO_CardData m_Card = null;

        public SlotBoard(EZoneCard.CardZones zone1)
        {
            m_Zone = zone1;
        }

        public EZoneCard.CardZones ZoneCard
        {
            get { return m_Zone; }
            set { m_Zone = value; }
        }

        public SO_CardData Card
        {
            get { return m_Card; }
            set { m_Card = value; }
        }
    }
}