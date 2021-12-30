using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUIController : MonoBehaviour
{
    public GameObject MainCanvas;
    public GameObject InGameCanvas;
    public GameObject GameOverCanvas;

    [Header("Detailed UI components")] 
    public Text scoreText;
    public Text gameOverText;
    
    private void Start()
    {
        UIManager.Instance.mainUIController = this;
        ;
        MainCanvas.SetActive(true);
        InGameCanvas.SetActive(false);
        GameOverCanvas.SetActive(false);
    }

    public void GameStart()
    {
        MainCanvas.SetActive(false);
        InGameCanvas.SetActive(true);
        GameOverCanvas.SetActive(false);
    }

    public void GameOver()
    {
        MainCanvas.SetActive(false);
        InGameCanvas.SetActive(false);
        GameOverCanvas.SetActive(true);
    }
    

    public void SetScoreText(int score)
    {
        scoreText.text = score.ToString();
        gameOverText.text = score.ToString();
    }

}
