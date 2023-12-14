/// <summary>
/// This script manages the hand of cards, allowing cards to be added and removed.
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.Easing;

public class Hand : MonoBehaviour
{
    [SerializeField] private List<GameObject> cardsInHand = new List<GameObject>(); // List to store the cards in hand
    [SerializeField] private GameObject cardPrefab; // Prefab for the card
    [SerializeField] private GameObject spawnPoint; // The point where new cards are spawned
    [SerializeField] private float duration = 1.0f; // Duration for the card movement animation
    [SerializeField] private EasingType easingType = EasingType.QuartOut; // Easing type for the card movement animation

    /// <summary>
    /// Method to add a card to the hand.
    /// </summary>
    public void AddCard()
    {
        // Instantiate a new card at the spawn point
        GameObject newCard = Instantiate(cardPrefab, spawnPoint.transform.position, Quaternion.identity); 
        newCard.transform.SetParent(transform); // Set the new card's parent to the hand

        // Add the new card to the list of cards in hand
        cardsInHand.Add(newCard); 

        // Calculate the total width of all cards in hand
        float cardWidth = newCard.GetComponent<RectTransform>().rect.width;
        float totalWidth = cardWidth * cardsInHand.Count;

        // Adjust the position of all cards in hand
        for (int i = 0; i < cardsInHand.Count; i++)
        {
            StartCoroutine(MoveCard(cardsInHand[i], new Vector3((i * cardWidth) - totalWidth / 2 + cardWidth / 2, 0, 0)));
        }

        Debug.Log("New card added to hand. Total cards in hand: " + cardsInHand.Count);
    }

    /// <summary>
    /// Method to remove a card from the hand.
    /// </summary>
    public void RemoveCard(GameObject card)
    {
        // Check if the card exists in the list of cards in hand
        if (cardsInHand.Contains(card))
        {
            // Remove the card from the list of cards in hand
            cardsInHand.Remove(card); 

            // Destroy the card GameObject
            Destroy(card); 

            // Re-center the remaining cards in hand
            CenterCards();
        }
    }

    public void CenterCards()
    {
        // Calculate the total width of all cards in hand
        float cardWidth = cardPrefab.GetComponent<RectTransform>().rect.width;
        float totalWidth = cardWidth * cardsInHand.Count;

        // Adjust the position of all cards in hand
        for (int i = 0; i < cardsInHand.Count; i++)
        {
            Vector3 targetPosition = new Vector3((i * cardWidth) - totalWidth / 2 + cardWidth / 2, 0, 0);
            StartCoroutine(MoveCard(cardsInHand[i], targetPosition));
        }
    }

    /// <summary>
    /// Coroutine to move a card to a target position.
    /// </summary>
    private IEnumerator MoveCard(GameObject card, Vector3 targetPosition)
    {
        float elapsed = 0.0f; // Time elapsed since the start of the animation
        Vector3 startPosition = card.transform.localPosition; // The card's initial position

        // Continue the animation until the duration has elapsed
        while (elapsed < duration)
        {
            // If the card has been destroyed, stop the animation
            if (card == null) 
            {
                yield break;
            }

            // Update the elapsed time
            elapsed += Time.deltaTime;

            // Calculate the eased time value
            float t = elapsed / duration;
            t = Utility.Easing.Functions.GetEaseValue(easingType, t); 

            // Update the card's position
            card.transform.localPosition = Vector3.Lerp(startPosition, targetPosition, t);

            // Wait for the next frame
            yield return null;
        }

        // If the card still exists, ensure it ends up at the exact target position
        if (card != null)
        {
            card.transform.localPosition = targetPosition; 
        }
    }
}