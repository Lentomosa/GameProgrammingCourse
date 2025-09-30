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

    //public GameObject[] bullets = GameObject.FindGameObjectsWithTag("PlayerBullet");

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        gameObject.SetActive(false);
        //ufoLeft = ufoLeftSpawn.transform;
        //ufoRight = ufoRightSpawn.transform;
        
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the ufo can move
        if (canMove)
        {

            transform.Translate(Vector2.down * speed * Time.deltaTime);

        }

        // Set the ufo inactive if out of the playable area
        if (transform.position.y > 16f)
        {
            gameObject.SetActive(false);
            gameManager.GetComponent<InvaderGameManager>().UfoKilled();
        }

        if (transform.position.y < -16f)
        {
            gameObject.SetActive(false);
            gameManager.GetComponent<InvaderGameManager>().UfoKilled();
        }
    }

    // Check if the ufo is hit by the player bullet
    private void OnTriggerEnter(Collider other)
    {


        if (other.gameObject.name == "Player")
        {


            List<GameObject> bullets = player.GetComponent<BulletPool>().bullets;

            //for (int i = 0; i < bullets.Length; i++)

           foreach (GameObject obj in bullets)
                
            {

                string weapon = "Plasma";
                print("ShieldTest!");
                obj.GetComponent<PlayerBullet>().LoadClipsFor(weapon);
                obj.GetComponent<PlayerBullet>().PlayClip();

            }

            print("Upgraded");




            //bullet.GetComponent<PlayerBullet>().LoadClipsFor(weapon);
            //bullet.GetComponent<PlayerBullet>().PlayClip();

            gameObject.SetActive(false);
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
