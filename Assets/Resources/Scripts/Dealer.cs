using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class Dealer : MonoBehaviour
{
    public GameObject cardImage;
    public int drawCount = 0;
    private Card card;
    private List<Card> cardLibrary = new List<Card>();
    private Queue<Card> cardQueue = new Queue<Card>();
    private void Start()
    {
        CreatCard();
        shuffle();
    }
    public void CreatCard()
    {
        for (int d = 0; d < 2; d++)
        {
            int cardType = 0;
            for (int i = 0; i < 52; i++)
            {
                if (i != 0 && i % 13 == 0)
                {
                    cardType++;
                }
                card = Instantiate(Resources.Load<Card>("Card"));
                card.gameObject.transform.position = Vector3.forward;
                card.gameObject.transform.rotation = Quaternion.identity;
                card.GetComponent<Card>().GetCardType = (CardType)cardType;
                card.GetComponent<Card>().GetCardValue = i % 13 + 1;
                card.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Pokers" + "/" + (CardType)cardType + (i % 13 + 1).ToString());
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
        for (int i = 0; i < 108; i++)
        {
            cards = cardQueue.Dequeue();
            if (i < 20)
            {
                switch (i % 4)
                {
                    case 0:
                        cards.GetTargetPos = GameObject.Find("Player 1").transform.position;
                        cards.transform.parent = GameObject.Find("Player 1").transform;
                        break;
                    case 1:
                        cards.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Pokers/CardBack");
                        cards.GetTargetPos = GameObject.Find("Player 2").transform.position;
                        cards.transform.parent = GameObject.Find("Player 2").transform;
                        break;
                    case 2:
                        cards.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Pokers/CardBack");
                        cards.GetTargetPos = GameObject.Find("Player 3").transform.position;
                        cards.transform.parent = GameObject.Find("Player 3").transform;
                        break;
                    case 3:
                        cards.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Pokers/CardBack");
                        cards.GetTargetPos = GameObject.Find("Player 4").transform.position;
                        cards.transform.parent = GameObject.Find("Player 4").transform;
                        break;
                }
                cards.gameObject.SetActive(true);
                cards.GetComponent<SpriteRenderer>().sortingOrder = i + 1;
                yield return new WaitForSeconds(0.2f);
            }
            else
            {
                cards.transform.parent = GameObject.Find("Holder").transform;
                cards.GetTargetPos = GameObject.Find("Holder").transform.position;
                cards.gameObject.SetActive(true);
                cards.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Pokers/CardBack");
            }
        }
        yield return new WaitForSeconds(1.2f);
        GameObject.Find("Holder").transform.GetChild(0).transform.parent = GameObject.Find("Goal").transform;
        GameObject.Find("Goal").transform.GetChild(0).GetComponent<Card>().GetTargetPos = GameObject.Find("Goal").transform.position;
        Card.RevealCard(GameObject.Find("Goal").transform.GetChild(0).gameObject);
        yield return new WaitForSeconds(1.2f);
        GameObject.Find("Canvas").transform.Find("GamePlay").gameObject.SetActive(true);   
    }
    public void Update()
    {
        if (GameObject.Find("Canvas/GamePlay/Available") != null && GameObject.Find("Canvas/GamePlay/Available").activeInHierarchy)
        {
            GameObject.Find("Canvas/GamePlay/Available").GetComponent<TMP_Text>().text = GameObject.Find("Holder").transform.childCount + " available";
        }
        GoalCorrection();
    }
    public void GoalCorrection()
    {
        if (GameObject.Find("Goal").transform.childCount > 0)
        {
            if (GameObject.Find("Goal").transform.GetChild(0).GetComponent<Card>().GetCardValue > 13)
            {
                GameObject.Find("Goal").transform.GetChild(0).GetComponent<Card>().GetTargetPos = GameObject.Find("Recycle").transform.position;
                GameObject.Find("Goal").transform.GetChild(0).transform.parent = GameObject.Find("Recycle").transform;
                GameObject.Find("Holder").transform.GetChild(0).GetComponent<Card>().GetTargetPos = GameObject.Find("Goal").transform.position;
                GameObject.Find("Holder").transform.GetChild(0).transform.parent = GameObject.Find("Goal").transform;
                Card.RevealCard(GameObject.Find("Goal").transform.GetChild(0).gameObject);
            }
            GameObject.FindObjectOfType<GameManager>().SetGoalCard();
        }
    }
    public void DrawCard()
    {
        if (GameObject.Find("Holder").transform.childCount > 0)
        {
            GameManager gameManager = FindObjectOfType<GameManager>();
            int currentPlayer = gameManager.currentPlayer;
            if (currentPlayer == 1)
            {
                Card.RevealCard(GameObject.Find("Holder").transform.GetChild(0).gameObject);
            }
            GameObject.Find("Holder").transform.GetChild(0).GetComponent<Card>().GetTargetPos = GameObject.Find("Player " + currentPlayer).transform.position;
            GameObject.Find("Holder").transform.GetChild(0).parent = GameObject.Find("Player " + currentPlayer).transform;
            drawCount++;
        }
    }
}
