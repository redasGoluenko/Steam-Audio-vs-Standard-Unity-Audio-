using UnityEngine;
using SteamAudio;
using System.Reflection;

public class SteamAudioSimulationRunner : MonoBehaviour
{
    SteamAudioSource[] sources;
    MethodInfo scheduleCommitScene;
    float logTimer = 0f;

    void Start()
    {
        sources = FindObjectsOfType<SteamAudioSource>();

        var manager = SteamAudioManager.Singleton;

        scheduleCommitScene = typeof(SteamAudioManager).GetMethod("ScheduleCommitScene",
            BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);

        if (scheduleCommitScene != null)
        {
            scheduleCommitScene.Invoke(manager, null);
            Debug.Log("ScheduleCommitScene invoked");
        }
    }

    void Update()
    {
        if (SteamAudioManager.Simulator == null) return;

        CoordinateSpace3 listenerCoords = new CoordinateSpace3();
        listenerCoords.right = Common.ConvertVector(Camera.main.transform.right);
        listenerCoords.up = Common.ConvertVector(Camera.main.transform.up);
        listenerCoords.ahead = Common.ConvertVector(Camera.main.transform.forward);
        listenerCoords.origin = Common.ConvertVector(Camera.main.transform.position);

        SimulationSharedInputs sharedInputs = new SimulationSharedInputs();
        sharedInputs.listener = listenerCoords;
        sharedInputs.numRays = 128;
        sharedInputs.numBounces = 4;
        sharedInputs.duration = 1.0f;
        sharedInputs.order = 1;
        sharedInputs.irradianceMinDistance = 1.0f;

        SteamAudioManager.Simulator.SetSharedInputs(SimulationFlags.Direct, sharedInputs);

        foreach (var source in sources)
            source.SetInputs(SimulationFlags.Direct);

        SteamAudioManager.Simulator.RunDirect();
        SteamAudioManager.Simulator.Commit();

        foreach (var source in sources)
            source.UpdateOutputs(SimulationFlags.Direct);

        logTimer += Time.deltaTime;
        if (logTimer >= 1f)
        {
            logTimer = 0f;
            foreach (var source in sources)
                Debug.Log("Source: " + source.gameObject.name + " | OcclusionValue: " + source.occlusionValue);
        }
    }
}