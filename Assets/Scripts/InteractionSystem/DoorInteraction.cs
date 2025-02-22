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
        if (doorIsOpen)
        {
            doorAnimator.SetBool("doorIsOpen", true);
        }
        else
        {
            doorAnimator.SetBool("doorIsOpen", false);
        }
    }
}
