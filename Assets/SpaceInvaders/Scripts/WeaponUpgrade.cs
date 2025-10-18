using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponUpgrade : MonoBehaviour
{

    public GameObject player;

    public float speed = 2f;
    public bool canMove = true;
    public GameObject gameManager;
    public float minX = -10f; // Minimum X position
    public float maxX = 10f;  // Maximum X position
    public float heightY = 13f;  // Height
    public Transform spawnPos;
    public int weaponTier;
    public List<string> weapons;
    public List<GameObject> bullets;
    public float upgradeDuration;

 

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        gameObject.SetActive(false);
        weaponTier = gameManager.GetComponent<InvaderGameManager>().weaponTier;
        upgradeDuration = gameManager.GetComponent<InvaderGameManager>().upgradeDuration;

    }

    // Update is called once per frame
    void Update()
    {
        // Check if the upgrade can move
        if (canMove)
        {

            transform.Translate(Vector2.down * speed * Time.deltaTime);

        }

        // Set the upgrade inactive if out of the playable area
        if (transform.position.y > 16f)
        {
            gameObject.SetActive(false);
            gameManager.GetComponent<InvaderGameManager>().upgradeActive = false;
        }

        if (transform.position.y < -16f)
        {
            gameObject.SetActive(false);
            gameManager.GetComponent<InvaderGameManager>().upgradeActive = false;
        }
    }

    // Check if the player collides with the upgrade
    private void OnTriggerEnter(Collider other)
    {


        if (other.gameObject.name == "Player")
        {


            // Set Upgrade inactive

            gameManager.GetComponent<InvaderGameManager>().upgradeActive = false;
            gameObject.SetActive(false);

            gameManager.GetComponent<InvaderGameManager>().playerHasUpgrade = true;

            // Activate the player visual effect for the upgrade
            other.GetComponent<PlayerController>().UpgradeShow();

            // Increase weapon tier
            gameManager.GetComponent<InvaderGameManager>().IncreaseWeaponTier();
            weaponTier = gameManager.GetComponent<InvaderGameManager>().weaponTier;


            // Get a list of bullets to upgrade
            bullets = player.GetComponent<BulletPool>().bullets;

            foreach (GameObject obj in bullets)
                
            {
 
                obj.GetComponent<PlayerBullet>().LoadClipsFor(weapons[weaponTier]);

            }


        }
    }
    
    // Load sounds for bullets
    public void LoadSounds()
    {
        bullets = player.GetComponent<BulletPool>().bullets;
        weaponTier = gameManager.GetComponent<InvaderGameManager>().weaponTier;
        foreach (GameObject obj in bullets)

        {

            obj.GetComponent<PlayerBullet>().LoadClipsFor(weapons[weaponTier]);

        }

    }
    

    // Activate and place upgrade
    public void ActivateUpgrade()

    {

        float randomX = Random.Range(minX, maxX);

        transform.position = new Vector3(randomX, heightY, 0);
        gameObject.SetActive(true);
        gameManager.GetComponent<InvaderGameManager>().upgradeActive = true;
        canMove = true;

    }





}
