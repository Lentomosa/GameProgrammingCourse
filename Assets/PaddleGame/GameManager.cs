using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool gameOver = false;

    public int score;
    public int sessionScore;
    public int hiScore;
    public int lives = 3;

    public GameObject scoreUI;
    public GameObject livesUI;
    public GameObject hiScoreUI;
    public GameObject retryButton;
    public GameObject gameOverText;

    //public GameObject ball;
    public GameObject[] ballsInGame;
    public int newBallScore;

    //public GameObject[] activeBalls;

    public BallSpawnerScript bs;
    public bool canSpawn = true;

    // Start is called before the first frame update
    void Start()
    {
        bs = GameObject.Find("BallSpawner").GetComponent<BallSpawnerScript>();
        ballsInGame = GameObject.FindGameObjectsWithTag("Ball");
        scoreUI.GetComponent<TextMeshProUGUI>().text = "Score: " + score.ToString();
        livesUI.GetComponent<TextMeshProUGUI>().text = "Lives: " + lives.ToString();
        hiScoreUI.GetComponent<TextMeshProUGUI>().text = "Hiscore: " + hiScore.ToString();
        retryButton.SetActive(false);
        gameOverText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        

       /* if(!gameOver)
        { 

            if(sessionScore == 300 && canSpawn == true)
            {
            bs.Spawn();
            canSpawn = false;
            }

            if (sessionScore == 500 && canSpawn == false)
            {
            bs.Spawn();
            canSpawn = true;

            }
            if (sessionScore == 1000 && canSpawn == true)
            {
            bs.Spawn();
            canSpawn = false;
            }
        } */
    }

    public void CheckLives()
    {
        lives--;
        sessionScore = 0;

        //activeBalls = GameObject.FindGameObjectsWithTag("Ball");

        /*if (activeBalls.Length == 0)
        {
            bs.Spawn();
        }
        */
        if(lives <= 0)
        {
            gameOver = true;
        }
    }

    public void LoseLife()
    {
        lives -= 1;

        if (lives <= 0)
        {
            GameOver();
        }

        ballsInGame = GameObject.FindGameObjectsWithTag("Ball");
        int ballsLeft = ballsInGame.Length;
        print(ballsLeft);

        livesUI.GetComponent<TextMeshProUGUI>().text = "Lives: " + lives.ToString();

        if (ballsLeft <= 1 && lives >=1)
        {
            bs.Spawn();

        }
    }

    public void Retry()
    {
        lives = 3;
        score = 0;
        bs.Spawn();
        retryButton.SetActive(false);
        scoreUI.GetComponent<TextMeshProUGUI>().text = "Score: " + score.ToString();
        livesUI.GetComponent<TextMeshProUGUI>().text = "Lives: " + lives.ToString();
        hiScoreUI.GetComponent<TextMeshProUGUI>().text = "Hiscore: " + hiScore.ToString();
        gameOverText.SetActive(false);
    }

    public void GameOver()
    {
        // Find remaining balls and destroy them
        ballsInGame = GameObject.FindGameObjectsWithTag("Ball");

        for (int i=0; i<ballsInGame.Length; i++)
        {
            Destroy(ballsInGame[i]);
        }

        print("Game Over");

        retryButton.SetActive(true);
        gameOverText.SetActive(true);

        if (score > hiScore)
        {
            hiScore = score;
            score = 0;
            hiScoreUI.GetComponent<TextMeshProUGUI>().text = "Hiscore: " + hiScore.ToString();
        }

    }

    public void AddScore()
    {
        score += 100;
        sessionScore += 100;
        newBallScore += 100;
        scoreUI.GetComponent<TextMeshProUGUI>().text = "Score: " + score.ToString();
    }


}
