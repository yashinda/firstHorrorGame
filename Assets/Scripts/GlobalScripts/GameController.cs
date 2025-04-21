using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject imageFirstNote;

    private void Update()
    {
        if (imageFirstNote.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            imageFirstNote.SetActive(false);
        }
    }
}
