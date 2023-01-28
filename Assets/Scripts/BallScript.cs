using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    Rigidbody2D _rigidBody;
    Vector2 direction;
    public float speed = 2f;
    public bool collided = false;

    // Start is called before the first frame update
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        move();
    }

    void Update() {
        if(collided && Input.anyKeyDown){
            print("double me");
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        checkBounds();
    }

    void move() {
        float x = Random.Range(0f, 1f);
        float y = Random.Range(0f, 1f);
        x = Random.Range(0, 2) == 1 ? x*=-1 : x;
        y = Random.Range(0, 2) == 1 ? y*=-1 : y;
        // Debug.Log("X:" + x + " Y: " + y);
        direction = new Vector2(x, y).normalized;
        _rigidBody.velocity = direction * speed;
        // _rigidBody.AddForce(direction * speed);
    }

    void checkBounds() {
        if (transform.position.x < -6) {
            GameManagerScript.updateScore(0);
            Destroy(gameObject);
            GameManagerScript.ballCount--;
        }
        else if (transform.position.x > 6) {
            GameManagerScript.updateScore(1);
            Destroy(gameObject);
            GameManagerScript.ballCount--;
        }
        else if (transform.position.y < -6) {
            GameManagerScript.updateScore(2);
            Destroy(gameObject);
            GameManagerScript.ballCount--;
        }
        else if (transform.position.y > 6) {
            GameManagerScript.updateScore(3);
            Destroy(gameObject);
            GameManagerScript.ballCount--;
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        collided = true;
        print("true");
        // Debug.Log(collision.collider.name + " collided with " + name);
        // Debug.Log("Velocity of ball: " + _rigidBody.velocity);
        // Debug.Log("Direction of ball: " + direction);
        // Debug.Log(collision.collider.transform.parent.tag + " collided with " + name);

        float curSpeed = speed;
        // if it hits a wall and dead player times button, increase ball speed
        // if (collision.collider.transform.parent.tag == "Wall" && Input.anyKeyDown) {
        //     curSpeed *= 2;
        //     print("Doubling speed!");
        // }

        //reflects ball at appropriate angle (https://en.wikipedia.org/wiki/Specular_reflection)
        Vector2 d = collision.GetContact(0).normal;
        direction -= (2*d*(Vector2.Dot(d,direction)));
        _rigidBody.velocity = direction * curSpeed;

        // ContactPoint2D cp = collision.GetContact(0);
        // Debug.DrawRay(cp.point, cp.normal * 100, Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f), 10f);

    // works for perfectly parallel collisions, but not for edges of paddle
    //     if(d.y == 1 || d.y == -1) {
    //         vel.x = direction.x;
    //         vel.y = direction.y * -1;
    //     }
    //     else if (d.x == 1 || d.x == -1) {
    //         vel.x = direction.x * -1;
    //         vel.y = direction.y;
    //     }
    //     else {
    //         vel = d;
    //     }
    //           if( d.x == 1 ) print("right");
    //   if( d.x == -1 ) print("left");
    //   if( d.y == 1 ) print("up");
    //   if( d.y == -1 ) print("down");

    // Does not work for sides or behind paddle, and not as elegant bc it checks tags
    // string colTag = collision.collider.tag;
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

    }

    void OnCollisionExit2D(Collision2D other) {
        StartCoroutine(stopCheckWallBounce());
        // if (other.collider.transform.parent.tag == "Wall" && Input.anyKeyDown) {
        //     print("Doubling speed!");
        // }
    }

    private IEnumerator stopCheckWallBounce() {
        yield return new WaitForSeconds(2f);
        collided = false;
        print("false");
    }
}
