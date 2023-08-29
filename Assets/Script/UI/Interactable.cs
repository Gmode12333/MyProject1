using System;
using UnityEngine;

public abstract class Interactable : MonoBehaviour, IInteract
{
    public abstract void Interact();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            IInteract.TurnOn(this);
            OnTurnOn();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            IInteract.TurnOff(this);
            OnTurnOff();
        }
    }

    protected virtual void OnTurnOn() { }
    protected virtual void OnTurnOff() { }
}
