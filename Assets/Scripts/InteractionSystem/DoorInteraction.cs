using UnityEngine;

public class DoorInteraction : Interactable
{
    [SerializeField] private Animator doorAnimator;
    [SerializeField] private bool doorIsOpen = false;
    void Start()
    {
        doorAnimator = GetComponent<Animator>();
    }

    protected override void Interact()
    {
        Debug.Log(textMessage);

        doorIsOpen = !doorIsOpen;
        doorAnimator.SetBool("doorIsOpen", doorIsOpen);
    }
}
