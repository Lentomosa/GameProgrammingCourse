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
        //ufoLeftSpawn.transform = ufoRight;

        ufoLeft = ufoLeftSpawn.transform;
        ufoRight = ufoRightSpawn.transform;

    }

    // Update is called once per frame
    void Update()
    {

        if (canMove)
        {


            transform.Translate(Vector2.right * speed * direction * Time.deltaTime);

        }

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

    private void OnTriggerEnter(Collider other)
    {
        GameObject[] invaders = GameObject.FindGameObjectsWithTag("Enemy");


        if (other.gameObject.CompareTag("PlayerBullet"))
        {
            Damage(1);
            print("hit by player");
            other.gameObject.SetActive(false);
        }
    }

    public void Damage(int dmgAmount)
    {
        HP -= dmgAmount;
        if (HP <= 0)
        {
            //Destroy(gameObject);
            gameObject.SetActive(false);
           
            gameManager.GetComponent<InvaderGameManager>().AddScore(1000);
            gameManager.GetComponent<InvaderGameManager>().UfoKilled();
        }
    }

    public void ActivateUfo()

    {
       
        gameManager.GetComponent<InvaderGameManager>().ufoActive = true;


        if (Random.value < 0.5f)
        {
            MoveFromRight();
        }
        else
        {
            MoveFromLeft();
        }
        

    }

    public void MoveFromLeft()

    {
        direction = 1;
        gameObject.SetActive(true);
        transform.position = ufoLeft.position;
        canMove = true;
    }

    public void MoveFromRight()

    {
        direction = -1;
        gameObject.SetActive(true);
        transform.position = ufoRight.position;
        canMove = true;

    }

}
