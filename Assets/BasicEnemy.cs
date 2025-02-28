using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    public float xPos;
    public float yPos;
    public float enemySpeed = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        xPos -= enemySpeed * Time.deltaTime;
        transform.position = new Vector3(xPos, yPos, 0);
    }
}
