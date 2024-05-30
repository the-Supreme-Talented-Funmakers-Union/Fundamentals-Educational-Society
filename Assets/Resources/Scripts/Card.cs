using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card Game/Card")]
public class Card : ScriptableObject
{
    public string cardName;
    public int cardValue;
    public bool isJoker;
    public Sprite cardImage;
}