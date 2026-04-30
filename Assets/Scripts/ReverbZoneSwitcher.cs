using UnityEngine;

[RequireComponent(typeof(AudioReverbZone))]
public class ReverbZoneSwitcher : MonoBehaviour
{
    private AudioReverbZone reverbZone;

    void Start()
    {
        reverbZone = GetComponent<AudioReverbZone>();
        UpdateReverbState();
    }

    void Update()
    {
        // Check the global audio mode every frame
        UpdateReverbState();
    }

    void UpdateReverbState()
    {
        if (reverbZone == null) return;

        // Enable if using Unity Audio, disable if using Steam
        reverbZone.enabled = !AudioTypeController.UseSteamAudio;
    }
}