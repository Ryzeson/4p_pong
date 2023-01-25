using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawnerScript : MonoBehaviour
{
    float spawnTime = 0;
    public GameObject ball;
    public float ballSpeed = 0;
    public Animator ballSpawnerAnimator;
    float clipLength;


    // Start is called before the first frame update
    void Start()
    {
        clipLength = ballSpawnerAnimator.runtimeAnimatorController.animationClips[0].length;
    }

    // Update is called once per frame
    void Update()
    {
        spawnBalls();
    }

    void spawnBalls() {
        spawnTime -= Time.deltaTime;
        // Debug.Log((spawnTime).ToString());
        if (spawnTime < 0)
        {
            ballSpawnerAnimator.enabled = false;
            GameObject ballClone = Instantiate(ball, transform.position, Quaternion.identity);
            ballClone.layer = 3; //"Ball"
            ballSpeed += .5f;
            ballClone.GetComponent<BallScript>().speed = ballSpeed;
            spawnTime = 8f;
        }
        if (spawnTime < clipLength * 3) {
            ballSpawnerAnimator.enabled = true;
        }
    }
}
