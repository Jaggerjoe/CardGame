using NetWork;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BoardGame : MonoBehaviour
{
    [SerializeField]
    private Transform[] m_CardsData = new Transform[5];

    [SerializeField]
    private SO_Board m_BoardAsset = null;

    //Permet de mettre la card dans le slot ciblé sur le jeu.
    public void ReplaceGameObjectZoneByGameObjectCardInArray()
    {
        for (int j = 0; j < m_CardsData.Length; j++)
        {
            if (m_CardsData[j].gameObject.transform.childCount != 0)
            {
                Transform l_Trs = m_CardsData[j].GetComponentInChildren<DataCard>().transform;
                SO_CardData l_Card = l_Trs.GetComponent<DataCard>().Card;
                m_CardsData[j] = l_Trs;
                for (int i = 0; i < m_BoardAsset.Side.m_Slot.Length; i++)
                {
                    i = j;
                    m_BoardAsset.Side.m_Slot[i].Card = l_Card;
                    Debug.Log($"ma carte est { m_BoardAsset.Side.m_Slot[i].Card} a ma position {m_BoardAsset.Side.m_Slot[i].ZoneCard}");
                    return;
                }
            }
        }
    }
}
