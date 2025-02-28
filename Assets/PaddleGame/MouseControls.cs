using UnityEngine;

public class MouseControls : MonoBehaviour
{
    public Transform target;
    public float transitionSpeed = 5f;
    public float minX;
    public float minY;
    public float maxX;
    public float maxY;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {

        // Mouse function
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(transform.position, ray.direction * 100f, Color.red, 1f);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100f))
        {
            Vector3 targetPos = new Vector3(Mathf.Clamp(hit.point.x,-31f,-3f),Mathf.Clamp(hit.point.y, 23f, 40f), hit.point.z);

            // Lerp. Move Between two points at given time
            target.position = Vector3.Lerp(target.position, targetPos, transitionSpeed * Time.deltaTime);

            // If clicked over an enemy object
            if (hit.transform.CompareTag("Enemy") && Input.GetButtonDown("Fire1"))
            {
                Destroy(hit.transform.gameObject);
            }
        }

    }

}