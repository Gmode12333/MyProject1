using UnityEngine;

public class NPC : Interactable
{
    public GameObject Mark;
    public override void Interact()
    {

    }

    protected override void OnTurnOn()
    {
        base.OnTurnOn();
        Mark.gameObject.SetActive(true);
    }

    protected override void OnTurnOff()
    {
        base.OnTurnOff();
        Mark.gameObject.SetActive(false);
    }
}
