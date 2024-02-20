using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    int score = 0;
    public TextMeshProUGUI scoreText;

    private void Update()
    {
        scoreText.text = "SCORE: " + score.ToString(); // Cập nhật nội dung của Text GameObject
    }
}
