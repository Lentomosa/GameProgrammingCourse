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
    public float enemyBulletThreshold = 0f;

    public float shieldMaxTime = 2f;
    public float shieldMinTime = 1f;
    public float enemyShieldTime = 0f;
    public float enemyShieldThreshold = 2f;
    public float enemyShieldWarning = 0.5f;

    public float ufoTime = 1;
    public float ufoThreshold;
    public bool ufoActive = false;
    public float upgradeTime;
    public float upgradeThreshold;
    public bool upgradeActive = false;
    public float upgradeDuration = 2f;
    public float upgradeActiveTime;
    public bool playerHasUpgrade;


    public bool gameOver = false;
    public bool pauseMenuOpen = false;
    public bool gamePaused = false;

    public int score;
   // public int sessionScore;
    public int hiScore;
    public int lives = 3;
    public float invaderChangeTime = 1;
    //public float defaulInvaderChangeTime = 3;
    public int weaponTier = 0;

    public GameObject player;
    public GameObject invaderSpawner;
    public GameObject ufo;
    public GameObject scoreUI;
    public GameObject livesUI;
    public GameObject hiScoreUI;
    public GameObject retryButton;
    public GameObject menuButton;
    public GameObject continueButton;
    public GameObject gameOverText;
    public GameObject upgrade;

    public List<GameObject> shieldInvaders;
    public GameObject[] invaders;

    // Start is called before the first frame update
    void Start()
    {
        // Get Score and Hiscore
        score = 0;
        hiScore = PlayerPrefs.GetInt("HiScore");

        // Set first random bullet and shield thresholds
        enemyBulletThreshold = Random.Range(bulletMinTime, bulletMaxTime);
        enemyShieldThreshold = Random.Range(shieldMinTime, shieldMaxTime);

        // Set up UI
        scoreUI.GetComponent<TextMeshProUGUI>().text = "Score: " + score.ToString();
        livesUI.GetComponent<TextMeshProUGUI>().text = "Lives: " + lives.ToString();
        hiScoreUI.GetComponent<TextMeshProUGUI>().text = "Hiscore: " + hiScore.ToString();

        // Set menu elements inactive
        retryButton.SetActive(false);
        gameOverText.SetActive(false);
        menuButton.SetActive(false);
        continueButton.SetActive(false);

        //Find Upgrade
        // upgrade = GameObject.Find("WeaponUpgrade");

        // Spawn First wave of invader
        invaderSpawner.GetComponent<AlienSpawner>().InvaderSpawn();

    }

    // Update is called once per frame
    void Update()
    {


        // Stop bullet and shield timers when the game is paused
        if (!gamePaused)
        {
            enemyBulletTime += Time.deltaTime;
            enemyShieldTime += Time.deltaTime;

            // Update Ufo timer if Ufo is not active
            if (!ufoActive)
            {
                ufoTime += Time.deltaTime;
            }

            // Update Upgrade timer if Upgrade is not active
            if (!upgradeActive)

            {
                upgradeTime += Time.deltaTime;

            }

            if (playerHasUpgrade)
            {
                upgradeActiveTime += Time.deltaTime;

            }
        }

        // Check if bullet threshold has been reached
        if (enemyBulletTime >= enemyBulletThreshold)
        {

            // Find all invaders tagged Enemy
            GameObject[] invaders = GameObject.FindGameObjectsWithTag("Enemy");

            if (invaders.Length > 1)
            {

                // Pick a random invader to fire its weapon
                GameObject invader = invaders[Random.Range(0, invaders.Length)];
                invader.GetComponent<InvaderScript>().Shoot();


                // Reset the timer
                enemyBulletTime = 0f;

                // Generate a new random threshold for the next shot
                enemyBulletThreshold = Random.Range(bulletMinTime, bulletMaxTime);
            }
        }


        // Check if shield threshold has been reached
        if (enemyShieldTime >= enemyShieldThreshold)
        {


            GameObject[] invaders = GameObject.FindGameObjectsWithTag("Enemy");

            if (invaders.Length > 1)
            {


                // Order all found invaders to check if they have room to activate the shield
                for (int i = 0; i < invaders.Length; i++)
                {

                    print("ShieldTest!");
                    invaders[i].GetComponent<InvaderScript>().ShieldTest();

                }

                // Reset the timer
                enemyShieldTime = 0f;

                // Generate a new random threshold for the next shield
                enemyShieldThreshold = Random.Range(shieldMinTime, shieldMaxTime);

                // Do a new search for invaders after ShieldTest
                AddToShieldList();
            }

        }


        // Open and close pause menu with ESC
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!pauseMenuOpen)
            {
                PauseMenu();
                pauseMenuOpen = true;
            }
            else
            {
                Continue();
                pauseMenuOpen = false;
            }


        }




        // Activate Ufo if time has been reached and Ufo is not already active
        if (ufoTime >= ufoThreshold && !ufoActive)
        {
            ufo.GetComponent<UfoScript>().ActivateUfo();

            ufoTime = 0;
        }


        // Activate Upgrade if time has been reached and Upgrade is not already active

        if (upgradeTime >= upgradeThreshold && !upgradeActive)
        {
            UpgradeSpawn();
            upgradeTime = 0;

        }

        // Switch upgrade off after when time runs out

        if (upgradeActiveTime >= upgradeDuration)
        {
            player.GetComponent<PlayerController>().UpgradeHide();
            upgradeActiveTime = 0f;
            playerHasUpgrade = false;
            ReduceWeaponTier();
            upgrade.GetComponent<WeaponUpgrade>().LoadSounds();
        }


    }


    // Search for Enemies to be checked for Tag "Enemy" and boolean "hasObstacle"
    public void AddToShieldList()
    {
        GameObject[] invaders = GameObject.FindGameObjectsWithTag("Enemy");


        if (invaders.Length > 1)

        {
            // Add each invader with false hasObstacle boolean to shieldInvaders list
            foreach (GameObject obj in invaders)
            {
                InvaderScript script = obj.GetComponent<InvaderScript>();

                if (script != null && !script.hasObstacle)
                {
                    shieldInvaders.Add(obj);

                }
            }

        }


        if (shieldInvaders != null)
        {
            int shieldListLenght = shieldInvaders.Count;

            // Pick a random invader to activate its shield
            GameObject shieldInvader = shieldInvaders[Random.Range(0, shieldListLenght)];
            shieldInvader.GetComponent<InvaderScript>().canUseShield = true;
            shieldInvaders.Clear();

        }
    }


    // Add Score from the kill
    public void AddScore(int scoreAmount)

    {
        score += scoreAmount;
        scoreUI.GetComponent<TextMeshProUGUI>().text = "Score: " + score.ToString();
        Debug.Log(score);
        PlayerPrefs.SetInt("Score", score);

    }

    // Player loses a live
    public void LoseLive()
    {
        lives -= 1;
        livesUI.GetComponent<TextMeshProUGUI>().text = "Lives: " + lives.ToString();
        gamePaused = true;
        DisableEnemies();
        DisablePlayer();
        DisableUpgrades();

        // Check if player has no more lives
        if (lives <= 0)
        {

            if (score > hiScore)
            {
                hiScore = score;

            }

            // Call for GameOver
            GameOver();

        }

    }

    // Player Retries or returns to main menu

    public void StoreHighscore()

    {
        if (score > hiScore)
        {
            hiScore = score;
            PlayerPrefs.SetInt("HiScore", hiScore);
            PlayerPrefs.Save();
        }


    }

    // Reset Hiscore
    public void ResetHiscore()
    {
        PlayerPrefs.SetInt("HiScore", 0);
        PlayerPrefs.Save();
    }

    // Reset Score
    public void ResetScore()
    {
        PlayerPrefs.SetInt("Score", 0);
    }


    // Unpause the game
    public void Continue()
    {

        ClosePauseMenu();
        EnablePlayer();
        gamePaused = false;
        EnableEnemies();
        EnableUpgrades();
    }

    // Open Pause menu
    public void PauseMenu()
    {

        DisableEnemies();
        DisablePlayer();
        DisableUpgrades();
        continueButton.SetActive(true);
        retryButton.SetActive(true);
        menuButton.SetActive(true);
        gamePaused = true;

    }

    // Close Pause menu
    public void ClosePauseMenu()
    {
        pauseMenuOpen = false;
        gameOverText.SetActive(false);
        retryButton.SetActive(false);
        menuButton.SetActive(false);
        continueButton.SetActive(false);
    }

    // Game over
    public void GameOver()
    {

        score = 0;
        PlayerPrefs.SetInt("Score", score);
        PlayerPrefs.SetInt("HiScore", hiScore);
        scoreUI.GetComponent<TextMeshProUGUI>().text = "Score: " + score.ToString();
        hiScoreUI.GetComponent<TextMeshProUGUI>().text = "HiScore: " + hiScore.ToString();

        gameOverText.SetActive(true);
        retryButton.SetActive(true);
        menuButton.SetActive(true);


        DisableEnemies();
        DisablePlayer();
        DisableUpgrades();
        ResetScore();


        PlayerPrefs.Save();
    }

    // Disable player and player bullets
    public void DisablePlayer()
    {
        player.GetComponent<PlayerController>().canMove = false;
        player.GetComponent<PlayerController>().canDamage = false;
        player.GetComponent<PlayerController>().canShoot = false;

        GameObject[] playerBullets = GameObject.FindGameObjectsWithTag("PlayerBullet");
        for (int i = 0; i < playerBullets.Length; i++)
        {
            playerBullets[i].GetComponent<PlayerBullet>().canMove = false;

        }

    }

    // Enable player and player bullets
    public void EnablePlayer()
    {
        player.GetComponent<PlayerController>().canMove = true;
        player.GetComponent<PlayerController>().canDamage = true;
        player.GetComponent<PlayerController>().canShoot = true;

        GameObject[] playerBullets = GameObject.FindGameObjectsWithTag("PlayerBullet");
        for (int i = 0; i < playerBullets.Length; i++)
        {
            playerBullets[i].GetComponent<PlayerBullet>().canMove = true;

        }

    }

    // Disable enemies and enemy bullets
    public void DisableEnemies()
    {
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

        GameObject[] invaderBullets = GameObject.FindGameObjectsWithTag("EnemyBullet");
        for (int i = 0; i < invaderBullets.Length; i++)
        {
            invaderBullets[i].GetComponent<EnemyBullet>().canMove = false;

        }
    }

    // Enable enemies and enemy bullets
    public void EnableEnemies()
    {
        GameObject[] invaders = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] ufos = GameObject.FindGameObjectsWithTag("Ufo");
        for (int i = 0; i < invaders.Length; i++)
        {
            invaders[i].GetComponent<InvaderScript>().canMove = true;
            //invaders[i].GetComponent<InvaderScript>().changeTime += 0.4f;
            invaders[i].GetComponent<InvaderScript>().canShoot = true;

        }

        for (int i = 0; i < ufos.Length; i++)
        {
            ufos[i].GetComponent<UfoScript>().canMove = true;
        }

        GameObject[] invaderBullets = GameObject.FindGameObjectsWithTag("EnemyBullet");

        for (int i = 0; i < invaderBullets.Length; i++)
        {
            invaderBullets[i].GetComponent<EnemyBullet>().canMove = true;

        }

    }

    public void DisableUpgrades()
    {
        upgrade.GetComponent<WeaponUpgrade>().canMove = false;

    }

    public void EnableUpgrades()
    {
        upgrade.GetComponent<WeaponUpgrade>().canMove = true;

    }

    // Called from UfoScript when the ufo is set inactive
    public void UfoKilled()
    {
        ufoActive = false;
        ufoTime = 0;
    }

    // Check if there are any invaders left after an invader is killed
    public void InvaderKilled()
    {

        print("INVADERKILLED CALLED");
        invaders = GameObject.FindGameObjectsWithTag("Enemy");
        print(invaders);

        // Spawn a new wave of invaders if no invaders are found
        if (invaders.Length <= 1)
        {
            invaderSpawner.GetComponent<AlienSpawner>().InvaderSpawn();
            print("ALL DEAD");
            IncreaseDifficulty();
        }
    }

    public void UpgradeSpawn()
    {

        print("UPGRADE CALLED");

        upgradeActive = true;
        upgrade.GetComponent<WeaponUpgrade>().ActivateUpgrade();


    }

    public void IncreaseWeaponTier()
    {
        weaponTier += 1;
        print("Upgraded");

    }

    public void ReduceWeaponTier()
    {
        weaponTier -= 1;
        print("Upgrade End");

    }


    // Increase difficulty of the game
    public void IncreaseDifficulty()

    {
        if(invaderChangeTime > 0.2f)
        { 
        invaderChangeTime -= 0.1f;
        }

        // Reduce invader movement interval
        GameObject[] invaders = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < invaders.Length; i++)
        {

            invaders[i].GetComponent<InvaderScript>().changeTime = invaderChangeTime;
            
        }

        // Reduce shield and bullet times
        if (shieldMaxTime > 0.4f && shieldMinTime > 0.4f && bulletMaxTime > 0.4f && bulletMinTime > 0.4f)
        { 
        shieldMaxTime -= 0.2f;
        shieldMinTime -= 0.2f;
        bulletMaxTime -= 0.2f;
        bulletMinTime -= 0.2f;
        }

    }





}
