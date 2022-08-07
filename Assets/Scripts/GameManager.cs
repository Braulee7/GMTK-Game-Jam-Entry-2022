using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    private int score;

    public TextMeshProUGUI scoreText;


    public void Start()
    {
        score = 0;
    }
    public void SetScore(int points)
    {
        this.score += points;
    }

    private void Update()
    {
        scoreText.text = "Score: " + score;
    }
}
