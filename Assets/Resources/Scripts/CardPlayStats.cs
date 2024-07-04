using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CardPlayStats : MonoBehaviour
{
    public GameObject Card1;
    public TMP_Text Card1Text;
    public GameObject Card2;
    public TMP_Text Card2Text;
    public GameObject Card3;
    public TMP_Text Card3Text;
    public GameObject Card4;
    public TMP_Text Card4Text;
    public GameObject Goal;
    public TMP_Text GoalValue;
    void Start()
    {
        Card1 = GameObject.Find("Card 1/Slot");
        Card1Text = GameObject.Find("Card 1/Canvas/Button/Text (TMP)").GetComponent<TMP_Text>();
        Card2 = GameObject.Find("Card 2/Slot");
        Card2Text = GameObject.Find("Card 2/Canvas/Button/Text (TMP)").GetComponent<TMP_Text>();
        if (GameObject.Find("Card 3") != null)
        {
            Card3 = GameObject.Find("Card 3/Slot");
            Card3Text = GameObject.Find("Card 3/Canvas/Button/Text (TMP)").GetComponent<TMP_Text>();
        }
        if (GameObject.Find("Card 4") != null)
        {
            Card4 = GameObject.Find("Card 4/Slot");
            Card4Text = GameObject.Find("Card 4/Canvas/Button/Text (TMP)").GetComponent<TMP_Text>();
        }
        Goal = GameObject.Find("Card G/Goal");
        GoalValue = GameObject.Find("Card G/Canvas/GoalValue").GetComponent<TMP_Text>();
    }
    void Update()
    {
        if (Card1.transform.childCount > 0)
        {
            Card1Text.text = Card1.transform.GetChild(0).GetComponent<Card>().GetCardValue.ToString();
        }
        else
        {
            Card1Text.text = "";
        }
        if (Card2.transform.childCount > 0)
        {
            Card2Text.text = Card2.transform.GetChild(0).GetComponent<Card>().GetCardValue.ToString();
        }
        else
        {
            Card2Text.text = "";
        }
        if (GameObject.Find("Card 3") != null)
        {
            if (Card3.transform.childCount > 0)
            {
                Card3Text.text = Card3.transform.GetChild(0).GetComponent<Card>().GetCardValue.ToString();
            }
            else
            {
                Card3Text.text = "";
            }
        }
        if (GameObject.Find("Card 4") != null)
        {
            if (Card4.transform.childCount > 0)
            {
                Card4Text.text = Card4.transform.GetChild(0).GetComponent<Card>().GetCardValue.ToString();
            }
            else
            {
                Card4Text.text = "";
            }
        }
        if (Goal.transform.childCount > 0)
        {
            GoalValue.text = Goal.transform.GetChild(0).GetComponent<Card>().GetCardValue.ToString();
        }
        else
        {
            GoalValue.text = "";
        }
    }
}
