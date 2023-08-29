using TMPro;
using UnityEngine;

public class Gold : GlobalReference<Gold>
{
    public TextMeshProUGUI GoldText;
    public int GoldAmount;

    private void Start()
    {
        GoldText.text = GoldAmount.ToString();
    }
    public void AddGold(int amount)
    {
        GoldAmount += amount;
        GoldText.text = GoldAmount.ToString();
    }
    public void RemoveGold(int amount)
    {
        GoldAmount -= amount;
        GoldText.text = GoldAmount.ToString();
    }
}
