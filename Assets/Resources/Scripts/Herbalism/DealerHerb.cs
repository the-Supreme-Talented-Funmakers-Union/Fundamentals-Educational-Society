using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DealerHerb : MonoBehaviour
{
    public GameplayHerb gameplay;
    public GameObject cardImage;
    public GameObject cardText;
    public Transform Deck;
    public Transform cardRow1;
    public Transform cardRow2;
    public Transform cardRow3;
    public Transform cardRow4;
    public Transform textRow1;
    public Transform textRow2;
    public Transform textRow3;
    public Transform textRow4;
    private CardHerb card;
    private CardHerb text;
    private List<string> plants;
    private List<CardHerb> cardLibrary = new List<CardHerb>();
    private Queue<CardHerb> cardQueue = new Queue<CardHerb>();
    private List<CardHerb> textLibrary = new List<CardHerb>();
    private Queue<CardHerb> textQueue = new Queue<CardHerb>();
    private AudioSource audioSource;
    public AudioClip dealSound;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        plants = new List<string>
        {
            "Apple",
            "Apricot",
            "Avocado",
            "Banana",
            "Blackberry",
            "Blackcurrant",
            "Blueberry",
            "Cantaloupe",
            "Carambola",
            "Cherry",
            "Citrus",
            "Coconut",
            "Cucumber",
            "Damson",
            "Date",
            "Dragonfruit",
            "Durian",
            "Eggplant",
            "Fig",
            "Gage",
            "Gooseberry",
            "Grape",
            "Guava",
            "Kiwifruit",
            "Lemon",
            "Longan",
            "Loquat",
            "Lychee",
            "Mandarin",
            "Mango",
            "Mangosteen",
            "Medlar",
            "Melon",
            "Mulberry",
            "Nectarine",
            "Olive",
            "Papaya",
            "Peach",
            "Pear",
            "Persimmon",
            "Physalis",
            "Pineapple",
            "Plum",
            "Pomegranate",
            "Pomelo",
            "Pumpkin",
            "Quince",
            "Raspberry",
            "Redcurrant",
            "Strawberry",
            "Watermelon",
            "Whitecurrant"
        };
        CreatCard();
        shuffle();
    }
    public void CreatCard()
    {
        foreach (var plant in plants)
        {
            card = Instantiate(Resources.Load<CardHerb>("Prefabs/Herbalism/CardImage"));
            card.gameObject.transform.position = Vector3.forward;
            card.gameObject.transform.rotation = Quaternion.identity;
            card.GetComponent<CardHerb>().GetPlantName = plant;
            card.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Herbalism/Plants" + "/" + card.GetPlantName);
            card.SetIsImage(true);
            card.GetTargetPos = Deck.position;
            card.transform.parent = Deck;
            card.gameObject.SetActive(false);
            cardLibrary.Add(card);

            text = Instantiate(Resources.Load<CardHerb>("Prefabs/Herbalism/CardText"));
            text.gameObject.transform.position = Vector3.forward;
            text.gameObject.transform.rotation = Quaternion.identity;
            text.GetComponent<CardHerb>().GetPlantName = plant;
            text.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Herbalism/Text" + "/" + card.GetPlantName);
            text.SetIsImage(false);
            text.GetTargetPos = Deck.position;
            text.transform.parent = Deck;
            text.gameObject.SetActive(false);
            textLibrary.Add(text);
        }
    }
    public void shuffle()
    {
        List<CardHerb> tempCardLibrary = new List<CardHerb>();
        List<CardHerb> tempTextLibrary = new List<CardHerb>();
        foreach (var card in cardLibrary)
        {
            int cardIndex = Random.Range(0, tempCardLibrary.Count + 1);
            tempCardLibrary.Insert(cardIndex, card);
        }
        cardLibrary.Clear();
        foreach (var cards in tempCardLibrary)
        {
            cardQueue.Enqueue(cards);
        }
        tempCardLibrary.Clear();
        foreach (var text in textLibrary)
        {
            int textIndex = Random.Range(0, tempTextLibrary.Count + 1);
            tempTextLibrary.Insert(textIndex, text);
        }
        textLibrary.Clear();
        foreach (var texts in tempTextLibrary)
        {
            textQueue.Enqueue(texts);
        }
        tempTextLibrary.Clear();
    }
    public IEnumerator Deal()
    {
        CardHerb cards;
        CardHerb texts;
        cardImage.SetActive(true);
        for (int i = 0; i < 52; i++)
        {
            cards = cardQueue.Dequeue();
            cards.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Herbalism/CardBack");
            switch (i % 4)
            {
                case 0:
                    cards.GetTargetPos = cardRow1.position;
                    cards.transform.parent = cardRow1;
                    break;
                case 1:
                    cards.GetTargetPos = cardRow2.position;
                    cards.transform.parent = cardRow2;
                    break;
                case 2:
                    cards.GetTargetPos = cardRow3.position;
                    cards.transform.parent = cardRow3;
                    break;
                case 3:
                    cards.GetTargetPos = cardRow4.position;
                    cards.transform.parent = cardRow4;
                    break;
            }
            cards.gameObject.SetActive(true);
            audioSource.PlayOneShot(dealSound);
            yield return new WaitForSeconds(0.1f);
        }
        cardText.SetActive(true);
        for (int i = 0; i < 52; i++)
        {
            texts = textQueue.Dequeue();
            texts.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Herbalism/TextBack");
            switch (i % 4)
            {
                case 0:
                    texts.GetTargetPos = textRow1.position;
                    texts.transform.parent = textRow1;
                    break;
                case 1:
                    texts.GetTargetPos = textRow2.position;
                    texts.transform.parent = textRow2;
                    break;
                case 2:
                    texts.GetTargetPos = textRow3.position;
                    texts.transform.parent = textRow3;
                    break;
                case 3:
                    texts.GetTargetPos = textRow4.position;
                    texts.transform.parent = textRow4;
                    break;
            }
            texts.gameObject.SetActive(true);
            audioSource.PlayOneShot(dealSound);
            yield return new WaitForSeconds(0.1f);
        }
        gameplay.Setup();
    }
}
