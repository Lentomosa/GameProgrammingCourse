using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    
    public bool bulletReflected = false;
    public float bulletSpeed = 10f;
    public bool canMove = true;

    public AudioSource audioSource;
    // public AudioClip[] audioClip;
    // private AudioClip activeSound;
    public AudioClip[] clips;

    

    // Start is called before the first frame update
    void Start()
    {
        FiringSound();
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            // Move bullet up
            if (!bulletReflected)
            {
                transform.Translate(Vector2.up * bulletSpeed * Time.deltaTime);
            }

            // Move bullet down
            if (bulletReflected)
            {
                transform.Translate(Vector2.down * bulletSpeed * Time.deltaTime);
            }

            // Set inactive if out of playable area
            if (transform.position.y > 12f)
            {
                gameObject.SetActive(false);
            }

            if (transform.position.y < -6f)
            {
                gameObject.SetActive(false);
            }
        }
    }

    // Called in InvaderShield when colliding with a player bullet
    public void ReflectBullet()
    {
        bulletReflected = true;
    }

    public void FiringSound()

    {
        // Set Active Sound



        // activeSound = audioClip[Random.Range(0, audioClip.Length)];

        clips = Resources.LoadAll<AudioClip>("Sounds");

        // Play sound
        //audioSource.PlayOneShot(activeSound);
        //audioSource.PlayOneShot(activeSound);



    }

    public void PlayClip()
        // int index
    {
        //int index = 0;

        int index = Random.Range(0, clips.Length);
        if (index < 0 || index >= clips.Length) return;
        AudioSource.PlayClipAtPoint(clips[index], Vector3.zero);

    }


    // Damage the player if the bullet was reflected towards the player
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player" && bulletReflected)
        {
            gameObject.SetActive(false);
            other.gameObject.GetComponent<PlayerController>().DecreaseLives();
            
        }

    }
}
