using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PaddleScript : MonoBehaviour

    
{

    public float bounceSpeed = 20f;
    public GameManager gm;
    public BallSpawnerScript bs;

    private void Awake()
    {
        //gm = Camera.main.GetComponent<GameManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        bs = GameObject.Find("BallSpawner").GetComponent<BallSpawnerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Ball"))
        {
            // other.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            other.gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * bounceSpeed);

            gm.AddScore();

            if (gm.newBallScore == 500)
            {
                bs.Spawn();
                gm.newBallScore = 0;
            }
        }

    }
}
