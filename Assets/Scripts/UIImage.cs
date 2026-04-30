using UnityEngine;
using UnityEngine.UI;

public class UIImage : MonoBehaviour
{
    private RectTransform rectTransform;
    private Image image;

    [Header("Movement")]
    public float moveAmount = 10f;     // how far it moves
    public float moveSpeed = 1f;       // speed of movement

    [Header("Color Pulse")]
    public float colorSpeed = 1f;
    public float colorIntensity = 0.1f; // how strong the change is

    private Vector3 startPos;
    private Color baseColor;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();

        startPos = rectTransform.anchoredPosition;
        baseColor = image.color;
    }

    void Update()
    {
        float time = Time.time;

        // Smooth floating movement
        float offsetX = Mathf.Sin(time * moveSpeed) * moveAmount;
        float offsetY = Mathf.Cos(time * moveSpeed * 0.8f) * moveAmount;

        rectTransform.anchoredPosition = startPos + new Vector3(offsetX, offsetY, 0);

        // Subtle color pulsing
        float pulse = (Mathf.Sin(time * colorSpeed) + 1f) / 2f; // 0–1
        float intensity = 1f + (pulse * colorIntensity);

        image.color = baseColor * intensity;
    }
}