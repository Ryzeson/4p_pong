using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawnerScript : MonoBehaviour
{
    float spawnTimer = 10f;
    public GameObject ball;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer -= Time.deltaTime;
        Debug.Log((spawnTimer).ToString());
        if (spawnTimer < 0)
        {
            Instantiate(ball, transform.position, Quaternion.identity);
        }
    }
}
