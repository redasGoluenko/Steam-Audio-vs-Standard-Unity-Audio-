using UnityEngine;
using SteamAudio; // needed for SteamAudioSource

[RequireComponent(typeof(CharacterController))]
public class FootstepManager : MonoBehaviour
{
    [Header("Footstep Settings")]
    public AudioSource unityFootstepSource;      // Your original Unity AudioSource
    public AudioSource steamFootstepAudioSource; // The AudioSource used by SteamAudioSource
    public SteamAudioSource steamFootstepSource; // The SteamAudioSource component
    public float stepInterval = 0.5f;
    public CharacterController controller;

    [Header("Pitch Variation")]
    [Range(0.8f, 1.2f)]
    public float minPitch = 0.95f;
    [Range(0.8f, 1.2f)]
    public float maxPitch = 1.05f;

    private float stepTimer = 0f;

    void Update()
    {
        HandleFootsteps();
    }

    void HandleFootsteps()
    {
        UnityEngine.Vector3 horizontalVelocity = new UnityEngine.Vector3(controller.velocity.x, 0, controller.velocity.z);

        if (horizontalVelocity.magnitude > 0.1f && controller.isGrounded)
        {
            stepTimer += Time.deltaTime;

            if (stepTimer >= stepInterval)
            {
                PlayFootstep();
                stepTimer = 0f;
            }
        }
        else
        {
            stepTimer = stepInterval; // reset timer for immediate first step
        }
    }

    void PlayFootstep()
    {
        float randomPitch = Random.Range(minPitch, maxPitch);

                unityFootstepSource.pitch = randomPitch;
                unityFootstepSource.Play();
         
    }
}