using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NetWork;

public class DragCard : MonoBehaviour
{
    private Vector3 m_Offset = Vector3.zero;

    private float m_Zcoord = 0f;

    private Vector3 m_OriginalPos;

    private UIBoard m_UIBoard = null;
    private ActionInTheBoard m_ActionInTheBoard = null;
    private SO_CardData m_DraggedCard = null;
    private Camera m_Cam;

    [SerializeField]
    private LayerMask m_Layer = 0;
    private void Start()
    {
        m_ActionInTheBoard = FindObjectOfType<ActionInTheBoard>();
        m_UIBoard = FindObjectOfType<UIBoard>();
        m_Cam = FindObjectOfType<Camera>();
    }
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
        Ray l_ray = m_Cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit l_hit;
        if (Physics.Raycast(l_ray, out l_hit, 10000f, m_Layer))
        {
            Debug.Log("couocu");
            m_DraggedCard = l_hit.transform.gameObject.GetComponent<DataCard>().Card;
            Debug.Log(m_DraggedCard);
        }
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
                if(l_Hits[i].transform.childCount == 0)
                {
                    this.transform.SetParent(l_Hits[i].transform);
                    this.transform.position = l_Hits[i].transform.position + new Vector3(0, .1f, 0);
                    this.transform.rotation = l_Hits[i].transform.rotation;
                    int l_IndexSlot = l_Hits[i].transform.gameObject.GetComponent<SlotInfo>().m_SlotIndex;
                    m_ActionInTheBoard.PlacementCard(MLAPI.NetworkManager.Singleton.LocalClientId, m_DraggedCard.m_Index, l_IndexSlot);

                    return;
                }
            }
        }
        this.transform.position = m_OriginalPos;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(Input.mousePosition, Vector3.down *100f);
    }
}

