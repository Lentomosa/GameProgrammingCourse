using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawnerScript : MonoBehaviour
{

    public GameObject ball;
    

    // Start is called before the first frame update
    void Start()
    {
        Spawn();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Spawn()
    {
        Instantiate(ball, transform.position, transform.rotation);
        
    }
}
