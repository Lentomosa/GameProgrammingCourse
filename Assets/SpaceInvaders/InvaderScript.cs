using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvaderScript : MonoBehaviour
{
    public int HP = 1;
    public bool canMove = true;
    public float timer;
    public float changeTime = 1f;
    public float speed = 3f;

    public float offSetX = 1f;

    public float speedLeft = -1f;
    public float speedRight = 1f;

    public bool collidedLeft = false;
    public bool collidedRight = false;

    public float offSetY = -1f;

    public Transform player;

    public Transform gun;
    public bool canReflect = false;

    public Transform shield;
    public GameObject shieldPrefab;
    private float shieldTimer;
    private float activeTimer;
    public float shieldTime = 3f;
    public bool shieldActive = false;
    public bool canUseShield = false;

    public GameObject gameManager;

    public InvaderBulletPool invaderBulletPool;

    public EnemyBulletPool bulletPool;

    private GameObject shieldInstance;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        player = GameObject.Find("Player").transform;
        gun = transform.GetChild(0);
        Transform shield = transform.GetChild(0);
        shieldInstance = Instantiate(shieldPrefab, shield.position, shield.rotation, shield);
        shieldInstance.SetActive(false);
        //invaderBulletPool = GetComponent<InvaderBulletPool>();
        bulletPool = GameObject.Find("Spawner").GetComponent<EnemyBulletPool>();
    }

    // Update is called once per frame
    void Update()
    {
 
        if (canMove)
        {
            timer += Time.deltaTime;
            shieldTimer += Time.deltaTime;

            if (timer > changeTime)
            {
                Move();
                
                timer = 0f;
            }

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

            if (transform.position.y <= player.transform.position.y)
            {
                print("game over");
            }
        }
    }

    // Shooting of pooled bullets
    public void Shoot()
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

    public void Move()
    {
        transform.position = new Vector2(transform.position.x + offSetX, transform.position.y);
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject[] invaders = GameObject.FindGameObjectsWithTag("Enemy");

        if (other.gameObject.name == "BumperRight" && !collidedRight)
        {
            for (int i = 0; i < invaders.Length; i++)
            {
                invaders[i].GetComponent<InvaderScript>().offSetX = speedLeft;
                invaders[i].transform.position = new Vector2(invaders[i].transform.position.x, invaders[i].transform.position.y + offSetY);
                invaders[i].GetComponent<InvaderScript>().collidedRight = true;
                invaders[i].GetComponent<InvaderScript>().collidedLeft = false;
            }
        }

        if (other.gameObject.name == "BumperLeft" && !collidedLeft)
        {
            for (int i = 0; i < invaders.Length; i++)
            {
                invaders[i].GetComponent<InvaderScript>().offSetX = speedRight;
                invaders[i].transform.position = new Vector2(invaders[i].transform.position.x, invaders[i].transform.position.y + offSetY);
                invaders[i].GetComponent<InvaderScript>().collidedLeft = true;
                invaders[i].GetComponent<InvaderScript>().collidedRight = false;
            }
        }

        if (other.gameObject.CompareTag("PlayerBullet") && !canReflect)
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
            Destroy(gameObject);
            gameManager.GetComponent<InvaderGameManager>().AddScore(100);
        }
    }
}