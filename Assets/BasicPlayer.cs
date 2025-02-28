using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BasicPlayer : MonoBehaviour
{

    public Rigidbody2D myRB;
    public float power = 400f;
    public float yClamp;
    public float movementSpeed = 5f;
    public float hor;

    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
     
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x,-40f,40f),Mathf.Clamp(transform.position.y,1f,20f),0);

        hor = Input.GetAxis("Horizontal");
        
        // Moves the player according to keyboard inputs
        transform.Translate(Vector3.right * hor * movementSpeed * Time.deltaTime);

        // Jumps the player
        if(Input.GetKeyDown(KeyCode.Space))
        {
            myRB.velocity = Vector2.zero;
            myRB.AddForce(Vector2.up * power);
        }

        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            SceneManager.LoadScene(0);
        }
    }
}
