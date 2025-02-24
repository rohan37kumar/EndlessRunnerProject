using UnityEngine;
using UnityEngine.SceneManagement;

public class Web3Manager : MonoBehaviour
{
    public static Web3Manager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void ReturnToMainScene()
    {
        SceneManager.LoadScene("MainScene");
    }

    // Additional Web3 related functions will be added here later
}
