using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner2 : MonoBehaviour
{

    public GameObject[] enemy;
    public float enemySpeedMultiplier;
    public float timer;
    public float spawnTime;
    public float spawnTimeMax = 5f;
    public float spawnPosY;


    // Start is called before the first frame update
    void Start()
    {
        spawnTime = Random.Range(1f, spawnTimeMax);
        spawnPosY = Random.Range(1f, 20f);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(timer > spawnTime)
        {
            Spawn();
        }
    }

    public void Spawn()
    {
        GameObject myEnemy = Instantiate(enemy[Random.Range(0,enemy.Length)], new Vector3(transform.position.x, spawnPosY, 0f), transform.rotation);
        myEnemy.GetComponent<BasicEnemy2>().speed *= enemySpeedMultiplier;
        timer = 0f;
        spawnTime = Random.Range(1f, 5f);
        spawnPosY = Random.Range(1f, 20f);
        enemySpeedMultiplier += 0.1f;
        

        if(spawnTimeMax > 1f)
        {
            spawnTimeMax -= 0.2f;
        }

    }
}
