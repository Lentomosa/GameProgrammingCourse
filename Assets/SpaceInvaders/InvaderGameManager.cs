using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvaderGameManager : MonoBehaviour
{

    public float enemyBulletTime = 1f;
    float enemyBulletThreshold = 0f;

    // Start is called before the first frame update
    void Start()
    {
        enemyBulletThreshold = Random.Range(3f, 8f);
    }

    // Update is called once per frame
    void Update()
    {
        enemyBulletTime += Time.deltaTime;

        if (enemyBulletTime >= enemyBulletThreshold)
        {
            GameObject[] invaders = GameObject.FindGameObjectsWithTag("Enemy");
            GameObject invader = invaders[Random.Range(0, invaders.Length)];
            invader.GetComponent<InvaderScript>().Shoot();

            // Reset the timer.
            enemyBulletTime = 0f;

            // Generate a new random threshold for the next shot.
            enemyBulletThreshold = Random.Range(3f, 8f);
        }
    }
}
