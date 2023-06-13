using UnityEngine;

public class SkipButton : MonoBehaviour
{
    public AudioSource audioSource;

    private void Start()
    {
        // Get the AudioSource component attached to the same GameObject
        audioSource = GetComponent<AudioSource>();
    }

    public void OnSkipButtonClick()
    {
        // Play the sound effect
        audioSource.Play();
    }
}
