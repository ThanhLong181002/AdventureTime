using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameOverOnScreen gameOverOnScreen;
    
    public void GameOver()
    {
        gameOverOnScreen.Setup();
    }

}
