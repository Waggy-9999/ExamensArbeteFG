using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    [SerializeField] private List<GameObject> cardsInHand = new List<GameObject>(); // List to store the cards in hand
    [SerializeField] private GameObject cardPrefab; // Prefab for the card

    // Method to add a card to the hand
    public void AddCard()
    {
        GameObject newCard = Instantiate(cardPrefab, transform.position, Quaternion.identity); // Instantiate a new card from the prefab
        newCard.transform.SetParent(transform);
        cardsInHand.Add(newCard); // Add the new card to the list

        Debug.Log("New card added to hand. Total cards in hand: " + cardsInHand.Count);
    }

    // Method to remove a card from the hand
    public void RemoveCard(GameObject card)
    {
        // if (cardsInHand.Contains(card)) // Check if the card is in the hand
        // {
        //     cardsInHand.Remove(card); // Remove the card from the list
        //     Destroy(card); // Destroy the card

        //     Debug.Log("Card removed from hand. Total cards in hand: " + cardsInHand.Count);
        // }

        //for list testing purposes
        if (cardsInHand.Count > 0) // Check if there are any cards in the hand
        {
            GameObject oldestCard = cardsInHand[0]; // Get the oldest card
            cardsInHand.RemoveAt(0); // Remove the oldest card from the list
            Destroy(oldestCard); // Destroy the card

            Debug.Log("Oldest card removed from hand. Total cards in hand: " + cardsInHand.Count);
        }
    }
}
