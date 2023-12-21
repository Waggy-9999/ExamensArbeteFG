using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turn : MonoBehaviour
{
    private int manaCount = 3; // Initialize manaCount to 3
    public void PlayCard()
    {
        if (manaCount > 0) // Check if there is enough mana to play a card
        {
            manaCount--; // Decrease manaCount by 1
            Debug.Log("Card played. Remaining mana: " + manaCount);
        }
        else
        {
            Debug.Log("Not enough mana to play a card.");
        }
    }
}
