using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections.LowLevel.Unsafe;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.WSA;
using static System.Net.Mime.MediaTypeNames;

public class Dealer : MonoBehaviour
{
    public GameObject cardImage;
    public GameObject easySetting;
    public GameObject mediumSetting;
    public GameObject hardSetting;
    public GameObject currentSetting;
    public GameManager gameManager;
    public bool cardDealt = false;
    public int drawCount = 0;
    private Card card;
    private List<Card> cardLibrary = new List<Card>();
    private Queue<Card> cardQueue = new Queue<Card>();
    private GameObject holder;
    private GameObject recycle;
    private GameObject goal;
    private int deckSize;
    private int cardsPerPlayer;
    void Start()
    {
        holder = GameObject.FindWithTag("CardHolder");
        recycle = GameObject.FindWithTag("CardRecycler");
    }
    public void SetMode(string mode)
    {
        if (currentSetting != null)
        {
            Destroy(currentSetting);
        }
        switch (mode)
        {
            case "Easy":
                DestroyImmediate(mediumSetting);
                DestroyImmediate(hardSetting);
                easySetting.SetActive(true);
                currentSetting = easySetting;
                deckSize = 2;
                cardsPerPlayer = 5;
                break;
            case "Medium":
                DestroyImmediate(easySetting);
                DestroyImmediate(hardSetting);
                mediumSetting.SetActive(true);
                currentSetting = mediumSetting;
                deckSize = 3;
                cardsPerPlayer = 7;
                break;
            case "Hard":
                DestroyImmediate(easySetting);
                DestroyImmediate(mediumSetting);
                hardSetting.SetActive(true);
                currentSetting = hardSetting;
                deckSize = 4;
                cardsPerPlayer = 9;
                break;
        }
        goal = GameObject.FindWithTag("GoalCard");
        CreateCard();
        shuffle();
        StartCoroutine(Deal());
    }
    public void CreateCard()
    {
        for (int d = 0; d < deckSize; d++)
        {
            int cardType = 0;
            for (int i = 0; i < 52; i++)
            {
                if (i != 0 && i % 13 == 0)
                {
                    cardType++;
                }
                card = Instantiate(Resources.Load<Card>("Prefabs/Card"));
                card.gameObject.transform.position = Vector3.forward;
                card.gameObject.transform.rotation = Quaternion.identity;
                card.GetComponent<Card>().GetCardType = (CardType)cardType;
                card.GetComponent<Card>().GetCardValue = i % 13 + 1;
                card.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Pokers" + "/" + (CardType)cardType + (i % 13 + 1).ToString());
                card.transform.parent = GameObject.Find("Deck").transform;
                card.gameObject.SetActive(false);
                cardLibrary.Add(card);
            }
            card = Instantiate(Resources.Load<Card>("Prefabs/Card"));
            card.gameObject.transform.position = Vector3.forward;
            card.gameObject.transform.rotation = Quaternion.identity;
            card.GetComponent<Card>().GetCardType = (CardType)cardType;
            card.GetComponent<Card>().GetCardValue = 14;
            card.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Pokers/JokerBlack");
            card.transform.parent = GameObject.Find("Deck").transform;
            card.gameObject.SetActive(false);
            cardLibrary.Add(card);
            card = Instantiate(Resources.Load<Card>("Prefabs/Card"));
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
        for (int i = 0; i < deckSize * 54; i++)
        {
            cards = cardQueue.Dequeue();
            if (i < cardsPerPlayer * 4)
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
                cards.transform.parent = holder.transform;
                cards.GetTargetPos = holder.transform.position;
                cards.gameObject.SetActive(true);
                cards.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Pokers/CardBack");
            }
        }
        yield return new WaitForSeconds(1.2f);
        holder.transform.GetChild(0).transform.parent = goal.transform;
        goal.transform.GetChild(0).GetComponent<Card>().GetTargetPos = goal.transform.position;
        Card.RevealCard(goal.transform.GetChild(0).gameObject);
        yield return new WaitForSeconds(1.2f);
        GameObject.FindWithTag("GamePanel").transform.Find("GamePlay").gameObject.SetActive(true);
        cardDealt = true;
    }
    public void Update()
    {
        GameObject.Find("Holder/Canvas/Available").GetComponent<TMP_Text>().text = holder.transform.childCount + " remaining";
        if (cardDealt)
        {
            GoalCorrection();
        }
        if (cardDealt && holder.transform.childCount == 0 && recycle.transform.childCount > 0)
        {
            StartCoroutine(Reshuffle());
        }
    }
    public IEnumerator Reshuffle()
    {
        if (!cardDealt)
        {
            yield break;
        }
        Transform usedCards = recycle.transform;
        while (usedCards.childCount > 0)
        {
            Transform cardTransform = usedCards.GetChild(0);
            Card card = cardTransform.GetComponent<Card>();
            card.gameObject.SetActive(false);
            cardLibrary.Add(card);
            cardTransform.SetParent(null);
        }
        shuffle();
        while (cardQueue.Count > 0)
        {
            Card card = cardQueue.Dequeue();
            card.transform.parent = holder.transform;
            card.GetTargetPos = holder.transform.position;
            card.gameObject.SetActive(true);
            card.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Pokers/CardBack");
        }
    }
    public void GoalCorrection()
    {
        if (goal.transform.childCount > 0)
        {
            if (goal.transform.GetChild(0).GetComponent<Card>().GetCardValue > 13)
            {
                goal.transform.GetChild(0).GetComponent<Card>().GetTargetPos = recycle.transform.position;
                goal.transform.GetChild(0).transform.parent = recycle.transform;
                holder.transform.GetChild(0).GetComponent<Card>().GetTargetPos = goal.transform.position;
                holder.transform.GetChild(0).transform.parent = goal.transform;
                Card.RevealCard(goal.transform.GetChild(0).gameObject);
            }
        }
    }
    //public void DrawCard()
    //{
    //    if (holder.transform.childCount > 0)
    //    {
    //        GameManager gameManager = FindObjectOfType<GameManager>();
    //        int currentPlayer = gameManager.currentPlayer;
    //        if (currentPlayer == 1)
    //        {
    //            Card.RevealCard(holder.transform.GetChild(0).gameObject);
    //        }
    //        holder.transform.GetChild(0).GetComponent<Card>().GetTargetPos = GameObject.Find("Player " + currentPlayer).transform.position;
    //        holder.transform.GetChild(0).parent = GameObject.Find("Player " + currentPlayer).transform;
    //        drawCount++;
    //    }
    //}
}
