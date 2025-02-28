using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    public GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        //gm = Camera.main.GetComponent<GameManager>();
        gm = GameObject.Find("GameManagerObject").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.name == "Ground")
        {
            //gm.lives -= 1;
            //gm.CheckLives();
            gm.LoseLife();
            Destroy(gameObject);
        }
    }
}
