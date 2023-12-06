using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scaleable : MonoBehaviour
{
    // Originalscale of the Object
    private Vector3 originalScale;
     // Duration of the ease animation
    public float scaleDuration = 0.5f;
    public float scaleAmount = 1.2f;
 
    // Duration of the ease animation
    public Vector3 hoverscale = new Vector3(1.5f, 1.5f, 1.5f);

    void start()
    {
        // Store the original scale of the object
        originalScale = transform.localScale;
    }

    void OnMouseEnter()
    {
        // Scale up when hovering
        LeanTween.scale(gameObject, hoverscale, scaleDuration).setEase(LeanTweenType.easeOutExpo);
    }

    void OnMouseExit()
    {
        // Reset the scale to normal
        LeanTween.scale(gameObject, originalScale, scaleDuration).setEase(LeanTweenType.easeOutExpo);
    }
}
