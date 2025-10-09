using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvaderScript : MonoBehaviour
{
    public int HP = 1;
    public bool canMove = true;
    public bool canShoot = true;
    public float timer;
    public float changeTime = 1f;
    public float speed = 3f;

    public float offSetX = 1f;

    public float speedLeft = -1f;
    public float speedRight = 1f;

    public bool collidedLeft = false;
    public bool collidedRight = false;

    public float offSetY = -1f;

    public Transform playerPos;

    public Transform gun;
    public bool canReflect = false;

    public Transform shield;
    public GameObject shieldPrefab;
    private float shieldTimer;
    private float activeTimer;
    public float shieldTime = 3f;
    public bool shieldActive = false;
    public bool canUseShield = false;
    public bool hasObstacle = false;

    public GameObject gameManager;

   

    public EnemyBulletPool bulletPool;

    private GameObject shieldInstance;

    public float rayDistance = 20f;

    // Start is called before the first frame update
    void Start()
    {
        speedLeft = -speed;
        speedRight = speed;
        gameManager = GameObject.Find("GameManager");
        playerPos = GameObject.Find("Player").transform;
        gun = transform.GetChild(0);
        Transform shield = transform.GetChild(0);
        shieldInstance = Instantiate(shieldPrefab, shield.position, shield.rotation, shield);
        shieldInstance.SetActive(false);
        bulletPool = GameObject.Find("Spawner").GetComponent<EnemyBulletPool>();
        changeTime = gameManager.GetComponent<InvaderGameManager>().invaderChangeTime;

    }

    // Update is called once per frame
    void Update()
    {

        // Check if allowed to move
        if (canMove)
        {
            timer += Time.deltaTime;
            shieldTimer += Time.deltaTime;

            // Move if changeTime is reached
            if (timer > changeTime)
            {
                Move();
                
                timer = 0f;
            }

            // Flip shield on or off
            if (canUseShield)
            { 

                if (shieldTimer > shieldTime && !shieldActive)
                {
                shieldInstance.SetActive(true);
                shieldTimer = 0f;
                shieldActive = true;
                }

                if (shieldTimer > shieldTime && shieldActive)
                {
                shieldInstance.SetActive(false);
                shieldTimer = 0f;
                shieldActive = false;
                canUseShield = false;
                }
            }
            
            // Make the player lose if an invader reaches the player y position
            if (transform.position.y <= playerPos.transform.position.y)
            {
                print("Invader Reached the Player");
                gameManager.GetComponent<InvaderGameManager>().GameOver();
            }
        }
    }

    // Shooting of pooled bullets
    public void Shoot()
    {
        if (canShoot)
        {
            if (bulletPool == null)
            {
                Debug.LogError("InvaderGameManager reference is not assigned!");
                return;
            }
            GameObject invaderBullet = bulletPool.GetPooledBullet();
            if (invaderBullet == null)
            {
                Debug.LogWarning("No inactive bullets available in the pool!");
                return;
            }
            // Position the bullet at the gun's position if available.
            if (gun != null)
            {
                invaderBullet.transform.position = gun.position;
                invaderBullet.transform.rotation = gun.rotation;
            }
            else
            {
                Debug.LogWarning("Gun transform is not assigned; using invader's position.");
                invaderBullet.transform.position = transform.position;
            }
            invaderBullet.SetActive(true);
            Debug.Log("Bullet activated at position: " + invaderBullet.transform.position);
        }
    }

    // Move the invader
    public void Move()
    {
        transform.position = new Vector2(transform.position.x + offSetX, transform.position.y);
    }

    private void OnTriggerEnter(Collider other)
    {

        // Check if collided with the BumperRight
        if (other.gameObject.name == "BumperRight" && !collidedRight)
        {
            GameObject[] invaders = GameObject.FindGameObjectsWithTag("Enemy");
            for (int i = 0; i < invaders.Length; i++)
            {
                invaders[i].GetComponent<InvaderScript>().offSetX = speedLeft;
                invaders[i].transform.position = new Vector2(invaders[i].transform.position.x, invaders[i].transform.position.y + offSetY);
                invaders[i].GetComponent<InvaderScript>().collidedRight = true;
                invaders[i].GetComponent<InvaderScript>().collidedLeft = false;
            }
        }

        // Check if collided with the BumperLeft
        if (other.gameObject.name == "BumperLeft" && !collidedLeft)
        {
            GameObject[] invaders = GameObject.FindGameObjectsWithTag("Enemy");
            for (int i = 0; i < invaders.Length; i++)
            {
                invaders[i].GetComponent<InvaderScript>().offSetX = speedRight;
                invaders[i].transform.position = new Vector2(invaders[i].transform.position.x, invaders[i].transform.position.y + offSetY);
                invaders[i].GetComponent<InvaderScript>().collidedLeft = true;
                invaders[i].GetComponent<InvaderScript>().collidedRight = false;
            }
        }

        // Check if hit by player bullet
        if (other.gameObject.CompareTag("PlayerBullet") && !canReflect)
        {
            Damage(1);
            print("hit by player");
            if (other.gameObject.GetComponent<PlayerBullet>().durability > 0)
            {
                other.gameObject.GetComponent<PlayerBullet>().durability -= 1;
            }
            else
            {
                other.gameObject.GetComponent<PlayerBullet>().RestoreDurability();
                //other.gameObject.SetActive(false);
                other.gameObject.GetComponent<PlayerBullet>().SetInactive();
            }
        }
    }

    //Damage by a set amount of damage
    public void Damage(int dmgAmount)
    {
        HP -= dmgAmount;
        // If HP is zero or lower
        if (HP <= 0)
        {
            Destroy(gameObject);
            gameManager.GetComponent<InvaderGameManager>().AddScore(100);
            gameManager.GetComponent<InvaderGameManager>().InvaderKilled();

        }
    }

    // Do a raycast check for shield
    public void ShieldTest()
    {
        //Draw a debug ray
        Debug.DrawRay(transform.position, Vector3.down * rayDistance, Color.red);

        // Raycast ALL

        RaycastHit[] hits = Physics.RaycastAll(transform.position, Vector3.down, rayDistance);

        // If any collider is hit

        if (hits.Length > 0)
        {


            foreach (RaycastHit hit in hits)
            {
                // Check if a game object with a similar tag is hit by the ray
                if (hit.collider.CompareTag(gameObject.tag))
                {
                    Debug.Log($"{gameObject.name} found an object with the same tag below: {hit.collider.gameObject.name}");
                    hasObstacle = true;
                }
                // No game objects with a similar tag found
                else
                {
                    Debug.Log($"{gameObject.name} found an object with a different tag below: {hit.collider.gameObject.name}");
                    hasObstacle = false;
                }
            }


        }

        // If any collider is not hit
        else
        {
            hasObstacle = false;
        }
    
    }
}