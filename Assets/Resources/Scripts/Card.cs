using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum CardType
{
    Spades,
    Hearts,
    Clubs,
    Diamonds,
}
public class Card : MonoBehaviour
{
    private CardType cardType;
    private int cardValue;
    private Vector3 targetPos;
    private bool isDragging = false;
    private bool Highlighted = false;
    private static Card highlightedCard = null;
    private Vector3 offset;
    private Transform originalParent;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                if (IsInPlayerHand() || IsInSlot())
                {
                    if (highlightedCard == this)
                    {
                        DehighlightCard();
                        offset = transform.position - GetMouseWorldPos();
                        originalParent = transform.parent;
                        transform.SetParent(null);
                        GetComponent<SpriteRenderer>().sortingOrder = 100;
                        isDragging = true;
                    }
                    else
                    {
                        HighlightCard();
                    }
                }
            }
        }
        if (isDragging)
        {
            transform.position = GetMouseWorldPos() + offset;
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (isDragging)
            {
                Transform targetSlot = GetSlotUnderMouse();
                if (targetSlot != null)
                {
                    if (targetSlot.childCount > 0)
                    {
                        Transform cardInSlot = targetSlot.GetChild(0);
                        cardInSlot.SetParent(originalParent);
                        cardInSlot.GetComponent<Card>().GetTargetPos = originalParent.position;
                    }
                    transform.SetParent(targetSlot);
                    targetPos = targetSlot.position;
                    GetComponent<SpriteRenderer>().sortingOrder = 1;
                }
                else
                {
                    transform.SetParent(GameObject.Find("Player 1").transform);
                    targetPos = GameObject.Find("Player 1").transform.position;
                }
                isDragging = false;
            }
        }
        if (!isDragging)
        {
            transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * 3);
            if (Vector3.Distance(transform.position, targetPos) <= 0.001f)
            {
                transform.position = targetPos;
            }
        }
    }
    public int GetCardValue
    {
        set { cardValue = value; }
        get { return cardValue; }
    }
    public CardType GetCardType
    {
        set { cardType = value; }
        get { return cardType; }
    }
    public Vector3 GetTargetPos
    {
        set { targetPos = value; }
        get { return targetPos; }
    }
    public bool IsHighlighted
    {
        set { Highlighted = value; }
        get { return Highlighted; }
    }
    public static void RevealCard(GameObject cardObject)
    {
        if (cardObject.GetComponent<Card>().GetCardValue <= 13)
        {
            cardObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Pokers" + "/" + cardObject.GetComponent<Card>().GetCardType + cardObject.GetComponent<Card>().GetCardValue.ToString());
        }
        else if (cardObject.GetComponent<Card>().GetCardValue <= 14)
        {
            cardObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Pokers/JokerBlack");
        }
        else if (cardObject.GetComponent<Card>().GetCardValue <= 15)
        {
            cardObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Pokers/JokerRed");
        }
    }
    public void HighlightCard()
    {
        if (highlightedCard != null)
        {
            highlightedCard.DehighlightCard();
        }
        highlightedCard = this;
        Highlighted = true;
        GetComponent<SpriteRenderer>().color = Color.yellow;
    }
    public void DehighlightCard()
    {
        Highlighted = false;
        GetComponent<SpriteRenderer>().color = Color.white;
        highlightedCard = null;
    }
    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = Camera.main.nearClipPlane;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
    private bool IsInPlayerHand()
    {
        return transform.parent != null && transform.parent.name == "Player 1";
    }
    private bool IsInSlot()
    {
        return transform.parent != null && transform.parent.CompareTag("Slot");
    }
    private Transform GetSlotUnderMouse()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D[] hits = Physics2D.OverlapPointAll(mousePosition);
        foreach (var hit in hits)
        {
            if (hit.CompareTag("Slot"))
            {
                return hit.transform;
            }
        }
        return null;
    }
}