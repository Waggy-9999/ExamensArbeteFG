using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHelper : MonoBehaviour
{
    [SerializeField] private float GrowSpeed = 1f;
    [SerializeField] private float GrowFactor = 1.4f; // How buch bigger the card will grow
    private Vector3 originalScale; // original scale of the card
    private float growValue = 0f;
    private GameObject cardReference;
    public void GrowCard(GameObject card)
    {
        StopCoroutine("Shrink");
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
            cardReference.transform.localScale = Vector3.Lerp(originalScale, originalScale * GrowFactor, growValue);
            yield return null;
        }
    }
    IEnumerator Shrink()
    {
        while (growValue > 0f)
        {
            growValue -= Time.unscaledDeltaTime * GrowSpeed;
            cardReference.transform.localScale = Vector3.Lerp(originalScale, originalScale * GrowFactor, growValue);
            yield return null;
        }
    }
}