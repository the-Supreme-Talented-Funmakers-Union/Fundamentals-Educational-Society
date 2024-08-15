using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameplayHerb : MonoBehaviour
{
    public DealerHerb dealer;
    public GameObject endGame;
    public Transform player1;
    public Transform player2;
    public Transform player3;
    public Transform player4;
    public TMP_Text Announcer;
    public TMP_Text Winner;
    public TMP_Text player1Score;
    public TMP_Text player2Score;
    public TMP_Text player3Score;
    public TMP_Text player4Score;
    public GameObject quit;
    public bool SinglePlayer;
    public int currentPlayer;
    private bool firstGuess, secondGuess;
    private CardHerb firstCard, secondCard;
    private List<CardHerb> cards = new List<CardHerb>();
    private List<CardHerb> imageCards = new List<CardHerb>();
    private List<CardHerb> textCards = new List<CardHerb>();
    private List<CardHerb> revealedImageCards = new List<CardHerb>();
    private List<CardHerb> revealedTextCards = new List<CardHerb>();
    private AudioSource audioSource;
    public AudioClip flipSound;
    public AudioClip buttonClickSound;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }
    public void ClassBegin(GameObject button)
    {
        StartCoroutine(dealer.Deal());
        button.SetActive(false);
    }
    public void Setup()
    {
        cards.AddRange(FindObjectsOfType<CardHerb>());
        foreach (CardHerb card in cards)
        {
            card.OnCardClicked += OnCardClicked;
            if (card.IsImage())
            {
                imageCards.Add(card);
            }
            else
            {
                textCards.Add(card);
            }
        }
        if (SinglePlayer)
        {
            currentPlayer = 1;
            Announcer.color = UnityEngine.Color.white;
            Announcer.text = "Let's Go!";
        }
        else
        {
            currentPlayer = Random.Range(1, 5);
            Announcer.color = UnityEngine.Color.white;
            Announcer.text = "Player " + currentPlayer + "'s Turn";
            if (currentPlayer != 1)
            {
                StartCoroutine(AITurn());
            }
        }
    }
    public void OnCardClicked(CardHerb card)
    {
        audioSource.PlayOneShot(flipSound);
        if (currentPlayer != 1) return;
        if (!firstGuess)
        {
            firstGuess = true;
            firstCard = card;
            firstCard.FlipCard();
            if (firstCard.IsImage())
            {
                if (!revealedImageCards.Contains(firstCard))
                {
                    revealedImageCards.Add(firstCard);
                }
            }
            else
            {
                if (!revealedTextCards.Contains(firstCard))
                {
                    revealedTextCards.Add(firstCard);
                }
            }
        }
        else if (!secondGuess && firstCard.IsImage() != card.IsImage())
        {
            secondGuess = true;
            secondCard = card;
            secondCard.FlipCard();
            if (secondCard.IsImage())
            {
                if (!revealedImageCards.Contains(secondCard))
                {
                    revealedImageCards.Add(secondCard);
                }
            }
            else
            {
                if (!revealedTextCards.Contains(secondCard))
                {
                    revealedTextCards.Add(secondCard);
                }
            }
            StartCoroutine(CheckMatch());
        }
    }
    public IEnumerator CheckMatch()
    {
        yield return new WaitForSeconds(1f);
        if (firstCard.GetPlantName == secondCard.GetPlantName)
        {
            Match();
            yield return new WaitForSeconds(1f);
            firstGuess = false;
            secondGuess = false;
            firstCard = null;
            secondCard = null;
            Announcer.color = UnityEngine.Color.white;
            Announcer.text = "Keep Going!";
            if (currentPlayer != 1)
            {
                StartCoroutine(AITurn());
            }
        }
        else
        {
            Announcer.color = UnityEngine.Color.yellow;
            Announcer.text = "No Match!";
            audioSource.PlayOneShot(flipSound);
            firstCard.FlipCardBack();
            secondCard.FlipCardBack();
            yield return new WaitForSeconds(1f);
            firstGuess = false;
            secondGuess = false;
            firstCard = null;
            secondCard = null;
            if (!SinglePlayer)
            {
                currentPlayer = currentPlayer % 4 + 1;
                Announcer.color = UnityEngine.Color.white;
                Announcer.text = "Player " + currentPlayer + "'s Turn";
                if (currentPlayer != 1)
                {
                    StartCoroutine(AITurn());
                }
            }
            else
            {
                Announcer.color = UnityEngine.Color.white;
                Announcer.text = "Keep Trying!";
            }
        }
    }
    public void Match()
    {
        Announcer.color = UnityEngine.Color.green;
        Announcer.text = "Match!";
        if (firstCard.IsImage())
        {
            imageCards.Remove(firstCard);
            revealedImageCards.Remove(firstCard);
            revealedTextCards.Remove(secondCard);
            textCards.Remove(secondCard);
            firstCard.GetTargetPos = GetCurrentPlayer(currentPlayer).position;
            firstCard.transform.parent = GetCurrentPlayer(currentPlayer);
            Destroy(secondCard.gameObject);
        }
        else
        {
            imageCards.Remove(secondCard);
            revealedImageCards.Remove(secondCard);
            revealedTextCards.Remove(firstCard);
            textCards.Remove(firstCard);
            secondCard.GetTargetPos = GetCurrentPlayer(currentPlayer).position;
            secondCard.transform.parent = GetCurrentPlayer(currentPlayer);
            Destroy(firstCard.gameObject);
        }
        if (imageCards.Count == 0 && textCards.Count == 0)
        {
            StartCoroutine(EndGame());
        }

    }
    private IEnumerator AITurn()
    {
        yield return new WaitForSeconds(1f);
        foreach (var revealedImageCard in revealedImageCards)
        {
            var matchingCard = revealedTextCards.Find(card => card.GetPlantName == revealedImageCard.GetPlantName);
            if (matchingCard != null)
            {
                yield return new WaitForSeconds(1f);
                audioSource.PlayOneShot(flipSound);
                firstCard = revealedImageCard;
                firstCard.FlipCard();
                yield return new WaitForSeconds(1f);
                audioSource.PlayOneShot(flipSound);
                secondCard = matchingCard;
                secondCard.FlipCard();
                yield return new WaitForSeconds(1f);
                StartCoroutine(CheckMatch());
                yield break;
            }
        }
        yield return new WaitForSeconds(1f);
        audioSource.PlayOneShot(flipSound);
        firstCard = imageCards[Random.Range(0, imageCards.Count)];
        firstCard.FlipCard();
        if (!revealedImageCards.Contains(firstCard))
        {
            revealedImageCards.Add(firstCard);
        }
        yield return new WaitForSeconds(1f);
        audioSource.PlayOneShot(flipSound);
        secondCard = textCards[Random.Range(0, textCards.Count)];
        secondCard.FlipCard();
        if (!revealedTextCards.Contains(secondCard))
        {
            revealedTextCards.Add(secondCard);
        }
        yield return new WaitForSeconds(1f);
        StartCoroutine(CheckMatch());
    }
    private Transform GetCurrentPlayer(int player)
    {
        switch (player)
        {
            case 2: return player2;
            case 3: return player3;
            case 4: return player4;
            default: return player1;
        }
    }
    private IEnumerator EndGame()
    {
        endGame.SetActive(true);
        if (!SinglePlayer)
        {
            yield return new WaitForSeconds(1.0f);
            player1Score.text = player1.childCount.ToString();
            yield return new WaitForSeconds(1.0f);
            player2Score.text = player2.childCount.ToString();
            yield return new WaitForSeconds(1.0f);
            player3Score.text = player3.childCount.ToString();
            yield return new WaitForSeconds(1.0f);
            player4Score.text = player4.childCount.ToString();
            yield return new WaitForSeconds(1.0f);
            if (player1.childCount > player2.childCount && player1.childCount > player3.childCount && player1.childCount > player4.childCount)
            {
                Winner.text = "You Win";
            }
            else if (player2.childCount > player1.childCount && player2.childCount > player3.childCount && player2.childCount > player4.childCount)
            {
                Winner.text = "Player 2 Win";
            }
            else if (player3.childCount > player1.childCount && player3.childCount > player2.childCount && player3.childCount > player4.childCount)
            {
                Winner.text = "Player 3 Win";
            }
            else if (player4.childCount > player1.childCount && player4.childCount > player2.childCount && player4.childCount > player3.childCount)
            {
                Winner.text = "Player 4 Win";
            }
        }
        else
        {
            yield return new WaitForSeconds(1.0f);
            Winner.text = "Thanks for plaing!";
        }
        yield return new WaitForSeconds(1.0f);
        quit.SetActive(true);
    }
    public void ExitGame()
    {
        audioSource.PlayOneShot(buttonClickSound);
        SceneManager.LoadScene(1);
    }
}
