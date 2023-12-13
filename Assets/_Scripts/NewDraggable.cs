/// <summary>
/// This script allows a GameObject to be draggable with the mouse in a 2D space.
/// </summary>
using UnityEngine;
using UnityEngine.EventSystems;

public class NewDraggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector2 offset;
    // OnBeginDrag is called on the first frame that the user starts dragging the card.
    public void OnBeginDrag(PointerEventData eventData)
    {
        offset = (Vector2)transform.position - eventData.position;
        Debug.Log("OnBeginDrag");
    }

    // OnDrag is called every frame while the user is dragging the GameObject.
    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag");
        transform.position = eventData.position + offset;
    }

    // OnEndDrag is called when the user stops dragging the GameObject.
    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");
    }
}