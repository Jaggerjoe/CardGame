using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardGame : MonoBehaviour
{
    [SerializeField]
    private Transform[] m_CardsData = new Transform[5];

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
                    Debug.Log($"oh moins 1 enfant chakal  sur la zone : {m_CardsData[i]}");
                    Transform l_Trs = m_CardsData[i].GetComponentInChildren<DataCard>().transform;
                    SO_CardData l_Card = l_Trs.GetComponent<DataCard>().Card;
                    EZoneCard.CardZones zone = m_CardsData[i].GetComponent<BoardZoneEmplacement>().Zone;
                    if (l_Card.m_CardZone.HasFlag(zone))
                    {
                        Debug.Log("je suis dans ma zone");
                    }
                    else if (l_Card.m_CardZone.HasFlag(EZoneCard.CardZones.Zone0))
                    {
                        Debug.Log("Elle peut etre mise partout");   
                    }
                    else
                    {
                        Debug.Log("je ne suis pas dans ma zone");
                    }
                    Debug.Log($"mon element est : { m_CardsData[i].GetComponentInChildren<DataCard>().transform}");
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
