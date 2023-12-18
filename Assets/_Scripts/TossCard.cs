using UnityEngine;
using UnityEngine.EventSystems;
using Utility.Easing;
public class TossCard : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Hand hand; // Reference to the hand script
    public Turn turn; // Reference to the turn script
    private Vector2 offset;
    private bool isMovingToCenter;
    private bool isFlyingAway;
    [SerializeField] private GameObject flyAwayTo;
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float closeEnoughDistance = 0.1f;
    [SerializeField] private EasingType easingType = EasingType.SineIn; // Easing type to use
    private float moveValue = 0f; // Value to use for easing

    public void OnBeginDrag(PointerEventData eventData)
    {
        offset = (Vector2)transform.position - eventData.position;

        // Move this card to the top of the UI
        transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isMovingToCenter && !isFlyingAway)
        {
            transform.position = eventData.position + offset;
        }
        if (eventData.pointerCurrentRaycast.gameObject != null)
        {
            //Debug.Log("Hovering over GameObject with tag: " + eventData.pointerCurrentRaycast.gameObject.tag);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Check if the card is over a GameObject with the tag "DiscardZone"
        if (eventData.pointerCurrentRaycast.gameObject != null)
        {
            //Debug.Log("Raycast hit: " + eventData.pointerCurrentRaycast.gameObject.name); // Log the name of the GameObject that was hit

            if (eventData.pointerCurrentRaycast.gameObject.CompareTag("DiscardZone"))
            {
                if (hand != null) // Check if hand is not null
                {
                    hand.RemoveCard(this.gameObject); // Remove this card from the hand
                    
                    //Debug.Log("Card removed from hand");
                }
            }
            else if (eventData.pointerCurrentRaycast.gameObject.CompareTag("DropZone"))
            {
                isMovingToCenter = true;

                // Decreases Mana by 1, if mana 0, resets hand
                turn.PlayCard();
                Debug.Log("Mana: " + turn.manaCount);
            }
            else
            {
                if (hand != null) // Check if hand is not null
                {
                    // If the card is not dropped on a "DropZone", re-center the cards in the hand
                    hand.CenterCards();
                }
            }
        }
        //Debug.Log("OnEndDrag");
    }

    private void Update()
    {
        if (isMovingToCenter)
        {
            moveValue += Time.unscaledDeltaTime * moveSpeed;
            float easedMoveValue = Functions.GetEaseValue(easingType, moveValue);
            transform.position = Vector3.Lerp(transform.position, new Vector3(Screen.width / 2, Screen.height / 2, 0), easedMoveValue);
            if (Vector3.Distance(transform.position, new Vector3(Screen.width / 2, Screen.height / 2, 0)) < closeEnoughDistance)
            {
                isMovingToCenter = false;
                isFlyingAway = true;
                moveValue = 0f; // Reset moveValue
            }
        }
        else if (isFlyingAway)
        {
            moveValue += Time.unscaledDeltaTime * moveSpeed;
            float easedMoveValue = Functions.GetEaseValue(easingType, moveValue);
            transform.position = Vector3.Lerp(transform.position, flyAwayTo.transform.position, easedMoveValue);

            // Check if the card has reached the "DiscardZone"
            GameObject discardZone = GameObject.FindGameObjectWithTag("DiscardZone");
            if (discardZone != null && Vector3.Distance(transform.position, discardZone.transform.position) < closeEnoughDistance)
            {
                hand.RemoveCard(this.gameObject); // Remove this card from the hand
                //Debug.Log("Card removed from hand");
            }

            if (Vector3.Distance(transform.position, flyAwayTo.transform.position) < closeEnoughDistance)
            {
                isFlyingAway = false;
                moveValue = 0f; // Reset moveValue
            }
        }
    }
}