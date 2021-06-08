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
        private EZoneCard.CardZones m_Zone = 0;
        public SO_CardData m_Card = null;

        public SlotBoard(EZoneCard.CardZones p_zone)
        {
            m_Zone = p_zone;
        }

       
        public SlotBoard()
        {
            //if(m_Card ==null)
            //{
            //    m_Card = ScriptableObject.CreateInstance<SO_CardData>();
            //    Debug.Log(m_Card);
            //}
            //else
            //{
            //    return;
            //}
            
            //Debug.Log("coucou je suis le 2e slotboard fonction");
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