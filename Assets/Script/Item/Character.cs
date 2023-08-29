using UnityEngine;
using UnityEngine.UI;
using Kryz.CharacterStats;
using UnityEngine.SceneManagement;

public class Character : MonoBehaviour
{
    [Header("--Health--")]
    public float Health;
    public int MaxHealth;
    [Header("--Mana--")]
    public float Mana;
    public int MaxMana;
    [Header("--Stats--")]
    public CharacterStat Strength;
    public CharacterStat Agility;
    public CharacterStat Intelligence;
    public CharacterStat Dexterity;
    [Header("--Public--")]
    public Inventory Inventory;
    public EquipmentPanel EquipmentPanel;
    [Header("--Serialize Field--")]
    [SerializeField] StatPanel statPanel;
    [SerializeField] ItemTooltip itemTooltip;
    [SerializeField] Image draggableItem;
    [SerializeField] DropItemArea dropItemArea;
    [SerializeField] QuestionDialog reallyDropItemDialog;
    //[SerializeField] ItemSaveManager itemSaveManager;
    [SerializeField] HealthBar healthBar;
    [SerializeField] ManaBar manaBar;
    [SerializeField] Transform effectPosition;
    [SerializeField] private Animator anim;
    [SerializeField] Item[] CheatsItem;
    [Header("--Audio--")]
    [SerializeField] AudioClip _useItem;
    [SerializeField] AudioClip _equipItem;
    [SerializeField] AudioClip _unequipItem;
    [SerializeField] AudioClip _startDrag;
    [SerializeField] AudioClip _endDrag;
    [SerializeField] AudioClip Hits;
    [SerializeField] AudioClip Dead;
    private BaseItemSlot dragItemSlot;

    private void OnValidate()
    {
        if (itemTooltip == null)
            itemTooltip = FindObjectOfType<ItemTooltip>();
    }

    private void Awake()
    {
        statPanel.SetStats(Strength, Intelligence, Agility, Dexterity);
        statPanel.UpdateStatValues();

        // Setup Events:
        // Right Click
        Inventory.OnRightClickEvent += InventoryRightClick;
        EquipmentPanel.OnRightClickEvent += EquipmentPanelRightClick;
        // Pointer Enter
        Inventory.OnPointerEnterEvent += ShowTooltip;
        EquipmentPanel.OnPointerEnterEvent += ShowTooltip;
        // Pointer Exit
        Inventory.OnPointerExitEvent += HideTooltip;
        EquipmentPanel.OnPointerExitEvent += HideTooltip;
        // Begin Drag
        Inventory.OnBeginDragEvent += BeginDrag;
        EquipmentPanel.OnBeginDragEvent += BeginDrag;
        // End Drag
        Inventory.OnEndDragEvent += EndDrag;
        EquipmentPanel.OnEndDragEvent += EndDrag;
        // Drag
        Inventory.OnDragEvent += Drag;
        EquipmentPanel.OnDragEvent += Drag;
        // Drop
        Inventory.OnDropEvent += Drop;
        EquipmentPanel.OnDropEvent += Drop;
        dropItemArea.OnDropEvent += DropItemOutsideUI;
    }
    private void Start()
    {
        //if (itemSaveManager != null)
        //{
        //    itemSaveManager.LoadEquipment(this);
        //    itemSaveManager.LoadInventory(this);
        //}
        SetMaxManaAndHealth();
        Health = MaxHealth;
        Mana = MaxMana;
    }
    private void SetMaxManaAndHealth()
    {
        MaxHealth = (int)Agility.Value * 20;
        MaxMana = (int)Intelligence.Value * 15;
        healthBar.SetMaxHealth(MaxHealth);
        manaBar.SetMaxMana(MaxMana);
    }
    private void SetCurrentManaAndHealth()
    {
        healthBar.SetCurrentHealth((int)Health);
        manaBar.SetCurrentMana((int)Mana);
    }
    private void OnDestroy()
    {
        //if (itemSaveManager != null)
        //{
        //    itemSaveManager.SaveEquipment(this);
        //    itemSaveManager.SaveInventory(this);
        //}
    }
    private void Update()
    {
        SetCurrentManaAndHealth();
        if (Input.GetKeyDown(KeyCode.W))
        {
            IInteract.InteractCurrent();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            SceneManager.LoadSceneAsync("Final Boss");
        }
        if(Input.GetKeyDown(KeyCode.Z)) 
        {
            Inventory.AddItem(CheatsItem[0]);
            Inventory.AddItem(CheatsItem[1]);
            Inventory.AddItem(CheatsItem[2]);
            Inventory.AddItem(CheatsItem[3]);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            MaxHealth += 10000;
            Health += 10000;
            MaxMana += 10000;
            Mana += 10000;
            Gold.Instance.AddGold(10000);
        }
    }
    private void InventoryRightClick(BaseItemSlot itemSlot)
    {
        if (itemSlot.Item is EquipableItem)
        {
            Equip((EquipableItem)itemSlot.Item);
            SoundManager.Instance.PlaySound(_equipItem);
        }
        else if (itemSlot.Item is UsableItem)
        {
            UsableItem usableItem = (UsableItem)itemSlot.Item;
            usableItem.Use(this);
            SoundManager.Instance.PlaySound(_useItem);
            if (usableItem.IsConsumable)
            {
                itemSlot.Amount--;
                usableItem.Destroy();
            }
        }
    }
    private void EquipmentPanelRightClick(BaseItemSlot itemSlot)
    {
        if (itemSlot.Item is EquipableItem)
        {
            Unequip((EquipableItem)itemSlot.Item);
            SoundManager.Instance.PlaySound(_unequipItem);
        }
    }
    private void ShowTooltip(BaseItemSlot itemSlot)
    {
        if (itemSlot.Item != null)
        {
            itemTooltip.ShowTooltip(itemSlot.Item);
        }
    }
    private void HideTooltip(BaseItemSlot itemSlot)
    {
        if (itemTooltip.gameObject.activeSelf)
        {
            itemTooltip.HideTooltip();
        }
    }
    private void BeginDrag(BaseItemSlot itemSlot)
    {
        if (itemSlot.Item != null)
        {
            dragItemSlot = itemSlot;
            draggableItem.sprite = itemSlot.Item.Icon;
            draggableItem.transform.position = Input.mousePosition;
            draggableItem.gameObject.SetActive(true);
        }
    }
    private void Drag(BaseItemSlot itemSlot)
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.nearClipPlane;
        draggableItem.transform.position = Camera.main.ScreenToWorldPoint(mousePosition);
    }
    private void EndDrag(BaseItemSlot itemSlot)
    {
        dragItemSlot = null;
        draggableItem.gameObject.SetActive(false);
        SoundManager.Instance.PlaySound(_endDrag);
    }
    private void Drop(BaseItemSlot dropItemSlot)
    {
        if (dragItemSlot == null) return;

        if (dropItemSlot.CanAddStack(dragItemSlot.Item))
        {
            AddStacks(dropItemSlot);
        }
        else if (dropItemSlot.CanReceiveItem(dragItemSlot.Item) && dragItemSlot.CanReceiveItem(dropItemSlot.Item))
        {
            SwapItems(dropItemSlot);
        }
    }
    private void DropItemOutsideUI()
    {
        if (dragItemSlot == null) return;

        reallyDropItemDialog.Show();
        BaseItemSlot baseItemSlot = dragItemSlot;
        reallyDropItemDialog.OnYesEvent += () => DestroyItemInSlot(baseItemSlot);
    }
    private void DestroyItemInSlot(BaseItemSlot itemSlot)
    {
        // If the item is equiped, unequip first
        if (itemSlot is EquipmentSlot)
        {
            EquipableItem equippableItem = (EquipableItem)itemSlot.Item;
            equippableItem.UnEquip(this);
        }

        itemSlot.Item.Destroy();
        itemSlot.Item = null;
    }
    private void AddStacks(BaseItemSlot dropItemSlot)
    {
        int numAddableStacks = dropItemSlot.Item.MaximumStack - dropItemSlot.Amount;
        int stacksToAdd = Mathf.Min(numAddableStacks, dragItemSlot.Amount);

        dropItemSlot.Amount += stacksToAdd;
        dragItemSlot.Amount -= stacksToAdd;
    }
    private void SwapItems(BaseItemSlot dropItemSlot)
    {
        EquipableItem dragEquipItem = dragItemSlot.Item as EquipableItem;
        EquipableItem dropEquipItem = dropItemSlot.Item as EquipableItem;

        if (dropItemSlot is EquipmentSlot)
        {
            if (dragEquipItem != null) dragEquipItem.Equip(this);
            if (dropEquipItem != null) dropEquipItem.UnEquip(this);
        }
        if (dragItemSlot is EquipmentSlot)
        {
            if (dragEquipItem != null) dragEquipItem.UnEquip(this);
            if (dropEquipItem != null) dropEquipItem.Equip(this);
        }
        statPanel.UpdateStatValues();

        Item draggedItem = dragItemSlot.Item;
        int draggedItemAmount = dragItemSlot.Amount;

        dragItemSlot.Item = dropItemSlot.Item;
        dragItemSlot.Amount = dropItemSlot.Amount;

        dropItemSlot.Item = draggedItem;
        dropItemSlot.Amount = draggedItemAmount;
    }
    public void Equip(EquipableItem item)
    {
        if (Inventory.RemoveItem(item))
        {
            EquipableItem previousItem;
            if (EquipmentPanel.AddItem(item, out previousItem))
            {
                if (previousItem != null)
                {
                    Inventory.AddItem(previousItem);
                    previousItem.UnEquip(this);
                    statPanel.UpdateStatValues();
                }
                item.Equip(this);
                statPanel.UpdateStatValues();
            }
            else
            {
                Inventory.AddItem(item);
            }
        }
    }
    public void Unequip(EquipableItem item)
    {
        if (Inventory.CanAddItem(item) && EquipmentPanel.RemoveItem(item))
        {
            item.UnEquip(this);
            statPanel.UpdateStatValues();
            Inventory.AddItem(item);
        }
    }
    private void TransferToItemContainer(BaseItemSlot itemSlot)
    {
        Item item = itemSlot.Item;
        if (item != null && openItemContainer.CanAddItem(item))
        {
            Inventory.RemoveItem(item);
            openItemContainer.AddItem(item);
        }
    }
    private void TransferToInventory(BaseItemSlot itemSlot)
    {
        Item item = itemSlot.Item;
        if (item != null && Inventory.CanAddItem(item))
        {
            openItemContainer.RemoveItem(item);
            Inventory.AddItem(item);
        }
    }

    private ItemContainer openItemContainer;
    public void OpenItemContainer(ItemContainer itemContainer)
    {
        openItemContainer = itemContainer;

        Inventory.OnRightClickEvent -= InventoryRightClick;
        Inventory.OnRightClickEvent += TransferToItemContainer;

        itemContainer.OnRightClickEvent += TransferToInventory;

        itemContainer.OnPointerEnterEvent += ShowTooltip;
        itemContainer.OnPointerExitEvent += HideTooltip;
        itemContainer.OnBeginDragEvent += BeginDrag;
        itemContainer.OnEndDragEvent += EndDrag;
        itemContainer.OnDragEvent += Drag;
        itemContainer.OnDropEvent += Drop;
    }
    public void CloseItemContainer(ItemContainer itemContainer)
    {
        openItemContainer = null;

        Inventory.OnRightClickEvent += InventoryRightClick;
        Inventory.OnRightClickEvent -= TransferToItemContainer;

        itemContainer.OnRightClickEvent -= TransferToInventory;

        itemContainer.OnPointerEnterEvent -= ShowTooltip;
        itemContainer.OnPointerExitEvent -= HideTooltip;
        itemContainer.OnBeginDragEvent -= BeginDrag;
        itemContainer.OnEndDragEvent -= EndDrag;
        itemContainer.OnDragEvent -= Drag;
        itemContainer.OnDropEvent -= Drop;
    }
    public void UpdateStatValues()
    {
        statPanel.UpdateStatValues();
    }
    private bool isDead = false;
    public void TakeDamage(int damage)
    {
        if (isDead) return;
        SoundManager.Instance.PlaySound(Hits);
        DamageFeedBack.Instance.DisplayDamage(effectPosition, damage);
        Health -= damage;
        anim.Play("Player_Hits");
        if (Health <= 0)
        {
            healthBar.SetCurrentHealth(0);
            Die();
        }
    }
    public void Die()
    {
        anim.Play("Player_Die");
        SoundManager.Instance.PlaySound(Dead);
        GetComponent<Movement>().enabled = false;
        GetComponent<BaseAttack>().enabled = false;
        GetComponent<Character>().enabled = false;
        this.isDead = true;
        SceneManager.LoadScene("Base");
    }
}
