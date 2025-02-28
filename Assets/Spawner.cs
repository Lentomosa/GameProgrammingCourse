using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject enemy;
    public int i = 0;
    public float timer;
    public float randomTime;

    // Start is called before the first frame update
    void Start()
    {
        // Spawns objects every x seconds
        //InvokeRepeating("Spawn", 1f, 1f);


        // For loop
        
       /* for(int i=10; i>0; i--)
        {
            
        }

        */



    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(timer > randomTime)
        {
            Spawn();
            randomTime = Random.Range(1f, 5f);
            timer = 0f;
        }
    }

    void Spawn()
    {
        
        Instantiate(enemy, transform.position, transform.rotation);
        
    }
}
