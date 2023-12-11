using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Draggable2 : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public CardType CardType { get; set; }
    public Transform originalParent; // Stores the original parent of the card.
    public EasingManager easingManager; // Manages smooth movement (easing) of the card.
    private Vector3 startPosition; // The initial position of the card before dragging.

    public Transform parentToReturnTo = null; // The parent object to return to after dragging.
    public Transform placeholderParent = null; // The parent for the placeholder.

    GameObject placeholder = null; // A temporary placeholder to show the card's new position during drag.

    // Called when dragging starts.
    public void OnBeginDrag(PointerEventData eventData)
    {
        // Setup initial values and placeholder.
        startPosition = transform.position;
        originalParent = transform.parent;

        Debug.Log("OnBeginDrag");

        // Create and configure the placeholder.
        placeholder = new GameObject();
        placeholder.transform.SetParent(this.transform.parent);
        LayoutElement le = placeholder.AddComponent<LayoutElement>();
        le.preferredWidth = this.GetComponent<LayoutElement>().preferredWidth;
        le.preferredHeight = this.GetComponent<LayoutElement>().preferredHeight;
        le.flexibleWidth = 0;
        le.flexibleHeight = 0;
        placeholder.transform.SetSiblingIndex(this.transform.GetSiblingIndex());

        parentToReturnTo = this.transform.parent;
        placeholderParent = parentToReturnTo;
        this.transform.SetParent(this.transform.parent.parent);

        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    // Called during dragging.
    public void OnDrag(PointerEventData eventData)
    {
        // Update card position to follow mouse cursor.
        Debug.Log("OnDrag");
        this.transform.position = eventData.position;

        // // Ensure placeholder remains in correct position relative to other cards.
        // if(placeholder.transform.parent != placeholderParent)
        //     placeholder.transform.SetParent(placeholderParent);

        // // Determine the new position in the card stack.
        // int newSiblingIndex = placeholderParent.childCount;
        // for(int i = 0; i < placeholderParent.childCount; i++)
        // {
        //     if(this.transform.position.x < placeholderParent.GetChild(i).position.x)
        //     {
        //         newSiblingIndex = i;
        //         if(placeholder.transform.GetSiblingIndex() < newSiblingIndex)
        //             newSiblingIndex--;
        //         break;
        //     }
        // }
        //placeholder.transform.SetSiblingIndex(newSiblingIndex);
    }

    // Called when dragging ends.
    public void OnEndDrag(PointerEventData eventData)
    {
        // Move the card smoothly to its new position.
        Vector3 targetPosition = placeholder.transform.position; // If there is a placeholder, ease to its position; otherwise, ease back to the start position
        if (easingManager != null)
        {
            easingManager.MoveWithEasing(gameObject, targetPosition);
        }

        // Reattach to original parent and adjust order in the UI.
        Debug.Log("OnEndDrag");
        this.transform.SetParent(parentToReturnTo);
        this.transform.SetSiblingIndex(placeholder.transform.GetSiblingIndex());
        GetComponent<CanvasGroup>().blocksRaycasts = true;

        Destroy(placeholder);
    }
}
