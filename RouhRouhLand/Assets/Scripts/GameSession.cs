using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class GameSession : MonoBehaviour
{

    [SerializeField] int playerLives = 3;
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI grainsText;

    [SerializeField] int enemyScorePoint = 5;


    public int totalScore = 0;
    public int totalGrains = 0;

    // Start is called before the first frame update
    void Awake()
    {
        int numOfGameSessions = FindObjectsOfType<GameSession>().Length;
        if(numOfGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        livesText.text = playerLives.ToString();
        scoreText.text = totalScore.ToString();
        grainsText.text = totalGrains.ToString();

    }

    public void UpdatePlayerTotalScore(int grainPoint, int enemyKillingPoint)
    {
        if(grainPoint > 0) { totalGrains++; }
        totalScore += (grainPoint + enemyKillingPoint);
        scoreText.text = totalScore.ToString();
        grainsText.text = totalGrains.ToString();


    }

    public void ProcessPlayerDeath()
    {
        if(playerLives > 1)
        {
            TakeLife();
        }
        else
        {
            totalScore = 0;
            ResetGameSession();
        }
    }

    private void TakeLife()
    {
        playerLives--;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        livesText.text = playerLives.ToString();

    }

    private void ResetGameSession()
    {
        FindObjectOfType<ScenePersist>().ResetScenePersist();
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }
}
