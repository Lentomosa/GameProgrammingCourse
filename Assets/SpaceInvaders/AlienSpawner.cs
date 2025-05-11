using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienSpawner : MonoBehaviour
{
    public GameObject alienPrefab;
    public int rows = 5;
    public int columns = 10;
    public float spacingX = 1.5f;
    public float spacingY = 1.5f;
    public Vector3 startPos = new Vector3(-7, 4, 0);

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        // Loop through grid indices to instantiate aliens as children of this container
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                Vector3 spawnPos = startPos + new Vector3(col * spacingX, -row * spacingY, 0);
                Instantiate(alienPrefab, spawnPos, Quaternion.identity, transform);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
