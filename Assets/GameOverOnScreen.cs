using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverOnScreen : MonoBehaviour
{
    public TextMeshProUGUI textScore;
    public void Setup()
    {
        gameObject.SetActive(true);
        textScore.text = "SCORE: " + Finn.Score.ToString(); // Cập nhật nội dung của Text GameObject
    }    
}
