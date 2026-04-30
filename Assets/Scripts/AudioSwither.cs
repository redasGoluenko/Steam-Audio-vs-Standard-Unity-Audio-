using UnityEngine;

public class AudioSwitcher : MonoBehaviour
{
    public AudioSource audioSource1;
    public AudioSource audioSource2;

    private bool isFirstActive = true;

    void Start()
    {
        // Start with audioSource1 enabled and audioSource2 disabled
        audioSource1.enabled = true;
        audioSource2.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            ToggleAudio();
        }
    }

    void ToggleAudio()
    {
        isFirstActive = !isFirstActive;

        audioSource1.enabled = isFirstActive;
        audioSource2.enabled = !isFirstActive;
    }
}