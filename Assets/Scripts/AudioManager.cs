using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioClip fireSound;
    public AudioClip explodeSound;
    public AudioClip hurtSound;
    public AudioClip gameOverSound;

    private AudioSource audioSource;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayFire()
    {
        if(fireSound != null) audioSource.PlayOneShot(fireSound);
    }

    public void PlayExplode()
    {
        if(explodeSound != null) audioSource.PlayOneShot(explodeSound);
    }

    public void PlayHurt()
    {
        if(hurtSound != null) audioSource.PlayOneShot(hurtSound);
    }

    public void PlayGameOver()
    {
        if(gameOverSound != null) audioSource.PlayOneShot(gameOverSound);
    }
}
