using UnityEngine;
public class CardHerbAlign : MonoBehaviour
{
    void Update()
    {
        int CardCount = transform.childCount;
        for (int i = 0; i < CardCount; i++)
        {
            Transform card = transform.GetChild(i);
            CardHerb cardComponent = card.GetComponent<CardHerb>();
            cardComponent.GetComponent<SpriteRenderer>().sortingOrder = i;
            if (transform.name == "CardRow1" || transform.name == "CardRow2" || transform.name == "CardRow3" || transform.name == "CardRow4")
            {
                float totalWidth = (CardCount - 1) * 0.96f;
                float xPos = -totalWidth / 2 + i * 0.96f;
                cardComponent.GetTargetPos = new Vector3(xPos, cardComponent.GetTargetPos.y, cardComponent.GetTargetPos.z);
            }
            else if (transform.name == "TextRow1" || transform.name == "TextRow2" || transform.name == "TextRow3" || transform.name == "TextRow4")
            {
                float totalWidth = (CardCount - 1) * 1.22f;
                float xPos = -totalWidth / 2 + i * 1.22f;
                cardComponent.GetTargetPos = new Vector3(xPos, cardComponent.GetTargetPos.y, cardComponent.GetTargetPos.z);
            }
            else if (transform.name == "P1Hand" || transform.name == "P2Hand" || transform.name == "P3Hand" || transform.name == "P4Hand")
            {
                float totalWidth = (CardCount - 1) * 0.69f;
                float xPos;
                float yPos;
                switch (transform.name)
                {
                    case "P1Hand":
                        xPos = -totalWidth / 2 + i * 0.69f;
                        card.rotation = Quaternion.Euler(0, 0, 0);
                        cardComponent.GetTargetPos = new Vector3(xPos, cardComponent.GetTargetPos.y, cardComponent.GetTargetPos.z);
                        break;
                    case "P2Hand":
                        yPos = -totalWidth / 2 + i * 0.69f;
                        card.rotation = Quaternion.Euler(0, 0, -90);
                        cardComponent.GetTargetPos = new Vector3(cardComponent.GetTargetPos.x, yPos, cardComponent.GetTargetPos.z);
                        break;
                    case "P3Hand":
                        xPos = -totalWidth / 2 + i * 0.69f;
                        card.rotation = Quaternion.Euler(0, 0, 180);
                        cardComponent.GetTargetPos = new Vector3(xPos, cardComponent.GetTargetPos.y, cardComponent.GetTargetPos.z);
                        break;
                    case "P4Hand":
                        yPos = -totalWidth / 2 + i * 0.69f;
                        card.rotation = Quaternion.Euler(0, 0, 90);
                        cardComponent.GetTargetPos = new Vector3(cardComponent.GetTargetPos.x, yPos, cardComponent.GetTargetPos.z);
                        break;
                }
            }
        }
    }
}
