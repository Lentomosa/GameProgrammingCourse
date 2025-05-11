using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvaderShield : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("PlayerBullet"))
        {

            print("Bullet Reflected");
            // other.gameObject.SetActive(false);
            other.gameObject.GetComponent<PlayerBullet>().ReflectBullet();
        }
    }

}
