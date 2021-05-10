using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneDrop3D : MonoBehaviour
{
    private EZoneCard.CardZones m_CardZone = 0;
    DragCard m_Child = null;
    private void Update()
    {
        if(transform.childCount != 0)
        {
            m_Child = transform.GetComponentInChildren<DragCard>();
            if(m_Child.CardZone == m_CardZone)
            {
                Debug.Log("ils ont la même zone");
            }
            else
            {
                Debug.Log("ils ont pas la même zone");
            }
            m_Child.transform.position = transform.position;
        }
    }
}
