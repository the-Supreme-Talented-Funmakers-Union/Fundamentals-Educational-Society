using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DealerH : MonoBehaviour
{
    public GameplayH gameplay;
    public GameObject cardImage;
    public Transform cardDeck;
    public Transform cardHolder;
    public Transform cardRecycler;
    public Transform cardGoal;
    public Transform player1;
    public Transform player2;
    public Transform player3;
    public Transform player4;
    public GameObject gameSet;
    public GameObject UI;
    public bool cardDealt = false;
    private Card card;
    private List<Card> cardLibrary = new List<Card>();
    private Queue<Card> cardQueue = new Queue<Card>();
    private AudioSource audioSource;
    public AudioClip dealSound;
    void Start()
    {
        CreatCard();
        shuffle();
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false; 
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
                card = Instantiate(Resources.Load<Card>("Prefabs/Math/Card"));
                card.gameObject.transform.position = Vector3.forward;
                card.gameObject.transform.rotation = Quaternion.identity;
                card.GetComponent<Card>().GetCardType = (CardType)cardType;
                card.GetComponent<Card>().GetCardValue = i % 13 + 1;
                card.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Math/Pokers" + "/" + (CardType)cardType + (i % 13 + 1).ToString());
                card.GetTargetPos = cardDeck.position;
                card.transform.parent = cardDeck;
                card.gameObject.SetActive(false);
                cardLibrary.Add(card);
            }
            card = Instantiate(Resources.Load<Card>("Prefabs/Math/Card"));
            card.gameObject.transform.position = Vector3.forward;
            card.gameObject.transform.rotation = Quaternion.identity;
            card.GetComponent<Card>().GetCardType = (CardType)cardType;
            card.GetComponent<Card>().GetCardValue = 14;
            card.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Math/Pokers/JokerBlack");
            card.GetTargetPos = cardDeck.position;
            card.transform.parent = cardDeck;
            card.gameObject.SetActive(false);
            cardLibrary.Add(card);
            card = Instantiate(Resources.Load<Card>("Prefabs/Math/Card"));
            card.gameObject.transform.position = Vector3.forward;
            card.gameObject.transform.rotation = Quaternion.identity;
            card.GetComponent<Card>().GetCardType = (CardType)cardType;
            card.GetComponent<Card>().GetCardValue = 15;
            card.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Math/Pokers/JokerRed");
            card.GetTargetPos = cardDeck.position;
            card.transform.parent = cardDeck;
            card.gameObject.SetActive(false);
            cardLibrary.Add(card);
        }
    }
    public void shuffle()
    {
        List<Card> tempLibrary = new List<Card>();
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
            if (i < 36)
            {
                switch (i % 4)
                {
                    case 0:
                        cards.GetTargetPos = player1.position;
                        cards.transform.parent = player1;
                        break;
                    case 1:
                        cards.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Math/Pokers/CardBack");
                        cards.GetTargetPos = player2.position;
                        cards.transform.parent = player2;
                        break;
                    case 2:
                        cards.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Math/Pokers/CardBack");
                        cards.GetTargetPos = player3.position;
                        cards.transform.parent = player3;
                        break;
                    case 3:
                        cards.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Math/Pokers/CardBack");
                        cards.GetTargetPos = player4.position;
                        cards.transform.parent = player4;
                        break;
                }
                cards.gameObject.SetActive(true);
                audioSource.PlayOneShot(dealSound);
                yield return new WaitForSeconds(0.2f);
            }
            else
            {
                cards.GetTargetPos = cardHolder.position;
                cards.transform.parent = cardHolder;
                cards.gameObject.SetActive(true);
                cards.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Math/Pokers/CardBack");
            }
        }
        yield return new WaitForSeconds(1f);
        gameSet.SetActive(true);
        yield return new WaitForSeconds(1f);
        cardHolder.GetChild(0).GetComponent<Card>().GetTargetPos = cardGoal.position;
        cardHolder.GetChild(0).transform.parent = cardGoal;
        Card.RevealCard(cardGoal.GetChild(0).gameObject);
        yield return new WaitForSeconds(1f);
        UI.SetActive(true);
        cardDealt = true;
        gameplay.GameStart();
    }
    void Update()
    {
        GoalCorrection();
        if (cardDealt && cardHolder.childCount == 0 && cardRecycler.childCount > 0)
        {
            StartCoroutine(Reshuffle());
        }
    }
    public void GoalCorrection()
    {
        if (cardGoal.childCount > 0)
        {
            if (cardGoal.GetChild(0).GetComponent<Card>().GetCardValue > 13)
            {
                cardGoal.GetChild(0).GetComponent<Card>().GetTargetPos = cardRecycler.position;
                cardGoal.GetChild(0).transform.parent = cardRecycler;
                cardHolder.GetChild(0).GetComponent<Card>().GetTargetPos = cardGoal.position;
                cardHolder.GetChild(0).transform.parent = cardGoal;
                Card.RevealCard(cardGoal.GetChild(0).gameObject);
            }
        }
    }
    public IEnumerator Reshuffle()
    {
        if (!cardDealt)
        {
            yield break;
        }
        while (cardRecycler.childCount > 0)
        {
            Transform cardTransform = cardRecycler.GetChild(0);
            Card card = cardTransform.GetComponent<Card>();
            card.gameObject.SetActive(false);
            cardLibrary.Add(card);
            cardTransform.SetParent(null);
        }
        shuffle();
        while (cardQueue.Count > 0)
        {
            Card card = cardQueue.Dequeue();
            card.GetTargetPos = cardHolder.position;
            card.transform.parent = cardHolder;
            card.gameObject.SetActive(true);
            card.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Math/Pokers/CardBack");
        }
    }
    public void Draw()
    {
        if (cardHolder.childCount > 0)
        {
            int currentPlayer = gameplay.currentPlayer;
            switch (currentPlayer)
            {
                case 1:
                    {
                        Card.RevealCard(cardHolder.GetChild(0).gameObject);
                        cardHolder.GetChild(0).GetComponent<Card>().GetTargetPos = player1.position;
                        cardHolder.GetChild(0).parent = player1;
                        break;
                    }
                case 2:
                    {
                        cardHolder.GetChild(0).GetComponent<Card>().GetTargetPos = player2.position;
                        cardHolder.GetChild(0).parent = player2;
                        break;
                    }
                case 3:
                    {
                        cardHolder.GetChild(0).GetComponent<Card>().GetTargetPos = player3.position;
                        cardHolder.GetChild(0).parent = player3;
                        break;
                    }
                case 4:
                    {
                        cardHolder.GetChild(0).GetComponent<Card>().GetTargetPos = player4.position;
                        cardHolder.GetChild(0).parent = player4;
                        break;
                    }
            }
        }
    }
}