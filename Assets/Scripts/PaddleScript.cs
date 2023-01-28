using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PaddleScript : MonoBehaviour
{
    Vector3 paddleStartXZ;
    Camera mainCamera;
    public GameObject walls;

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
        Scene currentScene = gameObject.scene;
        // Get the first of a list of objects in this scene
        GameObject mainCameraObject = currentScene.GetRootGameObjects()[0];
        // Get the camera component of the camera object
        mainCamera = mainCameraObject.GetComponent<Camera>();
        
    }

    void Update()
    {
        move();

        checkScore();

        // if(Input.anyKeyDown){
        //     print(Input.inputString);
        // }
    }

    private void checkScore()
    {
        if (gameObject.tag == "Left" && GameManagerScript.scores[0] == 0) {
            Destroy(gameObject);
            walls.transform.GetChild(0).gameObject.SetActive(true);
        }
        if (gameObject.tag == "Right" && GameManagerScript.scores[1] == 0) {
            Destroy(gameObject);
            walls.transform.GetChild(1).gameObject.SetActive(true);
        }
        if (gameObject.tag == "Bottom" && GameManagerScript.scores[2] == 0) {
            Destroy(gameObject);
            walls.transform.GetChild(2).gameObject.SetActive(true);
        }
        if (gameObject.tag == "Top" && GameManagerScript.scores[3] == 0) {
            Destroy(gameObject);
            walls.transform.GetChild(3).gameObject.SetActive(true);
        }
    }

    void move() {
        Vector3 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition); // Camera.main can be used in place of mainCamera
        Vector3 updatePos = Vector3.zero;
        if (gameObject.tag == "Left" || gameObject.tag == "Right") {
            updatePos = paddleStartXZ + new Vector3(0, mousePos.y, 0);
            if (updatePos.y > 3) {
                updatePos = new Vector3(updatePos.x, 3, updatePos.z);
            }
            else if (updatePos.y < -3) {
                updatePos = new Vector3(updatePos.x, -3, updatePos.z);
            }
        }
        else if (gameObject.tag == "Top" || gameObject.tag == "Bottom") {
            updatePos = paddleStartXZ + new Vector3(mousePos.x, 0, 0);
            if (updatePos.x > 3) {
                updatePos = new Vector3(3, updatePos.y, updatePos.z);
            }
            else if (updatePos.x < -3) {
                updatePos = new Vector3(-3, updatePos.y, updatePos.z);
            }
        }

        transform.position = updatePos;
    }
}
