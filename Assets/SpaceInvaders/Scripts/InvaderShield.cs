using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvaderShield : MonoBehaviour
{

    //public bool shieldOn = true;
    public float flickerInterval;
    public float timer;

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(Timer());
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > flickerInterval)
        {
            FlipShield();

            timer = 0f;
        }
    }

    // Reflect the player bullet
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("PlayerBullet"))
        {

            print("Bullet Reflected");
            // other.gameObject.SetActive(false);
            other.gameObject.GetComponent<PlayerBullet>().ReflectBullet();
        }
    }

    private void FlipShield()

    {
        if (gameObject.GetComponent<MeshRenderer>().enabled == false)
        {
            //gameObject.SetActive(true);
            gameObject.GetComponent<MeshRenderer>().enabled = true;
            //StartCoroutine(Timer());
        }
        else
        {
            //gameObject.SetActive(false);
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            //StartCoroutine(Timer());
        }


    }

    public void ResetFlickerTimer()

    {
        timer = 0f;

    }




    public IEnumerator Timer()
    {

        yield return new WaitForSeconds(1f);

        FlipShield();

    }

}
