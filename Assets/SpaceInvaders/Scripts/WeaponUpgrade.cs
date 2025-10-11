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

    //public GameObject[] bullets = GameObject.FindGameObjectsWithTag("PlayerBullet");

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        gameObject.SetActive(false);
        weaponTier = gameManager.GetComponent<InvaderGameManager>().weaponTier;


    }

    // Update is called once per frame
    void Update()
    {
        // Check if the ufo can move
        if (canMove)
        {

            transform.Translate(Vector2.down * speed * Time.deltaTime);

        }

        // Set the upgrade inactive if out of the playable area
        if (transform.position.y > 16f)
        {
            gameObject.SetActive(false);

        }

        if (transform.position.y < -16f)
        {
            gameObject.SetActive(false);
 
        }
    }

    // Check if the ufo is hit by the player bullet
    private void OnTriggerEnter(Collider other)
    {


        if (other.gameObject.name == "Player")
        {

            gameObject.SetActive(false);
           // List<GameObject> bullets = player.GetComponent<BulletPool>().bullets;
            gameManager.GetComponent<InvaderGameManager>().IncreaseWeaponTier();
            weaponTier = gameManager.GetComponent<InvaderGameManager>().weaponTier;
            //for (int i = 0; i < bullets.Length; i++)
            bullets = player.GetComponent<BulletPool>().bullets;

            foreach (GameObject obj in bullets)
                
            {
 
                //string weapon = "Plasma";
                //string weapon = "Plasma";
                obj.GetComponent<PlayerBullet>().LoadClipsFor(weapons[weaponTier]);
               // obj.GetComponent<PlayerBullet>().PlayClip();

            }

           




            //bullet.GetComponent<PlayerBullet>().LoadClipsFor(weapon);
            //bullet.GetComponent<PlayerBullet>().PlayClip();


        }
    }



    public void ActivateUpgrade()

    {

        float randomX = Random.Range(minX, maxX);

        transform.position = new Vector3(randomX, heightY, 0);
        gameObject.SetActive(true);
        //gameManager.GetComponent<InvaderGameManager>().ufoActive = true;


        // Pick Left or Right to approach from



       // transform.position = spawnPos.position;
        canMove = true;


    }



}
