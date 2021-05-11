using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class DropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        Draggable drag = eventData.pointerDrag.GetComponent<Draggable>();
        if(drag != null)
        {
            drag.m_Parent = this.transform;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("pointerEnter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("PointerExit");

    }
}
