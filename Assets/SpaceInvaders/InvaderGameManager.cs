using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InvaderGameManager : MonoBehaviour
{

    public float enemyBulletTime = 1f;
    float enemyBulletThreshold = 0f;
    public float enemyShieldTime = 0f;
    float enemyShieldThreshold = 2f;

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

    // Start is called before the first frame update
    void Start()
    {
        enemyBulletThreshold = Random.Range(3f, 8f);

        scoreUI.GetComponent<TextMeshProUGUI>().text = "Score: " + score.ToString();
        livesUI.GetComponent<TextMeshProUGUI>().text = "Lives: " + lives.ToString();
        hiScoreUI.GetComponent<TextMeshProUGUI>().text = "Hiscore: " + hiScore.ToString();
        retryButton.SetActive(false);
        gameOverText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        enemyBulletTime += Time.deltaTime;
        enemyShieldTime += Time.deltaTime;

        if (enemyBulletTime >= enemyBulletThreshold)
        {
            GameObject[] invaders = GameObject.FindGameObjectsWithTag("Enemy");
            GameObject invader = invaders[Random.Range(0, invaders.Length)];
            invader.GetComponent<InvaderScript>().Shoot();


            // Reset the timer.
            enemyBulletTime = 0f;

            // Generate a new random threshold for the next shot.
            enemyBulletThreshold = Random.Range(3f, 8f);
        }

        if (enemyShieldTime >= enemyShieldThreshold)
        {
            GameObject[] invaders = GameObject.FindGameObjectsWithTag("Enemy");
            GameObject invader = invaders[Random.Range(0, invaders.Length)];
            invader.GetComponent<InvaderScript>().canUseShield = true;

            // Reset the timer.
            enemyShieldTime = 0f;



        }
    }

    public void AddScore()
    {
        score += 100;
        sessionScore += 100;
        scoreUI.GetComponent<TextMeshProUGUI>().text = "Score: " + score.ToString();
    }

    public void LoseLife()
    {
        lives -= 1;
        livesUI.GetComponent<TextMeshProUGUI>().text = "Lives: " + lives.ToString();
        if (lives <= 0)
        {

            //GameOver();
        }

    }
}
