using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;  // Text component for displaying current score
    public Text ScoreText1;  // Text component for displaying high score
   // public Text ScoreText;
   // public Text highScoreText;
    public GameObject GameOverText;
   // public string playername;
    
    private bool m_Started = false;
    private int m_Points;
    public int highScore = 0;
    
    private bool m_GameOver = false;

    private void Awake()
    {
        //ScoreText.text = NameManager.Instance.playerName + "'s " + "Score :";
        //playername = NameManager.Instance.playerName;
    }
    // Start is called before the first frame update
    void Start()
    {
        LoadHighScore();  // Load the high score from MainGameManager
        ScoreText1.text = $"Best Score : {MainGameManager.Instance.playerName} : {MainGameManager.Instance.bestScore}";

        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
       
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = PlayerPrefs.GetString("CurrentPlayerName", "Player" ) + "'s " + $"Score : {m_Points}";
        if (m_Points > MainGameManager.Instance.bestScore)
        {
            MainGameManager.Instance.bestScore = m_Points;
            MainGameManager.Instance.playerName = PlayerPrefs.GetString("CurrentPlayerName", "Player"); // Update high score holder's name
            MainGameManager.Instance.SaveNameAndScore();
            UpdateHighScoreDisplay();
        }
        // NameManager.Instance.highScore = "Best Score: " + ScoreText.text;
        //NameManager.Instance.highScore = "Best Score: " + NameManager.Instance.playerName + m_Points;
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);
        UpdateHighScoreDisplay();
    }

    void LoadHighScore()
    {
        // Load high score from MainGameManager
        MainGameManager.Instance.LoadNameAndScore();
        UpdateHighScoreDisplay();
    }

    void UpdateHighScoreDisplay()
    {
        // Update UI for score and high score
        ScoreText.text = $"Score : {m_Points}";
    }
}
