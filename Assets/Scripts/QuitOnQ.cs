using UnityEngine;

public class QuitOnQ : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            QuitGame();
        }
    }

    void QuitGame()
    {
        Debug.Log("Quitting game...");

        Application.Quit();

        // This makes it also stop play mode in Unity Editor
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}