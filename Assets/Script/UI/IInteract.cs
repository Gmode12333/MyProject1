public interface IInteract
{
    private static IInteract interactable;

    public static void InteractCurrent()
    {
        if(interactable != null)
        {
            interactable.Interact();
        }
    }

    public static void TurnOn(IInteract interactable)
    {
        IInteract.interactable = interactable;
    }

    public static void TurnOff(IInteract interactable)
    {
        if(IInteract.interactable == interactable)
        {
            IInteract.interactable = null;
        }
    }

    public void Interact();
}
