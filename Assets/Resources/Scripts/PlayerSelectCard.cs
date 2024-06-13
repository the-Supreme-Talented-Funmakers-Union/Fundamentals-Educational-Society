using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelectCard : MonoBehaviour
{
    public GameObject highlightedCard;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
            if (hit.collider != null && hit.collider.gameObject.GetComponent<Card>() != null)
            {
                GameObject clickedCard = hit.collider.gameObject;
                if (clickedCard.transform.parent == transform)
                {
                    if (highlightedCard == clickedCard)
                    {
                        DehighlightCard();
                    }
                    else
                    {
                        HighlightCard(clickedCard);
                    }
                }
            }
        }
    }
    private void HighlightCard(GameObject card)
    {
        if (highlightedCard != null)
        {
            highlightedCard.GetComponent<Card>().IsHighlighted = false;
            highlightedCard.GetComponent<SpriteRenderer>().color = Color.white;
        }
        highlightedCard = card;
        highlightedCard.GetComponent<Card>().IsHighlighted = true;
        highlightedCard.GetComponent<SpriteRenderer>().color = Color.yellow;
    }
    private void DehighlightCard()
    {
        if (highlightedCard != null)
        {
            highlightedCard.GetComponent<Card>().IsHighlighted = false;
            highlightedCard.GetComponent<SpriteRenderer>().color = Color.white;
            highlightedCard = null;
        }
    }
}