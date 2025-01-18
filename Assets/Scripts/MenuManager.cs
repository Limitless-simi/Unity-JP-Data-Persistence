using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEditor;

public class MenuManager : MonoBehaviour
{
    public TMP_InputField playerNameInput; // Input field for entering the player's name
    public TextMeshProUGUI bestScoreText; // Text field to display the highest score
    public TextMeshProUGUI HighScoreText;
    public TextMeshProUGUI pointHighScoreText;

    void Start()
    {
        LoadBestScore(); // Load the highest score when the game starts
    }

    public void StartGame()
    {
        // Save the player's name to PlayerPrefs for use in the game scene
        PlayerPrefs.SetString("CurrentPlayerName", playerNameInput.text);

        // Load the game scene (index 1 in the build settings)
        SceneManager.LoadScene(1);
    }

    public void LoadBestScore()
    {
        // Display the highest score and corresponding player name from MainGameManager
        bestScoreText.text = "Best Score: " + MainGameManager.Instance.bestScore + " Name: " + MainGameManager.Instance.playerName;
        HighScoreText.text = MainGameManager.Instance.playerName;
        pointHighScoreText.text = MainGameManager.Instance.bestScore.ToString();
    }

    public void Quit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
        //MainManager.Instance.SaveColor();
    }
}
