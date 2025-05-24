using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int lives = 3;
    public bool canDamage = true;
    public bool canMove = true;
    public bool canShoot = true;
    public float speed = 5f;
    public BulletPool bulletPool;
    public Transform cannon;
    public GameObject gameManager;

    //public float enemyBulletTime = 1f;
    //float enemyBulletThreshold = 0f;



    // Start is called before the first frame update
    void Start()
    {
        bulletPool = GetComponent<BulletPool>();
        lives = gameManager.GetComponent<InvaderGameManager>().lives;
        // Set the initial random threshold once.
        //enemyBulletThreshold = Random.Range(3f, 8f);
    }

    // Update is called once per frame
    void Update()
    {
        float hor = Input.GetAxis("Horizontal");

        if (canMove)
        {
            transform.Translate(Vector2.right * hor * speed * Time.deltaTime);
        

             if(Input.GetButtonDown("Fire1") && canMove)
             {
                if(canShoot)
                { 
                Shoot();
                }
            }

        }

       /* enemyBulletTime += Time.deltaTime;
        


        if (enemyBulletTime >= enemyBulletThreshold)
        {
            GameObject[] invaders = GameObject.FindGameObjectsWithTag("Enemy");
            GameObject invader = invaders[Random.Range(0, invaders.Length)];
            invader.GetComponent<InvaderScript>().Shoot();

            // Reset the timer.
            enemyBulletTime = 0f;

            // Generate a new random threshold for the next shot.
            enemyBulletThreshold = Random.Range(0.5f, 3f);
        }
        */
    }

    public void Shoot()
    {
        print("Shoot");
        GameObject bullet = bulletPool.GetPooledBullet();
        if(bullet != null)
        {
            bullet.transform.position = cannon.position;
            bullet.SetActive(true);
            bullet.GetComponent<PlayerBullet>().bulletReflected = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("EnemyBullet"))
        {
            DecreaseLives();
        }


    }

    public void DecreaseLives()
    {
        if (canDamage)
        {
            canMove = false;
            canDamage = false;
            lives--;
            gameManager.GetComponent<InvaderGameManager>().LoseLife();
            

            /*
            GameObject[] invaders = GameObject.FindGameObjectsWithTag("Enemy");
            for (int i = 0; i < invaders.Length; i++)
            {
                invaders[i].GetComponent<InvaderScript>().canMove = false;
            }

            */

            if(lives > 0)
            {
                StartCoroutine(Continue());
            }
            

        }
    }

    public IEnumerator Continue()
    {
        yield return new WaitForSeconds(2f);
        transform.position = new Vector3(0f, -5f, 0f);
        canMove = true;
        gameManager.GetComponent<InvaderGameManager>().Continue();

        
       /* GameObject[] invaders = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < invaders.Length; i++)
        {
            invaders[i].GetComponent<InvaderScript>().canMove = true;
            invaders[i].GetComponent<InvaderScript>().changeTime += 0.4f;
        }
        */

        yield return new WaitForSeconds(1f);
        canDamage = true;
    }

}
