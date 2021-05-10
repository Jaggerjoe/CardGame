using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragCard : MonoBehaviour
{
    public GameObject m_Card = null;
    public Transform m_Parent = null;

    private Vector3 m_MousePos = Vector3.zero;

    private Vector3 m_Offset = Vector3.zero;

    private float m_Zcoord = 0f;

    private LayerMask m_Layer = 0;

    private ZoneDrop3D m_Zone = null;

    [SerializeField]
    private EZoneCard.CardZones m_CardZone = 0;

    private void Update()
    {
        m_MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

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
        m_Zcoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
    }

    private void OnMouseDrag()
    {
        this.transform.position = GetMouseWorldPos() + m_Offset;
        m_Card = this.gameObject;
       
        //gameObject.layer = 2;
    }

    private void OnMouseUpAsButton()
    {
        RaycastHit[] l_Hits = { };
        l_Hits = Physics.RaycastAll(transform.position, Vector3.forward, 100F);
        for (int i = 0; i < l_Hits.Length; i++)
        {
            RaycastHit hit = l_Hits[i];
            m_Zone = hit.transform.GetComponent<ZoneDrop3D>();
            if (m_Zone != null)
            {
                m_Card.transform.SetParent(m_Zone.transform);
                m_Card.transform.position = m_Zone.transform.position;
                m_Zone.CheckCardZone();
            }
        }
    }

    public EZoneCard.CardZones CardZone => m_CardZone;
}


