using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using UnityEngine;

namespace NetWork
{
    [System.Serializable]
    public class SlotBoard 
    {
        [SerializeField]
        private EZoneCard.CardZones m_Zone = 0;
        [SerializeField]
        private SO_CardData m_Card = null;

        public SlotBoard(EZoneCard.CardZones p_zone)
        {
            m_Zone = p_zone;
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