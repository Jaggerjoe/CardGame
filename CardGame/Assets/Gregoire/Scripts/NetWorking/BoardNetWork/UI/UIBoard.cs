using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NetWork
{
    public class UIBoard : MonoBehaviour
    {
        [SerializeField]
        private Transform[] m_CardsData = new Transform[5];

        SO_Board m_SoBoard = null;

        private void Awake()
        {
            FindObjectOfType<SO_Board>().DropCardEvent.AddListener(GetCardOnSlot);
        }

        //va recuperer les info et les appliquer à l'UI
        public void ListenBoard()
        {

        }

        public void GetCardOnSlot()
        {
            for (int j = 0; j < m_CardsData.Length; j++)
            {
                if (m_CardsData[j].gameObject.transform.childCount != 0)
                {
                    Transform l_Trs = m_CardsData[j].GetComponentInChildren<DataCard>().transform;
                    SO_CardData l_Card = l_Trs.GetComponent<DataCard>().Card;
                    m_CardsData[j] = l_Trs;
                    for (int i = 0; i < m_SoBoard.Side.m_Slot.Length; i++)
                    {
                        i = j;
                        m_SoBoard.Side.m_Slot[i].Card = l_Card;
                        Debug.Log($"ma carte est { m_SoBoard.Side.m_Slot[i].Card} a ma position {m_SoBoard.Side.m_Slot[i].ZoneCard}");

                        return;
                    }
                }

            }
        }
    }
}
