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


    [SerializeField] public string weapon;  // "Laser", "Plasma"


    // Start is called before the first frame update
    void Start()
    {


    }

    // Get audio source and load clips to it
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
        randomRotation =  Quaternion.Euler(0f, 0f, Random.Range(-150f, -210f));
        transform.rotation = randomRotation;
        bulletReflected = true;
    }

    public void DefaultFiringSound()

    {
        weapon = "Laser";
        LoadClipsFor(weapon);

    }
    

    public void LoadClipsFor(string weapon)
    {
        // Folder to load sounds from
        string folderPath = $"Sounds/Weapons/{weapon}";
        clips = Resources.LoadAll<AudioClip>(folderPath);

        if (clips == null || clips.Length == 0)
            Debug.LogWarning($"No audio found in Resources/{folderPath}");
    }


    public void SetActive()
    {
        transform.rotation = originalRotation;
        gameObject.SetActive(true);
        canMove = true;
    }


    public void SetInactive()
    {
        gameObject.SetActive(false);
        canMove = false;
    }





    // Play random loaded clip
    public void PlayClip()
        
    {
        
        int index = Random.Range(0, clips.Length);
        if (index < 0 || index >= clips.Length) return;
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

    // Restore bullet durability
    public void RestoreDurability()
    {
        durability = maxDurability;
    }
}
