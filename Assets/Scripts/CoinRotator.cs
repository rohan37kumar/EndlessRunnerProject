using UnityEngine;

public class CoinRotator : MonoBehaviour
{
    public float rotationSpeed = 100f;

    private void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.IncrementScore();
            Destroy(gameObject);
        }
    }
} 