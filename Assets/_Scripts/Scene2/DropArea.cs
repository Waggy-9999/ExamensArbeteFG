using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropArea : MonoBehaviour
{
    public LineRenderer lineRenderer; // assign a linedranderer to this in the inspector
    public CardType CardType { get; set; }

    private void UpdateLine(Vector3 cardPosition)
    {
        lineRenderer.SetPosition(0, cardPosition); // Set the first position of the line to the position of the card
        
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
        lineRenderer.SetPosition(1, mousePos); // Set the second position of the line to the mouse position in world space
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("OnPointerEnter");

        if (eventData.pointerDrag == null) // if there is no card being dragged
            return;

        Draggable2 draggable = eventData.pointerDrag.GetComponent<Draggable2>(); // get the draggable component of the card
        if (draggable != null)
        {
            switch (draggable.CardType)
{
            case CardType.CONSUMABLE:
                // Move to the middle and disappear
                StartCoroutine(MoveAndDisappear(draggable));
                break;
            case CardType.TARGETABLE:
                // Start drawing line
                if (lineRenderer != null)
                {
                    lineRenderer.enabled = true;
                    UpdateLine(draggable.transform.position);
                }
                break;
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("OnPointerExit");

         if (eventData.pointerDrag == null) // if there is no card being dragged
            return;

        Draggable2 draggable = eventData.pointerDrag.GetComponent<Draggable2>(); // get the draggable component of the card

        if(draggable != null && draggable.CardType == CardType.TARGETABLE)
        {
            // Stop drawing line
            if (lineRenderer != null)
            {
                lineRenderer.enabled = false;
            }
        }
            
    }
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log(eventData.pointerDrag.name + " was dropped on " + gameObject.name);

    }

    public IEnumerator MoveAndDisappear(Draggable2 draggable)
    {
        // Move to the centre of the droparea
        draggable.transform.position = transform.position;

        //Wait for a bit
        yield return new WaitForSeconds(1f);

        // Make card dissapear
        Destroy(draggable.gameObject);
    }
}
