using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Draggable.slot typeOfItem = Draggable.slot.MINION;
    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("OnPointerEnter");

        if (eventData.pointerDrag == null) // if there is no card being dragged
            return;

        Draggable d = eventData.pointerDrag.GetComponent<Draggable>(); // get the draggable component of the card
        if (d != null)
        {
            if(typeOfItem == d.typeOfItem) {
                d.placeholderParent = this.transform;
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("OnPointerExit");

         if (eventData.pointerDrag == null) // if there is no card being dragged
            return;

        Draggable d = eventData.pointerDrag.GetComponent<Draggable>(); // get the draggable component of the card
        if (d != null && d.placeholderParent == this.transform)
        {
            d.placeholderParent = d.parentToReturnTo;

            if(typeOfItem == d.typeOfItem) {
                d.placeholderParent = this.transform;
            }
        }
            
    }

    public void OnDrop(PointerEventData eventData)
    {
        // rest of your code
    }
}