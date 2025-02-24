using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public UIManager uiManager;

    private int currentRunCoins;
    private bool isGameRunning = false;
    private const string HIGH_SCORE_KEY = "HighScore";
    private const string TOTAL_COINS_KEY = "TotalCoins";
    private const int GAME_START_COST = 5;
    private const int INITIAL_COINS = 100;

    private void Awake()
    {
        Instance = this;
        InitializeTotalCoins();
    }

    private void InitializeTotalCoins()
    {
        if (!PlayerPrefs.HasKey(TOTAL_COINS_KEY))
        {
            PlayerPrefs.SetInt(TOTAL_COINS_KEY, INITIAL_COINS);
        }
    }

    public void StartGame()
    {
        if (GetTotalCoins() >= GAME_START_COST)
        {
            DeductCoins(GAME_START_COST);
            Time.timeScale = 1f;
            currentRunCoins = 0;
            ResetPlayerPosition();
            uiManager.UpdateScore(currentRunCoins);
            uiManager.UpdateTotalCoins(GetTotalCoins());
            isGameRunning = true;
            FindObjectOfType<PlayerController>().StartRunning();
        }
        else
        {
            uiManager.ShowInsufficientCoinsMessage();
        }
    }

    public void GameOver()
    {
        isGameRunning = false;
        Time.timeScale = 0f;
        
        AddToTotalCoins(currentRunCoins);
        
        int currentHighScore = PlayerPrefs.GetInt(HIGH_SCORE_KEY, 0);
        if (currentRunCoins > currentHighScore)
        {
            currentHighScore = currentRunCoins;
            PlayerPrefs.SetInt(HIGH_SCORE_KEY, currentHighScore);
            PlayerPrefs.Save();
        }
        
        uiManager.ShowGameOver(currentRunCoins, currentHighScore);
        uiManager.UpdateTotalCoins(GetTotalCoins());
    }

    public bool IsGameRunning() => isGameRunning;

    public void IncrementScore()
    {
        currentRunCoins++;
        uiManager.UpdateScore(currentRunCoins);
    }

    private void DeductCoins(int amount)
    {
        int currentTotal = GetTotalCoins();
        PlayerPrefs.SetInt(TOTAL_COINS_KEY, currentTotal - amount);
        PlayerPrefs.Save();
    }

    private void AddToTotalCoins(int amount)
    {
        int currentTotal = GetTotalCoins();
        PlayerPrefs.SetInt(TOTAL_COINS_KEY, currentTotal + amount);
        PlayerPrefs.Save();
    }

    public int GetTotalCoins()
    {
        return PlayerPrefs.GetInt(TOTAL_COINS_KEY, INITIAL_COINS);
    }

    private void ResetPlayerPosition()
    {
        PlayerController player = FindObjectOfType<PlayerController>();
        if (player != null)
        {
            player.ResetPosition();
        }
    }
}
