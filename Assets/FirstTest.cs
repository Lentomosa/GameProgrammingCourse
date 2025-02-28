using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstTest : MonoBehaviour
{
    public int score = 1;
    public float xPos;
    public float yPos;
    public float speed = 5;

    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        // Time.deltaTime = Time since last frame

        // Move Left
        if (Input.GetKey(KeyCode.A))
        {
            xPos -= speed * Time.deltaTime;
        }
        
        // Move right
        if (Input.GetKey(KeyCode.D))
        {
            xPos += speed * Time.deltaTime;
        }

        // Move Up
        if (Input.GetKey(KeyCode.W))
        {
            yPos += speed * Time.deltaTime;
        }

        // Move down
        if (Input.GetKey(KeyCode.S))
        {
            yPos -= speed * Time.deltaTime;
        }


        // Move the object
        transform.position = new Vector3(xPos, yPos, 0);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("pickup"))
        {
            score += 100;
            Destroy(other.gameObject);
        }

    }
}
