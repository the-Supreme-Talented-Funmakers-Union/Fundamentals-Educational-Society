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
    private bool canMove = false;
    private Vector3 targetPos;
    void Start()
    {
        Debug.Log(transform.position);
    }
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * 3);
        if (Vector3.Distance(transform.position, targetPos) <= 0.001f)
        {
            transform.position = targetPos;
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
    public bool GetCanMove
    {
        set { canMove = value; }
        get { return canMove; }
    }
    public Vector3 GetTargetPos
    {
        set { targetPos = value; }
        get { return targetPos; }
    }
}
