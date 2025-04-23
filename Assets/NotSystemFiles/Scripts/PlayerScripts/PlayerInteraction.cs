using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private Camera playerCamera;
    [SerializeField] private float rayDistance = 5.0f;

    private void Start()
    {
        playerCamera = Camera.main;
    }

    private void Update()
    {
        RaycastHit hit;
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        

        if (Physics.Raycast(ray, out hit, rayDistance))
        {
            if (hit.collider.GetComponent<Interactable>() != null)
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();

                if (Input.GetKeyDown(KeyCode.E))
                {
                    interactable.BaseInteract();
                }
            }
        }
    }
}
