using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    
    public float speed = 10f;
    public bool canMove = true;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Check if the bullet is allowed to move
        if (canMove)
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime);

            if (transform.position.y > 10f)
            {
                gameObject.SetActive(false);
            }

            if (transform.position.y < -10f)
            {
                gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the bullet collides with the player
        if(other.gameObject.name == "Player")
        {
            //Decrease player lives and set the bullet inactive
            other.gameObject.GetComponent<PlayerController>().DecreaseLives();
            gameObject.SetActive(false);
            gameObject.GetComponent<EnemyBullet>().canMove = true;
        }

    }
}
