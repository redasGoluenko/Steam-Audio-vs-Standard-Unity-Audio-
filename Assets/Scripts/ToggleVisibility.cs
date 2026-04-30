using UnityEngine;

public class VisibilityToggle : MonoBehaviour
{
    [SerializeField] private bool isVisible = true;
    [SerializeField] private Renderer targetRenderer;

    void Update()
    {
        if (targetRenderer != null)
        {
            targetRenderer.enabled = isVisible;
        }
    }
}