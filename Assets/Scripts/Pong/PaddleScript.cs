using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System.Linq;


public class PaddleScript : MonoBehaviour
{
    Scene currentScene;
    Camera mainCamera;
    public GameObject walls;

    Vector3 paddleStartXZ;
    public float bounds;
    public float pMoveSpeed;
    public float cpuMoveSpeed;

    public bool isCPU = false;
    PlayerControls controls;
    public bool moveUp = false;
    public bool moveDown = false;
    public bool moveLeft = false;
    public bool moveRight = false;
    public Vector2 stickMove = Vector2.zero;

    void Awake() {
        controls = new PlayerControls();
        controls.Gameplay.Test.performed += ctx => test();
        controls.Gameplay.MoveUp.started += ctx => MoveUpStart();
        controls.Gameplay.MoveUp.canceled += ctx => MoveUpCancel();
        controls.Gameplay.MoveDown.started += ctx => MoveDownStart();
        controls.Gameplay.MoveDown.canceled += ctx => MoveDownCancel();
        controls.Gameplay.MoveLeft.started += ctx => MoveLeftStart();
        controls.Gameplay.MoveLeft.canceled += ctx => MoveLeftCancel();
        controls.Gameplay.MoveRight.started += ctx => MoveRightStart();
        controls.Gameplay.MoveRight.canceled += ctx => MoveRightCancel();
        // controls.Gameplay.Move.performed += ctx => MovePerf(ctx.ReadValue<Vector2>());
        // controls.Gameplay.Move.performed += ctx => stickMove = ctx.ReadValue<Vector2>();
        // controls.Gameplay.Move.canceled += ctx => MoveCancel();


        // controls.Gameplay.MoveDown.performed += ctx => MoveDown();

        pMoveSpeed = .05f;
        cpuMoveSpeed = .1f;
        bounds = 3.25f;

        if (gameObject.tag == "Right")
            isCPU = true;
        if (gameObject.tag == "Top")
            isCPU = true;
        if (gameObject.tag == "Bottom")
            isCPU = true;
    }

    void OnEnable() {
        controls.Enable();
    }

    void test() {
        print("X pressed");
    }

    void MoveUpStart() {
        moveUp = true;
    }

    void MoveUpCancel() {
        moveUp = false;
    }

    void MoveDownStart() {
        moveDown = true;
    }

    void MoveDownCancel() {
        moveDown = false;
    }

    void MoveLeftStart() {
        moveLeft = true;
    }

    void MoveLeftCancel() {
        moveLeft = false;
    }

    void MoveRightStart() {
        moveRight = true;
    }

    void MoveRightCancel() {
        moveRight = false;
    }

    void MoveCancel() {
        moveUp = moveDown = moveRight = moveLeft = false;
    }

    void MovePerf(Vector2 dir) {
        if (dir.y > 0)
            moveUp = true;
        if (dir.y < 0)
            moveDown = true;
        if (dir.x > 0)
            moveRight = true;
        if (dir.x < 0)
            moveLeft = true;
    }
    

    void Start()
    {
        walls = transform.parent.transform.GetChild(4).gameObject;

        if (gameObject.tag == "Left") {
            paddleStartXZ = new Vector3(-4.5f, 0, 10f);
        }
        else if (gameObject.tag == "Right") {
            paddleStartXZ = new Vector3(4.5f, 0, 10f);
        }
        else if (gameObject.tag == "Top") {
            paddleStartXZ = new Vector3(0, 4.5f, 10f);
        }
        else if (gameObject.tag == "Bottom") {
            paddleStartXZ = new Vector3(0, -4.5f, 10f);
        }
        // transform.parent.GetChild(4);
        // Debug.Log(transform.Find("../Ball"));

        // Get current scene
        currentScene = gameObject.scene;
        // Get the first of a list of objects in this scene
        GameObject mainCameraObject = currentScene.GetRootGameObjects()[0];
        // Get the camera component of the camera object
        mainCamera = mainCameraObject.GetComponent<Camera>();
        
    }

    void Update()
    {

        isLose();

        // if(Input.anyKeyDown){
        //     print(Input.inputString);
        // }
    }

    void FixedUpdate() {
        if (isCPU)
           moveCPU();
        else 
            move(); //not really a physics operation, but we don't want to be able to move while paused
    }

    private void isLose()
    {
        if (gameObject.tag == "Left" && GameManagerScript.lives[0] == 0) {
            lose(0);
        }
        if (gameObject.tag == "Right" && GameManagerScript.lives[1] == 0) {
            lose(1);
        }
        if (gameObject.tag == "Bottom" && GameManagerScript.lives[2] == 0) {
            lose(2);
        }
        if (gameObject.tag == "Top" && GameManagerScript.lives[3] == 0) {
            lose(3);
        }
    }

    void lose(int paddleNum) {
        Destroy(gameObject);
        walls.transform.GetChild(paddleNum).gameObject.SetActive(true);
        GameManagerScript.alive[paddleNum] = false;
    }

    void move() {

        //////////////////////////////////////////
        // *Mouse controls*

        Vector3 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition); // Camera.main can be used in place of mainCamera
        Vector3 updatePos = Vector3.zero;
        if (gameObject.tag == "Left" || gameObject.tag == "Right") {
            updatePos = paddleStartXZ + new Vector3(0, mousePos.y, 0);
            if (updatePos.y > bounds) {
                updatePos = new Vector3(updatePos.x, bounds, updatePos.z);
            }
            else if (updatePos.y < -bounds) {
                updatePos = new Vector3(updatePos.x, -bounds, updatePos.z);
            }
        }
        else if (gameObject.tag == "Top" || gameObject.tag == "Bottom") {
            updatePos = paddleStartXZ + new Vector3(mousePos.x, 0, 0);
            if (updatePos.x > bounds) {
                updatePos = new Vector3(bounds, updatePos.y, updatePos.z);
            }
            else if (updatePos.x < -bounds) {
                updatePos = new Vector3(-bounds, updatePos.y, updatePos.z);
            }
        }

        transform.position = updatePos;

        /////////////////////////////////////////

        // change stick vector into discrete button presses (not working)
        // moveUp = moveDown = moveRight = moveLeft = false;
        // if (stickMove.y > 0)
        //     moveUp = true;
        // if (stickMove.y < 0)
        //     moveDown = true;
        // if (stickMove.x > 0)
        //     moveRight = true;
        // if (stickMove.x < 0)
        //     moveLeft = true;

        if (gameObject.tag == "Left" || gameObject.tag == "Right") {
            if ((Input.GetKey(KeyCode.UpArrow) || moveUp) && transform.position.y < bounds)
                transform.position = new Vector3(transform.position.x, transform.position.y + pMoveSpeed, 10f);
            if ((Input.GetKey(KeyCode.DownArrow) || moveDown) && transform.position.y > -bounds)
                transform.position = new Vector3(transform.position.x, transform.position.y - pMoveSpeed, 10f);
        }
        else if (gameObject.tag == "Top" || gameObject.tag == "Bottom") {
            if ((Input.GetKey(KeyCode.RightArrow) || moveRight) && transform.position.x < bounds)
                transform.position = new Vector3(transform.position.x + pMoveSpeed, transform.position.y, 10f);
            if ((Input.GetKey(KeyCode.LeftArrow) || moveLeft) && transform.position.x > -bounds)
                transform.position = new Vector3(transform.position.x - pMoveSpeed, transform.position.y, 10f);
        }

    }

    public void moveCPU() {
        // get all balls
        GameObject[] objects = currentScene.GetRootGameObjects();
        List<GameObject> balls = new List<GameObject>();
        foreach (GameObject obj in objects) {
            if (obj.tag == "Ball") {
                // (double xx, double yy) pos = (obj.transform.position.x, obj.transform.position.y);
                balls.Add(obj);
            }
        }
        if (balls.Any()) {
            GameObject rightMostBall = balls.OrderByDescending(pos => pos.transform.position.x).First();
            GameObject topMostBall = balls.OrderByDescending(pos => pos.transform.position.y).First();
            GameObject leftMostBall = balls.OrderByDescending(pos => pos.transform.position.x).Last();
            GameObject bottomMostBall = balls.OrderByDescending(pos => pos.transform.position.y).Last();
            
            if (gameObject.tag == "Left") {
                if (transform.position.y < leftMostBall.transform.position.y && transform.position.y < bounds)
                    transform.position = new Vector3(transform.position.x, transform.position.y + cpuMoveSpeed, 10f);
                if (transform.position.y > leftMostBall.transform.position.y && transform.position.y > -bounds)
                    transform.position = new Vector3(transform.position.x, transform.position.y - cpuMoveSpeed, 10f);
            }
            else if (gameObject.tag == "Right") {
                if (transform.position.y < rightMostBall.transform.position.y && transform.position.y < bounds)
                    transform.position = new Vector3(transform.position.x, transform.position.y + cpuMoveSpeed, 10f);
                if (transform.position.y > rightMostBall.transform.position.y && transform.position.y > -bounds)
                    transform.position = new Vector3(transform.position.x, transform.position.y - cpuMoveSpeed, 10f);
            }
            else if (gameObject.tag == "Top") {
                if (transform.position.x < topMostBall.transform.position.x && transform.position.x < bounds)
                    transform.position = new Vector3(transform.position.x + cpuMoveSpeed, transform.position.y, 10f);
                if (transform.position.x > topMostBall.transform.position.x && transform.position.x > -bounds)
                    transform.position = new Vector3(transform.position.x - cpuMoveSpeed, transform.position.y, 10f);
            }
            else if (gameObject.tag == "Bottom") {
                if (transform.position.x < bottomMostBall.transform.position.x && transform.position.x < bounds)
                    transform.position = new Vector3(transform.position.x + cpuMoveSpeed, transform.position.y, 10f);
                if (transform.position.x > bottomMostBall.transform.position.x && transform.position.x > -bounds)
                    transform.position = new Vector3(transform.position.x - cpuMoveSpeed, transform.position.y, 10f);
            }
        }
        // print((maxX, maxY));
        return;
    }
}
