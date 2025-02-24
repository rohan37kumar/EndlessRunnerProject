using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class UIManager : MonoBehaviour
{
    public Button startButton;
    public Button walletButton;
    public Button homeButton;
    public GameObject currentScoreObject;
    public Text currentScoreText;
    public GameObject gameOverPanel;
    public Text gameOverScoreText;
    public Text highScoreText;
    public Text totalCoinsText;
    public GameObject insufficientCoinsMessage;

    private const float GAME_OVER_DISPLAY_DURATION = 10f;
    private Coroutine gameOverCoroutine;

    private void Start()
    {
        Time.timeScale = 1f;  // Also reset time scale on start
        startButton.onClick.AddListener(StartGame);
        walletButton.onClick.AddListener(OpenWalletScene);
        homeButton.onClick.AddListener(ReturnToHome);
        insufficientCoinsMessage.gameObject.SetActive(false);
        gameOverPanel.SetActive(false);
        currentScoreObject.SetActive(false);
        homeButton.gameObject.SetActive(false);
        UpdateTotalCoins(GameManager.Instance.GetTotalCoins());
    }

    private void StartGame()
    {
        GameManager.Instance.StartGame();
        SetGameplayUI(true);
    }

    private void OpenWalletScene()
    {
        SceneManager.LoadScene("Web3Scene");
    }

    private void ReturnToHome()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ShowGameOver(int currentScore, int highScore)
    {
        SetGameplayUI(false);
        
        gameOverPanel.SetActive(true);
        gameOverScoreText.text = "Coins Collected: " + currentScore;
        highScoreText.text = "Best Run: " + highScore;

        if (gameOverCoroutine != null)
        {
            StopCoroutine(gameOverCoroutine);
        }
        gameOverCoroutine = StartCoroutine(HideGameOverAfterDelay());
    }

    private void SetGameplayUI(bool isGameplay)
    {
        startButton.gameObject.SetActive(false);
        walletButton.gameObject.SetActive(!isGameplay);
        currentScoreObject.SetActive(isGameplay);
        homeButton.gameObject.SetActive(!isGameplay);
        
        if (!isGameplay)
        {
            gameOverPanel.SetActive(false);
        }
    }

    private IEnumerator HideGameOverAfterDelay()
    {
        yield return new WaitForSeconds(GAME_OVER_DISPLAY_DURATION);
        gameOverPanel.SetActive(false);
    }

    public void UpdateScore(int newScore)
    {
        currentScoreText.text = newScore.ToString();
    }

    public void UpdateTotalCoins(int coins)
    {
        totalCoinsText.text = "Coins: " + coins;
    }

    public void ShowInsufficientCoinsMessage()
    {
        StartCoroutine(ShowMessageTemporarily());
    }

    private IEnumerator ShowMessageTemporarily()
    {
        insufficientCoinsMessage.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        insufficientCoinsMessage.gameObject.SetActive(false);
    }
}
