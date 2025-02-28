using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy2: MonoBehaviour
{

    public float speed = 3f;
    public float xPos;
    public float yPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        xPos -= speed * Time.deltaTime;
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }
}
