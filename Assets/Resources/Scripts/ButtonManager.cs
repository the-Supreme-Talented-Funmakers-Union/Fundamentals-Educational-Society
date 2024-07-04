using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public GameManager gameManager;
    public Dealer dealer;
    private bool Ready = false;
    private string[] operators = { "+", "-", "x", "¡Â", "%", "^", "=" };
    public void Update()
    {
        if (dealer.cardDealt)
        {
            GameObject currentSetting = dealer.currentSetting;
            CardPlayStats cardPlayStats = currentSetting.GetComponent<CardPlayStats>();
            if (currentSetting = dealer.easySetting)
            {
                if (cardPlayStats.Card1.transform.childCount != 0 && cardPlayStats.Card2.transform.childCount != 0)
                {
                    Ready = true;
                }
                else
                {
                    Ready = false;
                }
            }
            else if (currentSetting = dealer.mediumSetting)
            {
                if (cardPlayStats.Card1.transform.childCount != 0 && cardPlayStats.Card2.transform.childCount != 0 && cardPlayStats.Card3.transform.childCount != 0)
                {
                    Ready = true;
                }
                else
                {
                    Ready = false;
                }
            }
            else if (currentSetting = dealer.hardSetting)
            {
                if (cardPlayStats.Card1.transform.childCount != 0 && cardPlayStats.Card2.transform.childCount != 0 && cardPlayStats.Card3.transform.childCount != 0 && cardPlayStats.Card4.transform.childCount != 0)
                {
                    Ready = true;
                }
                else
                {
                    Ready = false;
                }
            }
        }
    }
    public void EasyMode()
    {
        SetUpLevel("Easy");
    }
    public void MediumMode()
    {
        SetUpLevel("Medium");
    }
    public void HardMode()
    {
        SetUpLevel("Hard");
    }
    private void SetUpLevel(string mode)
    {
        dealer.SetMode(mode);
        GameObject.FindWithTag("GameSetUpPanel").SetActive(false);
    }
    public void CycleOperator(TMP_Text operatorText)
    {
        int currentOperatorIndex = System.Array.IndexOf(operators, operatorText.text);
        currentOperatorIndex = (currentOperatorIndex + 1) % operators.Length;
        operatorText.text = operators[currentOperatorIndex];
    }
    public void ConfirmCard()
    {
<<<<<<< Updated upstream
        TakeCardFromSlot(cardSlot1);
        takeCard1Button.gameObject.SetActive(false);
        addCard1Button.gameObject.SetActive(true);
    }
    public void TakeCardFromSlot2()
    {
        TakeCardFromSlot(cardSlot2);
        takeCard2Button.gameObject.SetActive(false);
        addCard2Button.gameObject.SetActive(true);
    }
    private void TakeCardFromSlot(Transform slot)
    {
        Transform card = slot.GetChild(0);
        Vector3 targetPos = card.GetComponent<Card>().GetTargetPos;
        card.GetComponent<Card>().GetTargetPos = GameObject.Find("Player 1").transform.position;
        card.transform.parent = GameObject.Find("Player 1").transform;
    }
    public void ChooseCard1()
    {
        ChooseNewGoalCard(cardSlot1);
        chooseCard1Button.gameObject.SetActive(false);
        chooseCard2Button.gameObject.SetActive(false);
        addCard1Button.gameObject.SetActive(true);
        addCard2Button.gameObject.SetActive(true);
        gameManager.dealer.drawCount = 0;
        drawButton.gameObject.SetActive(true);
        skipButton.gameObject.SetActive(false);
        gameManager.currentPlayer = (gameManager.currentPlayer % 4) + 1;
        if (gameManager.currentPlayer != 1)
        {
            StartCoroutine(gameManager.AIPlayTurn(gameManager.currentPlayer));
        }
    }
    public void ChooseCard2()
    {
        ChooseNewGoalCard(cardSlot2);
        chooseCard1Button.gameObject.SetActive(false);
        chooseCard2Button.gameObject.SetActive(false);
        addCard1Button.gameObject.SetActive(true);
        addCard2Button.gameObject.SetActive(true);
        gameManager.dealer.drawCount = 0;
        drawButton.gameObject.SetActive(true);
        skipButton.gameObject.SetActive(false);
        gameManager.currentPlayer = (gameManager.currentPlayer % 4) + 1;
        if (gameManager.currentPlayer != 1)
        {
            StartCoroutine(gameManager.AIPlayTurn(gameManager.currentPlayer));
        }
    }
    private void ChooseNewGoalCard(Transform slot)
    {
        Transform recycleTransform = GameObject.Find("Recycle").transform;
        Transform goalTransform = GameObject.Find("Goal").transform;
        Transform card1 = cardSlot1.GetChild(0);
        Transform card2 = cardSlot2.GetChild(0);

        if (slot == cardSlot1)
        {
            goalTransform.GetChild(0).GetComponent<Card>().GetTargetPos = recycleTransform.position;
            goalTransform.GetChild(0).transform.parent = recycleTransform;
            card2.GetComponent<Card>().GetTargetPos = recycleTransform.position;
            card2.transform.parent = recycleTransform;
            card1.GetComponent<Card>().GetTargetPos = goalTransform.position;
            card1.transform.parent = goalTransform;
        }
        if (slot == cardSlot2)
        {
            goalTransform.GetChild(0).GetComponent<Card>().GetTargetPos = recycleTransform.position;
            goalTransform.GetChild(0).transform.parent = recycleTransform;
            card1.GetComponent<Card>().GetTargetPos = recycleTransform.position;
            card1.transform.parent = recycleTransform;
            card2.GetComponent<Card>().GetTargetPos = goalTransform.position;
            card2.transform.parent = goalTransform;
        }
    }
    public void SetOperatorAddition()
    {
        if (operatorText.text != "+")
        {
            operatorText.text = "+";
        }
        else
        {
            operatorText.text = "";
        }
    }

    public void SetOperatorSubtraction()
    {
        if (operatorText.text != "-")
        {
            operatorText.text = "-";
        }
        else
        {
            operatorText.text = "";
        }
    }

    public void SetOperatorMultiplication()
    {
        if (operatorText.text != "*")
        {
            operatorText.text = "*";
        }
        else
        {
            operatorText.text = "";
        }
    }

    public void SetOperatorDivision()
    {
        if (operatorText.text != "/")
        {
            operatorText.text = "/";
        }
        else
        {
            operatorText.text = "";
        }
    }

    public void SetOperatorModular()
    {
        if (operatorText.text != "%")
        {
            operatorText.text = "%";
        }
        else
        {
            operatorText.text = "";
        }
    }

    public void SetOperatorPower()
    {
        if (operatorText.text != "^")
        {
            operatorText.text = "^";
        }
        else
        {
            operatorText.text = "";
        }
    }

    public void SetOperatorRoot()
    {
        if (operatorText.text != "¡Ì")
        {
            operatorText.text = "¡Ì";
        }
        else
        {
            operatorText.text = "";
        }
    }
    public void DrawCard()
    {
        dealer.DrawCard();

        if (dealer.drawCount >= 5)
        {
            drawButton.gameObject.SetActive(false);
            skipButton.gameObject.SetActive(true);
        }
    }
    public void SkipTurn()
    {
        dealer.drawCount = 0;
        drawButton.gameObject.SetActive(true);
        skipButton.gameObject.SetActive(false);
        takeCard1Button.gameObject.SetActive(false);
        takeCard2Button.gameObject.SetActive(false);
        chooseCard1Button.gameObject.SetActive(false);
        chooseCard2Button.gameObject.SetActive(false);
        addCard1Button.gameObject.SetActive(true);
        addCard2Button.gameObject.SetActive(true);
        gameManager.currentPlayer = (gameManager.currentPlayer % 4) + 1;
        if (gameManager.currentPlayer != 1)
        {
            StartCoroutine(gameManager.AIPlayTurn(gameManager.currentPlayer));
        }
    }
    private void DehighlightCard()
    {
        if (highlightedCard != null)
        {
            highlightedCard.GetComponent<Card>().IsHighlighted = false;
            highlightedCard.GetComponent<SpriteRenderer>().color = Color.white;
            highlightedCard = null;
=======
        if (Ready)
        {
            gameManager.ConfirmSelection();
>>>>>>> Stashed changes
        }
    }
}