using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnAnyKey : MonoBehaviour
{
    [SerializeField] private string sceneName; // set this in Inspector

    void Update()
    {
        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}