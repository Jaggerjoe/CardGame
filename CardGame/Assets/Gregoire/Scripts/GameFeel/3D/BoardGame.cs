using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BoardGame : MonoBehaviour
{
    [System.Serializable]
    private class EventCard
    {
        public UnityEvent m_OnZone = new UnityEvent();
        public UnityEvent m_NotInZone = new UnityEvent();
    }
    
    [SerializeField]
    private Transform[] m_CardsData = new Transform[5];

    [SerializeField]
    private EventCard m_EventCard = null;
    private void Update()
    {
        //ReplaceGameObjectZoneByGameObjectCardInArray();
    }

    public void ReplaceGameObjectZoneByGameObjectCardInArray()
    {
        for (int i = 0; i < m_CardsData.Length; i++)
        {
            if (m_CardsData[i].TryGetComponent(out BoardZoneEmplacement p_Zone))
            {
                if(m_CardsData[i].gameObject.transform.childCount != 0)
                {
                    Transform l_Trs = m_CardsData[i].GetComponentInChildren<DataCard>().transform;
                    SO_CardData l_Card = l_Trs.GetComponent<DataCard>().Card;
                    EZoneCard.CardZones zone = m_CardsData[i].GetComponent<BoardZoneEmplacement>().Zone;
                    if (l_Card.m_CardZone.HasFlag(zone))
                    {
                        m_EventCard.m_OnZone.Invoke();
                        l_Trs.gameObject.layer = 2;
                    }
                    else if (l_Card.m_CardZone.HasFlag(EZoneCard.CardZones.Zone0))
                    {
                        m_EventCard.m_OnZone.Invoke();
                        l_Trs.gameObject.layer = 2;
                    }
                    else
                    {
                        m_EventCard.m_NotInZone.Invoke();
                        l_Trs.gameObject.layer = 2;
                    }
                    m_CardsData[i] = l_Trs;
                }
            }
            else
            {
                //Debug.Log("l'emplacement est déjà pris");
            }
        }
    }
}
