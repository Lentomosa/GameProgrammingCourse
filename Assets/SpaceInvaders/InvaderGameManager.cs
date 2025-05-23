using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class InvaderGameManager : MonoBehaviour
{

    public float enemyBulletTime = 1f;
    public float bulletMaxTime = 2f;
    public float bulletMinTime = 1f;
    float enemyBulletThreshold = 0f;
    public float enemyShieldTime = 0f;
    public float enemyShieldThreshold = 2f;

    public float ufoTime = 1;
    public bool ufoActive = false;
    public GameObject ufo;

    public bool gameOver = false;

    public int score;
    public int sessionScore;
    public int hiScore;
    public int lives = 3;
    public float invaderChangeTime = 1;

    public GameObject invaderSpawner;
    public GameObject scoreUI;
    public GameObject livesUI;
    public GameObject hiScoreUI;
    public GameObject retryButton;
    public GameObject menuButton;
    public GameObject gameOverText;

    public List<GameObject> myObjects;
    public GameObject[] invaders;

    // Start is called before the first frame update
    void Start()
    {

        score = PlayerPrefs.GetInt("Score");
        hiScore = PlayerPrefs.GetInt("HiScore");

        invaderChangeTime = PlayerPrefs.GetFloat("InvaderChangeTime");

        Debug.Log(PlayerPrefs.GetInt("Score", score));
        enemyBulletThreshold = Random.Range(bulletMinTime, bulletMaxTime);

        //ufo = GameObject.Find("Ufo");

        scoreUI.GetComponent<TextMeshProUGUI>().text = "Score: " + score.ToString();
        livesUI.GetComponent<TextMeshProUGUI>().text = "Lives: " + lives.ToString();
        hiScoreUI.GetComponent<TextMeshProUGUI>().text = "Hiscore: " + hiScore.ToString();
        retryButton.SetActive(false);
        gameOverText.SetActive(false);
        menuButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        enemyBulletTime += Time.deltaTime;
        enemyShieldTime += Time.deltaTime;

        
        if (enemyBulletTime >= enemyBulletThreshold)
        {


            GameObject[] invaders = GameObject.FindGameObjectsWithTag("Enemy");

            if (invaders.Length > 1)
            {


            GameObject invader = invaders[Random.Range(0, invaders.Length)];
            invader.GetComponent<InvaderScript>().Shoot();


            // Reset the timer.
            enemyBulletTime = 0f;

            // Generate a new random threshold for the next shot.
            enemyBulletThreshold = Random.Range(bulletMinTime, bulletMaxTime);
            }
        }

        

         if (enemyShieldTime >= enemyShieldThreshold)
         {


             GameObject[] invaders = GameObject.FindGameObjectsWithTag("Enemy");

             if (invaders.Length > 1)
            {
                 //GameObject invader = invaders[Random.Range(0, invaders.Length)];
                 //invader.GetComponent<InvaderScript>().canUseShield = true;


                 for (int i = 0; i < invaders.Length; i++)
             {
                 print("ShieldTest!");
                 invaders[i].GetComponent<InvaderScript>().ShieldTest();
                
             }

             // Reset the timer.
             enemyShieldTime = 0f;
             ActivateShields();
             }

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


    public void ActivateShields()
    {
        GameObject[] invaders = GameObject.FindGameObjectsWithTag("Enemy");
        //GameObject invader = invaders[Random.Range(0, invaders.Length)];
        //invader.GetComponent<InvaderScript>().canUseShield = true;



       if (invaders.Length > 1)

        {
            foreach (GameObject obj in invaders)
            {
                InvaderScript script = obj.GetComponent<InvaderScript>(); 

                if (script != null && !script.hasObstacle)
                {
                    myObjects.Add(obj);
                    //obj.GetComponent<InvaderScript>().canUseShield = true;
                    
                }
            }
            
       }

        if (myObjects != null)
        {
            int shieldListLenght = myObjects.Count;

            //if (shieldListLenght > 2)
            //{ 
            GameObject shieldInvader = myObjects[Random.Range(0, shieldListLenght)];
            shieldInvader.GetComponent<InvaderScript>().canUseShield = true;
            myObjects.Clear();
            //}
        }
    }

    public void ChooseInvader()
    {


    }

        public void AddScore(int scoreAmount)
    {
        score += scoreAmount;
        //hiScore += scoreAmount;
        //sessionScore += scoreAmount;
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

            if(score > hiScore)
            {
                hiScore = score;

            }
           


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
        menuButton.SetActive(true);

        invaderChangeTime = 1f;
        PlayerPrefs.SetFloat("InvaderChangeTime", invaderChangeTime);
        PlayerPrefs.Save();
    }

    public void UfoKilled()
    {
        ufoActive = false;
        ufoTime = 0;
    }

    public void InvaderKilled()
    {

        print("INVADERKILLED CALLED");
        invaders = GameObject.FindGameObjectsWithTag("Enemy");
        print(invaders);
        if (invaders.Length <= 1)
        {
            invaderSpawner.GetComponent<AlienSpawner>().InvaderSpawn();
            print("ALL DEAD");
            IncreaseDifficulty();
        }
    }

    public void IncreaseDifficulty()

    {
        if(invaderChangeTime > 0.4f)
        { 
        invaderChangeTime -= 0.2f;
        }
        PlayerPrefs.SetFloat("InvaderChangeTime", invaderChangeTime);

        GameObject[] invaders = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < invaders.Length; i++)
        {

            invaders[i].GetComponent<InvaderScript>().changeTime = invaderChangeTime;
            
        }
 

    }



}
