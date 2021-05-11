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
    private DropEvent m_EventDrop;

    public void CheckCardZone()
    {
        if (transform.childCount != 0)
        {
            m_Child = transform.GetComponentInChildren<DragCard>();
            if(m_Child.CardZone.HasFlag(m_CardZone))
            {
                m_EventDrop.m_DropInRightZone.Invoke();
            }
            else if(m_Child.CardZone.HasFlag(EZoneCard.CardZones.Zone0))
            {
                m_EventDrop.m_DropInRightZone.Invoke();
            }
            else
            {
                m_EventDrop.m_DropInWrongZone.Invoke();
            }
            m_Child.transform.position = transform.position;
        }

        if (transform.childCount != 0)
        {
            Debug.Log("j'ai au moins un enfant");
            m_Child = transform.GetComponentInChildren<DragCard>();
            switch(m_CardZone)
            {
                case EZoneCard.CardZones.Zone1:
                    Debug.Log("couco1");
                    break;
                case EZoneCard.CardZones.Zone3:
                    Debug.Log("couco3");
                    break;

            }
            m_Child.transform.position = transform.position;
        }
    }

}
