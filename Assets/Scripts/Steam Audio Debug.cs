using UnityEngine;
using SteamAudio;
using System.Reflection;

public class SteamAudioDebug : MonoBehaviour
{
    void Start()
    {
        var geometries = FindObjectsOfType<SteamAudioGeometry>();
        Debug.Log("Found " + geometries.Length + " SteamAudioGeometry component(s)");
        foreach (var geo in geometries)
        {
            Debug.Log("Geometry on: " + geo.gameObject.name +
                      " | exportAllChildren: " + geo.exportAllChildren);
        }

        var manager = FindObjectOfType<SteamAudioManager>();
        Debug.Log("SteamAudioManager in scene: " + (manager != null));

        // Check if Steam Audio's internal scene has been committed
        FieldInfo sceneField = typeof(SteamAudioManager).GetField("mScene",
            BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);
        if (sceneField != null)
        {
            var scene = sceneField.GetValue(manager);
            Debug.Log("Steam Audio internal scene: " + (scene != null ? scene.ToString() : "NULL"));
        }
        else
        {
            Debug.Log("Could not find mScene field - listing all fields:");
            FieldInfo[] fields = typeof(SteamAudioManager).GetFields(
                BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);
            foreach (var f in fields)
                Debug.Log("SteamAudioManager field: " + f.Name + " | type: " + f.FieldType);
        }
    }
}