using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    public GameObject player;
    private float destroyDistance = 200f;

    private void Update()
    {
        if (player == null) return;
        
        float distance = Mathf.Abs(transform.position.z - player.transform.position.z);
        if (distance > destroyDistance)
        {
            Destroy(gameObject);
        }
    }
} 