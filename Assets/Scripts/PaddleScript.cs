using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PaddleScript : MonoBehaviour
{
    Vector3 paddleLeftStartXZ;
    Camera mainCamera;

    void Start()
    {
        paddleLeftStartXZ = new Vector3(-4.5f, 0, 10f);
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
        Vector3 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition); // Camera.main can be used in place of mainCamera
        Vector3 updatePos = paddleLeftStartXZ + new Vector3(0, mousePos.y, 0);
        if (updatePos.y > 3) {
            updatePos = new Vector3(updatePos.x, 3, updatePos.z);
        }
        else if (updatePos.y < -3) {
            updatePos = new Vector3(updatePos.x, -3, updatePos.z);
        }
            transform.position = updatePos;
    }
}
