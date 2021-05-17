using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NetWork
{
    public class SlotBoard 
    {
        private  EZoneCard m_Zone = null;
        private SO_CardData m_Card = null;

        public EZoneCard ZoneCard
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