using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    Rigidbody2D _rigidBody;
    AudioSource _audioSource;
    Vector2 direction;
    public float speed = 2f;
    public float bounceMult = 3f;
    public bool[] colWall;

    // Start is called before the first frame update
    void Start()
    {
        colWall = new bool[] {false, false, false, false};
        _rigidBody = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();
        move();
    }

    void Update() {
        checkBounds();
        checkWallBounce();
    }

    private void checkWallBounce()
    {
        if(colWall[0] && Input.GetKey(KeyCode.A)){
            bounce(0);
        }
        if(colWall[1] && Input.GetKey(KeyCode.S)){
            bounce(1);
        }
        if(colWall[2] && Input.GetKey(KeyCode.D)){
            bounce(2);
        }
        if(colWall[3] && Input.GetKey(KeyCode.F)){
            bounce(3);
        }
    }

    void bounce(int wall) {
        _rigidBody.velocity *= bounceMult;
        _audioSource.Play();
        colWall[wall] = false;
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
        // Debug.Log(collision.collider.name + " collided with " + name);
        // Debug.Log(collision.collider.transform.parent.tag + " collided with " + name);

        // if it hits a wall and dead player times button, increase ball speed
        if (collision.collider.transform.parent.tag == "Wall") {
            print("Collided with wall");
            if (collision.collider.tag == "Left")
                colWall[0] = true;
            if (collision.collider.tag == "Right")
                colWall[1] = true;
            if (collision.collider.tag == "Bottom")
                colWall[2] = true;
            if (collision.collider.tag == "Top")
                colWall[3] = true;
        }

        //reflects ball at appropriate angle (https://en.wikipedia.org/wiki/Specular_reflection)
        // https://gamedev.stackexchange.com/questions/28124/reflect-angle-on-pong-clone
        Vector2 d = collision.GetContact(0).normal;
        direction -= (2*d*(Vector2.Dot(d,direction)));
        _rigidBody.velocity = direction * speed;

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
        // store collider only (collision object could get rewritten if this method is called
        // before coroutine stopWallBounceWindow finished)
        // https://forum.unity.com/threads/coroutine-collision-seems-to-overwrite-itself.1180180/
        // Could also be solved by disabled "Reuse Colliision Callbacks" in Project Settings
        Collider2D localCollider = other.collider;
        StartCoroutine(stopWallBounceWindow(localCollider));
    }

    private IEnumerator stopWallBounceWindow(Collider2D collider) {
        if (collider.transform.parent.tag == "Wall") {
            yield return new WaitForSeconds(.25f);
            if (collider.tag == "Left")
                colWall[0] = false;
            if (collider.tag == "Right")
                colWall[1] = false;
            if (collider.tag == "Bottom")
                colWall[2] = false;
            if (collider.tag == "Top")
                colWall[3] = false;
        }
    }
}
