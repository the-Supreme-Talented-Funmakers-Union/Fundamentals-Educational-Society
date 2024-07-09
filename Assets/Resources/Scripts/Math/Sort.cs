using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sort : MonoBehaviour
{
    public void CardSort()
    {
        Card[] cards = this.GetComponentsInChildren<Card>();
        for (int i = 0; i < cards.Length - 1; i++)
        {
            for (int j = i + 1; j < cards.Length; j++)
            {
                if (cards[i].GetCardValue < cards[j].GetCardValue)
                {
                    Sprite temp = cards[j].GetComponent<SpriteRenderer>().sprite;
                    cards[j].GetComponent<SpriteRenderer>().sprite = cards[i].GetComponent<SpriteRenderer>().sprite;
                    cards[i].GetComponent<SpriteRenderer>().sprite = temp;
                    int value = cards[i].GetCardValue;
                    cards[i].GetCardValue = cards[j].GetCardValue;
                    cards[j].GetCardValue = value;
                }
            }
        }
    }
}
