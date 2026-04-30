using UnityEngine;

public class SwayMovement : MonoBehaviour
{
    [Header("Sway Settings")]
    public float swayAmplitude = 2f;   // how far left/right
    public float swaySpeed = 1f;       // how fast it sways

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position; // remember original position
    }

    void Update()
    {
        // Calculate sway offset
        float swayOffset = Mathf.Sin(Time.time * swaySpeed) * swayAmplitude;

        // Apply sway along X axis
        transform.position = startPosition + new Vector3(swayOffset, 0f, 0f);
    }
}