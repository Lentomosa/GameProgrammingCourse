using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class BulletPool : MonoBehaviour
{
    public List<GameObject> bullets;
    public GameObject bulletToPool;
    public int bulletAmount;

    // Start is called before the first frame update
    void Start()
    {
        // Create bullet pool
        bullets = new List<GameObject>();
        GameObject tmp;
        for(int i=0; i < bulletAmount; i++)
        {
            tmp = Instantiate(bulletToPool);
            tmp.gameObject.GetComponent<PlayerBullet>().LoadSound();
            tmp.SetActive(false);
            bullets.Add(tmp);
            print("Added to the pool");
        }
        
    }

    //Get inactive bullets from pool
    public GameObject GetPooledBullet()
    {
        for(int i=0; i< bulletAmount; i++)
        {
            if(!bullets[i].activeInHierarchy)
            {
                return bullets[i];
            }
        }
        return null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
