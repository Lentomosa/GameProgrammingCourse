using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    
    public bool bulletReflected = false;
    public float bulletSpeed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!bulletReflected)
        {
            transform.Translate(Vector2.up * bulletSpeed * Time.deltaTime);
        }

        if (bulletReflected)
        {
            transform.Translate(Vector2.down * bulletSpeed * Time.deltaTime);
        }

        if (transform.position.y > 10f)
        {
            gameObject.SetActive(false);
        }

        if (transform.position.y < -10f)
        {
            gameObject.SetActive(false);
        }
    }

    public void ReflectBullet()
    {
        bulletReflected = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player" && bulletReflected)
        {
            gameObject.SetActive(false);
            other.gameObject.GetComponent<PlayerController>().DecreaseLives();
            
        }

    }
}
