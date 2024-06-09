using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    public ButtonManager buttonManager;
    public Dealer dealer;
    public TMP_Text resultText;
    public TMP_Text goalValueText;
    private Card goalCard;
    private Card card1;
    private Card card2;
    public TMP_Text player1ScoreText;
    public TMP_Text player2ScoreText;
    public TMP_Text player3ScoreText;
    public TMP_Text player4ScoreText;
    public int currentPlayer = 1;
    private int player1Score = 0;
    private int player2Score = 0;
    private int player3Score = 0;
    private int player4Score = 0;
    void Update()
    {
        if (buttonManager.cardSlot1.childCount > 0 && buttonManager.cardSlot2.childCount > 0)
        {
            card1 = buttonManager.cardSlot1.GetChild(0).GetComponent<Card>();
            card2 = buttonManager.cardSlot2.GetChild(0).GetComponent<Card>();
        }
        else
        {
            card1 = null;
            card2 = null;
        }
        CheckPlayerInterFace();
    }
    public void CheckPlayerInterFace()
    {
        if (GameObject.Find("Canvas/GamePlay") != null && GameObject.Find("Canvas/GamePlay").activeInHierarchy)
        {
            if (currentPlayer == 1)
            {
                GameObject.Find("Canvas/GamePlay").transform.Find("PlayerInterface").gameObject.SetActive(true);
            }
            if (currentPlayer != 1)
            {
                GameObject.Find("Canvas/GamePlay").transform.Find("PlayerInterface").gameObject.SetActive(false);
            }
        }
    }
    public void SetGoalCard()
    {
        if (GameObject.Find("Goal").transform.childCount > 0)
        {
            goalCard = GameObject.Find("Goal").transform.GetChild(0).GetComponent<Card>();
            if (goalCard != null)
            {
                goalValueText.text = goalCard.GetCardValue.ToString();
            }
        }
        else
        {
            goalValueText.text = "";
        }
    }
    public IEnumerator AIPlayTurn(int playerNumber)
    {
        yield return new WaitForSeconds(1.0f);
        Transform aiPlayer = GameObject.Find("Player " + playerNumber).transform;
        List<Card> aiHand = new List<Card>();
        for (int i = 0; i < aiPlayer.childCount; i++)
        {
            aiHand.Add(aiPlayer.GetChild(i).GetComponent<Card>());
        }
        bool foundValidMove = false;
        foreach (var card1 in aiHand)
        {
            foreach (var card2 in aiHand)
            {
                if (card1 != card2)
                {
                    foreach (var op in new string[] { "+", "-", "*", "/", "%", "^", "¡Ì" })
                    {
                        if ((op == "/" || op == "%") && card1.GetCardValue >= card2.GetCardValue)
                        {
                            continue;
                        }
                        float result = CalculateResult(card1.GetCardValue, card2.GetCardValue, op);
                        if (result == (float)goalCard.GetCardValue)
                        {
                            Card.RevealCard(card1.gameObject);
                            card1.GetComponent<Card>().GetTargetPos = buttonManager.cardSlot1.position;
                            card1.transform.SetParent(buttonManager.cardSlot1);
                            yield return new WaitForSeconds(1.0f);
                            Card.RevealCard(card2.gameObject);
                            card2.GetComponent<Card>().GetTargetPos = buttonManager.cardSlot2.position;
                            card2.transform.SetParent(buttonManager.cardSlot2);
                            yield return new WaitForSeconds(1.0f);
                            buttonManager.operatorText.text = op;
                            yield return new WaitForSeconds(1.0f);
                            ConfirmSelection();
                            foundValidMove = true;
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
            if (dealer.drawCount < 5)
            {
                buttonManager.DrawCard();
                StartCoroutine(AIPlayTurn(playerNumber));
            }
            else
            {
                buttonManager.SkipTurn();
            }
        }
    }
    private float CalculateResult(int value1, int value2, string op)
    {
        switch (op)
        {
            case "+": return value1 + value2;
            case "-": return value1 - value2;
            case "*": return value1 * value2;
            case "/": return (float)value1 / value2;
            case "%": return value1 % value2;
            case "^": return (int)Mathf.Pow(value1, value2);
            case "¡Ì": return (int)Mathf.Pow(value1, 1.0f / value2);
            default: return int.MaxValue;
        }
    }
    public IEnumerator AIChooseNewGoalCard()
    {
        yield return new WaitForSeconds(2.0f);
        if (Random.value > 0.5f)
        {
            buttonManager.ChooseCard1();
        }
        else
        {
            buttonManager.ChooseCard2();
        }
    }
    public void ConfirmSelection()
    {
        float result = 0;
        int value1 = card1.GetCardValue;
        int value2 = card2.GetCardValue;
        int score = value1 + value2;

        switch (buttonManager.operatorText.text)
        {
            case "+": result = value1 + value2; break;
            case "-": result = value1 - value2; break;
            case "*": result = value1 * value2; break;
            case "/": result = (float)value1 / value2; break;
            case "%": result = value1 % value2; break;
            case "^": result = (int)Mathf.Pow(value1, value2); break;
            case "¡Ì": result = (int)Mathf.Pow(value1, 1.0f / value2); break;
        }
        if (value1 > 13 || value2 > 13)
        {
            int jokerValue = value1 > 13 ? value1 : value2;
            int otherValue = value1 > 13 ? value2 : value1;
            resultText.text = "Joker!";
            buttonManager.operatorText.text = "";
            UpdateScore(currentPlayer, -otherValue);
            Transform currentPlayerTransform = GameObject.Find("Player " + currentPlayer).transform;
            if (currentPlayerTransform.childCount == 0)
            {
                if (GameObject.Find("Holder").transform.childCount > 0)
                {
                    Card.RevealCard(GameObject.Find("Holder").transform.GetChild(0).gameObject);
                    GameObject.Find("Holder").transform.GetChild(0).GetComponent<Card>().GetTargetPos = GameObject.Find("Player " + currentPlayer).transform.position;
                    GameObject.Find("Holder").transform.GetChild(0).parent = GameObject.Find("Player " + currentPlayer).transform;
                }
            }
            if (jokerValue == value1)
            {
                buttonManager.ChooseCard2();
            }
            else
            {
                buttonManager.ChooseCard1();
            }
        }
        else if (result == (float)goalCard.GetCardValue)
        {
            resultText.text = "Correct!";
            buttonManager.operatorText.text = "";
            UpdateScore(currentPlayer, score);
            Transform currentPlayerTransform = GameObject.Find("Player " + currentPlayer).transform;
            if (currentPlayerTransform.childCount == 0)
            {
                EndGame();
                return;
            }
            else
            {
                buttonManager.takeCard1Button.gameObject.SetActive(false);
                buttonManager.takeCard2Button.gameObject.SetActive(false);
                buttonManager.chooseCard1Button.gameObject.SetActive(true);
                buttonManager.chooseCard2Button.gameObject.SetActive(true);
                if (currentPlayer != 1)
                {
                    StartCoroutine(AIChooseNewGoalCard());
                }

            }
        }
        else
        {
            resultText.text = "Oops!";
            buttonManager.operatorText.text = "";
            UpdateScore(currentPlayer, -score);
            buttonManager.TakeCardFromSlot1();
            buttonManager.TakeCardFromSlot2();
            buttonManager.SkipTurn();
        }
    }
    private void UpdateScore(int player, int score)
    {
        switch (player)
        {
            case 1:
                player1Score += score;
                player1ScoreText.text = "Your Score: " + player1Score;
                break;
            case 2:
                player2Score += score;
                player2ScoreText.text = "Player 2 Score: " + player2Score;
                break;
            case 3:
                player3Score += score;
                player3ScoreText.text = "Player 3 Score: " + player3Score;
                break;
            case 4:
                player4Score += score;
                player4ScoreText.text = "Player 4 Score: " + player4Score;
                break;
        }
    }
    private void EndGame()
    {
        GameObject.Find("Canvas").transform.Find("Ending").gameObject.SetActive(true);
        GameObject.Find("Canvas").transform.Find("GamePlay").gameObject.SetActive(false);
        GameObject.Find("Canvas/Ending/Player1Score").GetComponent<TMP_Text>().text = player1ScoreText.text;
        GameObject.Find("Canvas/Ending/Player2Score").GetComponent<TMP_Text>().text = player2ScoreText.text;
        GameObject.Find("Canvas/Ending/Player3Score").GetComponent<TMP_Text>().text = player3ScoreText.text;
        GameObject.Find("Canvas/Ending/Player4Score").GetComponent<TMP_Text>().text = player4ScoreText.text;
        if (player1Score > player2Score && player1Score > player3Score && player1Score > player4Score)
        {
            GameObject.Find("Canvas/Ending/Result").GetComponent<TMP_Text>().text = "You Win!";
        }
        else
        {
            GameObject.Find("Canvas/Ending/Result").GetComponent<TMP_Text>().text = "You Lost...";
        }
    }
}