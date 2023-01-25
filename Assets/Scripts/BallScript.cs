using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    Rigidbody2D _rigidBody;
    Vector2 direction;
    public float speed = 2f;

    // Start is called before the first frame update
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        move();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        checkBounds();
                // Debug.Log("Ball velocity: " + _rigidBody.velocity);
    }

    void move() {
        float x = Random.Range(0f, 1f);
        float y = Random.Range(0f, 1f);
        x = Random.Range(0, 2) == 1 ? x*=-1 : x;
        y = Random.Range(0, 2) == 1 ? y*=-1 : y;
        Debug.Log("X:" + x + " Y: " + y);
        // Debug.Log("X:" + x + " Y: " + y + " other Y: " + otherY + " other X: " + otherX);
        direction = new Vector2(x, y).normalized;
        _rigidBody.velocity = direction * speed;
        // _rigidBody.AddForce(direction * speed);
    }

    void checkBounds() {
        if (transform.position.x < -6) {
            GameManagerScript.updateScore(0);
            Destroy(gameObject);
        }
        else if (transform.position.x > 6) {
            GameManagerScript.updateScore(1);
            Destroy(gameObject);
        }
        else if (transform.position.y < -6) {
            GameManagerScript.updateScore(2);
            Destroy(gameObject);
        }
        else if (transform.position.y > 6) {
            GameManagerScript.updateScore(3);
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        Debug.Log(collision.collider.name + " collided with " + name);
        // Debug.Log("Velocity of ball: " + _rigidBody.velocity);
        // Debug.Log("Direction of ball: " + direction);
        string colTag = collision.collider.tag;
        Vector2 vel = Vector2.zero;
        // if (colTag == "Top" || colTag == "Bottom") {
        //     vel.x = direction.x;
        //     vel.y = direction.y * -1;
        //     // Debug.Log("G");
        // }
        // else if (colTag == "Left" || colTag == "Right") {
        //     vel.x = direction.x * -1;
        //     vel.y = direction.y;
        //     // Debug.Log("Gd");
        // }
        // Debug.Log("Velocity " + vel);
        Vector2 d = collision.GetContact(0).normal;
        print(d);
        if(d.y == 1 || d.y == -1) {
            vel.x = direction.x;
            vel.y = direction.y * -1;
        }
        else if (d.x == 1 || d.x == -1) {
            vel.x = direction.x * -1;
            vel.y = direction.y;
        }

        direction = vel;
            _rigidBody.velocity = vel * speed;

      if( d.x == 1 ) print("right");
      if( d.x == -1 ) print("left");
      if( d.y == 1 ) print("up");
      if( d.y == -1 ) print("down");
    }
}
