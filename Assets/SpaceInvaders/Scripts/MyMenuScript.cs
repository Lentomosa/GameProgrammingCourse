using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MyMenuScript : MonoBehaviour
{
    public GameObject gameManager;
    public bool mainMenu = true;
    // Start is called before the first frame update
    void Start()
    {
        if (mainMenu)
        {
            ResetScore();
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Go to level
    public void GoToLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    // Quit game
    public void ExitGame()
    {
        Application.Quit();
    }

    // Continue the game
    public void Continue()
    {
        gameManager.GetComponent<InvaderGameManager>().Continue();
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
}
