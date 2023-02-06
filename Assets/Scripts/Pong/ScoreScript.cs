using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreScript : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public GameObject paddle;

    enum Paddles
  {
    Left,
    Right,
    Bottom,
    Top
  }

    // Start is called before the first frame update
    void Start()
    {
        
        scoreText = GetComponent<TextMeshProUGUI>();
        // print("paddle pos:" + paddle.transform.position);
        // print("text pos" + scoreText.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        // scoreText.SetText(GameManagerScript.lives[0].ToString());
        scoreText.transform.position =  Camera.main.WorldToScreenPoint(paddle.transform.position);
        if (paddle.tag == "Left") {
            scoreText.SetText(GameManagerScript.lives[(int) Paddles.Left].ToString());
        }
        else if (paddle.tag == "Right") {
            scoreText.SetText(GameManagerScript.lives[(int) Paddles.Right].ToString());
        }
        else if (paddle.tag == "Bottom") {
            scoreText.SetText(GameManagerScript.lives[(int) Paddles.Bottom].ToString());
        }
        else if (paddle.tag == "Top") {
            scoreText.SetText(GameManagerScript.lives[(int) Paddles.Top].ToString());
        }
    }
}
