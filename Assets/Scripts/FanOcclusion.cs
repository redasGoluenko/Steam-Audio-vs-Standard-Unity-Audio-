using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(AudioLowPassFilter))]
public class FanOcclusion : MonoBehaviour
{
    public Transform listener;
    public LayerMask occlusionLayers;

    [Header("Volume Settings")]
    public float normalVolume = 1f;
    public float muffledVolume = 0.4f;

    [Header("Low Pass Settings")]
    public float normalCutoff = 22000f;
    public float muffledCutoff = 800f;

    [Header("Transition Speed")]
    public float transitionSpeed = 5f;

    private AudioSource audioSource;
    private AudioLowPassFilter lowPass;

    private float targetVolume;
    private float targetCutoff;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        lowPass = GetComponent<AudioLowPassFilter>();

        targetVolume = normalVolume;
        targetCutoff = normalCutoff;
    }

    void Update()
    {
        CheckOcclusion();
        ApplySmoothTransition();
    }

    void CheckOcclusion()
    {
        Vector3 direction = listener.position - transform.position;
        float distance = direction.magnitude;

        if (Physics.Raycast(transform.position, direction.normalized, distance, occlusionLayers))
        {
            targetVolume = muffledVolume;
            targetCutoff = muffledCutoff;
        }
        else
        {
            targetVolume = normalVolume;
            targetCutoff = normalCutoff;
        }
    }

    void ApplySmoothTransition()
    {
        audioSource.volume = Mathf.Lerp(audioSource.volume, targetVolume, Time.deltaTime * transitionSpeed);
        lowPass.cutoffFrequency = Mathf.Lerp(lowPass.cutoffFrequency, targetCutoff, Time.deltaTime * transitionSpeed);
    }
}