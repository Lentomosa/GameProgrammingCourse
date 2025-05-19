using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UfoScript : MonoBehaviour
{
    public int HP = 1;
    public float speed = 2f;
    public bool canMove = true;
    public GameObject gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager");

    }

    // Update is called once per frame
    void Update()
    {

        if (canMove)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);




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
            Destroy(gameObject);
            gameManager.GetComponent<InvaderGameManager>().AddScore(1000);
        }
    }
}
