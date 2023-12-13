using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasingManager : MonoBehaviour
{
    // Duration of the ease animation
    public float easeDuration = 2;

    // Method to move a GameObject with easing
    public void EaseToPosition(GameObject objectToMove, Vector3 targetPosition)
    {
        LeanTween.move(objectToMove, targetPosition, easeDuration).setEaseInOutSine();
    }
}

