using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    int score = 0;
    TextMeshProUGUI scoreText;

    void Awake() 
    {
        scoreText = GetComponent<TextMeshProUGUI>();
    }

    void Start() 
    {
        scoreText.text = score.ToString("00000000");
    }

    public void IncreaseScore(int increaseAmount)
    {
        score += increaseAmount;
        scoreText.text = score.ToString("00000000");
        Debug.Log($"Score: {score}");
    }
    
}
