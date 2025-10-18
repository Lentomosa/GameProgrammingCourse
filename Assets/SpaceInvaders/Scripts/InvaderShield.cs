using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvaderShield : MonoBehaviour
{

    //public bool shieldOn = true;
    public float flickerInterval;
    public float timer;
    public float warningTimer;
    //public bool shieldWarning;
    public bool canReflect = false;
    public GameObject gameManager;
    public float shieldWarning;
    public float shieldDuration;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        shieldWarning = gameManager.GetComponent<InvaderGameManager>().enemyShieldWarning;
    }

    // Update is called once per frame
    void Update()
    {
        warningTimer += Time.deltaTime;
        ShieldUpdate();

    }

    // Update shield status
    public void ShieldUpdate()
    {
        if (warningTimer < shieldWarning || warningTimer > (shieldDuration - shieldWarning))
        {

            canReflect = false;
            timer += Time.deltaTime;

            if (timer > flickerInterval)
            {
                timer = 0f;
                FlipShield();
            }


        }
        else
        {
            canReflect = true;
            gameObject.GetComponent<MeshRenderer>().enabled = true;

        }
    }

    // Reflect the player bullet
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("PlayerBullet") && canReflect)
        {
            print("Bullet Reflected");
            other.gameObject.GetComponent<PlayerBullet>().ReflectBullet();
        }
    }

    // Switch shield renderer on and off
    private void FlipShield()

    {
        if (gameObject.GetComponent<MeshRenderer>().enabled == false)
        {

            gameObject.GetComponent<MeshRenderer>().enabled = true;

        }
        else
        {

            gameObject.GetComponent<MeshRenderer>().enabled = false;

        }


    }

    public void ResetFlickerTimer()

    {
        timer = 0f;
        warningTimer = 0f;
    }


}
