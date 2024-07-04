using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    public Dealer dealer;
    public TMP_Text resultText;
    public TMP_Text player1ScoreText;
    public TMP_Text player2ScoreText;
    public TMP_Text player3ScoreText;
    public TMP_Text player4ScoreText;
    public int currentPlayer = 1;
    private int player1Score = 0;
    private int player2Score = 0;
    private int player3Score = 0;
    private int player4Score = 0;
    private Transform cardSlot1, cardSlot2, cardSlot3, cardSlot4, goal;
    private TMP_Text operatorText1, operatorText2, operatorText3;
    public void Update()
    {
        if (dealer.cardDealt)
        {
            GameObject currentSetting = dealer.currentSetting;
            CardPlayStats cardPlayStats = currentSetting.GetComponent<CardPlayStats>();
            if (currentSetting == dealer.easySetting)
            {
                EasyMode(cardPlayStats);
            }
            else if (currentSetting == dealer.mediumSetting)
            {
                MediumMode(cardPlayStats);
            }
            else if (currentSetting == dealer.hardSetting)
            {
                HardMode(cardPlayStats);
            }
        }
    }
    private void EasyMode(CardPlayStats cardPlayStats)
    {
        cardSlot1 = cardPlayStats.Card1.transform;
        cardSlot2 = cardPlayStats.Card2.transform;
        operatorText1 = GameObject.Find("EasyMode/Operator 1/Canvas/Button/Text (TMP)").GetComponent<TMP_Text>();
        goal = cardPlayStats.Goal.transform;
    }
    private void MediumMode(CardPlayStats cardPlayStats)
    {
        cardSlot1 = cardPlayStats.Card1.transform;
        cardSlot2 = cardPlayStats.Card2.transform;
        cardSlot3 = cardPlayStats.Card3.transform;
        operatorText1 = GameObject.Find("MediumMode/Operator 1/Canvas/Button/Text (TMP)").GetComponent<TMP_Text>();
        operatorText2 = GameObject.Find("MediumMode/Operator 2/Canvas/Button/Text (TMP)").GetComponent<TMP_Text>();
        goal = cardPlayStats.Goal.transform;
    }
    private void HardMode(CardPlayStats cardPlayStats)
    {
        cardSlot1 = cardPlayStats.Card1.transform;
        cardSlot2 = cardPlayStats.Card2.transform;
        cardSlot3 = cardPlayStats.Card3.transform;
        cardSlot4 = cardPlayStats.Card4.transform;
        operatorText1 = GameObject.Find("HardMode/Operator 1/Canvas/Button/Text (TMP)").GetComponent<TMP_Text>();
        operatorText2 = GameObject.Find("HardMode/Operator 2/Canvas/Button/Text (TMP)").GetComponent<TMP_Text>();
        operatorText3 = GameObject.Find("HardMode/Operator 3/Canvas/Button/Text (TMP)").GetComponent<TMP_Text>();
        goal = cardPlayStats.Goal.transform;
    }
    public void ConfirmSelection()
    {
        float result = 0;
        int intResult = 0;

        if (dealer.currentSetting == dealer.easySetting)
        {
            int value1Int = cardSlot1.GetChild(0).GetComponent<Card>().GetCardValue;
            int value2Int = cardSlot2.GetChild(0).GetComponent<Card>().GetCardValue;
            string op1 = operatorText1.text;
            int goalValueInt = goal.GetChild(0).GetComponent<Card>().GetCardValue;
            intResult = CalculateIntResult(value1Int, value2Int, op1);
            EasyResult(intResult, goalValueInt);
        }
        else if (dealer.currentSetting == dealer.mediumSetting)
        {
            float value1Float = cardSlot1.GetChild(0).GetComponent<Card>().GetCardValue;
            float value2Float = cardSlot2.GetChild(0).GetComponent<Card>().GetCardValue;
            float value3Float = cardSlot3.GetChild(0).GetComponent<Card>().GetCardValue;
            string op1 = operatorText1.text;
            string op2 = operatorText2.text;
            float goalValue = goal.GetChild(0).GetComponent<Card>().GetCardValue;
            result = CalculateFloatResult(value1Float, value2Float, op1);
            result = CalculateFloatResult(result, value3Float, op2);
            MediumResult(result, goalValue);
        }
        else if (dealer.currentSetting == dealer.hardSetting)
        {
            float value1Float = cardSlot1.GetChild(0).GetComponent<Card>().GetCardValue;
            float value2Float = cardSlot2.GetChild(0).GetComponent<Card>().GetCardValue;
            float value3Float = cardSlot3.GetChild(0).GetComponent<Card>().GetCardValue;
            float value4Float = cardSlot4.GetChild(0).GetComponent<Card>().GetCardValue;
            string op1 = operatorText1.text;
            string op2 = operatorText2.text;
            string op3 = operatorText3.text;
            float goalValue = goal.GetChild(0).GetComponent<Card>().GetCardValue;
            result = CalculateFloatResult(value1Float, value2Float, op1);
            result = CalculateFloatResult(result, value3Float, op2);
            result = CalculateFloatResult(result, value4Float, op3);
            HardResult(result, goalValue);
        }
    }
<<<<<<< Updated upstream
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
                        if ((op == "/" || op == "%") && card2.GetCardValue >= card1.GetCardValue)
                        {
                            continue;
                        }
                        float result = CalculateResult(card1.GetCardValue, card2.GetCardValue, op);
                        if (result == goalCard.GetCardValue)
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
=======
    private int CalculateIntResult(int value1, int value2, string op)
>>>>>>> Stashed changes
    {
        switch (op)
        {
            case "+": return value1 + value2;
            case "-": return value1 - value2;
            case "x": return value1 * value2;
            case "/": return value1 / value2;
            case "%": return value1 % value2;
            default: return int.MaxValue;
        }
    }
    private void EasyResult(int result, int goalValue)
    {
<<<<<<< Updated upstream
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
        else if (result == goalCard.GetCardValue)
=======
        if (result == goalValue)
>>>>>>> Stashed changes
        {
            resultText.text = "Correct!";
            UpdateScore(result);
        }
        else
        {
            resultText.text = "Incorrect!";
            UpdateScore(-result);
        }
    }
    private float CalculateFloatResult(float value1, float value2, string op)
    {
        switch (op)
        {
            case "+": return value1 + value2;
            case "-": return value1 - value2;
            case "x": return value1 * value2;
            case "/": return value1 / value2;
            case "%": return value1 % value2;
            case "^": return Mathf.Pow(value1, value2);
            default: return float.MaxValue;
        }
    }
    private void MediumResult(float result, float goalValue)
    {
        if (result >= (goalValue - 0.5f) && result < goalValue + 0.5f)
        {
            resultText.text = "Correct!";
            UpdateScore(Mathf.RoundToInt(result));
        }
        else
        {
            resultText.text = "Incorrect!";
            UpdateScore(-Mathf.RoundToInt(result));
        }
    }
    private void HardResult(float result, float goalValue)
    {
        if (Mathf.Abs(result) >= (goalValue - 0.5f) && Mathf.Abs(result) < goalValue + 0.5f)
        {
            resultText.text = "Correct!";
            UpdateScore(Mathf.RoundToInt(result));
        }
        else
        {
            resultText.text = "Incorrect!";
            UpdateScore(-Mathf.RoundToInt(result));
        }
    }
    public IEnumerator AIPlayTurn(int player)
    {
        yield return new WaitForSeconds(2);
        Transform aiHandTransform = GameObject.Find("Player " + player).transform;
        List<Card> aiCards = new List<Card>();
        for (int i = 0; i < aiHandTransform.childCount; i++)
        {
            aiCards.Add(aiHandTransform.GetChild(i).GetComponent<Card>());
        }
        bool actionTaken = false;

        if (dealer.currentSetting == dealer.easySetting)
        {
            actionTaken = TryEasyMode(aiCards);
        }
        else if (dealer.currentSetting == dealer.mediumSetting)
        {
            actionTaken = TryMediumMode(aiCards);
        }
        else if (dealer.currentSetting == dealer.hardSetting)
        {
            actionTaken = TryHardMode(aiCards);
        }

        if (actionTaken)
        {
            ConfirmSelection();
        }
        currentPlayer = (currentPlayer % 4) + 1;
        if (currentPlayer != 1)
        {
            StartCoroutine(AIPlayTurn(currentPlayer));
        }
    }
    private bool TryEasyMode(List<Card> aiCards)
    {
        // Logic for AI to handle easy mode
        if (aiCards.Count < 2)
        {
            return false;
        }

        Card card1 = aiCards[0];
        Card card2 = aiCards[1];
        string op1 = operatorText1.text;
        int result = CalculateIntResult(card1.GetCardValue, card2.GetCardValue, op1);
        int goalValue = goal.GetChild(0).GetComponent<Card>().GetCardValue;

        if (result == goalValue)
        {
            // Place cards in slots
            cardSlot1.GetChild(0).SetParent(cardSlot1);
            cardSlot2.GetChild(0).SetParent(cardSlot2);
            return true;
        }

        return false;
    }
    private bool TryMediumMode(List<Card> aiCards)
    {
        // Logic for AI to handle medium mode
        if (aiCards.Count < 3)
        {
            return false;
        }

        Card card1 = aiCards[0];
        Card card2 = aiCards[1];
        Card card3 = aiCards[2];
        string op1 = operatorText1.text;
        string op2 = operatorText2.text;
        float result = CalculateFloatResult(card1.GetCardValue, card2.GetCardValue, op1);
        result = CalculateFloatResult(result, card3.GetCardValue, op2);
        float goalValue = goal.GetChild(0).GetComponent<Card>().GetCardValue;

        if (result >= (goalValue - 0.5f) && result < (goalValue + 0.5f))
        {
            // Place cards in slots
            cardSlot1.GetChild(0).SetParent(cardSlot1);
            cardSlot2.GetChild(0).SetParent(cardSlot2);
            cardSlot3.GetChild(0).SetParent(cardSlot3);
            return true;
        }

        return false;
    }
    private bool TryHardMode(List<Card> aiCards)
    {
        // Logic for AI to handle hard mode
        if (aiCards.Count < 4)
        {
            return false;
        }

        Card card1 = aiCards[0];
        Card card2 = aiCards[1];
        Card card3 = aiCards[2];
        Card card4 = aiCards[3];
        string op1 = operatorText1.text;
        string op2 = operatorText2.text;
        string op3 = operatorText3.text;
        float result = CalculateFloatResult(card1.GetCardValue, card2.GetCardValue, op1);
        result = CalculateFloatResult(result, card3.GetCardValue, op2);
        result = CalculateFloatResult(result, card4.GetCardValue, op3);
        float goalValue = goal.GetChild(0).GetComponent<Card>().GetCardValue;

        if ((Mathf.Abs(result - goalValue) <= 0.5f) || (Mathf.Abs(result + goalValue) <= 0.5f))
        {
            // Place cards in slots
            cardSlot1.GetChild(0).SetParent(cardSlot1);
            cardSlot2.GetChild(0).SetParent(cardSlot2);
            cardSlot3.GetChild(0).SetParent(cardSlot3);
            cardSlot4.GetChild(0).SetParent(cardSlot4);
            return true;
        }

        return false;
    }
    private void UpdateScore(/*int player, */int result)
    {
        //    switch (player)
        //    {
        //        case 1:
        //            player1Score += score;
        //            player1ScoreText.text = "Your Score: " + player1Score;
        //            break;
        //        case 2:
        //            player2Score += score;
        //            player2ScoreText.text = "Player 2 Score: " + player2Score;
        //            break;
        //        case 3:
        //            player3Score += score;
        //            player3ScoreText.text = "Player 3 Score: " + player3Score;
        //            break;
        //        case 4:
        //            player4Score += score;
        //            player4ScoreText.text = "Player 4 Score: " + player4Score;
        //            break;
        //    }
    }
}
