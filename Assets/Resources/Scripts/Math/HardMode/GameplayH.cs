using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class GameplayH : MonoBehaviour
{
    public ButtonH button;
    public DealerH dealer;
    public TMP_Text cardCount;
    public Transform Card1;
    public TMP_Text Card1Text;
    public TMP_Text Operator1;
    public Transform Card2;
    public TMP_Text Card2Text;
    public TMP_Text Operator2;
    public Transform Card3;
    public TMP_Text Card3Text;
    public TMP_Text Operator3;
    public Transform Card4;
    public TMP_Text Card4Text;
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
                Card3Text.text = "Pick";
                Card4Text.text = "Pick";
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
                if (Card3.childCount > 0)
                {
                    Card3Text.text = $"{Card3.GetChild(0).GetComponent<Card>().GetCardValue}";
                }
                else
                {
                    Card3Text.text = "";
                }
                if (Card4.childCount > 0)
                {
                    Card4Text.text = $"{Card4.GetChild(0).GetComponent<Card>().GetCardValue}";
                }
                else
                {
                    Card4Text.text = "";
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
            Card3Text.text = "";
            Card4Text.text = "";
            goalValue.text = "";
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
            if (Card1.childCount == 0 || Card2.childCount == 0 || Card3.childCount == 0 || Card4.childCount == 0)
            {
                yield break;
            }
            float result = 0f;
            float goal = (float)dealer.cardGoal.GetChild(0).GetComponent<Card>().GetCardValue;
            float value1 = (float)Card1.GetChild(0).GetComponent<Card>().GetCardValue;
            float value2 = (float)Card2.GetChild(0).GetComponent<Card>().GetCardValue;
            float value3 = (float)Card3.GetChild(0).GetComponent<Card>().GetCardValue;
            float value4 = (float)Card4.GetChild(0).GetComponent<Card>().GetCardValue;
            int score = (int)value1 + (int)value2 + (int)value3 + (int)value4;
            switch (Operator1.text)
            {
                case "+":
                    switch (Operator2.text)
                    {
                        case "+": 
                            switch (Operator3.text)
                            {
                                case "+": result = value1 + value2 + value3 + value4; break;
                                case "-": result = value1 + value2 + value3 - value4; break;
                                case "x": result = value1 + value2 + value3 * value4; break;
                                case "¡Â": result = value1 + value2 + value3 / value4; break;
                                case "%": result = (value3 >= value4) ? value1 + value2 + value3 % value4 : -999; break;
                                case "^": result = (value3 != 1) ? value1 + value2 + Mathf.Pow(value3, value4) : -999; break;
                                case "¡Ì": result = (value3 != 1) ? value1 + value2 + Mathf.Pow(value3, 1.0f / value4) : -999; break;
                                case "=": result = (value1 + value2 + value3 == value4) ? value4 : -999; break;
                            }
                            break;
                        case "-":
                            switch (Operator3.text)
                            {
                                case "+": result = value1 + value2 - value3 + value4; break;
                                case "-": result = value1 + value2 - value3 - value4; break;
                                case "x": result = value1 + value2 - value3 * value4; break;
                                case "¡Â": result = value1 + value2 - value3 / value4; break;
                                case "%": result = (value3 >= value4) ? value1 + value2 - value3 % value4 : -999; break;
                                case "^": result = (value3 != 1) ? value1 + value2 - Mathf.Pow(value3, value4) : -999; break;
                                case "¡Ì": result = (value3 != 1) ? value1 + value2 - Mathf.Pow(value3, 1.0f / value4) : -999; break;
                                case "=": result = (value1 + value2 - value3 == value4) ? value4 : -999; break;
                            }
                            break;
                        case "x":
                            switch (Operator3.text)
                            {
                                case "+": result = value1 + value2 * value3 + value4; break;
                                case "-": result = value1 + value2 * value3 - value4; break;
                                case "x": result = value1 + value2 * value3 * value4; break;
                                case "¡Â": result = value1 + value2 * value3 / value4; break;
                                case "%": result = (value2 * value3 >= value4) ? value1 + value2 * value3 % value4 : -999; break;
                                case "^": result = (value3 != 1) ? value1 + value2 * Mathf.Pow(value3, value4) : -999; break;
                                case "¡Ì": result = (value3 != 1) ? value1 + value2 * Mathf.Pow(value3, 1.0f / value4) : -999; break;
                                case "=": result = (value1 + value2 * value3 == value4) ? value4 : -999; break;
                            }
                            break;
                        case "¡Â":
                            switch (Operator3.text)
                            {
                                case "+": result = value1 + value2 / value3 + value4; break;
                                case "-": result = value1 + value2 / value3 - value4; break;
                                case "x": result = value1 + value2 / value3 * value4; break;
                                case "¡Â": result = value1 + value2 / value3 / value4; break;
                                case "%": result = (value2 / value3 >= value4) ? value1 + value2 / value3 % value4 : -999; break;
                                case "^": result = (value3 != 1) ? value1 + value2 / Mathf.Pow(value3, value4) : -999; break;
                                case "¡Ì": result = (value3 !=1) ? value1 + value2 / Mathf.Pow(value3, 1.0f / value4) : -999; break;
                                case "=": result = (value1 + value2 / value3 == value4) ? value4 : -999; break;
                            }
                            break;
                        case "%":
                            switch (Operator3.text)
                            {
                                case "+": result = (value2 >= value3) ? value1 + value2 % value3 + value4 : -999; break;
                                case "-": result = (value2 >= value3) ? value1 + value2 % value3 - value4 : -999; break;
                                case "x": result = (value2 >= value3) ? value1 + value2 % value3 * value4 : -999; break;
                                case "¡Â": result = (value2 >= value3) ? value1 + value2 % value3 / value4 : -999; break;
                                case "%": result = (value2 >= value3 && value2 % value3 >= value4) ? value1 + value2 % value3 % value4 : -999; break;
                                case "^": result = (value2 >= Mathf.Pow(value3, value4) && value3 != 1) ? value1 + value2 % Mathf.Pow(value3, value4) : -999; break;
                                case "¡Ì": result = (value2 >= Mathf.Pow(value3, 1.0f / value4) && value3 != 1) ? value1 + value2 % Mathf.Pow(value3, 1.0f / value4) : -999; break;
                                case "=": result = (value2 >= value3 && value1 + value2 % value3 == value4) ? value4 : -999; break;
                            }
                            break;
                        case "^":
                            switch (Operator3.text)
                            {
                                case "+": result = (value2 != 1) ? value1 + Mathf.Pow(value2, value3) + value4 : -999; break;
                                case "-": result = (value2 != 1) ? value1 + Mathf.Pow(value2, value3) - value4 : -999; break;
                                case "x": result = (value2 != 1) ? value1 + Mathf.Pow(value2, value3) * value4 : -999; break;
                                case "¡Â": result = (value2 != 1) ? value1 + Mathf.Pow(value2, value3) / value4 : -999; break;
                                case "%": result = (value2 != 1 && Mathf.Pow(value2, value3) >= value4) ? value1 + Mathf.Pow(value2, value3) % value4 : -999; break;
                                case "^": result = (value2 != 1) ? value1 + Mathf.Pow(Mathf.Pow(value2, value3), value4) : -999; break;
                                case "¡Ì": result = (value2 != 1) ? value1 + Mathf.Pow(Mathf.Pow(value2, 1.0f / value3), 1.0f / value4) : -999; break;
                                case "=": result = (value2 != 1 && value1 + Mathf.Pow(value2, value3) == value4) ? value4 : -999; break;
                            }
                            break;
                        case "¡Ì":
                            switch (Operator3.text)
                            {
                                case "+": result = (value2 != 1) ? value1 + Mathf.Pow(value2, 1.0f / value3) + value4 : -999; break;
                                case "-": result = (value2 != 1) ? value1 + Mathf.Pow(value2, 1.0f / value3) - value4 : -999; break;
                                case "x": result = (value2 != 1) ? value1 + Mathf.Pow(value2, 1.0f / value3) * value4 : -999; break;
                                case "¡Â": result = (value2 != 1) ? value1 + Mathf.Pow(value2, 1.0f / value3) / value4 : -999; break;
                                case "%": result = (value2 != 1 && Mathf.Pow(value2, 1.0f / value3) >= value4) ? value1 + Mathf.Pow(value2, 1.0f / value3) % value4 : -999; break;
                                case "^": result = (value2 != 1) ? value1 + Mathf.Pow(Mathf.Pow(value2, 1.0f / value3), value4) : -999; break;
                                case "¡Ì": result = (value2 != 1) ? value1 + Mathf.Pow(Mathf.Pow(value2, 1.0f / value3), 1.0f / value4) : -999; break;
                                case "=": result = (value2 != 1 && value1 + Mathf.Pow(value2, 1.0f / value3) == value4) ? value4 : -999; break;
                            }
                            break;
                        case "=":
                            switch (Operator3.text)
                            {
                                case "+": result = (value1 + value2 == value3 + value4) ? value1 + value2 : -999; break;
                                case "-": result = (value1 + value2 == value3 - value4) ? value1 + value2 : -999; break;
                                case "x": result = (value1 + value2 == value3 * value4) ? value1 + value2 : -999; break;
                                case "¡Â": result = (value1 + value2 == value3 / value4) ? value1 + value2 : -999; break;
                                case "%": result = (value1 + value2 == value3 % value4 && value3 >= value4) ? value1 + value2 : -999; break;
                                case "^": result = (value1 + value2 == Mathf.Pow(value3, value4) && value3 != 1) ? value1 + value2 : -999; break;
                                case "¡Ì": result = (value1 + value2 == Mathf.Pow(value3, 1.0f / value4) && value3 != 1) ? value1 + value2 : -999; break;
                                case "=": result = (value1 + value2 == value3 && value3 == value4) ? value4 : -999; break;
                            }
                            break;
                    }
                    break;
                case "-":
                    switch (Operator2.text)
                    {
                        case "+":
                            switch (Operator3.text)
                            {
                                case "+": result = value1 - value2 + value3 + value4; break;
                                case "-": result = value1 - value2 + value3 - value4; break;
                                case "x": result = value1 - value2 + value3 * value4; break;
                                case "¡Â": result = value1 - value2 + value3 / value4; break;
                                case "%": result = (value3 >= value4) ? value1 - value2 + value3 % value4 : -999; break;
                                case "^": result = (value3 != 1) ? value1 - value2 + Mathf.Pow(value3, value4) : -999; break;
                                case "¡Ì": result = (value3 != 1) ? value1 - value2 + Mathf.Pow(value3, 1.0f / value4) : -999; break;
                                case "=": result = (value1 - value2 + value3 == value4) ? value4 : -999; break;
                            }
                            break;
                        case "-":
                            switch (Operator3.text)
                            {
                                case "+": result = value1 - value2 - value3 + value4; break;
                                case "-": result = value1 - value2 - value3 - value4; break;
                                case "x": result = value1 - value2 - value3 * value4; break;
                                case "¡Â": result = value1 - value2 - value3 / value4; break;
                                case "%": result = (value3 >= value4) ? value1 - value2 - value3 % value4 : -999; break;
                                case "^": result = (value3 != 1) ? value1 - value2 - Mathf.Pow(value3, value4) : -999; break;
                                case "¡Ì": result = (value3 != 1) ? value1 - value2 - Mathf.Pow(value3, 1.0f / value4) : -999; break;
                                case "=": result = (value1 - value2 - value3 == value4) ? value4 : -999; break;
                            }
                            break;
                        case "x":
                            switch (Operator3.text)
                            {
                                case "+": result = value1 - value2 * value3 + value4; break;
                                case "-": result = value1 - value2 * value3 - value4; break;
                                case "x": result = value1 - value2 * value3 * value4; break;
                                case "¡Â": result = value1 - value2 * value3 / value4; break;
                                case "%": result = (value2 * value3 >= value4) ? value1 - value2 * value3 % value4 : -999; break;
                                case "^": result = (value3 != 1) ? value1 - value2 * Mathf.Pow(value3, value4) : -999; break;
                                case "¡Ì": result = (value3 != 1) ? value1 - value2 * Mathf.Pow(value3, 1.0f / value4) : -999; break;
                                case "=": result = (value1 - value2 * value3 == value4) ? value4 : -999; break;
                            }
                            break;
                        case "¡Â":
                            switch (Operator3.text)
                            {
                                case "+": result = value1 - value2 / value3 + value4; break;
                                case "-": result = value1 - value2 / value3 - value4; break;
                                case "x": result = value1 - value2 / value3 * value4; break;
                                case "¡Â": result = value1 - value2 / value3 / value4; break;
                                case "%": result = (value2 / value3 >= value4) ? value1 - value2 / value3 % value4 : -999; break;
                                case "^": result = (value3 != 1) ? value1 - value2 / Mathf.Pow(value3, value4) : -999; break;
                                case "¡Ì": result = (value3 != 1) ? value1 - value2 / Mathf.Pow(value3, 1.0f / value4) : -999; break;
                                case "=": result = (value1 - value2 / value3 == value4) ? value4 : -999; break;
                            }
                            break;
                        case "%":
                            switch (Operator3.text)
                            {
                                case "+": result = (value2 >= value3) ? value1 - value2 % value3 + value4 : -999; break;
                                case "-": result = (value2 >= value3) ? value1 - value2 % value3 - value4 : -999; break;
                                case "x": result = (value2 >= value3) ? value1 - value2 % value3 * value4 : -999; break;
                                case "¡Â": result = (value2 >= value3) ? value1 - value2 % value3 / value4 : -999; break;
                                case "%": result = (value2 >= value3 && value2 % value3 >= value4) ? value1 - value2 % value3 % value4 : -999; break;
                                case "^": result = (value2 >= Mathf.Pow(value3, value4) && value3 != 1) ? value1 - value2 % Mathf.Pow(value3, value4) : -999; break;
                                case "¡Ì": result = (value2 >= Mathf.Pow(value3, 1.0f / value4) && value3 != 1) ? value1 - value2 % Mathf.Pow(value3, 1.0f / value4) : -999; break;
                                case "=": result = (value2 >= value3 && value1 - value2 % value3 == value4) ? value4 : -999; break;
                            }
                            break;
                        case "^":
                            switch (Operator3.text)
                            {
                                case "+": result = (value2 != 1) ? value1 - Mathf.Pow(value2, value3) + value4 : -999; break;
                                case "-": result = (value2 != 1) ? value1 - Mathf.Pow(value2, value3) - value4 : -999; break;
                                case "x": result = (value2 != 1) ? value1 - Mathf.Pow(value2, value3) * value4 : -999; break;
                                case "¡Â": result = (value2 != 1) ? value1 - Mathf.Pow(value2, value3) / value4 : -999; break;
                                case "%": result = (value2 != 1 && Mathf.Pow(value2, value3) >= value4) ? value1 - Mathf.Pow(value2, value3) % value4: -999; break;
                                case "^": result = (value2 != 1) ? value1 - Mathf.Pow(Mathf.Pow(value2, value3), value4) : -999; break;
                                case "¡Ì": result = (value2 != 1) ? value1 - Mathf.Pow(Mathf.Pow(value2, 1.0f / value3), 1.0f / value4) : -999; break;
                                case "=": result = (value2 != 1 && value1 - Mathf.Pow(value2, value3) == value4) ? value4 : -999; break;
                            }
                            break;
                        case "¡Ì":
                            switch (Operator3.text)
                            {
                                case "+": result = (value2 != 1) ? value1 - Mathf.Pow(value2, 1.0f / value3) + value4 : -999; break;
                                case "-": result = (value2 != 1) ? value1 - Mathf.Pow(value2, 1.0f / value3) - value4 : -999; break;
                                case "x": result = (value2 != 1) ? value1 - Mathf.Pow(value2, 1.0f / value3) * value4 : -999; break;
                                case "¡Â": result = (value2 != 1) ? value1 - Mathf.Pow(value2, 1.0f / value3) / value4 : -999; break;
                                case "%": result = (value2 != 1 && Mathf.Pow(value2, 1.0f / value3) >= value4) ? value1 - Mathf.Pow(value2, 1.0f / value3) % value4 : -999; break;
                                case "^": result = (value2 != 1) ? value1 - Mathf.Pow(Mathf.Pow(value2, 1.0f / value3), value4) : -999; break;
                                case "¡Ì": result = (value2 != 1) ? value1 - Mathf.Pow(Mathf.Pow(value2, 1.0f / value3), 1.0f / value4) : -999; break;
                                case "=": result = (value2 != 1 && value1 - Mathf.Pow(value2, 1.0f / value3) == value4) ? value4 : -999; break;
                            }
                            break;
                        case "=":
                            switch (Operator3.text)
                            {
                                case "+": result = (value1 - value2 == value3 + value4) ? value1 - value2 : -999; break;
                                case "-": result = (value1 - value2 == value3 - value4) ? value1 - value2 : -999; break;
                                case "x": result = (value1 - value2 == value3 * value4) ? value1 - value2 : -999; break;
                                case "¡Â": result = (value1 - value2 == value3 / value4) ? value1 - value2 : -999; break;
                                case "%": result = (value1 - value2 == value3 % value4 && value3 >= value4) ? value1 - value2 : -999; break;
                                case "^": result = (value1 - value2 == Mathf.Pow(value3, value4) && value3 != 1) ? value1 - value2 : -999; break;
                                case "¡Ì": result = (value1 - value2 == Mathf.Pow(value3, 1.0f / value4) && value3 != 1) ? value1 - value2 : -999; break;
                                case "=": result = (value1 - value2 == value3 && value3 == value4) ? value4 : -999; break;
                            }
                            break;
                    }
                    break;
                case "x":
                    switch (Operator2.text)
                    {
                        case "+":
                            switch (Operator3.text)
                            {
                                case "+": result = value1 * value2 + value3 + value4; break;
                                case "-": result = value1 * value2 + value3 - value4; break;
                                case "x": result = value1 * value2 + value3 * value4; break;
                                case "¡Â": result = value1 * value2 + value3 / value4; break;
                                case "%": result = (value3 >= value4) ? value1 * value2 + value3 % value4 : -999; break;
                                case "^": result = (value3 != 1) ? value1 * value2 + Mathf.Pow(value3, value4) : -999; break;
                                case "¡Ì": result = (value3 != 1) ? value1 * value2 + Mathf.Pow(value3, 1.0f / value4) : -999; break;
                                case "=": result = (value1 * value2 + value3 == value4) ? value4 : -999; break;
                            }
                            break;
                        case "-":
                            switch (Operator3.text)
                            {
                                case "+": result = value1 * value2 - value3 + value4; break;
                                case "-": result = value1 * value2 - value3 - value4; break;
                                case "x": result = value1 * value2 - value3 * value4; break;
                                case "¡Â": result = value1 * value2 - value3 / value4; break;
                                case "%": result = (value3 >= value4) ? value1 * value2 - value3 % value4 : -999; break;
                                case "^": result = (value3 != 1) ? value1 * value2 - Mathf.Pow(value3, value4) : -999; break;
                                case "¡Ì": result = (value3 != 1) ? value1 * value2 - Mathf.Pow(value3, 1.0f / value4) : -999; break;
                                case "=": result = (value1 * value2 - value3 == value4) ? value4 : -999; break;
                            }
                            break;
                        case "x":
                            switch (Operator3.text)
                            {
                                case "+": result = value1 * value2 * value3 + value4; break;
                                case "-": result = value1 * value2 * value3 - value4; break;
                                case "x": result = value1 * value2 * value3 * value4; break;
                                case "¡Â": result = value1 * value2 * value3 / value4; break;
                                case "%": result = (value1 * value2 * value3 >= value4) ? value1 * value2 * value3 % value4 : -999; break;
                                case "^": result = (value3 != 1) ? value1 * value2 * Mathf.Pow(value3, value4) : -999; break;
                                case "¡Ì": result = (value3 != 1) ? value1 * value2 * Mathf.Pow(value3, 1.0f / value4) : -999; break;
                                case "=": result = (value1 * value2 * value3 == value4) ? value4 : -999; break;
                            }
                            break;
                        case "¡Â":
                            switch (Operator3.text)
                            {
                                case "+": result = value1 * value2 / value3 + value4; break;
                                case "-": result = value1 * value2 / value3 - value4; break;
                                case "x": result = value1 * value2 / value3 * value4; break;
                                case "¡Â": result = value1 * value2 / value3 / value4; break;
                                case "%": result = (value1 * value2 / value3 >= value4) ? value1 * value2 / value3 % value4 : -999; break;
                                case "^": result = (value3 != 1) ? value1 * value2 / Mathf.Pow(value3, value4) : -999; break;
                                case "¡Ì": result = (value3 != 1) ? value1 * value2 / Mathf.Pow(value3, 1.0f / value4) : -999; break;
                                case "=": result = (value1 * value2 / value3 == value4) ? value4 : -999; break;
                            }
                            break;
                        case "%":
                            switch (Operator3.text)
                            {
                                case "+": result = (value1 * value2 >= value3) ? value1 * value2 % value3 + value4 : -999; break;
                                case "-": result = (value1 * value2 >= value3) ? value1 * value2 % value3 - value4 : -999; break;
                                case "x": result = (value1 * value2 >= value3) ? value1 * value2 % value3 * value4 : -999; break;
                                case "¡Â": result = (value1 * value2 >= value3) ? value1 * value2 % value3 / value4 : -999; break;
                                case "%": result = (value1 * value2 >= value3 && value1 * value2 % value3 >= value4) ? value1 * value2 % value3 % value4 : -999; break;
                                case "^": result = (value1 * value2 >= Mathf.Pow(value3, value4) && value3 != 1) ? value1 * value2 % Mathf.Pow(value3, value4) : -999; break;
                                case "¡Ì": result = (value1 * value2 >= Mathf.Pow(value3, 1.0f / value4) && value3 != 1) ? value1 * value2 % Mathf.Pow(value3, 1.0f / value4) : -999; break;
                                case "=": result = (value1 * value2 >= value3 && value1 * value2 % value3 == value4) ? value4 : -999; break;
                            }
                            break;
                        case "^":
                            switch (Operator3.text)
                            {
                                case "+": result = (value2 != 1) ? value1 * Mathf.Pow(value2, value3) + value4 : -999; break;
                                case "-": result = (value2 != 1) ? value1 * Mathf.Pow(value2, value3) - value4 : -999; break;
                                case "x": result = (value2 != 1) ? value1 * Mathf.Pow(value2, value3) * value4 : -999; break;
                                case "¡Â": result = (value2 != 1) ? value1 * Mathf.Pow(value2, value3) / value4 : -999; break;
                                case "%": result = (value2 != 1 && value1 * Mathf.Pow(value2, value3) >= value4) ? value1 * Mathf.Pow(value2, value3) % value4 : -999; break;
                                case "^": result = (value2 != 1) ? value1 * Mathf.Pow(Mathf.Pow(value2, value3), value4) : -999; break;
                                case "¡Ì": result = (value2 != 1) ? value1 * Mathf.Pow(Mathf.Pow(value2, value3), 1.0f / value4) : -999; break;
                                case "=": result = (value2 != 1 && value1 * Mathf.Pow(value2, value3) == value4) ? value4 : -999; break;
                            }
                            break;
                        case "¡Ì":
                            switch (Operator3.text)
                            {
                                case "+": result = (value2 != 1) ? value1 * Mathf.Pow(value2, 1.0f/ value3) + value4 : -999; break;
                                case "-": result = (value2 != 1) ? value1 * Mathf.Pow(value2, 1.0f / value3) - value4 : -999; break;
                                case "x": result = (value2 != 1) ? value1 * Mathf.Pow(value2, 1.0f / value3) * value4 : -999; break;
                                case "¡Â": result = (value2 != 1) ? value1 * Mathf.Pow(value2, 1.0f / value3) / value4 : -999; break;
                                case "%": result = (value2 != 1 && value1 * Mathf.Pow(value2, 1.0f / value3) >= value4) ? value1 * Mathf.Pow(value2, 1.0f / value3) % value4 : -999; break;
                                case "^": result = (value2 != 1) ? value1 * Mathf.Pow(Mathf.Pow(value2, 1.0f / value3), value4) : -999; break;
                                case "¡Ì": result = (value2 != 1) ? value1 * Mathf.Pow(Mathf.Pow(value2, 1.0f / value3), 1.0f / value4) : -999; break;
                                case "=": result = (value2 != 1 && value1 * Mathf.Pow(value2, 1.0f / value3) == value4) ? value4 : -999; break;
                            }
                            break;
                        case "=":
                            switch (Operator3.text)
                            {
                                case "+": result = (value1 * value2 == value3 + value4) ? value1 * value2 : -999; break;
                                case "-": result = (value1 * value2 == value3 - value4) ? value1 * value2 : -999; break;
                                case "x": result = (value1 * value2 == value3 * value4) ? value1 * value2 : -999; break;
                                case "¡Â": result = (value1 * value2 == value3 / value4) ? value1 * value2 : -999; break;
                                case "%": result = (value1 * value2 == value3 % value4 && value3 >= value4) ? value1 * value2 : -999; break;
                                case "^": result = (value1 * value2 == Mathf.Pow(value3, value4) && value3 != 1) ? value1 * value2 : -999; break;
                                case "¡Ì": result = (value1 * value2 == Mathf.Pow(value3, 1.0f / value4) && value3 != 1) ? value1 * value2 : -999; break;
                                case "=": result = (value1 * value2 == value3 && value3 == value4) ? value4 : -999; break;
                            }
                            break;
                    }
                    break;
                case "¡Â":
                    switch (Operator2.text)
                    {
                        case "+":
                            switch (Operator3.text)
                            {
                                case "+": result = value1 / value2 + value3 + value4; break;
                                case "-": result = value1 / value2 + value3 - value4; break;
                                case "x": result = value1 / value2 + value3 * value4; break;
                                case "¡Â": result = value1 / value2 + value3 / value4; break;
                                case "%": result = (value3 >= value4) ? value1 / value2 + value3 % value4 : -999; break;
                                case "^": result = (value3 != 1) ? value1 / value2 + Mathf.Pow(value3, value4) : -999; break;
                                case "¡Ì": result = (value3 != 1) ? value1 / value2 + Mathf.Pow(value3, 1.0f / value4) : -999; break;
                                case "=": result = (value1 / value2 + value3 == value4) ? value4 : -999; break;
                            }
                            break;
                        case "-":
                            switch (Operator3.text)
                            {
                                case "+": result = value1 / value2 - value3 + value4; break;
                                case "-": result = value1 / value2 - value3 - value4; break;
                                case "x": result = value1 / value2 - value3 * value4; break;
                                case "¡Â": result = value1 / value2 - value3 / value4; break;
                                case "%": result = (value3 >= value4) ? value1 / value2 - value3 % value4 : -999; break;
                                case "^": result = (value3 != 1) ? value1 / value2 - Mathf.Pow(value3, value4) : -999; break;
                                case "¡Ì": result = (value3 != 1) ? value1 / value2 - Mathf.Pow(value3, 1.0f / value4) : -999; break;
                                case "=": result = (value1 / value2 - value3 == value4) ? value4 : -999; break;
                            }
                            break;
                        case "x":
                            switch (Operator3.text)
                            {
                                case "+": result = value1 / value2 * value3 + value4; break;
                                case "-": result = value1 / value2 * value3 - value4; break;
                                case "x": result = value1 / value2 * value3 * value4; break;
                                case "¡Â": result = value1 / value2 * value3 / value4; break;
                                case "%": result = (value1 / value2 * value3 >= value4) ? value1 / value2 * value3 % value4 : -999; break;
                                case "^": result = (value3 != 1) ? value1 / value2 * Mathf.Pow(value3, value4) : -999; break;
                                case "¡Ì": result = (value3 != 1) ? value1 / value2 * Mathf.Pow(value3, 1.0f / value4) : -999; break;
                                case "=": result = (value1 / value2 * value3 == value4) ? value4 : -999; break;
                            }
                            break;
                        case "¡Â":
                            switch (Operator3.text)
                            {
                                case "+": result = value1 / value2 / value3 + value4; break;
                                case "-": result = value1 / value2 / value3 - value4; break;
                                case "x": result = value1 / value2 / value3 * value4; break;
                                case "¡Â": result = value1 / value2 / value3 / value4; break;
                                case "%": result = (value1 / value2 / value3 >= value4) ? value1 / value2 / value3 % value4 : -999; break;
                                case "^": result = (value3 != 1) ? value1 / value2 / Mathf.Pow(value3, value4) : -999; break;
                                case "¡Ì": result = (value3 != 1) ? value1 / value2 / Mathf.Pow(value3, 1.0f / value4) : -999; break;
                                case "=": result = (value1 / value2 / value3 == value4) ? value4 : -999; break;
                            }
                            break;
                        case "%":
                            switch (Operator3.text)
                            {
                                case "+": result = (value1 / value2 >= value3) ? value1 / value2 % value3 + value4 : -999; break;
                                case "-": result = (value1 / value2 >= value3) ? value1 / value2 % value3 - value4 : -999; break;
                                case "x": result = (value1 / value2 >= value3) ? value1 / value2 % value3 * value4 : -999; break;
                                case "¡Â": result = (value1 / value2 >= value3) ? value1 / value2 % value3 / value4 : -999; break;
                                case "%": result = (value1 / value2 >= value3 && value1 / value2 % value3 >= value4) ? value1 / value2 % value3 % value4 : -999; break;
                                case "^": result = (value1 / value2 >= Mathf.Pow(value3, value4) && value3 != 1) ? value1 / value2 % Mathf.Pow(value3, value4) : -999; break;
                                case "¡Ì": result = (value1 / value2 >= Mathf.Pow(value3, 1.0f / value4) && value3 != 1) ? value1 / value2 % Mathf.Pow(value3, 1.0f / value4) : -999; break;
                                case "=": result = (value1 / value2 >= value3 && value1 / value2 % value3 == value4) ? value4 : -999; break;
                            }
                            break;
                        case "^":
                            switch (Operator3.text)
                            {
                                case "+": result = (value2 != 1) ? value1 / Mathf.Pow(value2, value3) + value4 : -999; break;
                                case "-": result = (value2 != 1) ? value1 / Mathf.Pow(value2, value3) - value4 : -999; break;
                                case "x": result = (value2 != 1) ? value1 / Mathf.Pow(value2, value3) * value4 : -999; break;
                                case "¡Â": result = (value2 != 1) ? value1 / Mathf.Pow(value2, value3) / value4 : -999; break;
                                case "%": result = (value2 != 1 && value1 / Mathf.Pow(value2, value3) >= value4) ? value1 / Mathf.Pow(value2, value3) % value4 : -999; break;
                                case "^": result = (value2 != 1) ? value1 / Mathf.Pow(Mathf.Pow(value2, value3), value4) : -999; break;
                                case "¡Ì": result = (value2 != 1) ? value1 / Mathf.Pow(Mathf.Pow(value2, value3), 1.0f / value4) : -999; break;
                                case "=": result = (value2 != 1 && value1 / Mathf.Pow(value2, value3) == value4) ? value4 : -999; break;
                            }
                            break;
                        case "¡Ì":
                            switch (Operator3.text)
                            {
                                case "+": result = (value2 != 1) ? value1 / Mathf.Pow(value2, 1.0f / value3) + value4 : -999; break;
                                case "-": result = (value2 != 1) ? value1 / Mathf.Pow(value2, 1.0f / value3) - value4 : -999; break;
                                case "x": result = (value2 != 1) ? value1 / Mathf.Pow(value2, 1.0f / value3) * value4 : -999; break;
                                case "¡Â": result = (value2 != 1) ? value1 / Mathf.Pow(value2, 1.0f / value3) / value4 : -999; break;
                                case "%": result = (value2 != 1 && value1 / Mathf.Pow(value2, 1.0f / value3) >= value4) ? value1 / Mathf.Pow(value2, 1.0f / value3) % value4 : -999; break;
                                case "^": result = (value2 != 1) ? value1 / Mathf.Pow(Mathf.Pow(value2, 1.0f / value3), value4) : -999; break;
                                case "¡Ì": result = (value2 != 1) ? value1 / Mathf.Pow(Mathf.Pow(value2, 1.0f / value3), 1.0f / value4) : -999; break;
                                case "=": result = (value2 != 1 && value1 / Mathf.Pow(value2, 1.0f / value3) == value4) ? value4 : -999; break;
                            }
                            break;
                        case "=":
                            switch (Operator3.text)
                            {
                                case "+": result = (value1 / value2 == value3 + value4) ? value1 / value2 : -999; break;
                                case "-": result = (value1 / value2 == value3 - value4) ? value1 / value2 : -999; break;
                                case "x": result = (value1 / value2 == value3 * value4) ? value1 / value2 : -999; break;
                                case "¡Â": result = (value1 / value2 == value3 / value4) ? value1 / value2 : -999; break;
                                case "%": result = (value1 / value2 == value3 % value4 && value3 >= value4) ? value1 / value2 : -999; break;
                                case "^": result = (value1 / value2 == Mathf.Pow(value3, value4) && value3 != 1) ? value1 / value2 : -999; break;
                                case "¡Ì": result = (value1 / value2 == Mathf.Pow(value3, 1.0f / value4) && value3 != 1) ? value1 / value2 : -999; break;
                                case "=": result = (value1 / value2 == value3 && value3 == value4) ? value4 : -999; break;
                            }
                            break;
                    }
                    break;
                case "%":
                    switch (Operator2.text)
                    {
                        case "+":
                            switch (Operator3.text)
                            {
                                case "+": result = (value1 >= value2) ? value1 % value2 + value3 + value4 : -999; break;
                                case "-": result = (value1 >= value2) ? value1 % value2 + value3 - value4 : -999; break;
                                case "x": result = (value1 >= value2) ? value1 % value2 + value3 * value4 : -999; break;
                                case "¡Â": result = (value1 >= value2) ? value1 % value2 + value3 / value4 : -999; break;
                                case "%": result = (value1 >= value2 && value3 >= value4) ? value1 % value2 + value3 % value4 : -999; break;
                                case "^": result = (value1 >= value2 && value3 != 1) ? value1 % value2 + Mathf.Pow(value3, value4) : -999; break;
                                case "¡Ì": result = (value1 >= value2 && value3 != 1) ? value1 % value2 + Mathf.Pow(value3, 1.0f / value4) : -999; break;
                                case "=": result = (value1 >= value2 && value1 % value2 + value3 == value4) ? value4 : -999; break;
                            }
                            break;
                        case "-":
                            switch (Operator3.text)
                            {
                                case "+": result = (value1 >= value2) ? value1 % value2 - value3 + value4 : -999; break;
                                case "-": result = (value1 >= value2) ? value1 % value2 - value3 - value4 : -999; break;
                                case "x": result = (value1 >= value2) ? value1 % value2 - value3 * value4 : -999; break;
                                case "¡Â": result = (value1 >= value2) ? value1 % value2 - value3 / value4 : -999; break;
                                case "%": result = (value1 >= value2 && value3 >= value4) ? value1 % value2 - value3 % value4 : -999; break;
                                case "^": result = (value1 >= value2 && value3 != 1) ? value1 % value2 - Mathf.Pow(value3, value4) : -999; break;
                                case "¡Ì": result = (value1 >= value2 && value3 != 1) ? value1 % value2 - Mathf.Pow(value3, 1.0f / value4) : -999; break;
                                case "=": result = (value1 >= value2 && value1 % value2 - value3 == value4) ? value4 : -999; break;
                            }
                            break;
                        case "x":
                            switch (Operator3.text)
                            {
                                case "+": result = (value1 >= value2) ? value1 % value2 * value3 + value4 : -999; break;
                                case "-": result = (value1 >= value2) ? value1 % value2 * value3 - value4 : -999; break;
                                case "x": result = (value1 >= value2) ? value1 % value2 * value3 * value4 : -999; break;
                                case "¡Â": result = (value1 >= value2) ? value1 % value2 * value3 / value4 : -999; break;
                                case "%": result = (value1 >= value2 && value1 % value2 * value3 >= value4) ? value1 % value2 * value3 % value4 : -999; break;
                                case "^": result = (value1 >= value2 && value3 != 1) ? value1 % value2 * Mathf.Pow(value3, value4) : -999; break;
                                case "¡Ì": result = (value1 >= value2 && value3 != 1) ? value1 % value2 * Mathf.Pow(value3, 1.0f / value4) : -999; break;
                                case "=": result = (value1 >= value2 && value1 % value2 * value3 == value4) ? value4 : -999; break;
                            }
                            break;
                        case "¡Â":
                            switch (Operator3.text)
                            {
                                case "+": result = (value1 >= value2) ? value1 % value2 / value3 + value4 : -999; break;
                                case "-": result = (value1 >= value2) ? value1 % value2 / value3 - value4 : -999; break;
                                case "x": result = (value1 >= value2) ? value1 % value2 / value3 * value4 : -999; break;
                                case "¡Â": result = (value1 >= value2) ? value1 % value2 / value3 / value4 : -999; break;
                                case "%": result = (value1 >= value2 && value1 % value2 / value3 >= value4) ? value1 % value2 / value3 % value4 : -999; break;
                                case "^": result = (value1 >= value2 && value3 != 1) ? value1 % value2 / Mathf.Pow(value3, value4) : -999; break;
                                case "¡Ì": result = (value1 >= value2 && value3 != 1) ? value1 % value2 / Mathf.Pow(value3, 1.0f / value4) : -999; break;
                                case "=": result = (value1 >= value2 && value1 % value2 / value3 == value4) ? value4 : -999; break;
                            }
                            break;
                        case "%":
                            switch (Operator3.text)
                            {
                                case "+": result = (value1 >= value2 && value1 % value2 >= value3) ? value1 % value2 % value3 + value4 : -999; break;
                                case "-": result = (value1 >= value2 && value1 % value2 >= value3) ? value1 % value2 % value3 - value4 : -999; break;
                                case "x": result = (value1 >= value2 && value1 % value2 >= value3) ? value1 % value2 % value3 * value4 : -999; break;
                                case "¡Â": result = (value1 >= value2 && value1 % value2 >= value3) ? value1 % value2 % value3 / value4 : -999; break;
                                case "%": result = (value1 >= value2 && value1 % value2 >= value3 && value1 % value2 % value3 >= value4) ? value1 % value2 % value3 % value4 : -999; break;
                                case "^": result = (value1 >= value2 && value1 % value2 >= Mathf.Pow(value3, value4) && value3 != 1) ? value1 % value2 % Mathf.Pow(value3, value4) : -999; break;
                                case "¡Ì": result = (value1 >= value2 && value1 % value2 >= Mathf.Pow(value3, 1.0f / value4) && value3 != 1) ? value1 % value2 % Mathf.Pow(value3, 1.0f / value4) : -999; break;
                                case "=": result = (value1 >= value2 && value1 % value2 >= value3 && value1 % value2 % value3 == value4) ? value4 : -999; break;
                            }
                            break;
                        case "^":
                            switch (Operator3.text)
                            {
                                case "+": result = (value1 >= value2 && value2 != 1) ? value1 % Mathf.Pow(value2, value3) + value4 : -999; break;
                                case "-": result = (value1 >= value2 && value2 != 1) ? value1 % Mathf.Pow(value2, value3) - value4 : -999; break;
                                case "x": result = (value1 >= value2 && value2 != 1) ? value1 % Mathf.Pow(value2, value3) * value4 : -999; break;
                                case "¡Â": result = (value1 >= value2 && value2 != 1) ? value1 % Mathf.Pow(value2, value3) / value4 : -999; break;
                                case "%": result = (value1 >= value2 && value2 != 1 && value1 % Mathf.Pow(value2, value3) >= value4) ? value1 % Mathf.Pow(value2, value3) % value4 : -999; break;
                                case "^": result = (value1 >= value2 && value2 != 1) ? value1 % Mathf.Pow(Mathf.Pow(value2, value3), value4) : -999; break;
                                case "¡Ì": result = (value1 >= value2 && value2 != 1) ? value1 % Mathf.Pow(Mathf.Pow(value2, value3), 1.0f / value4) : -999; break;
                                case "=": result = (value1 >= value2 && value2 != 1 && value1 % Mathf.Pow(value2, value3) == value4) ? value4 : -999; break;
                            }
                            break;
                        case "¡Ì":
                            switch (Operator3.text)
                            {
                                case "+": result = (value1 >= value2 && value2 != 1) ? value1 % Mathf.Pow(value2, 1.0f / value3) + value4 : -999; break;
                                case "-": result = (value1 >= value2 && value2 != 1) ? value1 % Mathf.Pow(value2, 1.0f / value3) - value4 : -999; break;
                                case "x": result = (value1 >= value2 && value2 != 1) ? value1 % Mathf.Pow(value2, 1.0f / value3) * value4 : -999; break;
                                case "¡Â": result = (value1 >= value2 && value2 != 1) ? value1 % Mathf.Pow(value2, 1.0f / value3) / value4 : -999; break;
                                case "%": result = (value1 >= value2 && value2 != 1 && value1 % Mathf.Pow(value2, 1.0f / value3) >= value4) ? value1 % Mathf.Pow(value2, 1.0f / value3) % value4 : -999; break;
                                case "^": result = (value1 >= value2 && value2 != 1) ? value1 % Mathf.Pow(Mathf.Pow(value2, 1.0f / value3), value4) : -999; break;
                                case "¡Ì": result = (value1 >= value2 && value2 != 1) ? value1 % Mathf.Pow(Mathf.Pow(value2, 1.0f / value3), 1.0f / value4) : -999; break;
                                case "=": result = (value1 >= value2 && value2 != 1 && value1 % Mathf.Pow(value2, 1.0f / value3) == value4) ? value4 : -999; break;
                            }
                            break;
                        case "=":
                            switch (Operator3.text)
                            {
                                case "+": result = (value1 >= value2 && value1 % value2 == value3 + value4) ? value1 % value2 : -999; break;
                                case "-": result = (value1 >= value2 && value1 % value2 == value3 - value4) ? value1 % value2 : -999; break;
                                case "x": result = (value1 >= value2 && value1 % value2 == value3 * value4) ? value1 % value2 : -999; break;
                                case "¡Â": result = (value1 >= value2 && value1 % value2 == value3 / value4) ? value1 % value2 : -999; break;
                                case "%": result = (value1 >= value2 && value1 % value2 == value3 % value4 && value3 >= value4) ? value1 % value2 : -999; break;
                                case "^": result = (value1 >= value2 && value1 % value2 == Mathf.Pow(value3, value4) && value3 != 1) ? value1 % value2 : -999; break;
                                case "¡Ì": result = (value1 >= value2 && value1 % value2 == Mathf.Pow(value3, 1.0f / value4) && value3 != 1) ? value1 % value2 : -999; break;
                                case "=": result = (value1 >= value2 && value1 % value2 == value3 && value3 == value4) ? value4 : -999; break;
                            }
                            break;
                    }
                    break;
                case "^":
                    switch (Operator2.text)
                    {
                        case "+":
                            switch (Operator3.text)
                            {
                                case "+": result = (value1 != 1) ? Mathf.Pow(value1, value2) + value3 + value4 : -999; break;
                                case "-": result = (value1 != 1) ? Mathf.Pow(value1, value2) + value3 - value4 : -999; break;
                                case "x": result = (value1 != 1) ? Mathf.Pow(value1, value2) + value3 * value4 : -999; break;
                                case "¡Â": result = (value1 != 1) ? Mathf.Pow(value1, value2) + value3 / value4 : -999; break;
                                case "%": result = (value1 != 1 && value3 >= value4) ? Mathf.Pow(value1, value2) + value3 % value4 : -999; break;
                                case "^": result = (value1 != 1 && value3 != 1) ? Mathf.Pow(value1, value2) + Mathf.Pow(value3, value4) : -999; break;
                                case "¡Ì": result = (value1 != 1 && value3 != 1) ? Mathf.Pow(value1, value2) + Mathf.Pow(value3, 1.0f / value4) : -999; break;
                                case "=": result = (value1 != 1 && Mathf.Pow(value1, value2) + value3 == value4) ? value4 : -999; break;
                            }
                            break;
                        case "-":
                            switch (Operator3.text)
                            {
                                case "+": result = (value1 != 1) ? Mathf.Pow(value1, value2) - value3 + value4 : -999; break;
                                case "-": result = (value1 != 1) ? Mathf.Pow(value1, value2) - value3 - value4 : -999; break;
                                case "x": result = (value1 != 1) ? Mathf.Pow(value1, value2) - value3 * value4 : -999; break;
                                case "¡Â": result = (value1 != 1) ? Mathf.Pow(value1, value2) - value3 / value4 : -999; break;
                                case "%": result = (value1 != 1 && value3 >= value4) ? Mathf.Pow(value1, value2) - value3 % value4 : -999; break;
                                case "^": result = (value1 != 1 && value3 != 1) ? Mathf.Pow(value1, value2) - Mathf.Pow(value3, value4) : -999; break;
                                case "¡Ì": result = (value1 != 1 && value3 != 1) ? Mathf.Pow(value1, value2) - Mathf.Pow(value3, 1.0f / value4) : -999; break;
                                case "=": result = (value1 != 1 && Mathf.Pow(value1, value2) - value3 == value4) ? value4 : -999; break;
                            }
                            break;
                        case "x":
                            switch (Operator3.text)
                            {
                                case "+": result = (value1 != 1) ? Mathf.Pow(value1, value2) * value3 + value4 : -999; break;
                                case "-": result = (value1 != 1) ? Mathf.Pow(value1, value2) * value3 - value4 : -999; break;
                                case "x": result = (value1 != 1) ? Mathf.Pow(value1, value2) * value3 * value4 : -999; break;
                                case "¡Â": result = (value1 != 1) ? Mathf.Pow(value1, value2) * value3 / value4 : -999; break;
                                case "%": result = (value1 != 1 && Mathf.Pow(value1, value2) * value3 >= value4) ? Mathf.Pow(value1, value2) * value3 % value4 : -999; break;
                                case "^": result = (value1 != 1 && value3 != 1) ? Mathf.Pow(value1, value2) * Mathf.Pow(value3, value4) : -999; break;
                                case "¡Ì": result = (value1 != 1 && value3 != 1) ? Mathf.Pow(value1, value2) * Mathf.Pow(value3, 1.0f / value4) : -999; break;
                                case "=": result = (value1 != 1 && Mathf.Pow(value1, value2) * value3 == value4) ? value4 : -999; break;
                            }
                            break;
                        case "¡Â":
                            switch (Operator3.text)
                            {
                                case "+": result = (value1 != 1) ? Mathf.Pow(value1, value2) / value3 + value4 : -999; break;
                                case "-": result = (value1 != 1) ? Mathf.Pow(value1, value2) / value3 - value4 : -999; break;
                                case "x": result = (value1 != 1) ? Mathf.Pow(value1, value2) / value3 * value4 : -999; break;
                                case "¡Â": result = (value1 != 1) ? Mathf.Pow(value1, value2) / value3 / value4 : -999; break;
                                case "%": result = (value1 != 1 && Mathf.Pow(value1, value2) / value3 >= value4) ? Mathf.Pow(value1, value2) / value3 % value4 : -999; break;
                                case "^": result = (value1 != 1 && value3 != 1) ? Mathf.Pow(value1, value2) / Mathf.Pow(value3, value4) : -999; break;
                                case "¡Ì": result = (value1 != 1 && value3 != 1) ? Mathf.Pow(value1, value2) / Mathf.Pow(value3, 1.0f / value4) : -999; break;
                                case "=": result = (value1 != 1 && Mathf.Pow(value1, value2) / value3 == value4) ? value4 : -999; break;
                            }
                            break;
                        case "%":
                            switch (Operator3.text)
                            {
                                case "+": result = (value1 != 1 && Mathf.Pow(value1, value2) >= value3) ? Mathf.Pow(value1, value2) % value3 + value4 : -999; break;
                                case "-": result = (value1 != 1 && Mathf.Pow(value1, value2) >= value3) ? Mathf.Pow(value1, value2) % value3 - value4 : -999; break;
                                case "x": result = (value1 != 1 && Mathf.Pow(value1, value2) >= value3) ? Mathf.Pow(value1, value2) % value3 * value4 : -999; break;
                                case "¡Â": result = (value1 != 1 && Mathf.Pow(value1, value2) >= value3) ? Mathf.Pow(value1, value2) % value3 / value4 : -999; break;
                                case "%": result = (value1 != 1 && Mathf.Pow(value1, value2) >= value3 && value2 % Mathf.Pow(value1, value2) % value3 >= value4) ? Mathf.Pow(value1, value2) % value3 % value4 : -999; break;
                                case "^": result = (value1 != 1 && Mathf.Pow(value1, value2) >= Mathf.Pow(value3, value4)) ? Mathf.Pow(value1, value2) % Mathf.Pow(value3, value4) : -999; break;
                                case "¡Ì": result = (value1 != 1 && Mathf.Pow(value1, value2) >= Mathf.Pow(value3, 1.0f / value4)) ? Mathf.Pow(value1, value2) % Mathf.Pow(value3, 1.0f / value4) : -999; break;
                                case "=": result = (value1 != 1 && Mathf.Pow(value1, value2) >= value3 && Mathf.Pow(value1, value2) % value3 == value4) ? value4 : -999; break;
                            }
                            break;
                        case "^":
                            switch (Operator3.text)
                            {
                                case "+": result = (value1 != 1) ? Mathf.Pow(Mathf.Pow(value1, value2), value3) + value4 : -999; break;
                                case "-": result = (value1 != 1) ? Mathf.Pow(Mathf.Pow(value1, value2), value3) - value4 : -999; break;
                                case "x": result = (value1 != 1) ? Mathf.Pow(Mathf.Pow(value1, value2), value3) * value4 : -999; break;
                                case "¡Â": result = (value1 != 1) ? Mathf.Pow(Mathf.Pow(value1, value2), value3) / value4 : -999; break;
                                case "%": result = (value1 != 1 && Mathf.Pow(Mathf.Pow(value1, value2), value3) >= value4) ? Mathf.Pow(Mathf.Pow(value1, value2), value3) % value4 : -999; break;
                                case "^": result = (value1 != 1) ? Mathf.Pow(Mathf.Pow(Mathf.Pow(value1, value2), value3), value4) : -999; break;
                                case "¡Ì": result = (value1 != 1) ? Mathf.Pow(Mathf.Pow(Mathf.Pow(value1, value2), value3), 1.0f / value4) : -999; break;
                                case "=": result = (value1 != 1 && Mathf.Pow(Mathf.Pow(value1, value2), value3) == value4) ? value4 : -999; break;
                            }
                            break;
                        case "¡Ì":
                            switch (Operator3.text)
                            {
                                case "+": result = (value1 != 1) ? Mathf.Pow(Mathf.Pow(value1, value2), 1.0f / value3) + value4 : -999; break;
                                case "-": result = (value1 != 1) ? Mathf.Pow(Mathf.Pow(value1, value2), 1.0f / value3) - value4 : -999; break;
                                case "x": result = (value1 != 1) ? Mathf.Pow(Mathf.Pow(value1, value2), 1.0f / value3) * value4 : -999; break;
                                case "¡Â": result = (value1 != 1) ? Mathf.Pow(Mathf.Pow(value1, value2), 1.0f / value3) / value4 : -999; break;
                                case "%": result = (value1 != 1 && Mathf.Pow(Mathf.Pow(value1, value2), 1.0f / value3) >= value4) ? Mathf.Pow(Mathf.Pow(value1, value2), 1.0f / value3) % value4 : -999; break;
                                case "^": result = (value1 != 1) ? Mathf.Pow(Mathf.Pow(Mathf.Pow(value1, value2), 1.0f / value3), value4) : -999; break;
                                case "¡Ì": result = (value1 != 1) ? Mathf.Pow(Mathf.Pow(Mathf.Pow(value1, value2), 1.0f / value3), 1.0f / value4) : -999; break;
                                case "=": result = (value1 != 1 && Mathf.Pow(Mathf.Pow(value1, value2), 1.0f / value3) == value4) ? value4 : -999; break;
                            }
                            break;
                        case "=":
                            switch (Operator3.text)
                            {
                                case "+": result = (value1 != 1 && Mathf.Pow(value1, value2) == value3 + value4) ? Mathf.Pow(value1, value2) : -999; break;
                                case "-": result = (value1 != 1 && Mathf.Pow(value1, value2) == value3 - value4) ? Mathf.Pow(value1, value2) : -999; break;
                                case "x": result = (value1 != 1 && Mathf.Pow(value1, value2) == value3 * value4) ? Mathf.Pow(value1, value2) : -999; break;
                                case "¡Â": result = (value1 != 1 && Mathf.Pow(value1, value2) == value3 / value4) ? Mathf.Pow(value1, value2) : -999; break;
                                case "%": result = (value1 != 1 && Mathf.Pow(value1, value2) == value3 % value4 && value3 >= value4) ? Mathf.Pow(value1, value2) : -999; break;
                                case "^": result = (value1 != 1 && Mathf.Pow(value1, value2) == Mathf.Pow(value3, value4) && value3 != 1) ? Mathf.Pow(value1, value2) : -999; break;
                                case "¡Ì": result = (value1 != 1 && Mathf.Pow(value1, value2) == Mathf.Pow(value3, 1.0f / value4) && value3 != 1) ? Mathf.Pow(value1, value2) : -999; break;
                                case "=": result = (value1 != 1 && Mathf.Pow(value1, value2) == value3 && value3 == value4) ? value4 : -999; break;
                            }
                            break;
                    }
                    break;
                case "¡Ì":
                    switch (Operator2.text)
                    {
                        case "+":
                            switch (Operator3.text)
                            {
                                case "+": result = (value1 != 1) ? Mathf.Pow(value1, 1.0f / value2) + value3 + value4 : -999; break;
                                case "-": result = (value1 != 1) ? Mathf.Pow(value1, 1.0f / value2) + value3 - value4 : -999; break;
                                case "x": result = (value1 != 1) ? Mathf.Pow(value1, 1.0f / value2) + value3 * value4 : -999; break;
                                case "¡Â": result = (value1 != 1) ? Mathf.Pow(value1, 1.0f / value2) + value3 / value4 : -999; break;
                                case "%": result = (value1 != 1 && value3 >= value4) ? Mathf.Pow(value1, 1.0f / value2) + value3 % value4 : -999; break;
                                case "^": result = (value1 != 1 && value3 != 1) ? Mathf.Pow(value1, 1.0f / value2) + Mathf.Pow(value3, value4) : -999; break;
                                case "¡Ì": result = (value1 != 1 && value3 != 1) ? Mathf.Pow(value1, 1.0f / value2) + Mathf.Pow(value3, 1.0f / value4) : -999; break;
                                case "=": result = (value1 != 1 && Mathf.Pow(value1, 1.0f / value2) + value3 == value4) ? value4 : -999; break;
                            }
                            break;
                        case "-":
                            switch (Operator3.text)
                            {
                                case "+": result = (value1 != 1) ? Mathf.Pow(value1, 1.0f / value2) - value3 + value4 : -999; break;
                                case "-": result = (value1 != 1) ? Mathf.Pow(value1, 1.0f / value2) - value3 - value4 : -999; break;
                                case "x": result = (value1 != 1) ? Mathf.Pow(value1, 1.0f / value2) - value3 * value4 : -999; break;
                                case "¡Â": result = (value1 != 1) ? Mathf.Pow(value1, 1.0f / value2) - value3 / value4 : -999; break;
                                case "%": result = (value1 != 1 && value3 >= value4) ? Mathf.Pow(value1, 1.0f / value2) - value3 % value4 : -999; break;
                                case "^": result = (value1 != 1 && value3 != 1) ? Mathf.Pow(value1, 1.0f / value2) - Mathf.Pow(value3, value4) : -999; break;
                                case "¡Ì": result = (value1 != 1 && value3 != 1) ? Mathf.Pow(value1, 1.0f / value2) - Mathf.Pow(value3, 1.0f / value4) : -999; break;
                                case "=": result = (value1 != 1 && Mathf.Pow(value1, 1.0f / value2) - value3 == value4) ? value4 : -999; break;
                            }
                            break;
                        case "x":
                            switch (Operator3.text)
                            {
                                case "+": result = (value1 != 1) ? Mathf.Pow(value1, 1.0f / value2) * value3 + value4 : -999; break;
                                case "-": result = (value1 != 1) ? Mathf.Pow(value1, 1.0f / value2) * value3 - value4 : -999; break;
                                case "x": result = (value1 != 1) ? Mathf.Pow(value1, 1.0f / value2) * value3 * value4 : -999; break;
                                case "¡Â": result = (value1 != 1) ? Mathf.Pow(value1, 1.0f / value2) * value3 / value4 : -999; break;
                                case "%": result = (value1 != 1 && Mathf.Pow(value1, 1.0f / value2) * value3 >= value4) ? Mathf.Pow(value1, 1.0f / value2) * value3 % value4 : -999; break;
                                case "^": result = (value1 != 1 && value3 != 1) ? Mathf.Pow(value1, 1.0f / value2) * Mathf.Pow(value3, value4) : -999; break;
                                case "¡Ì": result = (value1 != 1 && value3 != 1) ? Mathf.Pow(value1, 1.0f / value2) * Mathf.Pow(value3, 1.0f / value4) : -999; break;
                                case "=": result = (value1 != 1 && Mathf.Pow(value1, 1.0f / value2) * value3 == value4) ? value4 : -999; break;
                            }
                            break;
                        case "¡Â":
                            switch (Operator3.text)
                            {
                                case "+": result = (value1 != 1) ? Mathf.Pow(value1, 1.0f / value2) / value3 + value4 : -999; break;
                                case "-": result = (value1 != 1) ? Mathf.Pow(value1, 1.0f / value2) / value3 - value4 : -999; break;
                                case "x": result = (value1 != 1) ? Mathf.Pow(value1, 1.0f / value2) / value3 * value4 : -999; break;
                                case "¡Â": result = (value1 != 1) ? Mathf.Pow(value1, 1.0f / value2) / value3 / value4 : -999; break;
                                case "%": result = (value1 != 1 && Mathf.Pow(value1, 1.0f / value2) / value3 >= value4) ? Mathf.Pow(value1, 1.0f / value2) / value3 % value4 : -999; break;
                                case "^": result = (value1 != 1 && value3 != 1) ? Mathf.Pow(value1, 1.0f / value2) / Mathf.Pow(value3, value4) : -999; break;
                                case "¡Ì": result = (value1 != 1 && value3 != 1) ? Mathf.Pow(value1, 1.0f / value2) / Mathf.Pow(value3, 1.0f / value4) : -999; break;
                                case "=": result = (value1 != 1 && Mathf.Pow(value1, 1.0f / value2) / value3 == value4) ? value4 : -999; break;
                            }
                            break;
                        case "%":
                            switch (Operator3.text)
                            {
                                case "+": result = (value1 != 1 && Mathf.Pow(value1, 1.0f / value2) >= value3) ? Mathf.Pow(value1, 1.0f / value2) % value3 + value4 : -999; break;
                                case "-": result = (value1 != 1 && Mathf.Pow(value1, 1.0f / value2) >= value3) ? Mathf.Pow(value1, 1.0f / value2) % value3 - value4 : -999; break;
                                case "x": result = (value1 != 1 && Mathf.Pow(value1, 1.0f / value2) >= value3) ? Mathf.Pow(value1, 1.0f / value2) % value3 * value4 : -999; break;
                                case "¡Â": result = (value1 != 1 && Mathf.Pow(value1, 1.0f / value2) >= value3) ? Mathf.Pow(value1, 1.0f / value2) % value3 / value4 : -999; break;
                                case "%": result = (value1 != 1 && Mathf.Pow(value1, 1.0f / value2) >= value3 && value2 % Mathf.Pow(value1, 1.0f / value2) % value3 >= value4) ? Mathf.Pow(value1, 1.0f / value2) % value3 % value4 : -999; break;
                                case "^": result = (value1 != 1 && Mathf.Pow(value1, 1.0f / value2) >= Mathf.Pow(value3, value4)) ? Mathf.Pow(value1, 1.0f / value2) % Mathf.Pow(value3, value4) : -999; break;
                                case "¡Ì": result = (value1 != 1 && Mathf.Pow(value1, 1.0f / value2) >= Mathf.Pow(value3, 1.0f / value4)) ? Mathf.Pow(value1, 1.0f / value2) % Mathf.Pow(value3, 1.0f / value4) : -999; break;
                                case "=": result = (value1 != 1 && Mathf.Pow(value1, 1.0f / value2) >= value3 && Mathf.Pow(value1, 1.0f / value2) % value3 == value4) ? value4 : -999; break;
                            }
                            break;
                        case "^":
                            switch (Operator3.text)
                            {
                                case "+": result = (value1 != 1) ? Mathf.Pow(Mathf.Pow(value1, 1.0f / value2), value3) + value4 : -999; break;
                                case "-": result = (value1 != 1) ? Mathf.Pow(Mathf.Pow(value1, 1.0f / value2), value3) - value4 : -999; break;
                                case "x": result = (value1 != 1) ? Mathf.Pow(Mathf.Pow(value1, 1.0f / value2), value3) * value4 : -999; break;
                                case "¡Â": result = (value1 != 1) ? Mathf.Pow(Mathf.Pow(value1, 1.0f / value2), value3) / value4 : -999; break;
                                case "%": result = (value1 != 1 && Mathf.Pow(Mathf.Pow(value1, 1.0f / value2), value3) >= value4) ? Mathf.Pow(Mathf.Pow(value1, 1.0f / value2), value3) % value4 : -999; break;
                                case "^": result = (value1 != 1) ? Mathf.Pow(Mathf.Pow(Mathf.Pow(value1, 1.0f / value2), value3), value4) : -999; break;
                                case "¡Ì": result = (value1 != 1) ? Mathf.Pow(Mathf.Pow(Mathf.Pow(value1, 1.0f / value2), value3), 1.0f / value4) : -999; break;
                                case "=": result = (value1 != 1 && Mathf.Pow(Mathf.Pow(value1, 1.0f / value2), value3) == value4) ? value4 : -999; break;
                            }
                            break;
                        case "¡Ì":
                            switch (Operator3.text)
                            {
                                case "+": result = (value1 != 1) ? Mathf.Pow(Mathf.Pow(value1, 1.0f / value2), 1.0f / value3) + value4 : -999; break;
                                case "-": result = (value1 != 1) ? Mathf.Pow(Mathf.Pow(value1, 1.0f / value2), 1.0f / value3) - value4 : -999; break;
                                case "x": result = (value1 != 1) ? Mathf.Pow(Mathf.Pow(value1, 1.0f / value2), 1.0f / value3) * value4 : -999; break;
                                case "¡Â": result = (value1 != 1) ? Mathf.Pow(Mathf.Pow(value1, 1.0f / value2), 1.0f / value3) / value4 : -999; break;
                                case "%": result = (value1 != 1 && Mathf.Pow(Mathf.Pow(value1, 1.0f / value2), 1.0f / value3) >= value4) ? Mathf.Pow(Mathf.Pow(value1, 1.0f / value2), 1.0f / value3) % value4 : -999; break;
                                case "^": result = (value1 != 1) ? Mathf.Pow(Mathf.Pow(Mathf.Pow(value1, 1.0f / value2), 1.0f / value3), value4) : -999; break;
                                case "¡Ì": result = (value1 != 1) ? Mathf.Pow(Mathf.Pow(Mathf.Pow(value1, 1.0f / value2), 1.0f / value3), 1.0f / value4) : -999; break;
                                case "=": result = (value1 != 1 && Mathf.Pow(Mathf.Pow(value1, 1.0f / value2), 1.0f / value3) == value4) ? value4 : -999; break;
                            }
                            break;
                        case "=":
                            switch (Operator3.text)
                            {
                                case "+": result = (value1 != 1 && Mathf.Pow(value1, 1.0f / value2) == value3 + value4) ? Mathf.Pow(value1, 1.0f / value2) : -999; break;
                                case "-": result = (value1 != 1 && Mathf.Pow(value1, 1.0f / value2) == value3 - value4) ? Mathf.Pow(value1, 1.0f / value2) : -999; break;
                                case "x": result = (value1 != 1 && Mathf.Pow(value1, 1.0f / value2) == value3 * value4) ? Mathf.Pow(value1, 1.0f / value2) : -999; break;
                                case "¡Â": result = (value1 != 1 && Mathf.Pow(value1, 1.0f / value2) == value3 / value4) ? Mathf.Pow(value1, 1.0f / value2) : -999; break;
                                case "%": result = (value1 != 1 && Mathf.Pow(value1, 1.0f / value2) == value3 % value4 && value3 >= value4) ? Mathf.Pow(value1, 1.0f / value2) : -999; break;
                                case "^": result = (value1 != 1 && Mathf.Pow(value1, 1.0f / value2) == Mathf.Pow(value3, value4) && value3 != 1) ? Mathf.Pow(value1, 1.0f / value2) : -999; break;
                                case "¡Ì": result = (value1 != 1 && Mathf.Pow(value1, 1.0f / value2) == Mathf.Pow(value3, 1.0f / value4) && value3 != 1) ? Mathf.Pow(value1, 1.0f / value2) : -999; break;
                                case "=": result = (value1 != 1 && Mathf.Pow(value1, 1.0f / value2) == value3 && value3 == value4) ? value4 : -999; break;
                            }
                            break;
                    }
                    break;
                case "=":
                    switch (Operator2.text)
                    {
                        case "+":
                            switch (Operator3.text)
                            {
                                case "+": result = (value1 == value2 + value3 + value4) ? value1 : -999; break;
                                case "-": result = (value1 == value2 + value3 - value4) ? value1 : -999; break;
                                case "x": result = (value1 == value2 + value3 * value4) ? value1 : -999; break;
                                case "¡Â": result = (value1 == value2 + value3 / value4) ? value1 : -999; break;
                                case "%": result = (value1 == value2 + value3 % value4 && value3 >= value4) ? value1 : -999; break;
                                case "^": result = (value1 == value2 + Mathf.Pow(value3, value4) && value3 != 1) ? value1 : -999; break;
                                case "¡Ì": result = (value1 == value2 + Mathf.Pow(value3, 1.0f / value4) && value3 != 1) ? value1 : -999; break;
                                case "=": result = (value1 == value2 + value3 && value2 + value3 == value4) ? value1 : -999; break;
                            }
                            break;
                        case "-":
                            switch (Operator3.text)
                            {
                                case "+": result = (value1 == value2 - value3 + value4) ? value1 : -999; break;
                                case "-": result = (value1 == value2 - value3 - value4) ? value1 : -999; break;
                                case "x": result = (value1 == value2 - value3 * value4) ? value1 : -999; break;
                                case "¡Â": result = (value1 == value2 - value3 / value4) ? value1 : -999; break;
                                case "%": result = (value1 == value2 - value3 % value4 && value3 >= value4) ? value1 : -999; break;
                                case "^": result = (value1 == value2 - Mathf.Pow(value3, value4) && value3 != 1) ? value1 : -999; break;
                                case "¡Ì": result = (value1 == value2 - Mathf.Pow(value3, 1.0f / value4) && value3 != 1) ? value1 : -999; break;
                                case "=": result = (value1 == value2 - value3 && value2 - value3 == value4) ? value1 : -999; break;
                            }
                            break;
                        case "x":
                            switch (Operator3.text)
                            {
                                case "+": result = (value1 == value2 * value3 + value4) ? value1 : -999; break;
                                case "-": result = (value1 == value2 * value3 - value4) ? value1 : -999; break;
                                case "x": result = (value1 == value2 * value3 * value4) ? value1 : -999; break;
                                case "¡Â": result = (value1 == value2 * value3 / value4) ? value1 : -999; break;
                                case "%": result = (value1 == value2 * value3 % value4 && value2 * value3 >= value4) ? value1 : -999; break;
                                case "^": result = (value1 == value2 * Mathf.Pow(value3, value4) && value3 != 1) ? value1 : -999; break;
                                case "¡Ì": result = (value1 == value2 * Mathf.Pow(value3, 1.0f / value4) && value3 != 1) ? value1 : -999; break;
                                case "=": result = (value1 == value2 * value3 && value2 * value3 == value4) ? value1 : -999; break;
                            }
                            break;
                        case "¡Â":
                            switch (Operator3.text)
                            {
                                case "+": result = (value1 == value2 / value3 + value4) ? value1 : -999; break;
                                case "-": result = (value1 == value2 / value3 - value4) ? value1 : -999; break;
                                case "x": result = (value1 == value2 / value3 * value4) ? value1 : -999; break;
                                case "¡Â": result = (value1 == value2 / value3 / value4) ? value1 : -999; break;
                                case "%": result = (value1 == value2 / value3 % value4 && value2 / value3 >= value4) ? value1 : -999; break;
                                case "^": result = (value1 == value2 / Mathf.Pow(value3, value4) && value3 != 1) ? value1 : -999; break;
                                case "¡Ì": result = (value1 == value2 / Mathf.Pow(value3, 1.0f / value4) && value3 != 1) ? value1 : -999; break;
                                case "=": result = (value1 == value2 / value3 && value2 / value3 == value4) ? value1 : -999; break;
                            }
                            break;
                        case "%":
                            switch (Operator3.text)
                            {
                                case "+": result = (value1 == value2 % value3 + value4 && value2 >= value3) ? value1 : -999; break;
                                case "-": result = (value1 == value2 % value3 - value4 && value2 >= value3) ? value1 : -999; break;
                                case "x": result = (value1 == value2 % value3 * value4 && value2 >= value3) ? value1 : -999; break;
                                case "¡Â": result = (value1 == value2 % value3 / value4 && value2 >= value3) ? value1 : -999; break;
                                case "%": result = (value1 == value2 % value3 % value4 && value2 >= value3 && value2 % value3 >= value4) ? value1 : -999; break;
                                case "^": result = (value1 == value2 % Mathf.Pow(value3, value4) && value2 >= Mathf.Pow(value3, value4) && value3 != 1) ? value1 : -999; break;
                                case "¡Ì": result = (value1 == value2 % Mathf.Pow(value3, 1.0f / value4) && value2 >= Mathf.Pow(value3, 1.0f / value4) && value3 != 1) ? value1 : -999; break;
                                case "=": result = (value1 == value2 % value3 && value2 % value3 == value4 && value2 >= value3) ? value1 : -999; break;
                            }
                            break;
                        case "^":
                            switch (Operator3.text)
                            {
                                case "+": result = (value1 == Mathf.Pow(value2, value3) + value4 && value2 != 1) ? value1 : -999; break;
                                case "-": result = (value1 == Mathf.Pow(value2, value3) - value4 && value2 != 1) ? value1 : -999; break;
                                case "x": result = (value1 == Mathf.Pow(value2, value3) * value4 && value2 != 1) ? value1 : -999; break;
                                case "¡Â": result = (value1 == Mathf.Pow(value2, value3) / value4 && value2 != 1) ? value1 : -999; break;
                                case "%": result = (value1 == Mathf.Pow(value2, value3) % value4 && value2 != 1 && Mathf.Pow(value2, value3) >= value4) ? value1 : -999; break;
                                case "^": result = (value1 == Mathf.Pow(Mathf.Pow(value2, value3), value4) && value2 != 1) ? value1 : -999; break;
                                case "¡Ì": result = (value1 == Mathf.Pow(Mathf.Pow(value2, value3), 1.0f / value4) && value2 != 1) ? value1 : -999; break;
                                case "=": result = (value1 == Mathf.Pow(value2, value3) && Mathf.Pow(value2, value3) == value4 && value2 != 1) ? value1 : -999; break;
                            }
                            break;
                        case "¡Ì":
                            switch (Operator3.text)
                            {
                                case "+": result = (value1 == Mathf.Pow(value2, 1.0f / value3) + value4 && value2 != 1) ? value1 : -999; break;
                                case "-": result = (value1 == Mathf.Pow(value2, 1.0f / value3) - value4 && value2 != 1) ? value1 : -999; break;
                                case "x": result = (value1 == Mathf.Pow(value2, 1.0f / value3) * value4 && value2 != 1) ? value1 : -999; break;
                                case "¡Â": result = (value1 == Mathf.Pow(value2, 1.0f / value3) / value4 && value2 != 1) ? value1 : -999; break;
                                case "%": result = (value1 == Mathf.Pow(value2, 1.0f / value3) % value4 && value2 != 1 && Mathf.Pow(value2, 1.0f / value3) >= value4) ? value1 : -999; break;
                                case "^": result = (value1 == Mathf.Pow(Mathf.Pow(value2, 1.0f / value3), value4) && value2 != 1) ? value1 : -999; break;
                                case "¡Ì": result = (value1 == Mathf.Pow(Mathf.Pow(value2, 1.0f / value3), 1.0f / value4) && value2 != 1) ? value1 : -999; break;
                                case "=": result = (value1 == Mathf.Pow(value2, 1.0f / value3) && Mathf.Pow(value2, 1.0f / value3) == value4 && value2 != 1) ? value1 : -999; break;
                            }
                            break;
                        case "=":
                            switch (Operator3.text)
                            {
                                case "+": result = (value1 == value2 && value2 == value3 + value4) ? value1 : -999; break;
                                case "-": result = (value1 == value2 && value2 == value3 - value4) ? value1 : -999; break;
                                case "x": result = (value1 == value2 && value2 == value3 * value4) ? value1 : -999; break;
                                case "¡Â": result = (value1 == value2 && value2 == value3 / value4) ? value1 : -999; break;
                                case "%": result = (value1 == value2 && value2 == value3 % value4 && value3 >= value4) ? value1 : -999; break;
                                case "^": result = (value1 == value2 && value2 == Mathf.Pow(value3, value4) && value3 != 1) ? value1 : -999; break;
                                case "¡Ì": result = (value1 == value2 && value2 == Mathf.Pow(value3, 1.0f / value4) && value3 != 1) ? value1 : -999; break;
                                case "=": result = (value1 == value2 && value2 == value3 && value3 == value4) ? value1 : -999; break;
                            }
                            break;
                    }
                    break;
            }
            if (value1 > 13 || value2 > 13 || value3 > 13 || value4 > 13)
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
                if (value3 > 13)
                {
                    score = score - (int)value3;
                    jokerCount++;
                }
                if (value4 > 13)
                {
                    score = score - (int)value4;
                    jokerCount++;
                }
                if (jokerCount == 4)
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
                    switch (jokerCount)
                    {
                        case 1:
                            if (value1 > 13)
                            {
                                int randomChoice = Random.Range(0, 3);
                                switch (randomChoice)
                                {
                                    case 0:
                                        goalChoice = 2;
                                        break;
                                    case 1:
                                        goalChoice = 3;
                                        break;
                                    case 2:
                                        goalChoice = 4;
                                        break;
                                }
                            }
                            else if (value2 > 13)
                            {
                                int randomChoice = Random.Range(0, 3);
                                switch (randomChoice)
                                {
                                    case 0:
                                        goalChoice = 1;
                                        break;
                                    case 1:
                                        goalChoice = 3;
                                        break;
                                    case 2:
                                        goalChoice = 4;
                                        break;
                                }
                            }
                            else if (value3 > 13)
                            {
                                int randomChoice = Random.Range(0, 3);
                                switch (randomChoice)
                                {
                                    case 0:
                                        goalChoice = 1;
                                        break;
                                    case 1:
                                        goalChoice = 2;
                                        break;
                                    case 2:
                                        goalChoice = 4;
                                        break;
                                }
                            }
                            else
                            {
                                int randomChoice = Random.Range(0, 3);
                                switch (randomChoice)
                                {
                                    case 0:
                                        goalChoice = 1;
                                        break;
                                    case 1:
                                        goalChoice = 2;
                                        break;
                                    case 2:
                                        goalChoice = 3;
                                        break;
                                }
                            }
                            break;
                        case 2:
                            if (value1 > 13 && value2 > 13)
                            {
                                int randomChoice = Random.Range(0, 2);
                                switch (randomChoice)
                                {
                                    case 0:
                                        goalChoice = 3;
                                        break;
                                    case 1:
                                        goalChoice = 4;
                                        break;
                                }
                            }
                            else if (value1 > 13 && value3 > 13)
                            {
                                int randomChoice = Random.Range(0, 2);
                                switch (randomChoice)
                                {
                                    case 0:
                                        goalChoice = 2;
                                        break;
                                    case 1:
                                        goalChoice = 4;
                                        break;
                                }
                            }
                            else if (value1 > 13 && value4 > 13)
                            {
                                int randomChoice = Random.Range(0, 2);
                                switch (randomChoice)
                                {
                                    case 0:
                                        goalChoice = 2;
                                        break;
                                    case 1:
                                        goalChoice = 3;
                                        break;
                                }
                            }
                            else if (value2 > 13 && value3 > 13)
                            {
                                int randomChoice = Random.Range(0, 2);
                                switch (randomChoice)
                                {
                                    case 0:
                                        goalChoice = 1;
                                        break;
                                    case 1:
                                        goalChoice = 4;
                                        break;
                                }
                            }
                            else if (value2 > 13 && value4 > 13)
                            {
                                int randomChoice = Random.Range(0, 2);
                                switch (randomChoice)
                                {
                                    case 0:
                                        goalChoice = 1;
                                        break;
                                    case 1:
                                        goalChoice = 3;
                                        break;
                                }
                            }
                            else
                            {
                                int randomChoice = Random.Range(0, 2);
                                switch (randomChoice)
                                {
                                    case 0:
                                        goalChoice = 1;
                                        break;
                                    case 1:
                                        goalChoice = 2;
                                        break;
                                }
                            }
                            break;
                        case 3:
                            if (value1 > 13 && value2 > 13 && value3 > 13)
                            {
                                goalChoice = 4;
                            }
                            else if (value1 > 13 && value2 > 13 && value4 > 13)
                            {
                                goalChoice = 3;
                            }
                            else if (value1 > 13 && value3 > 13 && value4 > 13)
                            {
                                goalChoice = 2;
                            }
                            else
                            {
                                goalChoice = 1;
                            }
                            break;
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
                                button.DrawCard();
                                button.DrawCard();
                            }
                            break;
                        case 2:
                            if (dealer.player2.childCount == 0)
                            {
                                button.DrawCard();
                                button.DrawCard();
                                button.DrawCard();
                                button.DrawCard();
                            }
                            break;
                        case 3:
                            if (dealer.player3.childCount == 0)
                            {
                                button.DrawCard();
                                button.DrawCard();
                                button.DrawCard();
                                button.DrawCard();
                            }
                            break;
                        case 4:
                            if (dealer.player4.childCount == 0)
                            {
                                button.DrawCard();
                                button.DrawCard();
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
            else if (Mathf.Round(result) == goal)
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
                resultText.text = "Oops";
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
                foreach (var card3 in aiHand)
                {
                    foreach (var card4 in aiHand)
                    {

                        if (card1 != card2 && card1 != card3 && card1 != card4 && card2 != card3 && card2 != card4 && card3 != card4)
                        {
                            foreach (var op1 in new string[] { "+", "-", "x", "¡Â", "%", "^", "¡Ì", "=" })
                            {
                                foreach (var op2 in new string[] { "+", "-", "x", "¡Â", "%", "^", "¡Ì", "=" })
                                {
                                    foreach (var op3 in new string[] { "+", "-", "x", "¡Â", "%", "^", "¡Ì", "=" })
                                    {

                                        float result = 0;
                                        float value1 = (float)card1.GetCardValue;
                                        float value2 = (float)card2.GetCardValue;
                                        float value3 = (float)card3.GetCardValue;
                                        float value4 = (float)card3.GetCardValue;
                                        float goal = (float)dealer.cardGoal.GetChild(0).GetComponent<Card>().GetCardValue;
                                        switch (op1)
                                        {
                                            case "+":
                                                switch (op2)
                                                {
                                                    case "+":
                                                        switch (op3)
                                                        {
                                                            case "+": result = value1 + value2 + value3 + value4; break;
                                                            case "-": result = value1 + value2 + value3 - value4; break;
                                                            case "x": result = value1 + value2 + value3 * value4; break;
                                                            case "¡Â": result = value1 + value2 + value3 / value4; break;
                                                            case "%": result = (value3 >= value4) ? value1 + value2 + value3 % value4 : -999; break;
                                                            case "^": result = (value3 != 1) ? value1 + value2 + Mathf.Pow(value3, value4) : -999; break;
                                                            case "¡Ì": result = (value3 != 1) ? value1 + value2 + Mathf.Pow(value3, 1.0f / value4) : -999; break;
                                                            case "=": result = (value1 + value2 + value3 == value4) ? value4 : -999; break;
                                                        }
                                                        break;
                                                    case "-":
                                                        switch (op3)
                                                        {
                                                            case "+": result = value1 + value2 - value3 + value4; break;
                                                            case "-": result = value1 + value2 - value3 - value4; break;
                                                            case "x": result = value1 + value2 - value3 * value4; break;
                                                            case "¡Â": result = value1 + value2 - value3 / value4; break;
                                                            case "%": result = (value3 >= value4) ? value1 + value2 - value3 % value4 : -999; break;
                                                            case "^": result = (value3 != 1) ? value1 + value2 - Mathf.Pow(value3, value4) : -999; break;
                                                            case "¡Ì": result = (value3 != 1) ? value1 + value2 - Mathf.Pow(value3, 1.0f / value4) : -999; break;
                                                            case "=": result = (value1 + value2 - value3 == value4) ? value4 : -999; break;
                                                        }
                                                        break;
                                                    case "x":
                                                        switch (op3)
                                                        {
                                                            case "+": result = value1 + value2 * value3 + value4; break;
                                                            case "-": result = value1 + value2 * value3 - value4; break;
                                                            case "x": result = value1 + value2 * value3 * value4; break;
                                                            case "¡Â": result = value1 + value2 * value3 / value4; break;
                                                            case "%": result = (value2 * value3 >= value4) ? value1 + value2 * value3 % value4 : -999; break;
                                                            case "^": result = (value3 != 1) ? value1 + value2 * Mathf.Pow(value3, value4) : -999; break;
                                                            case "¡Ì": result = (value3 != 1) ? value1 + value2 * Mathf.Pow(value3, 1.0f / value4) : -999; break;
                                                            case "=": result = (value1 + value2 * value3 == value4) ? value4 : -999; break;
                                                        }
                                                        break;
                                                    case "¡Â":
                                                        switch (op3)
                                                        {
                                                            case "+": result = value1 + value2 / value3 + value4; break;
                                                            case "-": result = value1 + value2 / value3 - value4; break;
                                                            case "x": result = value1 + value2 / value3 * value4; break;
                                                            case "¡Â": result = value1 + value2 / value3 / value4; break;
                                                            case "%": result = (value2 / value3 >= value4) ? value1 + value2 / value3 % value4 : -999; break;
                                                            case "^": result = (value3 != 1) ? value1 + value2 / Mathf.Pow(value3, value4) : -999; break;
                                                            case "¡Ì": result = (value3 != 1) ? value1 + value2 / Mathf.Pow(value3, 1.0f / value4) : -999; break;
                                                            case "=": result = (value1 + value2 / value3 == value4) ? value4 : -999; break;
                                                        }
                                                        break;
                                                    case "%":
                                                        switch (op3)
                                                        {
                                                            case "+": result = (value2 >= value3) ? value1 + value2 % value3 + value4 : -999; break;
                                                            case "-": result = (value2 >= value3) ? value1 + value2 % value3 - value4 : -999; break;
                                                            case "x": result = (value2 >= value3) ? value1 + value2 % value3 * value4 : -999; break;
                                                            case "¡Â": result = (value2 >= value3) ? value1 + value2 % value3 / value4 : -999; break;
                                                            case "%": result = (value2 >= value3 && value2 % value3 >= value4) ? value1 + value2 % value3 % value4 : -999; break;
                                                            case "^": result = (value2 >= Mathf.Pow(value3, value4) && value3 != 1) ? value1 + value2 % Mathf.Pow(value3, value4) : -999; break;
                                                            case "¡Ì": result = (value2 >= Mathf.Pow(value3, 1.0f / value4) && value3 != 1) ? value1 + value2 % Mathf.Pow(value3, 1.0f / value4) : -999; break;
                                                            case "=": result = (value2 >= value3 && value1 + value2 % value3 == value4) ? value4 : -999; break;
                                                        }
                                                        break;
                                                    case "^":
                                                        switch (op3)
                                                        {
                                                            case "+": result = (value2 != 1) ? value1 + Mathf.Pow(value2, value3) + value4 : -999; break;
                                                            case "-": result = (value2 != 1) ? value1 + Mathf.Pow(value2, value3) - value4 : -999; break;
                                                            case "x": result = (value2 != 1) ? value1 + Mathf.Pow(value2, value3) * value4 : -999; break;
                                                            case "¡Â": result = (value2 != 1) ? value1 + Mathf.Pow(value2, value3) / value4 : -999; break;
                                                            case "%": result = (value2 != 1 && Mathf.Pow(value2, value3) >= value4) ? value1 + Mathf.Pow(value2, value3) % value4 : -999; break;
                                                            case "^": result = (value2 != 1) ? value1 + Mathf.Pow(Mathf.Pow(value2, value3), value4) : -999; break;
                                                            case "¡Ì": result = (value2 != 1) ? value1 + Mathf.Pow(Mathf.Pow(value2, 1.0f / value3), 1.0f / value4) : -999; break;
                                                            case "=": result = (value2 != 1 && value1 + Mathf.Pow(value2, value3) == value4) ? value4 : -999; break;
                                                        }
                                                        break;
                                                    case "¡Ì":
                                                        switch (op3)
                                                        {
                                                            case "+": result = (value2 != 1) ? value1 + Mathf.Pow(value2, 1.0f / value3) + value4 : -999; break;
                                                            case "-": result = (value2 != 1) ? value1 + Mathf.Pow(value2, 1.0f / value3) - value4 : -999; break;
                                                            case "x": result = (value2 != 1) ? value1 + Mathf.Pow(value2, 1.0f / value3) * value4 : -999; break;
                                                            case "¡Â": result = (value2 != 1) ? value1 + Mathf.Pow(value2, 1.0f / value3) / value4 : -999; break;
                                                            case "%": result = (value2 != 1 && Mathf.Pow(value2, 1.0f / value3) >= value4) ? value1 + Mathf.Pow(value2, 1.0f / value3) % value4 : -999; break;
                                                            case "^": result = (value2 != 1) ? value1 + Mathf.Pow(Mathf.Pow(value2, 1.0f / value3), value4) : -999; break;
                                                            case "¡Ì": result = (value2 != 1) ? value1 + Mathf.Pow(Mathf.Pow(value2, 1.0f / value3), 1.0f / value4) : -999; break;
                                                            case "=": result = (value2 != 1 && value1 + Mathf.Pow(value2, 1.0f / value3) == value4) ? value4 : -999; break;
                                                        }
                                                        break;
                                                    case "=":
                                                        switch (op3)
                                                        {
                                                            case "+": result = (value1 + value2 == value3 + value4) ? value1 + value2 : -999; break;
                                                            case "-": result = (value1 + value2 == value3 - value4) ? value1 + value2 : -999; break;
                                                            case "x": result = (value1 + value2 == value3 * value4) ? value1 + value2 : -999; break;
                                                            case "¡Â": result = (value1 + value2 == value3 / value4) ? value1 + value2 : -999; break;
                                                            case "%": result = (value1 + value2 == value3 % value4 && value3 >= value4) ? value1 + value2 : -999; break;
                                                            case "^": result = (value1 + value2 == Mathf.Pow(value3, value4) && value3 != 1) ? value1 + value2 : -999; break;
                                                            case "¡Ì": result = (value1 + value2 == Mathf.Pow(value3, 1.0f / value4) && value3 != 1) ? value1 + value2 : -999; break;
                                                            case "=": result = (value1 + value2 == value3 && value3 == value4) ? value4 : -999; break;
                                                        }
                                                        break;
                                                }
                                                break;
                                            case "-":
                                                switch (op2)
                                                {
                                                    case "+":
                                                        switch (op3)
                                                        {
                                                            case "+": result = value1 - value2 + value3 + value4; break;
                                                            case "-": result = value1 - value2 + value3 - value4; break;
                                                            case "x": result = value1 - value2 + value3 * value4; break;
                                                            case "¡Â": result = value1 - value2 + value3 / value4; break;
                                                            case "%": result = (value3 >= value4) ? value1 - value2 + value3 % value4 : -999; break;
                                                            case "^": result = (value3 != 1) ? value1 - value2 + Mathf.Pow(value3, value4) : -999; break;
                                                            case "¡Ì": result = (value3 != 1) ? value1 - value2 + Mathf.Pow(value3, 1.0f / value4) : -999; break;
                                                            case "=": result = (value1 - value2 + value3 == value4) ? value4 : -999; break;
                                                        }
                                                        break;
                                                    case "-":
                                                        switch (op3)
                                                        {
                                                            case "+": result = value1 - value2 - value3 + value4; break;
                                                            case "-": result = value1 - value2 - value3 - value4; break;
                                                            case "x": result = value1 - value2 - value3 * value4; break;
                                                            case "¡Â": result = value1 - value2 - value3 / value4; break;
                                                            case "%": result = (value3 >= value4) ? value1 - value2 - value3 % value4 : -999; break;
                                                            case "^": result = (value3 != 1) ? value1 - value2 - Mathf.Pow(value3, value4) : -999; break;
                                                            case "¡Ì": result = (value3 != 1) ? value1 - value2 - Mathf.Pow(value3, 1.0f / value4) : -999; break;
                                                            case "=": result = (value1 - value2 - value3 == value4) ? value4 : -999; break;
                                                        }
                                                        break;
                                                    case "x":
                                                        switch (op3)
                                                        {
                                                            case "+": result = value1 - value2 * value3 + value4; break;
                                                            case "-": result = value1 - value2 * value3 - value4; break;
                                                            case "x": result = value1 - value2 * value3 * value4; break;
                                                            case "¡Â": result = value1 - value2 * value3 / value4; break;
                                                            case "%": result = (value2 * value3 >= value4) ? value1 - value2 * value3 % value4 : -999; break;
                                                            case "^": result = (value3 != 1) ? value1 - value2 * Mathf.Pow(value3, value4) : -999; break;
                                                            case "¡Ì": result = (value3 != 1) ? value1 - value2 * Mathf.Pow(value3, 1.0f / value4) : -999; break;
                                                            case "=": result = (value1 - value2 * value3 == value4) ? value4 : -999; break;
                                                        }
                                                        break;
                                                    case "¡Â":
                                                        switch (op3)
                                                        {
                                                            case "+": result = value1 - value2 / value3 + value4; break;
                                                            case "-": result = value1 - value2 / value3 - value4; break;
                                                            case "x": result = value1 - value2 / value3 * value4; break;
                                                            case "¡Â": result = value1 - value2 / value3 / value4; break;
                                                            case "%": result = (value2 / value3 >= value4) ? value1 - value2 / value3 % value4 : -999; break;
                                                            case "^": result = (value3 != 1) ? value1 - value2 / Mathf.Pow(value3, value4) : -999; break;
                                                            case "¡Ì": result = (value3 != 1) ? value1 - value2 / Mathf.Pow(value3, 1.0f / value4) : -999; break;
                                                            case "=": result = (value1 - value2 / value3 == value4) ? value4 : -999; break;
                                                        }
                                                        break;
                                                    case "%":
                                                        switch (op3)
                                                        {
                                                            case "+": result = (value2 >= value3) ? value1 - value2 % value3 + value4 : -999; break;
                                                            case "-": result = (value2 >= value3) ? value1 - value2 % value3 - value4 : -999; break;
                                                            case "x": result = (value2 >= value3) ? value1 - value2 % value3 * value4 : -999; break;
                                                            case "¡Â": result = (value2 >= value3) ? value1 - value2 % value3 / value4 : -999; break;
                                                            case "%": result = (value2 >= value3 && value2 % value3 >= value4) ? value1 - value2 % value3 % value4 : -999; break;
                                                            case "^": result = (value2 >= Mathf.Pow(value3, value4) && value3 != 1) ? value1 - value2 % Mathf.Pow(value3, value4) : -999; break;
                                                            case "¡Ì": result = (value2 >= Mathf.Pow(value3, 1.0f / value4) && value3 != 1) ? value1 - value2 % Mathf.Pow(value3, 1.0f / value4) : -999; break;
                                                            case "=": result = (value2 >= value3 && value1 - value2 % value3 == value4) ? value4 : -999; break;
                                                        }
                                                        break;
                                                    case "^":
                                                        switch (op3)
                                                        {
                                                            case "+": result = (value2 != 1) ? value1 - Mathf.Pow(value2, value3) + value4 : -999; break;
                                                            case "-": result = (value2 != 1) ? value1 - Mathf.Pow(value2, value3) - value4 : -999; break;
                                                            case "x": result = (value2 != 1) ? value1 - Mathf.Pow(value2, value3) * value4 : -999; break;
                                                            case "¡Â": result = (value2 != 1) ? value1 - Mathf.Pow(value2, value3) / value4 : -999; break;
                                                            case "%": result = (value2 != 1 && Mathf.Pow(value2, value3) >= value4) ? value1 - Mathf.Pow(value2, value3) % value4 : -999; break;
                                                            case "^": result = (value2 != 1) ? value1 - Mathf.Pow(Mathf.Pow(value2, value3), value4) : -999; break;
                                                            case "¡Ì": result = (value2 != 1) ? value1 - Mathf.Pow(Mathf.Pow(value2, 1.0f / value3), 1.0f / value4) : -999; break;
                                                            case "=": result = (value2 != 1 && value1 - Mathf.Pow(value2, value3) == value4) ? value4 : -999; break;
                                                        }
                                                        break;
                                                    case "¡Ì":
                                                        switch (op3)
                                                        {
                                                            case "+": result = (value2 != 1) ? value1 - Mathf.Pow(value2, 1.0f / value3) + value4 : -999; break;
                                                            case "-": result = (value2 != 1) ? value1 - Mathf.Pow(value2, 1.0f / value3) - value4 : -999; break;
                                                            case "x": result = (value2 != 1) ? value1 - Mathf.Pow(value2, 1.0f / value3) * value4 : -999; break;
                                                            case "¡Â": result = (value2 != 1) ? value1 - Mathf.Pow(value2, 1.0f / value3) / value4 : -999; break;
                                                            case "%": result = (value2 != 1 && Mathf.Pow(value2, 1.0f / value3) >= value4) ? value1 - Mathf.Pow(value2, 1.0f / value3) % value4 : -999; break;
                                                            case "^": result = (value2 != 1) ? value1 - Mathf.Pow(Mathf.Pow(value2, 1.0f / value3), value4) : -999; break;
                                                            case "¡Ì": result = (value2 != 1) ? value1 - Mathf.Pow(Mathf.Pow(value2, 1.0f / value3), 1.0f / value4) : -999; break;
                                                            case "=": result = (value2 != 1 && value1 - Mathf.Pow(value2, 1.0f / value3) == value4) ? value4 : -999; break;
                                                        }
                                                        break;
                                                    case "=":
                                                        switch (op3)
                                                        {
                                                            case "+": result = (value1 - value2 == value3 + value4) ? value1 - value2 : -999; break;
                                                            case "-": result = (value1 - value2 == value3 - value4) ? value1 - value2 : -999; break;
                                                            case "x": result = (value1 - value2 == value3 * value4) ? value1 - value2 : -999; break;
                                                            case "¡Â": result = (value1 - value2 == value3 / value4) ? value1 - value2 : -999; break;
                                                            case "%": result = (value1 - value2 == value3 % value4 && value3 >= value4) ? value1 - value2 : -999; break;
                                                            case "^": result = (value1 - value2 == Mathf.Pow(value3, value4) && value3 != 1) ? value1 - value2 : -999; break;
                                                            case "¡Ì": result = (value1 - value2 == Mathf.Pow(value3, 1.0f / value4) && value3 != 1) ? value1 - value2 : -999; break;
                                                            case "=": result = (value1 - value2 == value3 && value3 == value4) ? value4 : -999; break;
                                                        }
                                                        break;
                                                }
                                                break;
                                            case "x":
                                                switch (op2)
                                                {
                                                    case "+":
                                                        switch (op3)
                                                        {
                                                            case "+": result = value1 * value2 + value3 + value4; break;
                                                            case "-": result = value1 * value2 + value3 - value4; break;
                                                            case "x": result = value1 * value2 + value3 * value4; break;
                                                            case "¡Â": result = value1 * value2 + value3 / value4; break;
                                                            case "%": result = (value3 >= value4) ? value1 * value2 + value3 % value4 : -999; break;
                                                            case "^": result = (value3 != 1) ? value1 * value2 + Mathf.Pow(value3, value4) : -999; break;
                                                            case "¡Ì": result = (value3 != 1) ? value1 * value2 + Mathf.Pow(value3, 1.0f / value4) : -999; break;
                                                            case "=": result = (value1 * value2 + value3 == value4) ? value4 : -999; break;
                                                        }
                                                        break;
                                                    case "-":
                                                        switch (op3)
                                                        {
                                                            case "+": result = value1 * value2 - value3 + value4; break;
                                                            case "-": result = value1 * value2 - value3 - value4; break;
                                                            case "x": result = value1 * value2 - value3 * value4; break;
                                                            case "¡Â": result = value1 * value2 - value3 / value4; break;
                                                            case "%": result = (value3 >= value4) ? value1 * value2 - value3 % value4 : -999; break;
                                                            case "^": result = (value3 != 1) ? value1 * value2 - Mathf.Pow(value3, value4) : -999; break;
                                                            case "¡Ì": result = (value3 != 1) ? value1 * value2 - Mathf.Pow(value3, 1.0f / value4) : -999; break;
                                                            case "=": result = (value1 * value2 - value3 == value4) ? value4 : -999; break;
                                                        }
                                                        break;
                                                    case "x":
                                                        switch (op3)
                                                        {
                                                            case "+": result = value1 * value2 * value3 + value4; break;
                                                            case "-": result = value1 * value2 * value3 - value4; break;
                                                            case "x": result = value1 * value2 * value3 * value4; break;
                                                            case "¡Â": result = value1 * value2 * value3 / value4; break;
                                                            case "%": result = (value1 * value2 * value3 >= value4) ? value1 * value2 * value3 % value4 : -999; break;
                                                            case "^": result = (value3 != 1) ? value1 * value2 * Mathf.Pow(value3, value4) : -999; break;
                                                            case "¡Ì": result = (value3 != 1) ? value1 * value2 * Mathf.Pow(value3, 1.0f / value4) : -999; break;
                                                            case "=": result = (value1 * value2 * value3 == value4) ? value4 : -999; break;
                                                        }
                                                        break;
                                                    case "¡Â":
                                                        switch (op3)
                                                        {
                                                            case "+": result = value1 * value2 / value3 + value4; break;
                                                            case "-": result = value1 * value2 / value3 - value4; break;
                                                            case "x": result = value1 * value2 / value3 * value4; break;
                                                            case "¡Â": result = value1 * value2 / value3 / value4; break;
                                                            case "%": result = (value1 * value2 / value3 >= value4) ? value1 * value2 / value3 % value4 : -999; break;
                                                            case "^": result = (value3 != 1) ? value1 * value2 / Mathf.Pow(value3, value4) : -999; break;
                                                            case "¡Ì": result = (value3 != 1) ? value1 * value2 / Mathf.Pow(value3, 1.0f / value4) : -999; break;
                                                            case "=": result = (value1 * value2 / value3 == value4) ? value4 : -999; break;
                                                        }
                                                        break;
                                                    case "%":
                                                        switch (op3)
                                                        {
                                                            case "+": result = (value1 * value2 >= value3) ? value1 * value2 % value3 + value4 : -999; break;
                                                            case "-": result = (value1 * value2 >= value3) ? value1 * value2 % value3 - value4 : -999; break;
                                                            case "x": result = (value1 * value2 >= value3) ? value1 * value2 % value3 * value4 : -999; break;
                                                            case "¡Â": result = (value1 * value2 >= value3) ? value1 * value2 % value3 / value4 : -999; break;
                                                            case "%": result = (value1 * value2 >= value3 && value1 * value2 % value3 >= value4) ? value1 * value2 % value3 % value4 : -999; break;
                                                            case "^": result = (value1 * value2 >= Mathf.Pow(value3, value4) && value3 != 1) ? value1 * value2 % Mathf.Pow(value3, value4) : -999; break;
                                                            case "¡Ì": result = (value1 * value2 >= Mathf.Pow(value3, 1.0f / value4) && value3 != 1) ? value1 * value2 % Mathf.Pow(value3, 1.0f / value4) : -999; break;
                                                            case "=": result = (value1 * value2 >= value3 && value1 * value2 % value3 == value4) ? value4 : -999; break;
                                                        }
                                                        break;
                                                    case "^":
                                                        switch (op3)
                                                        {
                                                            case "+": result = (value2 != 1) ? value1 * Mathf.Pow(value2, value3) + value4 : -999; break;
                                                            case "-": result = (value2 != 1) ? value1 * Mathf.Pow(value2, value3) - value4 : -999; break;
                                                            case "x": result = (value2 != 1) ? value1 * Mathf.Pow(value2, value3) * value4 : -999; break;
                                                            case "¡Â": result = (value2 != 1) ? value1 * Mathf.Pow(value2, value3) / value4 : -999; break;
                                                            case "%": result = (value2 != 1 && value1 * Mathf.Pow(value2, value3) >= value4) ? value1 * Mathf.Pow(value2, value3) % value4 : -999; break;
                                                            case "^": result = (value2 != 1) ? value1 * Mathf.Pow(Mathf.Pow(value2, value3), value4) : -999; break;
                                                            case "¡Ì": result = (value2 != 1) ? value1 * Mathf.Pow(Mathf.Pow(value2, value3), 1.0f / value4) : -999; break;
                                                            case "=": result = (value2 != 1 && value1 * Mathf.Pow(value2, value3) == value4) ? value4 : -999; break;
                                                        }
                                                        break;
                                                    case "¡Ì":
                                                        switch (op3)
                                                        {
                                                            case "+": result = (value2 != 1) ? value1 * Mathf.Pow(value2, 1.0f / value3) + value4 : -999; break;
                                                            case "-": result = (value2 != 1) ? value1 * Mathf.Pow(value2, 1.0f / value3) - value4 : -999; break;
                                                            case "x": result = (value2 != 1) ? value1 * Mathf.Pow(value2, 1.0f / value3) * value4 : -999; break;
                                                            case "¡Â": result = (value2 != 1) ? value1 * Mathf.Pow(value2, 1.0f / value3) / value4 : -999; break;
                                                            case "%": result = (value2 != 1 && value1 * Mathf.Pow(value2, 1.0f / value3) >= value4) ? value1 * Mathf.Pow(value2, 1.0f / value3) % value4 : -999; break;
                                                            case "^": result = (value2 != 1) ? value1 * Mathf.Pow(Mathf.Pow(value2, 1.0f / value3), value4) : -999; break;
                                                            case "¡Ì": result = (value2 != 1) ? value1 * Mathf.Pow(Mathf.Pow(value2, 1.0f / value3), 1.0f / value4) : -999; break;
                                                            case "=": result = (value2 != 1 && value1 * Mathf.Pow(value2, 1.0f / value3) == value4) ? value4 : -999; break;
                                                        }
                                                        break;
                                                    case "=":
                                                        switch (op3)
                                                        {
                                                            case "+": result = (value1 * value2 == value3 + value4) ? value1 * value2 : -999; break;
                                                            case "-": result = (value1 * value2 == value3 - value4) ? value1 * value2 : -999; break;
                                                            case "x": result = (value1 * value2 == value3 * value4) ? value1 * value2 : -999; break;
                                                            case "¡Â": result = (value1 * value2 == value3 / value4) ? value1 * value2 : -999; break;
                                                            case "%": result = (value1 * value2 == value3 % value4 && value3 >= value4) ? value1 * value2 : -999; break;
                                                            case "^": result = (value1 * value2 == Mathf.Pow(value3, value4) && value3 != 1) ? value1 * value2 : -999; break;
                                                            case "¡Ì": result = (value1 * value2 == Mathf.Pow(value3, 1.0f / value4) && value3 != 1) ? value1 * value2 : -999; break;
                                                            case "=": result = (value1 * value2 == value3 && value3 == value4) ? value4 : -999; break;
                                                        }
                                                        break;
                                                }
                                                break;
                                            case "¡Â":
                                                switch (op2)
                                                {
                                                    case "+":
                                                        switch (op3)
                                                        {
                                                            case "+": result = value1 / value2 + value3 + value4; break;
                                                            case "-": result = value1 / value2 + value3 - value4; break;
                                                            case "x": result = value1 / value2 + value3 * value4; break;
                                                            case "¡Â": result = value1 / value2 + value3 / value4; break;
                                                            case "%": result = (value3 >= value4) ? value1 / value2 + value3 % value4 : -999; break;
                                                            case "^": result = (value3 != 1) ? value1 / value2 + Mathf.Pow(value3, value4) : -999; break;
                                                            case "¡Ì": result = (value3 != 1) ? value1 / value2 + Mathf.Pow(value3, 1.0f / value4) : -999; break;
                                                            case "=": result = (value1 / value2 + value3 == value4) ? value4 : -999; break;
                                                        }
                                                        break;
                                                    case "-":
                                                        switch (op3)
                                                        {
                                                            case "+": result = value1 / value2 - value3 + value4; break;
                                                            case "-": result = value1 / value2 - value3 - value4; break;
                                                            case "x": result = value1 / value2 - value3 * value4; break;
                                                            case "¡Â": result = value1 / value2 - value3 / value4; break;
                                                            case "%": result = (value3 >= value4) ? value1 / value2 - value3 % value4 : -999; break;
                                                            case "^": result = (value3 != 1) ? value1 / value2 - Mathf.Pow(value3, value4) : -999; break;
                                                            case "¡Ì": result = (value3 != 1) ? value1 / value2 - Mathf.Pow(value3, 1.0f / value4) : -999; break;
                                                            case "=": result = (value1 / value2 - value3 == value4) ? value4 : -999; break;
                                                        }
                                                        break;
                                                    case "x":
                                                        switch (op3)
                                                        {
                                                            case "+": result = value1 / value2 * value3 + value4; break;
                                                            case "-": result = value1 / value2 * value3 - value4; break;
                                                            case "x": result = value1 / value2 * value3 * value4; break;
                                                            case "¡Â": result = value1 / value2 * value3 / value4; break;
                                                            case "%": result = (value1 / value2 * value3 >= value4) ? value1 / value2 * value3 % value4 : -999; break;
                                                            case "^": result = (value3 != 1) ? value1 / value2 * Mathf.Pow(value3, value4) : -999; break;
                                                            case "¡Ì": result = (value3 != 1) ? value1 / value2 * Mathf.Pow(value3, 1.0f / value4) : -999; break;
                                                            case "=": result = (value1 / value2 * value3 == value4) ? value4 : -999; break;
                                                        }
                                                        break;
                                                    case "¡Â":
                                                        switch (op3)
                                                        {
                                                            case "+": result = value1 / value2 / value3 + value4; break;
                                                            case "-": result = value1 / value2 / value3 - value4; break;
                                                            case "x": result = value1 / value2 / value3 * value4; break;
                                                            case "¡Â": result = value1 / value2 / value3 / value4; break;
                                                            case "%": result = (value1 / value2 / value3 >= value4) ? value1 / value2 / value3 % value4 : -999; break;
                                                            case "^": result = (value3 != 1) ? value1 / value2 / Mathf.Pow(value3, value4) : -999; break;
                                                            case "¡Ì": result = (value3 != 1) ? value1 / value2 / Mathf.Pow(value3, 1.0f / value4) : -999; break;
                                                            case "=": result = (value1 / value2 / value3 == value4) ? value4 : -999; break;
                                                        }
                                                        break;
                                                    case "%":
                                                        switch (op3)
                                                        {
                                                            case "+": result = (value1 / value2 >= value3) ? value1 / value2 % value3 + value4 : -999; break;
                                                            case "-": result = (value1 / value2 >= value3) ? value1 / value2 % value3 - value4 : -999; break;
                                                            case "x": result = (value1 / value2 >= value3) ? value1 / value2 % value3 * value4 : -999; break;
                                                            case "¡Â": result = (value1 / value2 >= value3) ? value1 / value2 % value3 / value4 : -999; break;
                                                            case "%": result = (value1 / value2 >= value3 && value1 / value2 % value3 >= value4) ? value1 / value2 % value3 % value4 : -999; break;
                                                            case "^": result = (value1 / value2 >= Mathf.Pow(value3, value4) && value3 != 1) ? value1 / value2 % Mathf.Pow(value3, value4) : -999; break;
                                                            case "¡Ì": result = (value1 / value2 >= Mathf.Pow(value3, 1.0f / value4) && value3 != 1) ? value1 / value2 % Mathf.Pow(value3, 1.0f / value4) : -999; break;
                                                            case "=": result = (value1 / value2 >= value3 && value1 / value2 % value3 == value4) ? value4 : -999; break;
                                                        }
                                                        break;
                                                    case "^":
                                                        switch (op3)
                                                        {
                                                            case "+": result = (value2 != 1) ? value1 / Mathf.Pow(value2, value3) + value4 : -999; break;
                                                            case "-": result = (value2 != 1) ? value1 / Mathf.Pow(value2, value3) - value4 : -999; break;
                                                            case "x": result = (value2 != 1) ? value1 / Mathf.Pow(value2, value3) * value4 : -999; break;
                                                            case "¡Â": result = (value2 != 1) ? value1 / Mathf.Pow(value2, value3) / value4 : -999; break;
                                                            case "%": result = (value2 != 1 && value1 / Mathf.Pow(value2, value3) >= value4) ? value1 / Mathf.Pow(value2, value3) % value4 : -999; break;
                                                            case "^": result = (value2 != 1) ? value1 / Mathf.Pow(Mathf.Pow(value2, value3), value4) : -999; break;
                                                            case "¡Ì": result = (value2 != 1) ? value1 / Mathf.Pow(Mathf.Pow(value2, value3), 1.0f / value4) : -999; break;
                                                            case "=": result = (value2 != 1 && value1 / Mathf.Pow(value2, value3) == value4) ? value4 : -999; break;
                                                        }
                                                        break;
                                                    case "¡Ì":
                                                        switch (op3)
                                                        {
                                                            case "+": result = (value2 != 1) ? value1 / Mathf.Pow(value2, 1.0f / value3) + value4 : -999; break;
                                                            case "-": result = (value2 != 1) ? value1 / Mathf.Pow(value2, 1.0f / value3) - value4 : -999; break;
                                                            case "x": result = (value2 != 1) ? value1 / Mathf.Pow(value2, 1.0f / value3) * value4 : -999; break;
                                                            case "¡Â": result = (value2 != 1) ? value1 / Mathf.Pow(value2, 1.0f / value3) / value4 : -999; break;
                                                            case "%": result = (value2 != 1 && value1 / Mathf.Pow(value2, 1.0f / value3) >= value4) ? value1 / Mathf.Pow(value2, 1.0f / value3) % value4 : -999; break;
                                                            case "^": result = (value2 != 1) ? value1 / Mathf.Pow(Mathf.Pow(value2, 1.0f / value3), value4) : -999; break;
                                                            case "¡Ì": result = (value2 != 1) ? value1 / Mathf.Pow(Mathf.Pow(value2, 1.0f / value3), 1.0f / value4) : -999; break;
                                                            case "=": result = (value2 != 1 && value1 / Mathf.Pow(value2, 1.0f / value3) == value4) ? value4 : -999; break;
                                                        }
                                                        break;
                                                    case "=":
                                                        switch (op3)
                                                        {
                                                            case "+": result = (value1 / value2 == value3 + value4) ? value1 / value2 : -999; break;
                                                            case "-": result = (value1 / value2 == value3 - value4) ? value1 / value2 : -999; break;
                                                            case "x": result = (value1 / value2 == value3 * value4) ? value1 / value2 : -999; break;
                                                            case "¡Â": result = (value1 / value2 == value3 / value4) ? value1 / value2 : -999; break;
                                                            case "%": result = (value1 / value2 == value3 % value4 && value3 >= value4) ? value1 / value2 : -999; break;
                                                            case "^": result = (value1 / value2 == Mathf.Pow(value3, value4) && value3 != 1) ? value1 / value2 : -999; break;
                                                            case "¡Ì": result = (value1 / value2 == Mathf.Pow(value3, 1.0f / value4) && value3 != 1) ? value1 / value2 : -999; break;
                                                            case "=": result = (value1 / value2 == value3 && value3 == value4) ? value4 : -999; break;
                                                        }
                                                        break;
                                                }
                                                break;
                                            case "%":
                                                switch (op2)
                                                {
                                                    case "+":
                                                        switch (op3)
                                                        {
                                                            case "+": result = (value1 >= value2) ? value1 % value2 + value3 + value4 : -999; break;
                                                            case "-": result = (value1 >= value2) ? value1 % value2 + value3 - value4 : -999; break;
                                                            case "x": result = (value1 >= value2) ? value1 % value2 + value3 * value4 : -999; break;
                                                            case "¡Â": result = (value1 >= value2) ? value1 % value2 + value3 / value4 : -999; break;
                                                            case "%": result = (value1 >= value2 && value3 >= value4) ? value1 % value2 + value3 % value4 : -999; break;
                                                            case "^": result = (value1 >= value2 && value3 != 1) ? value1 % value2 + Mathf.Pow(value3, value4) : -999; break;
                                                            case "¡Ì": result = (value1 >= value2 && value3 != 1) ? value1 % value2 + Mathf.Pow(value3, 1.0f / value4) : -999; break;
                                                            case "=": result = (value1 >= value2 && value1 % value2 + value3 == value4) ? value4 : -999; break;
                                                        }
                                                        break;
                                                    case "-":
                                                        switch (op3)
                                                        {
                                                            case "+": result = (value1 >= value2) ? value1 % value2 - value3 + value4 : -999; break;
                                                            case "-": result = (value1 >= value2) ? value1 % value2 - value3 - value4 : -999; break;
                                                            case "x": result = (value1 >= value2) ? value1 % value2 - value3 * value4 : -999; break;
                                                            case "¡Â": result = (value1 >= value2) ? value1 % value2 - value3 / value4 : -999; break;
                                                            case "%": result = (value1 >= value2 && value3 >= value4) ? value1 % value2 - value3 % value4 : -999; break;
                                                            case "^": result = (value1 >= value2 && value3 != 1) ? value1 % value2 - Mathf.Pow(value3, value4) : -999; break;
                                                            case "¡Ì": result = (value1 >= value2 && value3 != 1) ? value1 % value2 - Mathf.Pow(value3, 1.0f / value4) : -999; break;
                                                            case "=": result = (value1 >= value2 && value1 % value2 - value3 == value4) ? value4 : -999; break;
                                                        }
                                                        break;
                                                    case "x":
                                                        switch (op3)
                                                        {
                                                            case "+": result = (value1 >= value2) ? value1 % value2 * value3 + value4 : -999; break;
                                                            case "-": result = (value1 >= value2) ? value1 % value2 * value3 - value4 : -999; break;
                                                            case "x": result = (value1 >= value2) ? value1 % value2 * value3 * value4 : -999; break;
                                                            case "¡Â": result = (value1 >= value2) ? value1 % value2 * value3 / value4 : -999; break;
                                                            case "%": result = (value1 >= value2 && value1 % value2 * value3 >= value4) ? value1 % value2 * value3 % value4 : -999; break;
                                                            case "^": result = (value1 >= value2 && value3 != 1) ? value1 % value2 * Mathf.Pow(value3, value4) : -999; break;
                                                            case "¡Ì": result = (value1 >= value2 && value3 != 1) ? value1 % value2 * Mathf.Pow(value3, 1.0f / value4) : -999; break;
                                                            case "=": result = (value1 >= value2 && value1 % value2 * value3 == value4) ? value4 : -999; break;
                                                        }
                                                        break;
                                                    case "¡Â":
                                                        switch (op3)
                                                        {
                                                            case "+": result = (value1 >= value2) ? value1 % value2 / value3 + value4 : -999; break;
                                                            case "-": result = (value1 >= value2) ? value1 % value2 / value3 - value4 : -999; break;
                                                            case "x": result = (value1 >= value2) ? value1 % value2 / value3 * value4 : -999; break;
                                                            case "¡Â": result = (value1 >= value2) ? value1 % value2 / value3 / value4 : -999; break;
                                                            case "%": result = (value1 >= value2 && value1 % value2 / value3 >= value4) ? value1 % value2 / value3 % value4 : -999; break;
                                                            case "^": result = (value1 >= value2 && value3 != 1) ? value1 % value2 / Mathf.Pow(value3, value4) : -999; break;
                                                            case "¡Ì": result = (value1 >= value2 && value3 != 1) ? value1 % value2 / Mathf.Pow(value3, 1.0f / value4) : -999; break;
                                                            case "=": result = (value1 >= value2 && value1 % value2 / value3 == value4) ? value4 : -999; break;
                                                        }
                                                        break;
                                                    case "%":
                                                        switch (op3)
                                                        {
                                                            case "+": result = (value1 >= value2 && value1 % value2 >= value3) ? value1 % value2 % value3 + value4 : -999; break;
                                                            case "-": result = (value1 >= value2 && value1 % value2 >= value3) ? value1 % value2 % value3 - value4 : -999; break;
                                                            case "x": result = (value1 >= value2 && value1 % value2 >= value3) ? value1 % value2 % value3 * value4 : -999; break;
                                                            case "¡Â": result = (value1 >= value2 && value1 % value2 >= value3) ? value1 % value2 % value3 / value4 : -999; break;
                                                            case "%": result = (value1 >= value2 && value1 % value2 >= value3 && value1 % value2 % value3 >= value4) ? value1 % value2 % value3 % value4 : -999; break;
                                                            case "^": result = (value1 >= value2 && value1 % value2 >= Mathf.Pow(value3, value4) && value3 != 1) ? value1 % value2 % Mathf.Pow(value3, value4) : -999; break;
                                                            case "¡Ì": result = (value1 >= value2 && value1 % value2 >= Mathf.Pow(value3, 1.0f / value4) && value3 != 1) ? value1 % value2 % Mathf.Pow(value3, 1.0f / value4) : -999; break;
                                                            case "=": result = (value1 >= value2 && value1 % value2 >= value3 && value1 % value2 % value3 == value4) ? value4 : -999; break;
                                                        }
                                                        break;
                                                    case "^":
                                                        switch (op3)
                                                        {
                                                            case "+": result = (value1 >= value2 && value2 != 1) ? value1 % Mathf.Pow(value2, value3) + value4 : -999; break;
                                                            case "-": result = (value1 >= value2 && value2 != 1) ? value1 % Mathf.Pow(value2, value3) - value4 : -999; break;
                                                            case "x": result = (value1 >= value2 && value2 != 1) ? value1 % Mathf.Pow(value2, value3) * value4 : -999; break;
                                                            case "¡Â": result = (value1 >= value2 && value2 != 1) ? value1 % Mathf.Pow(value2, value3) / value4 : -999; break;
                                                            case "%": result = (value1 >= value2 && value2 != 1 && value1 % Mathf.Pow(value2, value3) >= value4) ? value1 % Mathf.Pow(value2, value3) % value4 : -999; break;
                                                            case "^": result = (value1 >= value2 && value2 != 1) ? value1 % Mathf.Pow(Mathf.Pow(value2, value3), value4) : -999; break;
                                                            case "¡Ì": result = (value1 >= value2 && value2 != 1) ? value1 % Mathf.Pow(Mathf.Pow(value2, value3), 1.0f / value4) : -999; break;
                                                            case "=": result = (value1 >= value2 && value2 != 1 && value1 % Mathf.Pow(value2, value3) == value4) ? value4 : -999; break;
                                                        }
                                                        break;
                                                    case "¡Ì":
                                                        switch (op3)
                                                        {
                                                            case "+": result = (value1 >= value2 && value2 != 1) ? value1 % Mathf.Pow(value2, 1.0f / value3) + value4 : -999; break;
                                                            case "-": result = (value1 >= value2 && value2 != 1) ? value1 % Mathf.Pow(value2, 1.0f / value3) - value4 : -999; break;
                                                            case "x": result = (value1 >= value2 && value2 != 1) ? value1 % Mathf.Pow(value2, 1.0f / value3) * value4 : -999; break;
                                                            case "¡Â": result = (value1 >= value2 && value2 != 1) ? value1 % Mathf.Pow(value2, 1.0f / value3) / value4 : -999; break;
                                                            case "%": result = (value1 >= value2 && value2 != 1 && value1 % Mathf.Pow(value2, 1.0f / value3) >= value4) ? value1 % Mathf.Pow(value2, 1.0f / value3) % value4 : -999; break;
                                                            case "^": result = (value1 >= value2 && value2 != 1) ? value1 % Mathf.Pow(Mathf.Pow(value2, 1.0f / value3), value4) : -999; break;
                                                            case "¡Ì": result = (value1 >= value2 && value2 != 1) ? value1 % Mathf.Pow(Mathf.Pow(value2, 1.0f / value3), 1.0f / value4) : -999; break;
                                                            case "=": result = (value1 >= value2 && value2 != 1 && value1 % Mathf.Pow(value2, 1.0f / value3) == value4) ? value4 : -999; break;
                                                        }
                                                        break;
                                                    case "=":
                                                        switch (op3)
                                                        {
                                                            case "+": result = (value1 >= value2 && value1 % value2 == value3 + value4) ? value1 % value2 : -999; break;
                                                            case "-": result = (value1 >= value2 && value1 % value2 == value3 - value4) ? value1 % value2 : -999; break;
                                                            case "x": result = (value1 >= value2 && value1 % value2 == value3 * value4) ? value1 % value2 : -999; break;
                                                            case "¡Â": result = (value1 >= value2 && value1 % value2 == value3 / value4) ? value1 % value2 : -999; break;
                                                            case "%": result = (value1 >= value2 && value1 % value2 == value3 % value4 && value3 >= value4) ? value1 % value2 : -999; break;
                                                            case "^": result = (value1 >= value2 && value1 % value2 == Mathf.Pow(value3, value4) && value3 != 1) ? value1 % value2 : -999; break;
                                                            case "¡Ì": result = (value1 >= value2 && value1 % value2 == Mathf.Pow(value3, 1.0f / value4) && value3 != 1) ? value1 % value2 : -999; break;
                                                            case "=": result = (value1 >= value2 && value1 % value2 == value3 && value3 == value4) ? value4 : -999; break;
                                                        }
                                                        break;
                                                }
                                                break;
                                            case "^":
                                                switch (op2)
                                                {
                                                    case "+":
                                                        switch (op3)
                                                        {
                                                            case "+": result = (value1 != 1) ? Mathf.Pow(value1, value2) + value3 + value4 : -999; break;
                                                            case "-": result = (value1 != 1) ? Mathf.Pow(value1, value2) + value3 - value4 : -999; break;
                                                            case "x": result = (value1 != 1) ? Mathf.Pow(value1, value2) + value3 * value4 : -999; break;
                                                            case "¡Â": result = (value1 != 1) ? Mathf.Pow(value1, value2) + value3 / value4 : -999; break;
                                                            case "%": result = (value1 != 1 && value3 >= value4) ? Mathf.Pow(value1, value2) + value3 % value4 : -999; break;
                                                            case "^": result = (value1 != 1 && value3 != 1) ? Mathf.Pow(value1, value2) + Mathf.Pow(value3, value4) : -999; break;
                                                            case "¡Ì": result = (value1 != 1 && value3 != 1) ? Mathf.Pow(value1, value2) + Mathf.Pow(value3, 1.0f / value4) : -999; break;
                                                            case "=": result = (value1 != 1 && Mathf.Pow(value1, value2) + value3 == value4) ? value4 : -999; break;
                                                        }
                                                        break;
                                                    case "-":
                                                        switch (op3)
                                                        {
                                                            case "+": result = (value1 != 1) ? Mathf.Pow(value1, value2) - value3 + value4 : -999; break;
                                                            case "-": result = (value1 != 1) ? Mathf.Pow(value1, value2) - value3 - value4 : -999; break;
                                                            case "x": result = (value1 != 1) ? Mathf.Pow(value1, value2) - value3 * value4 : -999; break;
                                                            case "¡Â": result = (value1 != 1) ? Mathf.Pow(value1, value2) - value3 / value4 : -999; break;
                                                            case "%": result = (value1 != 1 && value3 >= value4) ? Mathf.Pow(value1, value2) - value3 % value4 : -999; break;
                                                            case "^": result = (value1 != 1 && value3 != 1) ? Mathf.Pow(value1, value2) - Mathf.Pow(value3, value4) : -999; break;
                                                            case "¡Ì": result = (value1 != 1 && value3 != 1) ? Mathf.Pow(value1, value2) - Mathf.Pow(value3, 1.0f / value4) : -999; break;
                                                            case "=": result = (value1 != 1 && Mathf.Pow(value1, value2) - value3 == value4) ? value4 : -999; break;
                                                        }
                                                        break;
                                                    case "x":
                                                        switch (op3)
                                                        {
                                                            case "+": result = (value1 != 1) ? Mathf.Pow(value1, value2) * value3 + value4 : -999; break;
                                                            case "-": result = (value1 != 1) ? Mathf.Pow(value1, value2) * value3 - value4 : -999; break;
                                                            case "x": result = (value1 != 1) ? Mathf.Pow(value1, value2) * value3 * value4 : -999; break;
                                                            case "¡Â": result = (value1 != 1) ? Mathf.Pow(value1, value2) * value3 / value4 : -999; break;
                                                            case "%": result = (value1 != 1 && Mathf.Pow(value1, value2) * value3 >= value4) ? Mathf.Pow(value1, value2) * value3 % value4 : -999; break;
                                                            case "^": result = (value1 != 1 && value3 != 1) ? Mathf.Pow(value1, value2) * Mathf.Pow(value3, value4) : -999; break;
                                                            case "¡Ì": result = (value1 != 1 && value3 != 1) ? Mathf.Pow(value1, value2) * Mathf.Pow(value3, 1.0f / value4) : -999; break;
                                                            case "=": result = (value1 != 1 && Mathf.Pow(value1, value2) * value3 == value4) ? value4 : -999; break;
                                                        }
                                                        break;
                                                    case "¡Â":
                                                        switch (op3)
                                                        {
                                                            case "+": result = (value1 != 1) ? Mathf.Pow(value1, value2) / value3 + value4 : -999; break;
                                                            case "-": result = (value1 != 1) ? Mathf.Pow(value1, value2) / value3 - value4 : -999; break;
                                                            case "x": result = (value1 != 1) ? Mathf.Pow(value1, value2) / value3 * value4 : -999; break;
                                                            case "¡Â": result = (value1 != 1) ? Mathf.Pow(value1, value2) / value3 / value4 : -999; break;
                                                            case "%": result = (value1 != 1 && Mathf.Pow(value1, value2) / value3 >= value4) ? Mathf.Pow(value1, value2) / value3 % value4 : -999; break;
                                                            case "^": result = (value1 != 1 && value3 != 1) ? Mathf.Pow(value1, value2) / Mathf.Pow(value3, value4) : -999; break;
                                                            case "¡Ì": result = (value1 != 1 && value3 != 1) ? Mathf.Pow(value1, value2) / Mathf.Pow(value3, 1.0f / value4) : -999; break;
                                                            case "=": result = (value1 != 1 && Mathf.Pow(value1, value2) / value3 == value4) ? value4 : -999; break;
                                                        }
                                                        break;
                                                    case "%":
                                                        switch (op3)
                                                        {
                                                            case "+": result = (value1 != 1 && Mathf.Pow(value1, value2) >= value3) ? Mathf.Pow(value1, value2) % value3 + value4 : -999; break;
                                                            case "-": result = (value1 != 1 && Mathf.Pow(value1, value2) >= value3) ? Mathf.Pow(value1, value2) % value3 - value4 : -999; break;
                                                            case "x": result = (value1 != 1 && Mathf.Pow(value1, value2) >= value3) ? Mathf.Pow(value1, value2) % value3 * value4 : -999; break;
                                                            case "¡Â": result = (value1 != 1 && Mathf.Pow(value1, value2) >= value3) ? Mathf.Pow(value1, value2) % value3 / value4 : -999; break;
                                                            case "%": result = (value1 != 1 && Mathf.Pow(value1, value2) >= value3 && value2 % Mathf.Pow(value1, value2) % value3 >= value4) ? Mathf.Pow(value1, value2) % value3 % value4 : -999; break;
                                                            case "^": result = (value1 != 1 && Mathf.Pow(value1, value2) >= Mathf.Pow(value3, value4)) ? Mathf.Pow(value1, value2) % Mathf.Pow(value3, value4) : -999; break;
                                                            case "¡Ì": result = (value1 != 1 && Mathf.Pow(value1, value2) >= Mathf.Pow(value3, 1.0f / value4)) ? Mathf.Pow(value1, value2) % Mathf.Pow(value3, 1.0f / value4) : -999; break;
                                                            case "=": result = (value1 != 1 && Mathf.Pow(value1, value2) >= value3 && Mathf.Pow(value1, value2) % value3 == value4) ? value4 : -999; break;
                                                        }
                                                        break;
                                                    case "^":
                                                        switch (op3)
                                                        {
                                                            case "+": result = (value1 != 1) ? Mathf.Pow(Mathf.Pow(value1, value2), value3) + value4 : -999; break;
                                                            case "-": result = (value1 != 1) ? Mathf.Pow(Mathf.Pow(value1, value2), value3) - value4 : -999; break;
                                                            case "x": result = (value1 != 1) ? Mathf.Pow(Mathf.Pow(value1, value2), value3) * value4 : -999; break;
                                                            case "¡Â": result = (value1 != 1) ? Mathf.Pow(Mathf.Pow(value1, value2), value3) / value4 : -999; break;
                                                            case "%": result = (value1 != 1 && Mathf.Pow(Mathf.Pow(value1, value2), value3) >= value4) ? Mathf.Pow(Mathf.Pow(value1, value2), value3) % value4 : -999; break;
                                                            case "^": result = (value1 != 1) ? Mathf.Pow(Mathf.Pow(Mathf.Pow(value1, value2), value3), value4) : -999; break;
                                                            case "¡Ì": result = (value1 != 1) ? Mathf.Pow(Mathf.Pow(Mathf.Pow(value1, value2), value3), 1.0f / value4) : -999; break;
                                                            case "=": result = (value1 != 1 && Mathf.Pow(Mathf.Pow(value1, value2), value3) == value4) ? value4 : -999; break;
                                                        }
                                                        break;
                                                    case "¡Ì":
                                                        switch (op3)
                                                        {
                                                            case "+": result = (value1 != 1) ? Mathf.Pow(Mathf.Pow(value1, value2), 1.0f / value3) + value4 : -999; break;
                                                            case "-": result = (value1 != 1) ? Mathf.Pow(Mathf.Pow(value1, value2), 1.0f / value3) - value4 : -999; break;
                                                            case "x": result = (value1 != 1) ? Mathf.Pow(Mathf.Pow(value1, value2), 1.0f / value3) * value4 : -999; break;
                                                            case "¡Â": result = (value1 != 1) ? Mathf.Pow(Mathf.Pow(value1, value2), 1.0f / value3) / value4 : -999; break;
                                                            case "%": result = (value1 != 1 && Mathf.Pow(Mathf.Pow(value1, value2), 1.0f / value3) >= value4) ? Mathf.Pow(Mathf.Pow(value1, value2), 1.0f / value3) % value4 : -999; break;
                                                            case "^": result = (value1 != 1) ? Mathf.Pow(Mathf.Pow(Mathf.Pow(value1, value2), 1.0f / value3), value4) : -999; break;
                                                            case "¡Ì": result = (value1 != 1) ? Mathf.Pow(Mathf.Pow(Mathf.Pow(value1, value2), 1.0f / value3), 1.0f / value4) : -999; break;
                                                            case "=": result = (value1 != 1 && Mathf.Pow(Mathf.Pow(value1, value2), 1.0f / value3) == value4) ? value4 : -999; break;
                                                        }
                                                        break;
                                                    case "=":
                                                        switch (op3)
                                                        {
                                                            case "+": result = (value1 != 1 && Mathf.Pow(value1, value2) == value3 + value4) ? Mathf.Pow(value1, value2) : -999; break;
                                                            case "-": result = (value1 != 1 && Mathf.Pow(value1, value2) == value3 - value4) ? Mathf.Pow(value1, value2) : -999; break;
                                                            case "x": result = (value1 != 1 && Mathf.Pow(value1, value2) == value3 * value4) ? Mathf.Pow(value1, value2) : -999; break;
                                                            case "¡Â": result = (value1 != 1 && Mathf.Pow(value1, value2) == value3 / value4) ? Mathf.Pow(value1, value2) : -999; break;
                                                            case "%": result = (value1 != 1 && Mathf.Pow(value1, value2) == value3 % value4 && value3 >= value4) ? Mathf.Pow(value1, value2) : -999; break;
                                                            case "^": result = (value1 != 1 && Mathf.Pow(value1, value2) == Mathf.Pow(value3, value4) && value3 != 1) ? Mathf.Pow(value1, value2) : -999; break;
                                                            case "¡Ì": result = (value1 != 1 && Mathf.Pow(value1, value2) == Mathf.Pow(value3, 1.0f / value4) && value3 != 1) ? Mathf.Pow(value1, value2) : -999; break;
                                                            case "=": result = (value1 != 1 && Mathf.Pow(value1, value2) == value3 && value3 == value4) ? value4 : -999; break;
                                                        }
                                                        break;
                                                }
                                                break;
                                            case "¡Ì":
                                                switch (op2)
                                                {
                                                    case "+":
                                                        switch (op3)
                                                        {
                                                            case "+": result = (value1 != 1) ? Mathf.Pow(value1, 1.0f / value2) + value3 + value4 : -999; break;
                                                            case "-": result = (value1 != 1) ? Mathf.Pow(value1, 1.0f / value2) + value3 - value4 : -999; break;
                                                            case "x": result = (value1 != 1) ? Mathf.Pow(value1, 1.0f / value2) + value3 * value4 : -999; break;
                                                            case "¡Â": result = (value1 != 1) ? Mathf.Pow(value1, 1.0f / value2) + value3 / value4 : -999; break;
                                                            case "%": result = (value1 != 1 && value3 >= value4) ? Mathf.Pow(value1, 1.0f / value2) + value3 % value4 : -999; break;
                                                            case "^": result = (value1 != 1 && value3 != 1) ? Mathf.Pow(value1, 1.0f / value2) + Mathf.Pow(value3, value4) : -999; break;
                                                            case "¡Ì": result = (value1 != 1 && value3 != 1) ? Mathf.Pow(value1, 1.0f / value2) + Mathf.Pow(value3, 1.0f / value4) : -999; break;
                                                            case "=": result = (value1 != 1 && Mathf.Pow(value1, 1.0f / value2) + value3 == value4) ? value4 : -999; break;
                                                        }
                                                        break;
                                                    case "-":
                                                        switch (op3)
                                                        {
                                                            case "+": result = (value1 != 1) ? Mathf.Pow(value1, 1.0f / value2) - value3 + value4 : -999; break;
                                                            case "-": result = (value1 != 1) ? Mathf.Pow(value1, 1.0f / value2) - value3 - value4 : -999; break;
                                                            case "x": result = (value1 != 1) ? Mathf.Pow(value1, 1.0f / value2) - value3 * value4 : -999; break;
                                                            case "¡Â": result = (value1 != 1) ? Mathf.Pow(value1, 1.0f / value2) - value3 / value4 : -999; break;
                                                            case "%": result = (value1 != 1 && value3 >= value4) ? Mathf.Pow(value1, 1.0f / value2) - value3 % value4 : -999; break;
                                                            case "^": result = (value1 != 1 && value3 != 1) ? Mathf.Pow(value1, 1.0f / value2) - Mathf.Pow(value3, value4) : -999; break;
                                                            case "¡Ì": result = (value1 != 1 && value3 != 1) ? Mathf.Pow(value1, 1.0f / value2) - Mathf.Pow(value3, 1.0f / value4) : -999; break;
                                                            case "=": result = (value1 != 1 && Mathf.Pow(value1, 1.0f / value2) - value3 == value4) ? value4 : -999; break;
                                                        }
                                                        break;
                                                    case "x":
                                                        switch (op3)
                                                        {
                                                            case "+": result = (value1 != 1) ? Mathf.Pow(value1, 1.0f / value2) * value3 + value4 : -999; break;
                                                            case "-": result = (value1 != 1) ? Mathf.Pow(value1, 1.0f / value2) * value3 - value4 : -999; break;
                                                            case "x": result = (value1 != 1) ? Mathf.Pow(value1, 1.0f / value2) * value3 * value4 : -999; break;
                                                            case "¡Â": result = (value1 != 1) ? Mathf.Pow(value1, 1.0f / value2) * value3 / value4 : -999; break;
                                                            case "%": result = (value1 != 1 && Mathf.Pow(value1, 1.0f / value2) * value3 >= value4) ? Mathf.Pow(value1, 1.0f / value2) * value3 % value4 : -999; break;
                                                            case "^": result = (value1 != 1 && value3 != 1) ? Mathf.Pow(value1, 1.0f / value2) * Mathf.Pow(value3, value4) : -999; break;
                                                            case "¡Ì": result = (value1 != 1 && value3 != 1) ? Mathf.Pow(value1, 1.0f / value2) * Mathf.Pow(value3, 1.0f / value4) : -999; break;
                                                            case "=": result = (value1 != 1 && Mathf.Pow(value1, 1.0f / value2) * value3 == value4) ? value4 : -999; break;
                                                        }
                                                        break;
                                                    case "¡Â":
                                                        switch (op3)
                                                        {
                                                            case "+": result = (value1 != 1) ? Mathf.Pow(value1, 1.0f / value2) / value3 + value4 : -999; break;
                                                            case "-": result = (value1 != 1) ? Mathf.Pow(value1, 1.0f / value2) / value3 - value4 : -999; break;
                                                            case "x": result = (value1 != 1) ? Mathf.Pow(value1, 1.0f / value2) / value3 * value4 : -999; break;
                                                            case "¡Â": result = (value1 != 1) ? Mathf.Pow(value1, 1.0f / value2) / value3 / value4 : -999; break;
                                                            case "%": result = (value1 != 1 && Mathf.Pow(value1, 1.0f / value2) / value3 >= value4) ? Mathf.Pow(value1, 1.0f / value2) / value3 % value4 : -999; break;
                                                            case "^": result = (value1 != 1 && value3 != 1) ? Mathf.Pow(value1, 1.0f / value2) / Mathf.Pow(value3, value4) : -999; break;
                                                            case "¡Ì": result = (value1 != 1 && value3 != 1) ? Mathf.Pow(value1, 1.0f / value2) / Mathf.Pow(value3, 1.0f / value4) : -999; break;
                                                            case "=": result = (value1 != 1 && Mathf.Pow(value1, 1.0f / value2) / value3 == value4) ? value4 : -999; break;
                                                        }
                                                        break;
                                                    case "%":
                                                        switch (op3)
                                                        {
                                                            case "+": result = (value1 != 1 && Mathf.Pow(value1, 1.0f / value2) >= value3) ? Mathf.Pow(value1, 1.0f / value2) % value3 + value4 : -999; break;
                                                            case "-": result = (value1 != 1 && Mathf.Pow(value1, 1.0f / value2) >= value3) ? Mathf.Pow(value1, 1.0f / value2) % value3 - value4 : -999; break;
                                                            case "x": result = (value1 != 1 && Mathf.Pow(value1, 1.0f / value2) >= value3) ? Mathf.Pow(value1, 1.0f / value2) % value3 * value4 : -999; break;
                                                            case "¡Â": result = (value1 != 1 && Mathf.Pow(value1, 1.0f / value2) >= value3) ? Mathf.Pow(value1, 1.0f / value2) % value3 / value4 : -999; break;
                                                            case "%": result = (value1 != 1 && Mathf.Pow(value1, 1.0f / value2) >= value3 && value2 % Mathf.Pow(value1, 1.0f / value2) % value3 >= value4) ? Mathf.Pow(value1, 1.0f / value2) % value3 % value4 : -999; break;
                                                            case "^": result = (value1 != 1 && Mathf.Pow(value1, 1.0f / value2) >= Mathf.Pow(value3, value4)) ? Mathf.Pow(value1, 1.0f / value2) % Mathf.Pow(value3, value4) : -999; break;
                                                            case "¡Ì": result = (value1 != 1 && Mathf.Pow(value1, 1.0f / value2) >= Mathf.Pow(value3, 1.0f / value4)) ? Mathf.Pow(value1, 1.0f / value2) % Mathf.Pow(value3, 1.0f / value4) : -999; break;
                                                            case "=": result = (value1 != 1 && Mathf.Pow(value1, 1.0f / value2) >= value3 && Mathf.Pow(value1, 1.0f / value2) % value3 == value4) ? value4 : -999; break;
                                                        }
                                                        break;
                                                    case "^":
                                                        switch (op3)
                                                        {
                                                            case "+": result = (value1 != 1) ? Mathf.Pow(Mathf.Pow(value1, 1.0f / value2), value3) + value4 : -999; break;
                                                            case "-": result = (value1 != 1) ? Mathf.Pow(Mathf.Pow(value1, 1.0f / value2), value3) - value4 : -999; break;
                                                            case "x": result = (value1 != 1) ? Mathf.Pow(Mathf.Pow(value1, 1.0f / value2), value3) * value4 : -999; break;
                                                            case "¡Â": result = (value1 != 1) ? Mathf.Pow(Mathf.Pow(value1, 1.0f / value2), value3) / value4 : -999; break;
                                                            case "%": result = (value1 != 1 && Mathf.Pow(Mathf.Pow(value1, 1.0f / value2), value3) >= value4) ? Mathf.Pow(Mathf.Pow(value1, 1.0f / value2), value3) % value4 : -999; break;
                                                            case "^": result = (value1 != 1) ? Mathf.Pow(Mathf.Pow(Mathf.Pow(value1, 1.0f / value2), value3), value4) : -999; break;
                                                            case "¡Ì": result = (value1 != 1) ? Mathf.Pow(Mathf.Pow(Mathf.Pow(value1, 1.0f / value2), value3), 1.0f / value4) : -999; break;
                                                            case "=": result = (value1 != 1 && Mathf.Pow(Mathf.Pow(value1, 1.0f / value2), value3) == value4) ? value4 : -999; break;
                                                        }
                                                        break;
                                                    case "¡Ì":
                                                        switch (op3)
                                                        {
                                                            case "+": result = (value1 != 1) ? Mathf.Pow(Mathf.Pow(value1, 1.0f / value2), 1.0f / value3) + value4 : -999; break;
                                                            case "-": result = (value1 != 1) ? Mathf.Pow(Mathf.Pow(value1, 1.0f / value2), 1.0f / value3) - value4 : -999; break;
                                                            case "x": result = (value1 != 1) ? Mathf.Pow(Mathf.Pow(value1, 1.0f / value2), 1.0f / value3) * value4 : -999; break;
                                                            case "¡Â": result = (value1 != 1) ? Mathf.Pow(Mathf.Pow(value1, 1.0f / value2), 1.0f / value3) / value4 : -999; break;
                                                            case "%": result = (value1 != 1 && Mathf.Pow(Mathf.Pow(value1, 1.0f / value2), 1.0f / value3) >= value4) ? Mathf.Pow(Mathf.Pow(value1, 1.0f / value2), 1.0f / value3) % value4 : -999; break;
                                                            case "^": result = (value1 != 1) ? Mathf.Pow(Mathf.Pow(Mathf.Pow(value1, 1.0f / value2), 1.0f / value3), value4) : -999; break;
                                                            case "¡Ì": result = (value1 != 1) ? Mathf.Pow(Mathf.Pow(Mathf.Pow(value1, 1.0f / value2), 1.0f / value3), 1.0f / value4) : -999; break;
                                                            case "=": result = (value1 != 1 && Mathf.Pow(Mathf.Pow(value1, 1.0f / value2), 1.0f / value3) == value4) ? value4 : -999; break;
                                                        }
                                                        break;
                                                    case "=":
                                                        switch (op3)
                                                        {
                                                            case "+": result = (value1 != 1 && Mathf.Pow(value1, 1.0f / value2) == value3 + value4) ? Mathf.Pow(value1, 1.0f / value2) : -999; break;
                                                            case "-": result = (value1 != 1 && Mathf.Pow(value1, 1.0f / value2) == value3 - value4) ? Mathf.Pow(value1, 1.0f / value2) : -999; break;
                                                            case "x": result = (value1 != 1 && Mathf.Pow(value1, 1.0f / value2) == value3 * value4) ? Mathf.Pow(value1, 1.0f / value2) : -999; break;
                                                            case "¡Â": result = (value1 != 1 && Mathf.Pow(value1, 1.0f / value2) == value3 / value4) ? Mathf.Pow(value1, 1.0f / value2) : -999; break;
                                                            case "%": result = (value1 != 1 && Mathf.Pow(value1, 1.0f / value2) == value3 % value4 && value3 >= value4) ? Mathf.Pow(value1, 1.0f / value2) : -999; break;
                                                            case "^": result = (value1 != 1 && Mathf.Pow(value1, 1.0f / value2) == Mathf.Pow(value3, value4) && value3 != 1) ? Mathf.Pow(value1, 1.0f / value2) : -999; break;
                                                            case "¡Ì": result = (value1 != 1 && Mathf.Pow(value1, 1.0f / value2) == Mathf.Pow(value3, 1.0f / value4) && value3 != 1) ? Mathf.Pow(value1, 1.0f / value2) : -999; break;
                                                            case "=": result = (value1 != 1 && Mathf.Pow(value1, 1.0f / value2) == value3 && value3 == value4) ? value4 : -999; break;
                                                        }
                                                        break;
                                                }
                                                break;
                                            case "=":
                                                switch (op2)
                                                {
                                                    case "+":
                                                        switch (op3)
                                                        {
                                                            case "+": result = (value1 == value2 + value3 + value4) ? value1 : -999; break;
                                                            case "-": result = (value1 == value2 + value3 - value4) ? value1 : -999; break;
                                                            case "x": result = (value1 == value2 + value3 * value4) ? value1 : -999; break;
                                                            case "¡Â": result = (value1 == value2 + value3 / value4) ? value1 : -999; break;
                                                            case "%": result = (value1 == value2 + value3 % value4 && value3 >= value4) ? value1 : -999; break;
                                                            case "^": result = (value1 == value2 + Mathf.Pow(value3, value4) && value3 != 1) ? value1 : -999; break;
                                                            case "¡Ì": result = (value1 == value2 + Mathf.Pow(value3, 1.0f / value4) && value3 != 1) ? value1 : -999; break;
                                                            case "=": result = (value1 == value2 + value3 && value2 + value3 == value4) ? value1 : -999; break;
                                                        }
                                                        break;
                                                    case "-":
                                                        switch (op3)
                                                        {
                                                            case "+": result = (value1 == value2 - value3 + value4) ? value1 : -999; break;
                                                            case "-": result = (value1 == value2 - value3 - value4) ? value1 : -999; break;
                                                            case "x": result = (value1 == value2 - value3 * value4) ? value1 : -999; break;
                                                            case "¡Â": result = (value1 == value2 - value3 / value4) ? value1 : -999; break;
                                                            case "%": result = (value1 == value2 - value3 % value4 && value3 >= value4) ? value1 : -999; break;
                                                            case "^": result = (value1 == value2 - Mathf.Pow(value3, value4) && value3 != 1) ? value1 : -999; break;
                                                            case "¡Ì": result = (value1 == value2 - Mathf.Pow(value3, 1.0f / value4) && value3 != 1) ? value1 : -999; break;
                                                            case "=": result = (value1 == value2 - value3 && value2 - value3 == value4) ? value1 : -999; break;
                                                        }
                                                        break;
                                                    case "x":
                                                        switch (op3)
                                                        {
                                                            case "+": result = (value1 == value2 * value3 + value4) ? value1 : -999; break;
                                                            case "-": result = (value1 == value2 * value3 - value4) ? value1 : -999; break;
                                                            case "x": result = (value1 == value2 * value3 * value4) ? value1 : -999; break;
                                                            case "¡Â": result = (value1 == value2 * value3 / value4) ? value1 : -999; break;
                                                            case "%": result = (value1 == value2 * value3 % value4 && value2 * value3 >= value4) ? value1 : -999; break;
                                                            case "^": result = (value1 == value2 * Mathf.Pow(value3, value4) && value3 != 1) ? value1 : -999; break;
                                                            case "¡Ì": result = (value1 == value2 * Mathf.Pow(value3, 1.0f / value4) && value3 != 1) ? value1 : -999; break;
                                                            case "=": result = (value1 == value2 * value3 && value2 * value3 == value4) ? value1 : -999; break;
                                                        }
                                                        break;
                                                    case "¡Â":
                                                        switch (op3)
                                                        {
                                                            case "+": result = (value1 == value2 / value3 + value4) ? value1 : -999; break;
                                                            case "-": result = (value1 == value2 / value3 - value4) ? value1 : -999; break;
                                                            case "x": result = (value1 == value2 / value3 * value4) ? value1 : -999; break;
                                                            case "¡Â": result = (value1 == value2 / value3 / value4) ? value1 : -999; break;
                                                            case "%": result = (value1 == value2 / value3 % value4 && value2 / value3 >= value4) ? value1 : -999; break;
                                                            case "^": result = (value1 == value2 / Mathf.Pow(value3, value4) && value3 != 1) ? value1 : -999; break;
                                                            case "¡Ì": result = (value1 == value2 / Mathf.Pow(value3, 1.0f / value4) && value3 != 1) ? value1 : -999; break;
                                                            case "=": result = (value1 == value2 / value3 && value2 / value3 == value4) ? value1 : -999; break;
                                                        }
                                                        break;
                                                    case "%":
                                                        switch (op3)
                                                        {
                                                            case "+": result = (value1 == value2 % value3 + value4 && value2 >= value3) ? value1 : -999; break;
                                                            case "-": result = (value1 == value2 % value3 - value4 && value2 >= value3) ? value1 : -999; break;
                                                            case "x": result = (value1 == value2 % value3 * value4 && value2 >= value3) ? value1 : -999; break;
                                                            case "¡Â": result = (value1 == value2 % value3 / value4 && value2 >= value3) ? value1 : -999; break;
                                                            case "%": result = (value1 == value2 % value3 % value4 && value2 >= value3 && value2 % value3 >= value4) ? value1 : -999; break;
                                                            case "^": result = (value1 == value2 % Mathf.Pow(value3, value4) && value2 >= Mathf.Pow(value3, value4) && value3 != 1) ? value1 : -999; break;
                                                            case "¡Ì": result = (value1 == value2 % Mathf.Pow(value3, 1.0f / value4) && value2 >= Mathf.Pow(value3, 1.0f / value4) && value3 != 1) ? value1 : -999; break;
                                                            case "=": result = (value1 == value2 % value3 && value2 % value3 == value4 && value2 >= value3) ? value1 : -999; break;
                                                        }
                                                        break;
                                                    case "^":
                                                        switch (op3)
                                                        {
                                                            case "+": result = (value1 == Mathf.Pow(value2, value3) + value4 && value2 != 1) ? value1 : -999; break;
                                                            case "-": result = (value1 == Mathf.Pow(value2, value3) - value4 && value2 != 1) ? value1 : -999; break;
                                                            case "x": result = (value1 == Mathf.Pow(value2, value3) * value4 && value2 != 1) ? value1 : -999; break;
                                                            case "¡Â": result = (value1 == Mathf.Pow(value2, value3) / value4 && value2 != 1) ? value1 : -999; break;
                                                            case "%": result = (value1 == Mathf.Pow(value2, value3) % value4 && value2 != 1 && Mathf.Pow(value2, value3) >= value4) ? value1 : -999; break;
                                                            case "^": result = (value1 == Mathf.Pow(Mathf.Pow(value2, value3), value4) && value2 != 1) ? value1 : -999; break;
                                                            case "¡Ì": result = (value1 == Mathf.Pow(Mathf.Pow(value2, value3), 1.0f / value4) && value2 != 1) ? value1 : -999; break;
                                                            case "=": result = (value1 == Mathf.Pow(value2, value3) && Mathf.Pow(value2, value3) == value4 && value2 != 1) ? value1 : -999; break;
                                                        }
                                                        break;
                                                    case "¡Ì":
                                                        switch (op3)
                                                        {
                                                            case "+": result = (value1 == Mathf.Pow(value2, 1.0f / value3) + value4 && value2 != 1) ? value1 : -999; break;
                                                            case "-": result = (value1 == Mathf.Pow(value2, 1.0f / value3) - value4 && value2 != 1) ? value1 : -999; break;
                                                            case "x": result = (value1 == Mathf.Pow(value2, 1.0f / value3) * value4 && value2 != 1) ? value1 : -999; break;
                                                            case "¡Â": result = (value1 == Mathf.Pow(value2, 1.0f / value3) / value4 && value2 != 1) ? value1 : -999; break;
                                                            case "%": result = (value1 == Mathf.Pow(value2, 1.0f / value3) % value4 && value2 != 1 && Mathf.Pow(value2, 1.0f / value3) >= value4) ? value1 : -999; break;
                                                            case "^": result = (value1 == Mathf.Pow(Mathf.Pow(value2, 1.0f / value3), value4) && value2 != 1) ? value1 : -999; break;
                                                            case "¡Ì": result = (value1 == Mathf.Pow(Mathf.Pow(value2, 1.0f / value3), 1.0f / value4) && value2 != 1) ? value1 : -999; break;
                                                            case "=": result = (value1 == Mathf.Pow(value2, 1.0f / value3) && Mathf.Pow(value2, 1.0f / value3) == value4 && value2 != 1) ? value1 : -999; break;
                                                        }
                                                        break;
                                                    case "=":
                                                        switch (op3)
                                                        {
                                                            case "+": result = (value1 == value2 && value2 == value3 + value4) ? value1 : -999; break;
                                                            case "-": result = (value1 == value2 && value2 == value3 - value4) ? value1 : -999; break;
                                                            case "x": result = (value1 == value2 && value2 == value3 * value4) ? value1 : -999; break;
                                                            case "¡Â": result = (value1 == value2 && value2 == value3 / value4) ? value1 : -999; break;
                                                            case "%": result = (value1 == value2 && value2 == value3 % value4 && value3 >= value4) ? value1 : -999; break;
                                                            case "^": result = (value1 == value2 && value2 == Mathf.Pow(value3, value4) && value3 != 1) ? value1 : -999; break;
                                                            case "¡Ì": result = (value1 == value2 && value2 == Mathf.Pow(value3, 1.0f / value4) && value3 != 1) ? value1 : -999; break;
                                                            case "=": result = (value1 == value2 && value2 == value3 && value3 == value4) ? value1 : -999; break;
                                                        }
                                                        break;
                                                }
                                                break;
                                        }
                                        if (Mathf.Round(result) == goal)
                                        {
                                            foundValidMove = true;
                                            yield return new WaitForSeconds(1.5f);
                                            card1.GetTargetPos = Card1.position;
                                            card1.transform.parent = Card1;
                                            Card.RevealCard(Card1.GetChild(0).gameObject);
                                            card2.GetTargetPos = Card2.position;
                                            card2.transform.parent = Card2;
                                            Card.RevealCard(Card2.GetChild(0).gameObject);
                                            card3.GetTargetPos = Card3.position;
                                            card3.transform.parent = Card3;
                                            Card.RevealCard(Card3.GetChild(0).gameObject);
                                            card4.GetTargetPos = Card4.position;
                                            card4.transform.parent = Card4;
                                            Card.RevealCard(Card4.GetChild(0).gameObject);
                                            yield return new WaitForSeconds(1.5f);
                                            Operator1.text = op1;
                                            Operator2.text = op2;
                                            Operator3.text = op3;
                                            yield return new WaitForSeconds(1.5f);
                                            StartCoroutine(ConfirmSelection());
                                            break;
                                        }
                                    }
                                    if (foundValidMove) break;
                                }
                                if (foundValidMove) break;
                            }
                            if (foundValidMove) break;
                        }
                    }
                    if (foundValidMove) break;
                }
                if (foundValidMove) break;
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
        int randomChoice = Random.Range(0, 4);
        switch (randomChoice)
        {
            case 0:
                button.SetGoal(1);
                break;
            case 1:
                button.SetGoal(2);
                break;
            case 2:
                button.SetGoal(3);
                break;
            case 3:
                button.SetGoal(4);
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