using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Deck DeckManager;
    private List<Card> playerHand;

    void Start()
    {
        DeckManager.ShuffleDeck(); // Shuffle the deck at the start
        playerHand = new List<Card>();
        DrawStartingHand();
    }

    void DrawStartingHand()
    {
        for (int i = 0; i < 5; i++)
        {
            Card drawnCard = DeckManager.DrawCard();
            if (drawnCard != null)
            {
                playerHand.Add(drawnCard);
                Debug.Log($"Drawn card: {drawnCard.cardName} with value {drawnCard.cardValue}");
            }
        }
    }

    // Additional game logic for your card game goes here
}