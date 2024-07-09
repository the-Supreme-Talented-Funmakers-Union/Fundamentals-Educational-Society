using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ButtonH : MonoBehaviour
{
    public GameplayH gameplay;
    public DealerH dealer;
    public GameObject draw;
    public GameObject skip;
    public int drawCount = 0;
    public string[] operators = { "+", "-", "x", "¡Â", "%", "^", "¡Ì", "=" };
    public void ClassBegin(GameObject button)
    {
        StartCoroutine(dealer.Deal());
        button.SetActive(false);
    }
    public void CycleOperator(TMP_Text operatorText)
    {
        if (gameplay.currentPlayer == 1)
        {
            int currentOperatorIndex = System.Array.IndexOf(operators, operatorText.text);
            currentOperatorIndex = (currentOperatorIndex + 1) % operators.Length;
            operatorText.text = operators[currentOperatorIndex];
        }
    }
    public void Confirm()
    {
        StartCoroutine(gameplay.ConfirmSelection());
    }
    public void DrawCard()
    {
        if (!gameplay.newGoal)
        {
            dealer.Draw();
            drawCount++;
            CheckDraw();
        }
    }
    public void SkipTurn()
    {
        switch (gameplay.currentPlayer)
        {
            case 1:
                {
                    if (gameplay.Card1.childCount > 0)
                    {
                        gameplay.Card1.GetChild(0).GetComponent<Card>().GetTargetPos = dealer.player1.position;
                        gameplay.Card1.GetChild(0).transform.parent = dealer.player1;
                    }
                    if (gameplay.Card2.childCount > 0)
                    {
                        gameplay.Card2.GetChild(0).GetComponent<Card>().GetTargetPos = dealer.player1.position;
                        gameplay.Card2.GetChild(0).transform.parent = dealer.player1;
                    }
                    if (gameplay.Card3.childCount > 0)
                    {
                        gameplay.Card3.GetChild(0).GetComponent<Card>().GetTargetPos = dealer.player1.position;
                        gameplay.Card3.GetChild(0).transform.parent = dealer.player1;
                    }
                    if (gameplay.Card4.childCount > 0)
                    {
                        gameplay.Card4.GetChild(0).GetComponent<Card>().GetTargetPos = dealer.player1.position;
                        gameplay.Card4.GetChild(0).transform.parent = dealer.player1;
                    }
                    break;
                }
            case 2:
                {
                    if (gameplay.Card1.childCount > 0)
                    {
                        gameplay.Card1.GetChild(0).GetComponent<Card>().GetTargetPos = dealer.player2.position;
                        gameplay.Card1.GetChild(0).transform.parent = dealer.player2;
                    }
                    if (gameplay.Card2.childCount > 0)
                    {
                        gameplay.Card2.GetChild(0).GetComponent<Card>().GetTargetPos = dealer.player2.position;
                        gameplay.Card2.GetChild(0).transform.parent = dealer.player2;
                    }
                    if (gameplay.Card3.childCount > 0)
                    {
                        gameplay.Card3.GetChild(0).GetComponent<Card>().GetTargetPos = dealer.player2.position;
                        gameplay.Card3.GetChild(0).transform.parent = dealer.player2;
                    }
                    if (gameplay.Card4.childCount > 0)
                    {
                        gameplay.Card4.GetChild(0).GetComponent<Card>().GetTargetPos = dealer.player2.position;
                        gameplay.Card4.GetChild(0).transform.parent = dealer.player2;
                    }
                    break;
                }
            case 3:
                {
                    if (gameplay.Card1.childCount > 0)
                    {
                        gameplay.Card1.GetChild(0).GetComponent<Card>().GetTargetPos = dealer.player3.position;
                        gameplay.Card1.GetChild(0).transform.parent = dealer.player3;
                    }
                    if (gameplay.Card2.childCount > 0)
                    {
                        gameplay.Card2.GetChild(0).GetComponent<Card>().GetTargetPos = dealer.player3.position;
                        gameplay.Card2.GetChild(0).transform.parent = dealer.player3;
                    }
                    if (gameplay.Card3.childCount > 0)
                    {
                        gameplay.Card3.GetChild(0).GetComponent<Card>().GetTargetPos = dealer.player3.position;
                        gameplay.Card3.GetChild(0).transform.parent = dealer.player3;
                    }
                    if (gameplay.Card4.childCount > 0)
                    {
                        gameplay.Card4.GetChild(0).GetComponent<Card>().GetTargetPos = dealer.player3.position;
                        gameplay.Card4.GetChild(0).transform.parent = dealer.player3;
                    }
                    break;
                }
            case 4:
                {
                    if (gameplay.Card1.childCount > 0)
                    {
                        gameplay.Card1.GetChild(0).GetComponent<Card>().GetTargetPos = dealer.player4.position;
                        gameplay.Card1.GetChild(0).transform.parent = dealer.player4;
                    }
                    if (gameplay.Card2.childCount > 0)
                    {
                        gameplay.Card2.GetChild(0).GetComponent<Card>().GetTargetPos = dealer.player4.position;
                        gameplay.Card2.GetChild(0).transform.parent = dealer.player4;
                    }
                    if (gameplay.Card3.childCount > 0)
                    {
                        gameplay.Card3.GetChild(0).GetComponent<Card>().GetTargetPos = dealer.player4.position;
                        gameplay.Card3.GetChild(0).transform.parent = dealer.player4;
                    }
                    if (gameplay.Card4.childCount > 0)
                    {
                        gameplay.Card4.GetChild(0).GetComponent<Card>().GetTargetPos = dealer.player4.position;
                        gameplay.Card4.GetChild(0).transform.parent = dealer.player4;
                    }
                    break;
                }
        }
        gameplay.resultText.text = "";
        gameplay.scoreText.text = "";
        drawCount = 0;
        CheckDraw();
        gameplay.currentPlayer = gameplay.currentPlayer % 4 + 1;
        if (gameplay.currentPlayer != 1)
        {
            StartCoroutine(gameplay.AITurn(gameplay.currentPlayer));
        }
    }
    public void SetGoal(int cardslot)
    {
        if (gameplay.newGoal)
        {
            dealer.cardGoal.GetChild(0).GetComponent<Card>().GetTargetPos = dealer.cardRecycler.position;
            dealer.cardGoal.GetChild(0).parent = dealer.cardRecycler;
            switch (cardslot)
            {
                case 1:
                    gameplay.Card1.GetChild(0).GetComponent<Card>().GetTargetPos = dealer.cardGoal.position;
                    gameplay.Card1.GetChild(0).parent = dealer.cardGoal;
                    gameplay.Card2.GetChild(0).GetComponent<Card>().GetTargetPos = dealer.cardRecycler.position;
                    gameplay.Card2.GetChild(0).parent = dealer.cardRecycler;
                    gameplay.Card3.GetChild(0).GetComponent<Card>().GetTargetPos = dealer.cardRecycler.position;
                    gameplay.Card3.GetChild(0).parent = dealer.cardRecycler;
                    gameplay.Card4.GetChild(0).GetComponent<Card>().GetTargetPos = dealer.cardRecycler.position;
                    gameplay.Card4.GetChild(0).parent = dealer.cardRecycler;
                    break;
                case 2:
                    gameplay.Card1.GetChild(0).GetComponent<Card>().GetTargetPos = dealer.cardRecycler.position;
                    gameplay.Card1.GetChild(0).parent = dealer.cardRecycler;
                    gameplay.Card2.GetChild(0).GetComponent<Card>().GetTargetPos = dealer.cardGoal.position;
                    gameplay.Card2.GetChild(0).parent = dealer.cardGoal;
                    gameplay.Card3.GetChild(0).GetComponent<Card>().GetTargetPos = dealer.cardRecycler.position;
                    gameplay.Card3.GetChild(0).parent = dealer.cardRecycler;
                    gameplay.Card4.GetChild(0).GetComponent<Card>().GetTargetPos = dealer.cardRecycler.position;
                    gameplay.Card4.GetChild(0).parent = dealer.cardRecycler;
                    break;
                case 3:
                    gameplay.Card1.GetChild(0).GetComponent<Card>().GetTargetPos = dealer.cardRecycler.position;
                    gameplay.Card1.GetChild(0).parent = dealer.cardRecycler;
                    gameplay.Card2.GetChild(0).GetComponent<Card>().GetTargetPos = dealer.cardRecycler.position;
                    gameplay.Card2.GetChild(0).parent = dealer.cardRecycler;
                    gameplay.Card3.GetChild(0).GetComponent<Card>().GetTargetPos = dealer.cardGoal.position;
                    gameplay.Card3.GetChild(0).parent = dealer.cardGoal;
                    gameplay.Card4.GetChild(0).GetComponent<Card>().GetTargetPos = dealer.cardRecycler.position;
                    gameplay.Card4.GetChild(0).parent = dealer.cardRecycler;
                    break;
                case 4:
                    gameplay.Card1.GetChild(0).GetComponent<Card>().GetTargetPos = dealer.cardRecycler.position;
                    gameplay.Card1.GetChild(0).parent = dealer.cardRecycler;
                    gameplay.Card2.GetChild(0).GetComponent<Card>().GetTargetPos = dealer.cardRecycler.position;
                    gameplay.Card2.GetChild(0).parent = dealer.cardRecycler;
                    gameplay.Card3.GetChild(0).GetComponent<Card>().GetTargetPos = dealer.cardRecycler.position;
                    gameplay.Card3.GetChild(0).parent = dealer.cardRecycler;
                    gameplay.Card4.GetChild(0).GetComponent<Card>().GetTargetPos = dealer.cardGoal.position;
                    gameplay.Card4.GetChild(0).parent = dealer.cardGoal;
                    break;
            }
        }
        gameplay.resultText.text = "";
        gameplay.scoreText.text = "";
        gameplay.newGoal = false;
        drawCount = 0;
        CheckDraw();
        gameplay.currentPlayer = gameplay.currentPlayer % 4 + 1;
        if (gameplay.currentPlayer != 1)
        {
            StartCoroutine(gameplay.AITurn(gameplay.currentPlayer));
        }
    }
    public void CheckDraw()
    {
        if (drawCount == 5)
        {
            draw.SetActive(false);
            skip.SetActive(true);
        }
        else if (drawCount == 0)
        {
            skip.SetActive(false);
            draw.SetActive(true);
        }
    }
    public void ExitGame()
    {
        SceneManager.LoadScene(1);
    }
}