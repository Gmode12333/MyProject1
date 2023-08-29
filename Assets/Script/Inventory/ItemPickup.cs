using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    [SerializeField] Item item;
    [SerializeField] Inventory inventory;
    [SerializeField] int amount = 1;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] KeyCode itemPickupKey = KeyCode.Z;
    [SerializeField] Gold gold;
    private bool isEmpty;

    private void Start()
    {
        spriteRenderer.sprite = item.Icon;
    }
    private void OnValidate()
    {
        if(inventory == null)
        {
            inventory = GetComponent<Inventory>();
        }
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        spriteRenderer.sprite = item.Icon;
        spriteRenderer.enabled = true;
    }
    private void Update()
    {
        if (Input.GetKeyDown(itemPickupKey) && !isEmpty)
        {
            if (item.ID == "0529939a6761b184999a8520b6437424")
            {
                gold.AddGold(amount);
                amount = 0;
                if (amount == 0)
                {
                    isEmpty = true;
                    spriteRenderer.sprite = null;
                    this.gameObject.SetActive(false);
                }
                return;
            }
            Item itemCopy = item.GetCopy();
            if (inventory.AddItem(Instantiate(item)))
            {
                amount--;
                if (amount == 0)
                {
                    isEmpty = true;
                    spriteRenderer.sprite = null;
                    this.gameObject.SetActive(false);   
                }
            }
            else
            {
                itemCopy.Destroy();
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

    }
    private void OnTriggerExit2D(Collider2D collision)
    {

    }
}
