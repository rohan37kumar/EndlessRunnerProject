using UnityEngine;

public class SpawnTrigger : MonoBehaviour
{
    private TrackManager trackManager;
    private bool hasTriggered = false;

    private void Start()
    {
        trackManager = FindObjectOfType<TrackManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hasTriggered || !other.CompareTag("Player")) return;
        
        hasTriggered = true;
        float newZPosition = transform.parent.position.z + trackManager.spawnDistance;
        trackManager.SpawnTrack(newZPosition);
    }
} 