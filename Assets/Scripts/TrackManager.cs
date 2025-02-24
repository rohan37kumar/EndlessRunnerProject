using UnityEngine;

public class TrackManager : MonoBehaviour
{
    public GameObject trackPrefab;
    public GameObject player;
    public float trackSpeed = 10f;
    public float spawnDistance = 95f;

    private void Start()
    {
        
    }

    private void Update()
    {
        if (!GameManager.Instance.IsGameRunning()) return;
        
        MoveTracksBackward();
    }

    private void MoveTracksBackward()
    {
        foreach (Transform track in transform)
        {
            track.Translate(Vector3.back * trackSpeed * Time.deltaTime);
        }
    }

    public void SpawnTrack(float zPosition)
    {
        Vector3 spawnPos = new Vector3(0, 0, zPosition);
        GameObject newTrack = Instantiate(trackPrefab, spawnPos, Quaternion.identity, transform);
        //Debug.Log("spawned track");
        AutoDestroy autoDestroy = newTrack.AddComponent<AutoDestroy>();
        autoDestroy.player = player;
    }
} 