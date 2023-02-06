using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class UIScript : MonoBehaviour
{
    public TextMeshProUGUI uiText;
    [SerializeField] private TMP_ColorGradient[] gradients = new TMP_ColorGradient[4];


    // Start is called before the first frame update
    void Start()
    {
        uiText = GetComponent<TextMeshProUGUI>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManagerScript.gameOver) {
            uiText.colorGradientPreset = gradients[GameManagerScript.winner];
            uiText.SetText("Player " + (GameManagerScript.winner + 1) + " wins!");
        }
    }
}
