using UnityEngine;
using UnityEngine.EventSystems;
public class TossCard : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector2 offset;
    private bool isMovingToCenter;
    private bool isFlyingAway;
    [SerializeField] private GameObject flyAwayTarget;
    [SerializeField] private float moveSpeed = 1f; // Speed at which the card moves to the center and flies away
    [SerializeField] private float closeEnoughDistance = 0.1f; // Distance at which the card is considered close enough to its target

    public void OnBeginDrag(PointerEventData eventData)
    {
        offset = (Vector2)transform.position - eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isMovingToCenter && !isFlyingAway)
        {
            transform.position = eventData.position + offset;
        }
        if (eventData.pointerCurrentRaycast.gameObject != null)
        {
            Debug.Log("Hovering over GameObject with tag: " + eventData.pointerCurrentRaycast.gameObject.tag);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject != null && eventData.pointerCurrentRaycast.gameObject.CompareTag("DropZone"))
        {
            isMovingToCenter = true;
        }
    }

    private void Update()
    {
        if (isMovingToCenter)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(Screen.width / 2, Screen.height / 2, 0), Time.deltaTime * moveSpeed);
            if (Vector3.Distance(transform.position, new Vector3(Screen.width / 2, Screen.height / 2, 0)) < closeEnoughDistance)
            {
                isMovingToCenter = false;
                isFlyingAway = true;
            }
        }
        else if (isFlyingAway)
        {
            transform.position = Vector3.Lerp(transform.position, flyAwayTarget.transform.position, Time.deltaTime * moveSpeed); // Use the position of flyAwayTarget
            if (Vector3.Distance(transform.position, flyAwayTarget.transform.position) < closeEnoughDistance) // Use the position of flyAwayTarget
            {
                isFlyingAway = false;
            }
        }
    }
}