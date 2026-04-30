using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class DisableOnSteamAudio : MonoBehaviour
{
    private AudioSource audioSource;

    [Header("The competing audio source to toggle against")]
    public AudioSource steamAudioSource;

    [Header("Disable when Steam Audio is active?")]
    public bool disableInSteamMode = true;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        UpdateAudioState();
    }

    void Update()
    {
        UpdateAudioState();
    }

    void UpdateAudioState()
    {
        if (!audioSource) return;

        if (disableInSteamMode)
        {
            audioSource.enabled = !AudioTypeController.UseSteamAudio;
            if (steamAudioSource) steamAudioSource.enabled = AudioTypeController.UseSteamAudio;
        }
        else
        {
            audioSource.enabled = AudioTypeController.UseSteamAudio;
            if (steamAudioSource) steamAudioSource.enabled = !AudioTypeController.UseSteamAudio;
        }
    }
}