using UnityEngine;

public abstract class Interactable: MonoBehaviour
{
    public string textMessage;

    public void BaseInteract()
    {
        Interact();
    }

    protected abstract void Interact();

}
