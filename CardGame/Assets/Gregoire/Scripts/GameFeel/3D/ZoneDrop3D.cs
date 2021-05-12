using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ZoneDrop3D : MonoBehaviour
{
    [System.Serializable]
    private class DropEvent
    {
        public UnityEvent m_DropInRightZone = new UnityEvent();
        public UnityEvent m_DropInWrongZone = new UnityEvent();
    }

    [SerializeField]
    private EZoneCard.CardZones m_CardZone = 0;
    DragCard m_Child = null;

    [SerializeField]
    private BoardGame m_BoardGame = null;

    [SerializeField]
    private DropEvent m_EventDrop;
    private void Start()
    {
        m_BoardGame = FindObjectOfType<BoardGame>();
    }

    public void CheckCardZone()
    {
        if (transform.childCount != 0)
        {
            m_Child = transform.GetComponentInChildren<DragCard>();
            if(m_Child.CardZone.HasFlag(m_CardZone))
            {
                m_BoardGame.ReplaceGameObjectZoneByGameObjectCardInArray();
                m_EventDrop.m_DropInRightZone.Invoke();
            }
            else if(m_Child.CardZone.HasFlag(EZoneCard.CardZones.Zone0))
            {
                m_BoardGame.ReplaceGameObjectZoneByGameObjectCardInArray();
                m_EventDrop.m_DropInRightZone.Invoke();
            }
            else
            {
                m_BoardGame.ReplaceGameObjectZoneByGameObjectCardInArray();
                m_EventDrop.m_DropInWrongZone.Invoke();
            }
            m_Child.transform.position = transform.position + new Vector3(0,0.1f,0);
        }
    }
}
