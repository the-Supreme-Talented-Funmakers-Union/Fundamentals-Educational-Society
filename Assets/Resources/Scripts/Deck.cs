using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public List<Card> deck;
    public void ShuffleDeck()
    {
        Debug.Log("Shuffling deck...");
        for (int i = 0; i < deck.Count; i++)
        {
            Card temp = deck[i];
            int randomIndex = Random.Range(i, deck.Count);
            deck[i] = deck[randomIndex];
            deck[randomIndex] = temp;
        }

        // Debug log to verify shuffle
        Debug.Log("Deck after shuffling:");
        for (int i = 0; i < deck.Count; i++)
        {
            Debug.Log($"Card {i}: {deck[i].cardName} with value {deck[i].cardValue}");
        }
    }

    public Card DrawCard()
    {
        if (deck.Count == 0) return null;
        Card drawnCard = deck[0];
        deck.RemoveAt(0);
        return drawnCard;
    }

    public void ResetDeck()
    {
        // Logic to reset the deck if needed
    }
}
