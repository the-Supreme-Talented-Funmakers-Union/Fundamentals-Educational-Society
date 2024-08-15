using TMPro;
using UnityEngine;
public class CardHerb : MonoBehaviour
{
    private string plantName;
    private Vector3 targetPos;
    private bool isFlipped = false;
    private bool isImage;
    public delegate void CardClicked(CardHerb card);
    public event CardClicked OnCardClicked;
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * 3);
        if (Vector3.Distance(transform.position, targetPos) <= 0.001f)
        {
            transform.position = targetPos;
        }
    }
    public string GetPlantName
    {
        set { plantName = value; }
        get { return plantName; }
    }
    public Vector3 GetTargetPos
    {
        set { targetPos = value; }
        get { return targetPos; }
    }
    public void SetIsImage(bool value)
    {
        isImage = value;
    }
    public bool IsImage()
    {
        return isImage;
    }
    void OnMouseDown()
    {
        if (!isFlipped)
        {
            OnCardClicked?.Invoke(this);
        }
    }
    public void FlipCard()
    {
        isFlipped = true;
        if (isImage)
        {
            GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Herbalism/Plants" + "/" + GetPlantName);
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Herbalism/Text" + "/" + GetPlantName);
        }
    }
    public void FlipCardBack()
    {
        isFlipped = false;
        if (isImage)
        {
            GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Herbalism/CardBack");
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Herbalism/TextBack");
        }
    }
}
