using UnityEngine;

public class LightInteraction : Interactable
{
    [SerializeField] private Light lightSource;
    private bool lightEnable;

    private void Start()
    {
        lightSource = GetComponent<Light>();
    }

    protected override void Interact()
    {
        lightEnable = !lightEnable;

        lightSource.enabled = lightEnable;
    }
}
