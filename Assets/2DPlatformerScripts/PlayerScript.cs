using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float playerSpeed = 5f;
    public float jumpPower = 100000f;

    public int jumpCount;
    public int jumpLimit = 1;

    public Rigidbody2D myRB;

    public Transform spawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float hor = Input.GetAxis("Horizontal");
        transform.Translate(Vector2.right * hor * playerSpeed * Time.deltaTime);

        if(Input.GetButtonDown("Jump") && jumpCount <= jumpLimit)
        {
            myRB.velocity =Vector2.zero;
            myRB.AddForce(Vector2.up * jumpPower);
            jumpCount++;
        }
        if(transform.position.y < -5f)
        {
            transform.position = spawnPoint.position;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        jumpCount = 0;
    }
}
