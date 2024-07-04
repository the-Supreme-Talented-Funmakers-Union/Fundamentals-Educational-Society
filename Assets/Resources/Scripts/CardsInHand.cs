using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsInHand : MonoBehaviour
{
    void Update()
    {
        AlignCards();
    }
    void AlignCards()
    {
        int CardCount = transform.childCount;
        float totalWidth = (CardCount - 1) * 0.8f;

        for (int i = 0; i < CardCount; i++)
        {
            Transform card = transform.GetChild(i);
            Card cardComponent = card.GetComponent<Card>();
            if (transform.name == "Player 1" || transform.name == "Player 3")
            {
                float xPos = -totalWidth / 2 + i * 0.8f;
                cardComponent.GetTargetPos = new Vector3(xPos, cardComponent.GetTargetPos.y, cardComponent.GetTargetPos.z);
            }
            else if (transform.name == "Player 2" || transform.name == "Player 4")
            {
                float yPos = totalWidth / 2 - i * 0.8f;
                cardComponent.GetTargetPos = new Vector3(cardComponent.GetTargetPos.x, yPos, cardComponent.GetTargetPos.z);
            }
        }
    }
}
