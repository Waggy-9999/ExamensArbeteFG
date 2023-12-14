using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.Easing;

public class Hand : MonoBehaviour
{
    [SerializeField] private List<GameObject> cardsInHand = new List<GameObject>(); // List to store the cards in hand
    [SerializeField] private GameObject cardPrefab; // Prefab for the card
    [SerializeField] private GameObject spawnPoint;
    [SerializeField] private float duration = 1.0f;

    [SerializeField] private EasingType easingType = EasingType.QuartOut;

    // Method to add a card to the hand
    public void AddCard()
    {
        GameObject newCard = Instantiate(cardPrefab, spawnPoint.transform.position, Quaternion.identity); // Instantiate a new card from the spawn point
        newCard.transform.SetParent(transform);

        cardsInHand.Add(newCard); // Add the new card to the list

        // Adjust the position of all cards based on the total number of cards
        float cardWidth = newCard.GetComponent<RectTransform>().rect.width;
        float totalWidth = cardWidth * cardsInHand.Count;
        for (int i = 0; i < cardsInHand.Count; i++)
        {
            StartCoroutine(MoveCard(cardsInHand[i], new Vector3((i * cardWidth) - totalWidth / 2 + cardWidth / 2, 0, 0)));
        }

        Debug.Log("New card added to hand. Total cards in hand: " + cardsInHand.Count);
    }
    public void RemoveCard(GameObject card)
    {
        if (cardsInHand.Contains(card)) // Check if the card is in the hand
        {
            cardsInHand.Remove(card); // Remove the card from the list
            Destroy(card); // Destroy the card

            // Adjust the position of all remaining cards based on the total number of cards
            float cardWidth = card.GetComponent<RectTransform>().rect.width;
            float totalWidth = cardWidth * cardsInHand.Count;
            for (int i = 0; i < cardsInHand.Count; i++)
            {
                StartCoroutine(MoveCard(cardsInHand[i], new Vector3((i * cardWidth) - totalWidth / 2 + cardWidth / 2, 0, 0)));
            }

            Debug.Log("Card removed from hand. Total cards in hand: " + cardsInHand.Count);
        }
    }

    private IEnumerator MoveCard(GameObject card, Vector3 targetPosition)
    {
        float elapsed = 0.0f;
        Vector3 startPosition = card.transform.localPosition;

        while (elapsed < duration)
        {
            // Check if the card still exists
            if (card == null) // this is here cause if i delete cards too fast it breaks cause it cant find the card
            {
                yield break;
            }

            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            t = Utility.Easing.Functions.GetEaseValue(easingType, t); // Use the easing function
            card.transform.localPosition = Vector3.Lerp(startPosition, targetPosition, t);
            yield return null;
        }
        // Check if the card still exists
        if (card != null)
        {
            card.transform.localPosition = targetPosition; // Ensure the card ends up at the exact target position
        }
    }
}
