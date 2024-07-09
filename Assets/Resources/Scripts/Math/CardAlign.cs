using UnityEngine;
public class CardAlign : MonoBehaviour
{
    void Update()
    {
        int CardCount = transform.childCount;
        float totalWidth = (CardCount - 1) * 0.8f;
        for (int i = 0; i < CardCount; i++)
        {
            Transform card = transform.GetChild(i);
            Card cardComponent = card.GetComponent<Card>();
            cardComponent.GetComponent<SpriteRenderer>().sortingOrder = i;
            if (transform.name == "P1Hand" || transform.name == "P3Hand")
            {
                float xPos = -totalWidth / 2 + i * 0.8f;
                cardComponent.GetTargetPos = new Vector3(xPos, cardComponent.GetTargetPos.y, cardComponent.GetTargetPos.z);
            }
            else if (transform.name == "P2Hand" || transform.name == "P4Hand")
            {
                float yPos = totalWidth / 2 - i * 0.8f;
                cardComponent.GetTargetPos = new Vector3(cardComponent.GetTargetPos.x, yPos, cardComponent.GetTargetPos.z);
            }
        }
    }
}
