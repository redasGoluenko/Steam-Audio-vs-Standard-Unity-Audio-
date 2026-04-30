using UnityEngine;
using TMPro;
using System.Collections;

public class AudioTypeController : MonoBehaviour
{
    // 🔥 Global audio mode access for other scripts
    public static bool UseSteamAudio = false;

    [Header("Text Reference")]
    public TextMeshPro textMesh;

    [Header("Billboard Settings")]
    public Transform playerCamera;

    [Header("Animation Settings")]
    public float fadeDuration = 0.25f;
    public float scalePunchAmount = 1.15f;

    private Coroutine switchRoutine;
    private Vector3 originalScale;

    void Start()
    {
        if (!textMesh)
            textMesh = GetComponent<TextMeshPro>();

        if (!playerCamera && Camera.main != null)
            playerCamera = Camera.main.transform;

        originalScale = transform.localScale;

        UpdateTextInstant();
    }

    void Update()
    {
        HandleBillboard();

        if (Input.GetKeyDown(KeyCode.T))
        {
            ToggleAudioType();
        }
    }

    void HandleBillboard()
    {
        if (!playerCamera) return;

        Vector3 direction = playerCamera.position - transform.position;
        direction.y = 0f;

        if (direction.sqrMagnitude > 0.001f)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = lookRotation * Quaternion.Euler(0f, 180f, 0f);
        }
    }

    void ToggleAudioType()
    {
        UseSteamAudio = !UseSteamAudio;

        if (switchRoutine != null)
            StopCoroutine(switchRoutine);

        switchRoutine = StartCoroutine(SmoothSwitch());
    }

    void UpdateTextInstant()
    {
        textMesh.text = UseSteamAudio ? "Steam Audio" : "Unity Audio";
    }

    IEnumerator SmoothSwitch()
    {
        Color baseColor = textMesh.color;

        // Fade Out
        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, t / fadeDuration);
            textMesh.color = new Color(baseColor.r, baseColor.g, baseColor.b, alpha);
            yield return null;
        }

        // Change Text
        UpdateTextInstant();

        // Scale Punch
        transform.localScale = originalScale * scalePunchAmount;

        // Fade In + Smooth Scale Back
        t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;

            float alpha = Mathf.Lerp(0f, 1f, t / fadeDuration);
            textMesh.color = new Color(baseColor.r, baseColor.g, baseColor.b, alpha);

            transform.localScale = Vector3.Lerp(
                originalScale * scalePunchAmount,
                originalScale,
                t / fadeDuration
            );

            yield return null;
        }

        // Reset cleanly
        textMesh.color = baseColor;
        transform.localScale = originalScale;
    }
}