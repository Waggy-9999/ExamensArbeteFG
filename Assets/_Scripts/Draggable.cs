using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Transform originalParent;
    public EasingManager easingManager;
    private Vector3 startPosition;

    public Transform parentToReturnTo = null; // the parent of the card
    public Transform placeholderParent = null; // the parent of the placeholder

    GameObject placeholder = null; // the placeholder for the card
    
    public enum slot {MINION, CONSUMABLE}
    public slot typeOfItem = slot.MINION;

    public void OnBeginDrag(PointerEventData eventData)
    {
        startPosition = transform.position;
        originalParent = transform.parent;

        Debug.Log("OnBeginDrag");

        placeholder = new GameObject(); // create a new gameobject
        placeholder.transform.SetParent(this.transform.parent); // set the parent of the placeholder to the parent of the card
        LayoutElement le = placeholder.AddComponent<LayoutElement>();
        le.preferredWidth = this.GetComponent<LayoutElement>().preferredWidth; // set the width of the placeholder to the width of the card
        le.preferredHeight = this.GetComponent<LayoutElement>().preferredHeight; // set the height of the placeholder to the height of the card
        le.flexibleWidth = 0; // set the flexible width of the placeholder to 0
        le.flexibleHeight = 0; // set the flexible height of the placeholder to 0

        placeholder.transform.SetSiblingIndex(this.transform.GetSiblingIndex()); // set the sibling index of the placeholder to the sibling index of the card

        parentToReturnTo = this.transform.parent;
        placeholderParent = parentToReturnTo;
        this.transform.SetParent(this.transform.parent.parent);

        GetComponent<CanvasGroup>().blocksRaycasts = false; // this is so that the raycast doesn't hit the card
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("OnDrag");

        this.transform.position = eventData.position; // this is the position of the mouse

        if(placeholder.transform.parent != placeholderParent) // if the parent of the placeholder is not the parent of the card
            placeholder.transform.SetParent(placeholderParent); // set the parent of the placeholder to the parent of the card (this is so that the placeholder doesn't move with the card

        int newSiblingIndex = placeholderParent.childCount; // set the new sibling index to the number of children of the parent

        for(int i=0; i < placeholderParent.childCount; i++) // loop through all the children of the parent
        {
            if(this.transform.position.x < placeholderParent.GetChild(i).position.x) // if the position of the mouse is less than the position of the child
            {
                newSiblingIndex = i;

                if(placeholder.transform.GetSiblingIndex() < newSiblingIndex) // if the sibling index of the placeholder is less than the new sibling index
                    newSiblingIndex--; // decrement the new sibling index
                    
                break;
            }
        }
        placeholder.transform.SetSiblingIndex(newSiblingIndex); // set the sibling index of the placeholder to the new sibling index
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // If there is a placeholder, ease to its position; otherwise, ease back to the start position
        Vector3 targetPosition = placeholder != null ? placeholder.transform.position : startPosition;

        if (easingManager != null)
        {
            easingManager.EaseToPosition(gameObject, targetPosition);
        }

        // Reset parent
        transform.SetParent(originalParent);

        Debug.Log("OnEndDrag");
        this.transform.SetParent(parentToReturnTo);
        this.transform.SetSiblingIndex(placeholder.transform.GetSiblingIndex()); // set the sibling index of the card to the sibling index of the placeholder
        GetComponent<CanvasGroup>().blocksRaycasts = true;

        Destroy(placeholder);
    }
}
