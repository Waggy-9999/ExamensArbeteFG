using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.Easing;

public class CardHelper : MonoBehaviour
{
    [SerializeField] private float GrowSpeed = 10f;
    [SerializeField] private float GrowFactor = 1.4f; // How buch bigger the card will grow
    private Vector3 originalScale; // original scale of the card
    private float growValue = 0f;
    private GameObject cardReference;
    [SerializeField] private EasingType easingType = EasingType.SineIn;
    public void GrowCard(GameObject card)
    {
        if (cardReference != null) // if there is a card already growing, shrink it
        {
            StopCoroutine("Grow");
            StartCoroutine("Shrink");
        }
        cardReference = card;
        StartCoroutine("Grow");
    }
    public void ShrinkCard()
    {
        StopCoroutine("Grow");
        StartCoroutine("Shrink");
    }
    IEnumerator Grow()
    {
        if (growValue <= 0f) originalScale = cardReference.transform.localScale; // save original scale
        while (growValue < 1f)
        {
            growValue += Time.unscaledDeltaTime * GrowSpeed;
            float easedGrowValue = Functions.GetEaseValue(easingType, growValue);
            cardReference.transform.localScale = Vector3.Lerp(originalScale, originalScale * GrowFactor, easedGrowValue);
            yield return null;
        }
    }
    IEnumerator Shrink()
    {
        while (growValue > 0f)
        {
            growValue -= Time.unscaledDeltaTime * GrowSpeed;
            float easedGrowValue = Functions.GetEaseValue(EasingType.SineIn, growValue);
            cardReference.transform.localScale = Vector3.Lerp(originalScale, originalScale * GrowFactor, easedGrowValue);
            yield return null;
        }
    }
}