using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InvaderGameManager : MonoBehaviour
{

    public float enemyBulletTime = 1f;
    public float bulletMaxTime = 2f;
    public float bulletMinTime = 1f;
    float enemyBulletThreshold = 0f;
    public float enemyShieldTime = 0f;
    float enemyShieldThreshold = 2f;

    public float ufoTime = 1;
    public bool ufoActive = false;
    public GameObject ufo;

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

        score = PlayerPrefs.GetInt("Score");
        hiScore = PlayerPrefs.GetInt("HiScore");
        Debug.Log(PlayerPrefs.GetInt("Score", score));
        enemyBulletThreshold = Random.Range(bulletMinTime, bulletMaxTime);

        //ufo = GameObject.Find("Ufo");

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
            //invader.GetComponent<InvaderScript>().Shoot();


            // Reset the timer.
            enemyBulletTime = 0f;

            // Generate a new random threshold for the next shot.
            enemyBulletThreshold = Random.Range(bulletMinTime, bulletMaxTime);
        }

        if (enemyShieldTime >= enemyShieldThreshold)
        {
            GameObject[] invaders = GameObject.FindGameObjectsWithTag("Enemy");
            //GameObject invader = invaders[Random.Range(0, invaders.Length)];
            //invader.GetComponent<InvaderScript>().canUseShield = true;


            for (int i = 0; i < invaders.Length; i++)
            {

                invaders[i].GetComponent<InvaderScript>().ShieldTest();

            }


            // Reset the timer.
            enemyShieldTime = 0f;



        }

        if (!ufoActive)
        {
            ufoTime += Time.deltaTime;
        }

        if (ufoTime >= 5 && !ufoActive)
        {
            ufo.GetComponent<UfoScript>().ActivateUfo();
            ufoTime = 0;
        }
    }

    public void AddScore(int scoreAmount)
    {
        score += scoreAmount;
        hiScore += scoreAmount;
        sessionScore += scoreAmount;
        scoreUI.GetComponent<TextMeshProUGUI>().text = "Score: " + score.ToString();
        Debug.Log(score);
        PlayerPrefs.SetInt("Score", score);

    }

    public void LoseLife()
    {
        lives -= 1;
        livesUI.GetComponent<TextMeshProUGUI>().text = "Lives: " + lives.ToString();

        GameObject[] invaders = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] ufos = GameObject.FindGameObjectsWithTag("Ufo");
        for (int i = 0; i < invaders.Length; i++)
        {
            invaders[i].GetComponent<InvaderScript>().canMove = false;
            invaders[i].GetComponent<InvaderScript>().canShoot = false;
            
        }

        for (int i = 0; i < ufos.Length; i++)
        {
            ufos[i].GetComponent<UfoScript>().canMove = false;
        }


        if (lives <= 0)
        {
            score = 0;
            PlayerPrefs.SetInt("Score", score);
            PlayerPrefs.SetInt("HiScore", hiScore);
            scoreUI.GetComponent<TextMeshProUGUI>().text = "Score: " + score.ToString();
            hiScoreUI.GetComponent<TextMeshProUGUI>().text = "HiScore: " + hiScore.ToString();
            //PlayerPrefs.Save();
            GameOver();

            //SceneManager.LoadScene("MenuLevel");
        }

    }

    public void Continue()
    {

        GameObject[] invaders = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] ufos = GameObject.FindGameObjectsWithTag("Ufo");
        for (int i = 0; i < invaders.Length; i++)
        {
            invaders[i].GetComponent<InvaderScript>().canMove = true;
            invaders[i].GetComponent<InvaderScript>().changeTime += 0.4f;
            invaders[i].GetComponent<InvaderScript>().canShoot = true;
           
        }

        for (int i = 0; i < ufos.Length; i++)
        {
            ufos[i].GetComponent<UfoScript>().canMove = true;
        }
    }

    public void GameOver()
    {
        gameOverText.SetActive(true);
        retryButton.SetActive(true);

    }

    public void UfoKilled()
    {
        ufoActive = false;
        ufoTime = 0;
    }


}
