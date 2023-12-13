using UnityEngine;
using UnityEngine.EventSystems;
using Utility.Easing;
public class TossCard : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector2 offset;
    private bool isMovingToCenter;
    private bool isFlyingAway;
    [SerializeField] private GameObject flyAwayTarget;
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float closeEnoughDistance = 0.1f;
    [SerializeField] private EasingType easingType = EasingType.SineIn; // Easing type to use
    private float moveValue = 0f; // Value to use for easing

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
            transform.position = Vector3.Lerp(transform.position, flyAwayTarget.transform.position, easedMoveValue);
            if (Vector3.Distance(transform.position, flyAwayTarget.transform.position) < closeEnoughDistance)
            {
                isFlyingAway = false;
                moveValue = 0f; // Reset moveValue
            }
        }
    }
}