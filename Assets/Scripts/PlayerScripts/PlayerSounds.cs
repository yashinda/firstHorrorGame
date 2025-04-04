using UnityEngine;
using UnityEngine.Rendering;

public class PlayerSounds : MonoBehaviour
{
    private PlayerMovement playerMovement;

    public AudioSource audioSource;
    public AudioClip movementClip;
    private float nextStepTimeWalk = 0.8f;
    private float nextStepTimeRun = 0.4f;
    private float stepCooldown;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        MovementSounds();
    }

    private void MovementSounds()
    {
        if (!playerMovement.playerOnGround || !playerMovement.isMoving)
        {
            stepCooldown = 0.0f;
            return;
        }

        stepCooldown -= Time.deltaTime;

        if (stepCooldown <= 0)
        {
            float interval = playerMovement.isRunning ? nextStepTimeRun : nextStepTimeWalk;
            stepCooldown = interval;

            audioSource.PlayOneShot(movementClip, 0.8f);
            audioSource.pitch = Random.Range(0.9f, 1.1f);
        }
    }
}
