using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasingManager : MonoBehaviour
{
    // Duration of the ease animation
    public float easeDuration = 2;

    // Centralized method to move a GameObject with easing
    public void MoveWithEasing(GameObject objectToMove, Vector3 targetPosition, float duration = -1, LeanTweenType easingType = LeanTweenType.easeInOutSine)
    {
        if (duration < 0)
        {
            duration = easeDuration;
        }
        LeanTween.move(objectToMove, targetPosition, duration).setEase(easingType);
    }
}