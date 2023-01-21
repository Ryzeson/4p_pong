using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    Rigidbody2D _rigidBody;
    float speed = 140f;

    // Start is called before the first frame update
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        move();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void move() {
        float x = Random.Range(-1f, 1f);
        float y = Random.Range(-1f, 1f);
        Vector2 direction = new Vector2(x, y).normalized;
        _rigidBody.AddForce(direction * speed);
    }

    void OnCollisionEnter2D(Collision2D collision) {
        // Debug.Log("Ball velocity: " + _rigidBody.velocity);
        // move();
    }
}
