using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class GameplayE : MonoBehaviour
{
    public ButtonE button;
    public DealerE dealer;
    public TMP_Text cardCount;
    public Transform Card1;
    public TMP_Text Card1Text;
    public TMP_Text Operator1;
    public Transform Card2;
    public TMP_Text Card2Text;
    public TMP_Text goalValue;
    public GameObject Interface;
    public TMP_Text resultText;
    public TMP_Text scoreText;
    public TMP_Text P1Score;
    public TMP_Text P2Score;
    public TMP_Text P3Score;
    public TMP_Text P4Score;
    public GameObject endGame;
    public TMP_Text Winner;
    public TMP_Text P1Final;
    public TMP_Text P2Final;
    public TMP_Text P3Final;
    public TMP_Text P4Final;
    public GameObject quit;
    public int currentPlayer = 1;
    public bool newGoal = false;
    private int player1Score = 0;
    private int player2Score = 0;
    private int player3Score = 0;
    private int player4Score = 0;
    void Update()
    {
        if (dealer.cardDealt)
        {
            cardCount.text = $"{dealer.cardHolder.childCount}";
            if (newGoal)
            {
                Card1Text.text = "Pick";
                Card2Text.text = "Pick";
            }
            else
            {
                if (Card1.childCount > 0)
                {
                    Card1Text.text = $"{Card1.GetChild(0).GetComponent<Card>().GetCardValue}";
                }
                else
                {
                    Card1Text.text = "";
                }
                if (Card2.childCount > 0)
                {
                    Card2Text.text = $"{Card2.GetChild(0).GetComponent<Card>().GetCardValue}";
                }
                else
                {
                    Card2Text.text = "";
                }
            }
            if (dealer.cardGoal.childCount > 0)
            {
                goalValue.text = $"{dealer.cardGoal.GetChild(0).GetComponent<Card>().GetCardValue}";
            }
            else
            {
                goalValue.text = "";
            }
            CheckTurn();
        }
        else
        {
            cardCount.text = "";
            Card1Text.text = "";
            Card2Text.text = "";
            goalValue.text = "";
        }
    }
    public void GameStart()
    {
        currentPlayer = Random.Range(1, 5);
        if (currentPlayer != 1)
        {
            StartCoroutine(AITurn(currentPlayer));
        }
    }
    private void CheckTurn()
    {
        if (currentPlayer == 1)
        {
            Interface.SetActive(true);
        }
        else
        {
            Interface.SetActive(false);
        }
    }
    public IEnumerator ConfirmSelection()
    {
        if (!newGoal)
        {
            if (Card1.childCount == 0 || Card2.childCount == 0)
            {
                yield break;
            }
            float result = 0f;
            float goal = (float)dealer.cardGoal.GetChild(0).GetComponent<Card>().GetCardValue;
            float value1 = (float)Card1.GetChild(0).GetComponent<Card>().GetCardValue;
            float value2 = (float)Card2.GetChild(0).GetComponent<Card>().GetCardValue;
            int score = (int)value1 + (int)value2;
            switch (Operator1.text)
            {
                case "+": result = value1 + value2; break;
                case "-": result = value1 - value2; break;
                case "x": result = value1 * value2; break;
                case "¡Â": result = value1 / value2; break;
                case "^": result = (value1 != 1) ? Mathf.Pow(value1, value2) : -999; break;
                case "=": result = (value1 == value2) ? value1 : -999; break;
            }
            if (value1 > 13 || value2 > 13)
            {
                int jokerCount = 0;
                int goalChoice = 0;
                if (value1 > 13)
                {
                    score = score - (int)value1;
                    jokerCount++;
                }
                if (value2 > 13)
                {
                    score = score - (int)value2;
                    jokerCount++;
                }
                if (jokerCount == 2)
                {
                    score = -1;
                    resultText.color = UnityEngine.Color.black;
                    resultText.text = "Too many Joker";
                    scoreText.color = UnityEngine.Color.red;
                    scoreText.text = $"{score}";
                    UpdateScore(currentPlayer, score);
                    yield return new WaitForSeconds(2.0f);
                    button.SkipTurn();
                }
                else
                {
                    if (value1 > 13)
                    {
                        goalChoice = 2;
                    }
                    else
                    {
                        goalChoice = 1;
                    }
                    score = -score;
                    resultText.color = UnityEngine.Color.black;
                    resultText.text = "Joker";
                    scoreText.color = UnityEngine.Color.red;
                    scoreText.text = $"{score}";
                    UpdateScore(currentPlayer, score);
                    switch (currentPlayer)
                    {
                        case 1:
                            if (dealer.player1.childCount == 0)
                            {
                                button.DrawCard();
                                button.DrawCard();
                            }
                            break;
                        case 2:
                            if (dealer.player2.childCount == 0)
                            {
                                button.DrawCard();
                                button.DrawCard();
                            }
                            break;
                        case 3:
                            if (dealer.player3.childCount == 0)
                            {
                                button.DrawCard();
                                button.DrawCard();
                            }
                            break;
                        case 4:
                            if (dealer.player4.childCount == 0)
                            {
                                button.DrawCard();
                                button.DrawCard();
                            }
                            break;
                    }
                    newGoal = true;
                    yield return new WaitForSeconds(2.0f);
                    button.SetGoal(goalChoice);
                }
            }
            else if (Mathf.Approximately(result, goal))
            {
                resultText.color = UnityEngine.Color.white;
                resultText.text = "Your result is " + result.ToString("F2");
                yield return new WaitForSeconds(2.0f);
                resultText.color = UnityEngine.Color.yellow;
                resultText.text = "Correct";
                scoreText.color = UnityEngine.Color.yellow;
                scoreText.text = $"{score}";
                UpdateScore(currentPlayer, score);
                switch (currentPlayer)
                {
                    case 1:
                        if (dealer.player1.childCount == 0)
                        {
                            yield return new WaitForSeconds(2.0f);
                            StartCoroutine(EndGame());
                            yield break;
                        }
                        break;
                    case 2:
                        if (dealer.player2.childCount == 0)
                        {
                            yield return new WaitForSeconds(2.0f);
                            StartCoroutine(EndGame());
                            yield break;
                        }
                        break;
                    case 3:
                        if (dealer.player3.childCount == 0)
                        {
                            yield return new WaitForSeconds(2.0f);
                            StartCoroutine(EndGame());
                            yield break;
                        }
                        break;
                    case 4:
                        if (dealer.player4.childCount == 0)
                        {
                            yield return new WaitForSeconds(2.0f);
                            StartCoroutine(EndGame());
                            yield break;
                        }
                        break;
                }
                newGoal = true;
                if (currentPlayer != 1)
                {
                    yield return new WaitForSeconds(2.0f);
                    StartCoroutine(AINewGoal());
                }
            }
            else if (result == -999)
            {
                score = -score;
                resultText.color = UnityEngine.Color.red;
                resultText.text = "Illegal Calculation";
                scoreText.color = UnityEngine.Color.red;
                scoreText.text = $"{score}";
                UpdateScore(currentPlayer, score);
                yield return new WaitForSeconds(2.0f);
                button.SkipTurn();
            }
            else
            {
                resultText.color = UnityEngine.Color.white;
                resultText.text = "Your result is " + result.ToString("F2");
                yield return new WaitForSeconds(2.0f);
                score = -score;
                resultText.color = UnityEngine.Color.red;
                resultText.text = "Incorrect";
                scoreText.color = UnityEngine.Color.red;
                scoreText.text = $"{score}";
                UpdateScore(currentPlayer, score);
                yield return new WaitForSeconds(2.0f);
                button.SkipTurn();
            }
        }
    }
    private void UpdateScore(int player, int score)
    {
        switch (player)
        {
            case 1:
                player1Score += score;
                P1Score.text = "Your Score: " + player1Score;
                break;
            case 2:
                player2Score += score;
                P2Score.text = "Player 2 Score: " + player2Score;
                break;
            case 3:
                player3Score += score;
                P3Score.text = "Player 3 Score: " + player3Score;
                break;
            case 4:
                player4Score += score;
                P4Score.text = "Player 4 Score: " + player4Score;
                break;
        }
    }
    public IEnumerator AITurn(int player)
    {
        Transform aiPlayer = null;
        List<Card> aiHand = new List<Card>();
        switch (player)
        {
            case 2:
                aiPlayer = dealer.player2;
                break;
            case 3:
                aiPlayer = dealer.player3;
                break;
            case 4:
                aiPlayer = dealer.player4;
                break;
        }
        for (int i = 0; i < aiPlayer.childCount; i++)
        {
            aiHand.Add(aiPlayer.GetChild(i).GetComponent<Card>());
        }
        yield return new WaitForSeconds(1.0f);
        bool foundValidMove = false;
        foreach (var card1 in aiHand)
        {
            foreach (var card2 in aiHand)
            {
                if (card1 != card2)
                {
                    foreach (var op in new string[] { "+", "-", "x", "¡Â", "=" })
                    {
                        float result = 0;
                        float value1 = (float)card1.GetCardValue;
                        float value2 = (float)card2.GetCardValue;
                        float goal = (float)dealer.cardGoal.GetChild(0).GetComponent<Card>().GetCardValue;
                        switch (op)
                        {
                            case "+": result = value1 + value2; break;
                            case "-": result = value1 - value2; break;
                            case "x": result = value1 * value2; break;
                            case "¡Â": result = value1 / value2; break;
                            case "=": result = (value1 == value2) ? value1 : -999; break;
                        }
                        if (Mathf.Approximately(result, goal))
                        {
                            foundValidMove = true;
                            yield return new WaitForSeconds(1.5f);
                            card1.GetTargetPos = Card1.position;
                            card1.transform.parent = Card1;
                            Card.RevealCard(Card1.GetChild(0).gameObject);
                            card2.GetTargetPos = Card2.position;
                            card2.transform.parent = Card2;
                            Card.RevealCard(Card2.GetChild(0).gameObject);
                            yield return new WaitForSeconds(1.5f);
                            Operator1.text = op;
                            yield return new WaitForSeconds(1.5f);
                            StartCoroutine(ConfirmSelection());
                            break;
                        }
                    }
                    if (foundValidMove) break;
                }
            }
            if (foundValidMove) break;
        }
        if (!foundValidMove)
        {
            if (button.drawCount < 5)
            {
                yield return new WaitForSeconds(1.5f);
                button.DrawCard();
                StartCoroutine(AITurn(currentPlayer));
            }
            else
            {
                button.SkipTurn();
            }
        }
    }
    public IEnumerator AINewGoal()
    {
        yield return new WaitForSeconds(2.0f);
        int randomChoice = Random.Range(0, 2);
        switch (randomChoice)
        {
            case 0:
                button.SetGoal(1);
                break;
            case 1:
                button.SetGoal(2);
                break;
        }
    }
    private IEnumerator EndGame()
    {
        endGame.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        P2Final.text = P2Score.text;
        yield return new WaitForSeconds(1.0f);
        P3Final.text = P3Score.text;
        yield return new WaitForSeconds(1.0f);
        P4Final.text = P4Score.text;
        yield return new WaitForSeconds(1.0f);
        P1Final.text = P1Score.text;
        yield return new WaitForSeconds(1.0f);
        if (player1Score > player2Score && player1Score > player3Score && player1Score > player4Score)
        {
            Winner.text = "You Win";
        }
        else if (player2Score > player1Score && player2Score > player3Score && player2Score > player4Score)
        {
            Winner.text = "Player 2 Win";
        }
        else if (player3Score > player1Score && player3Score > player2Score && player3Score > player4Score)
        {
            Winner.text = "Player 3 Win";
        }
        else if (player4Score > player1Score && player4Score > player2Score && player4Score > player3Score)
        {
            Winner.text = "Player 4 Win";
        }
        yield return new WaitForSeconds(1.0f);
        quit.SetActive(true);
    }
}