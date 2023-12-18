using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turn : MonoBehaviour
{
    public Hand hand; // Reference to the Hand script
    public int manaCount = 3; // Initialize manaCount to 3

    public void PlayCard()
    {
        if (manaCount > 0) // Check if there is enough mana to play a card
        {
            manaCount--; // Decrease manaCount by 1
            Debug.Log("Card played. Remaining mana: " + manaCount);

            if (manaCount == 0) // Check if manaCount is 0 after a card is played
            {
                hand.ResetHand(); // Toss all cards, reset manaCount to 3, and add multiple new cards
            }
        }
        else
        {
            Debug.Log("Not enough mana to play a card.");
        }
    }
}
