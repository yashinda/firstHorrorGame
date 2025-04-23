using UnityEngine;

public class PistolSounds : MonoBehaviour
{
    public AudioSource pistolAudio;

    public AudioClip shotClip;
    public AudioClip reloadClip;
    public AudioClip emptyMagClip;

    public void Shooting()
    {
        pistolAudio.PlayOneShot(shotClip);
    }

    public void Reloading()
    {
        pistolAudio.PlayOneShot(reloadClip);
    }

    public void EmptyMag()
    {
        pistolAudio.PlayOneShot(emptyMagClip);
    }    
}
