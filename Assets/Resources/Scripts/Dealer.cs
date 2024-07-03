using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dealer : MonoBehaviour
{
    public GameObject cardImage;
    public Button button;
    private Card card;
    private int cardType = 0;
    private List<Card> cardLibrary = new List<Card>();
    private Queue<Card> cardQueue = new Queue<Card>();
    private void Start()
    {
        CreatCard();
        shuffle();
    }
    public void CreatCard()
    {
        for (int i = 0; i < 52; i++)
        {
            if (i !=0 && i % 13 == 0)
            {
                cardType++;
            }
            card = Instantiate(Resources.Load<Card>("Card"));
            card.gameObject.transform.position = Vector3.forward;
            card.gameObject.transform.rotation = Quaternion.identity;
            card.GetComponent<Card>().GetCardType = (CardType)cardType;
            card.GetComponent<Card>().GetCardValue = i % 13 + 1;
            card.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Pokers/" + (CardType)cardType + (i % 13 + 1).ToString());
            card.transform.parent = GameObject.Find("Deck").transform;
            card.gameObject.SetActive(false);
            cardLibrary.Add(card);
        }
        card = Instantiate(Resources.Load<Card>("Card"));
        card.gameObject.transform.position = Vector3.forward;
        card.gameObject.transform.rotation = Quaternion.identity;
        card.GetComponent<Card>().GetCardType = (CardType)cardType;
        card.GetComponent<Card>().GetCardValue = 14;
        card.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Pokers/JokerBlack");
        card.transform.parent = GameObject.Find("Deck").transform;
        card.gameObject.SetActive(false);
        cardLibrary.Add(card);
        card = Instantiate(Resources.Load<Card>("Card"));
        card.gameObject.transform.position = Vector3.forward;
        card.gameObject.transform.rotation = Quaternion.identity;
        card.GetComponent<Card>().GetCardType = (CardType)cardType;
        card.GetComponent<Card>().GetCardValue = 15;
        card.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Pokers/JokerRed");
        card.transform.parent = GameObject.Find("Deck").transform;
        card.gameObject.SetActive(false);
        cardLibrary.Add(card);

    }
    public void shuffle()
    {
        List<Card> tempLibrary = new List<Card> (); 
        foreach (var cards in cardLibrary)
        {
            int cardIndex = Random.Range(0, tempLibrary.Count + 1);
            tempLibrary.Insert(cardIndex, cards); 
        }
        cardLibrary.Clear();
        foreach (var cardss in tempLibrary)
        {
            cardQueue.Enqueue(cardss);
        }
        tempLibrary.Clear();
    }
    public IEnumerator Deal()
    {
        Card cards;
        cardImage.SetActive(true);
        for (int i = 0; i < 54; i++)
        {
            cards = cardQueue.Dequeue();
            if (i < 20)
            {
                switch (i % 4)
                {
                    case 0:
                        cards.transform.parent = GameObject.Find("Player 1").transform;
                        cards.GetTargetPos = new Vector3(-1.6f + 0.2f * i, -4, 1);
                        break;
                    case 1:
                        cards.transform.parent = GameObject.Find("Player 2").transform;
                        cards.GetTargetPos = new Vector3(-6, 1.35f - 0.15f * i, 1);
                        //cards.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Pokers/CardBack");
                        break;
                    case 2:
                        cards.transform.parent = GameObject.Find("Player 3").transform;
                        cards.GetTargetPos = new Vector3(-1.6f + 0.2f * i, 4, 1);
                        //cards.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Pokers/CardBack");
                        break;
                    case 3:
                        cards.transform.parent = GameObject.Find("Player 4").transform;
                        cards.GetTargetPos = new Vector3(6, 1.35f - 0.15f * i, 1);
                        //cards.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Pokers/CardBack");
                        break;
                }
            }
            else
            {
                cards.GetTargetPos = new Vector3(0, 0, 0);
                cards.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Pokers/CardBack");
            }
            //cards.GetCanMove = true;
            cards.gameObject.SetActive(true);
            cards.GetComponent<SpriteRenderer>().sortingOrder = i + 1;
            yield return new WaitForSeconds(0.2f);
        }
        cardImage.SetActive(false);
    }
    public void OnDeal()
    {
        StartCoroutine(Deal());
        button.gameObject.SetActive(false);
    }
}
