using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonManager : MonoBehaviour
{
    public GameManager gameManager;
    public Dealer dealer;
    public Button startButton;
    public Button exitButton;
    public Button addCard1Button;
    public Button addCard2Button;
    public TMP_Text operatorText;
    public Button confirmButton;
    public Button takeCard1Button;
    public Button takeCard2Button;
    public Button chooseCard1Button;
    public Button chooseCard2Button;
    public Button drawButton;
    public Button skipButton;
    public Transform cardSlot1;
    public Transform cardSlot2;
    private GameObject highlightedCard;
    private PlayerSelectCard playerSelectCard;
    void Start()
    {
        playerSelectCard = GameObject.FindObjectOfType<PlayerSelectCard>();
    }
    void Update()
    {
        if (playerSelectCard != null)
        {
            highlightedCard = playerSelectCard.highlightedCard;
        }
        if (cardSlot1.childCount > 0 && cardSlot2.childCount > 0 && operatorText.text != "")
        {
            confirmButton.gameObject.SetActive(true);
        }
        else
        {
            confirmButton.gameObject.SetActive(false);
        }
    }
    public void OnDeal()
    {
        GameObject dealerObject = GameObject.FindObjectOfType<Dealer>().gameObject;
        if (dealerObject != null)
        {
            dealerObject.SendMessage("Deal");
            startButton.gameObject.SetActive(false);
        }
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void MoveCardToSlot1()
    {
        if (highlightedCard != null)
        {
            MoveCardToSlot(cardSlot1);
            addCard1Button.gameObject.SetActive(false);
            takeCard1Button.gameObject.SetActive(true);
        }
    }
    public void MoveCardToSlot2()
    {
        if (highlightedCard != null)
        {
            MoveCardToSlot(cardSlot2);
            addCard2Button.gameObject.SetActive(false);
            takeCard2Button.gameObject.SetActive(true);
        }
    }
    private void MoveCardToSlot(Transform slot)
    {
        if (highlightedCard != null)
        {
            highlightedCard.GetComponent<Card>().GetTargetPos = slot.position;
            highlightedCard.transform.parent = slot;
            DehighlightCard();
        }
    }
    public void TakeCardFromSlot1()
    {
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
        }
    }
}