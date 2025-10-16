using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    
    public bool bulletReflected = false;
    public float bulletSpeed = 10f;
    public bool canMove = true;
    //public Vector3 reflectVector;
    public Quaternion randomRotation;
    public Quaternion originalRotation;

    public AudioSource audioSource;
    // public AudioClip[] audioClip;
    // private AudioClip activeSound;
    public AudioClip[] clips;

    public int durability = 0;
    public int maxDurability = 0;
    //public string weaponType;


    [SerializeField] public string weapon;  // e.g. "Laser", "Rocket"


    // Start is called before the first frame update
    void Start()
    {
        //weaponType = Laser;




    }

    public void LoadSound()
    {
        audioSource = GetComponent<AudioSource>();
        DefaultFiringSound();
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


                
                transform.Translate(Vector2.up * bulletSpeed * Time.deltaTime, gameObject.transform);
                
            }

            // Set inactive if out of playable area
            if (transform.position.y > 12f)
            {
                //gameObject.SetActive(false);
                SetInactive();
            }

            if (transform.position.y < -6f)
            {
                //gameObject.SetActive(false);
                SetInactive();
            }
        }
    }

    // Called in InvaderShield when colliding with a player bullet
    public void ReflectBullet()
    {
        //reflectVector = new Vector3(UnityEngine.Random.Range(-0.5f, 0.5f), UnityEngine.Random.Range(-0.5f, -0.5f), 0).normalized;
        randomRotation =  Quaternion.Euler(0f, 0f, Random.Range(-150f, -210f));
        transform.rotation = randomRotation;
        bulletReflected = true;
    }

    public void DefaultFiringSound()

    {
        // Set Active Sound



        // activeSound = audioClip[Random.Range(0, audioClip.Length)];

        //clips = Resources.LoadAll<AudioClip>("Sounds/Weapons/Laser");

        // Play sound
        //audioSource.PlayOneShot(activeSound);
        //audioSource.PlayOneShot(activeSound);


        //LoadClipsFor(weaponName);
        // string 
        weapon = "Laser";
        LoadClipsFor(weapon);


    }
    

    public void LoadClipsFor(string weapon)
    {
        // Capitalization and whitespace must match your folder names
        string folderPath = $"Sounds/Weapons/{weapon}";
        clips = Resources.LoadAll<AudioClip>(folderPath);

        if (clips == null || clips.Length == 0)
            Debug.LogWarning($"No audio found in Resources/{folderPath}");
    }

    /*
    public void PlayRandom()
    {
        if (clips == null || clips.Length == 0) return;
        audioSource.PlayOneShot(clips[Random.Range(0, clips.Length)]);
    }
    */

    public void SetActive()
    {
        transform.rotation = originalRotation;
        gameObject.SetActive(true);
        canMove = true;
    }


    public void SetInactive()
    {
        gameObject.SetActive(false);
        //transform.rotation = originalRotation;
        canMove = false;
    }





public void PlayClip()
        // int index
    {
        //int index = 0;

        int index = Random.Range(0, clips.Length);
        if (index < 0 || index >= clips.Length) return;
        //AudioSource.PlayClipAtPoint(clips[index], Vector3.zero);
        audioSource.PlayOneShot(clips[index]);

    }


    // Damage the player if the bullet was reflected towards the player
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player" && bulletReflected)
        {
            
            other.gameObject.GetComponent<PlayerController>().DecreaseLives();
            SetInactive();
        }

    }

    //
    public void RestoreDurability()
    {
        durability = maxDurability;
    }
}
