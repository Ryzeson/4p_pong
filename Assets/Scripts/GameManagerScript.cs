using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq; //isGameOver();
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript Instance { get; private set; }
    public static int[] scores = new int[4];
    public static int ballCount = 0;
    public static int nPlayers = 4;
    public static bool[] alive;
    public static bool gameOver = false;
    public static int winner = -1;

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
            scores[i] = 1;
        }
        alive = new bool[nPlayers];
        for (int i = 0; i < alive.Length; i++) {
            alive[i] = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameOver) {
            isGameOver();
        }  
    }

    private void isGameOver()
    {
        gameOver = alive.Count(c => c) == 1;

        //if the game is over, determine the winner
        if (gameOver) {
            for (int i = 0; i < alive.Length; i++) {
                if (alive[i])
                    winner = i;
            }
        }
    }

    public static void updateScore(int player) {
        scores[player]--;
    }
}
