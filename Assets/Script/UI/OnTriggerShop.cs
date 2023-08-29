using UnityEngine;

public class OnTriggerShop : Interactable
{
    public GameObject Shop;
    public GameObject Mark;
    public AudioClip InteractSound;
    public override void Interact()
    {
        Shop.gameObject.SetActive(!Shop.gameObject.activeSelf);
    }

    protected override void OnTurnOn()
    {
        base.OnTurnOn();
        Mark.gameObject.SetActive(true);
    }

    protected override void OnTurnOff()
    {
        base.OnTurnOff();
        Shop.gameObject.SetActive(false);
        Mark.gameObject.SetActive(false);
    }
}
