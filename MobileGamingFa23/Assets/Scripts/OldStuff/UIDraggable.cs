using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIDraggable : MonoBehaviour, IDragHandler
{
    Vector2 PosStart;

    void Start()
    {
        PosStart = transform.position;
    }
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }
    public void ResetPosition()
    {
        transform.position = PosStart;
    }

}
