using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class InvaderBulletPool : MonoBehaviour
{
    public List<GameObject> invaderBullets;
    public GameObject invaderbulletToPool;
    public int invaderBulletAmount;

    // Start is called before the first frame update
    void Start()
    {
        invaderBullets = new List<GameObject>();

        for (int i = 0; i < invaderBulletAmount; i++)
        {
            GameObject tmp = Instantiate(invaderbulletToPool);
            tmp.SetActive(false);
            invaderBullets.Add(tmp);
            Debug.Log("Added bullet to the pool: " + i);
        }
    }

    public GameObject InvaderGetPooledBullet()
    {
        for (int i = 0; i < invaderBullets.Count; i++)
        {
            // Ensure the bullet reference is not null and inactive
            if (invaderBullets[i] != null && !invaderBullets[i].activeInHierarchy)
            {
                return invaderBullets[i];
            }
        }
        Debug.LogWarning("No inactive bullet available in the pool!");
        return null;
    }
}