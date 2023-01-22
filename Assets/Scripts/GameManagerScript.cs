using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript Instance { get; private set; }
    public static int[] scores = new int[4];

        private void Awake() 
    { 
        // If there is an instance, and it's not me, delete myself.
        
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this;
        }
        // SceneManager.LoadScene("Level_1", LoadSceneMode.Additive);

        // Get camera (not working)
        // mainCamera = GameObject.Find("MainCamera").GetComponent<Camera>();
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < scores.Length; i++) {
            scores[i] = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void updateScore(int player) {
        scores[player]++;
    }
}
