using UnityEngine;
using UnityEngine.UI;

public class NoteInteraction : Interactable
{
    public GameObject image;

    private void Start()
    {
        image.SetActive(false);
    }

    protected override void Interact()
    {
        image.SetActive(true);
        Destroy(gameObject);
    }
}
