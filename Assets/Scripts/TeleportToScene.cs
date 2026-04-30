using UnityEngine;
using UnityEngine.SceneManagement; // Required for switching scenes

public sealed class TeleportToScene : MonoBehaviour
{
    [Header("Settings")]
    public string sceneToLoad;      // The exact name of your next scene
    public float detectRange = 2.0f; // How close the player needs to be
    public Transform player;        // Drag your Player/Capsule here

    private void Update()
    {
        if (player == null) return;

        // Calculate the distance between this object and the player
        float distance = Vector3.Distance(transform.position, player.position);

        // If the player enters the "creep zone," switch scenes
        if (distance <= detectRange)
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }

    // Visualizes the "teleport zone" in the editor so you can see the range
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectRange);
    }
}