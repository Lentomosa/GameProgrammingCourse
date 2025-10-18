using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UfoScript : MonoBehaviour
{
    public int HP = 1;
    public float speed = 2f;
    public bool canMove = true;
    public GameObject gameManager;
    public GameObject ufoLeftSpawn;
    public GameObject ufoRightSpawn;
    public Transform ufoLeft;
    public Transform ufoRight;
    public int direction = 1;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        gameObject.SetActive(false);
        ufoLeft = ufoLeftSpawn.transform;
        ufoRight = ufoRightSpawn.transform;

    }

    // Update is called once per frame
    void Update()
    {
        // Check if the ufo can move
        if (canMove)
        {

            transform.Translate(Vector2.right * speed * direction * Time.deltaTime);

        }

        // Set the ufo inactive if out of the playable area
        if (transform.position.x > 16f)
        {
            gameObject.SetActive(false);
            gameManager.GetComponent<InvaderGameManager>().UfoKilled();
        }

        if (transform.position.x < -16f)
        {
            gameObject.SetActive(false);
            gameManager.GetComponent<InvaderGameManager>().UfoKilled();
        }
    }

    // Check if the ufo is hit by the player bullet
    private void OnTriggerEnter(Collider other)
    {
        

        if (other.gameObject.CompareTag("PlayerBullet"))
        {
            Damage(1);
            print("hit by player");
            other.gameObject.GetComponent<PlayerBullet>().SetInactive();
        }
    }

    public void Damage(int dmgAmount)
    {
        HP -= dmgAmount;
        // If HP is zero or lower
        if (HP <= 0)
        {
            gameObject.SetActive(false);
           
            gameManager.GetComponent<InvaderGameManager>().AddScore(1000);
            gameManager.GetComponent<InvaderGameManager>().UfoKilled();
        }
    }


    // Activate the ufo
    public void ActivateUfo()

    {
       
        gameManager.GetComponent<InvaderGameManager>().ufoActive = true;


        // Pick Left or Right to approach from
        if (Random.value < 0.5f)
        {
            MoveFromRight();
        }
        else
        {
            MoveFromLeft();
        }
        

    }

    // Move from left
    public void MoveFromLeft()

    {
        direction = 1;
        gameObject.SetActive(true);
        transform.position = ufoLeft.position;
        canMove = true;
    }

    // Move from right
    public void MoveFromRight()

    {
        direction = -1;
        gameObject.SetActive(true);
        transform.position = ufoRight.position;
        canMove = true;

    }

}
