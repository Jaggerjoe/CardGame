using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NetWork;

public class DragCard : MonoBehaviour
{
    private Vector3 m_Offset = Vector3.zero;

    private float m_Zcoord = 0f;

    private Vector3 m_OriginalPos;

    [SerializeField]
    private EZoneCard.CardZones m_CardZone = 0;

    private void Update()
    {
        m_Offset = gameObject.transform.position - GetMouseWorldPos();
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 l_MousePos = Input.mousePosition;

        l_MousePos.z = m_Zcoord;

        return Camera.main.ScreenToWorldPoint(l_MousePos);
    }

    private void OnMouseDown()
    {
        m_OriginalPos = transform.position;
        m_Zcoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;    
    }

    private void OnMouseDrag()
    {
        this.transform.position = GetMouseWorldPos() + m_Offset;
    }

    private void OnMouseUpAsButton()
    {
        RaycastHit[] l_Hits = { };
        l_Hits = Physics.RaycastAll(transform.position, Vector3.down, 100F);
        for (int i = 0; i < l_Hits.Length; i++)
        {
            RaycastHit hit = l_Hits[i];

            if (l_Hits[i].transform.gameObject.layer == 6)
            {
                this.transform.SetParent(l_Hits[i].transform);
                this.transform.position = l_Hits[i].transform.position + new Vector3(0, .1f, 0);
                this.transform.rotation = l_Hits[i].transform.rotation;
                this.GetComponentInParent<BoardGame>().ReplaceGameObjectZoneByGameObjectCardInArray();
                return;
            }
        }
        this.transform.position = m_OriginalPos;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(Input.mousePosition, Vector3.down *100f);
    }

    public EZoneCard.CardZones CardZone => m_CardZone;
}


