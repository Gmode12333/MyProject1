using TMPro;
using UnityEngine;

public class ItemShop : MonoBehaviour
{
    public Item Item;
    public TextMeshProUGUI PriceText;
    public Gold CurrentGold;
    public int Price;
    public Inventory Inventory;
    [SerializeField] private AudioClip _Buy;
    [SerializeField] private AudioClip _Click;
    private void Start()
    {
        PriceText.text = Price.ToString() + " Gold";
    }
    public void Buy()
    {
       if (CurrentGold.GoldAmount >= Price)
       {
            CurrentGold.RemoveGold(Price);
            Inventory.AddItem(Item);
            SoundManager.Instance.PlaySound(_Buy);
       }
        SoundManager.Instance.PlaySound(_Click);
    }

}
