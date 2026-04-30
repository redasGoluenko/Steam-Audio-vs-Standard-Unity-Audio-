using UnityEngine;

public class SlidingDoubleDoorWithDopplerGate : MonoBehaviour
{
    [Header("Doors")]
    public Transform leftDoor;
    public Transform rightDoor;

    [Header("Settings")]
    public Transform player;
    public float openDistance = 3f;
    public float slideAmount = 2f;
    public float speed = 3f;

    [Header("Door Audio")]
    public AudioSource doorAudio;        // sliding door sound
    public float doorFadeSpeed = 3f;     // fade in/out speed
    public float doorMaxVolume = 1f;

    [Header("Doppler Audio Behind Doors")]
    public AudioSource dopplerAudio;     // spaceship Doppler sound
    public float dopplerMaxVolume = 1f;
    public float dopplerFadeSpeed = 3f;

    private Vector3 leftClosedPos;
    private Vector3 rightClosedPos;
    private Vector3 leftOpenPos;
    private Vector3 rightOpenPos;

    private bool isOpen = false;

    void Start()
    {
        leftClosedPos = leftDoor.position;
        rightClosedPos = rightDoor.position;

        leftOpenPos = leftClosedPos + new Vector3(-slideAmount, 0f, 0f);
        rightOpenPos = rightClosedPos + new Vector3(slideAmount, 0f, 0f);

        if (doorAudio) doorAudio.volume = 0f;
        if (dopplerAudio) dopplerAudio.volume = 0f;
    }

    void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);
        bool shouldBeOpen = distance <= openDistance;

        // Move doors
        leftDoor.position = Vector3.MoveTowards(leftDoor.position, shouldBeOpen ? leftOpenPos : leftClosedPos, speed * Time.deltaTime);
        rightDoor.position = Vector3.MoveTowards(rightDoor.position, shouldBeOpen ? rightOpenPos : rightClosedPos, speed * Time.deltaTime);

        // Check if doors are moving
        bool doorsMoving = (Vector3.Distance(leftDoor.position, shouldBeOpen ? leftOpenPos : leftClosedPos) > 0.01f) ||
                           (Vector3.Distance(rightDoor.position, shouldBeOpen ? rightOpenPos : rightClosedPos) > 0.01f);

        // Handle Door Audio (plays only while sliding)
        if (doorAudio)
        {
            if (doorsMoving)
            {
                if (!doorAudio.isPlaying) doorAudio.Play();
                doorAudio.volume = Mathf.Lerp(doorAudio.volume, doorMaxVolume, Time.deltaTime * doorFadeSpeed);
            }
            else
            {
                doorAudio.volume = Mathf.Lerp(doorAudio.volume, 0f, Time.deltaTime * doorFadeSpeed);
                if (doorAudio.volume < 0.01f) doorAudio.Stop();
            }
        }

        // Handle Doppler Audio (fade based on door openness)
        if (dopplerAudio)
        {
            float leftPercent = Mathf.InverseLerp(leftClosedPos.x, leftOpenPos.x, leftDoor.position.x);
            float rightPercent = Mathf.InverseLerp(rightClosedPos.x, rightOpenPos.x, rightDoor.position.x);
            float openness = (leftPercent + rightPercent) / 2f;

            float targetVolume = openness * dopplerMaxVolume;
            dopplerAudio.volume = Mathf.Lerp(dopplerAudio.volume, targetVolume, Time.deltaTime * dopplerFadeSpeed);

            if (!dopplerAudio.isPlaying && targetVolume > 0f)
                dopplerAudio.Play();
            else if (targetVolume == 0f && dopplerAudio.isPlaying && dopplerAudio.volume < 0.01f)
                dopplerAudio.Stop();
        }

        isOpen = shouldBeOpen;
    }
}