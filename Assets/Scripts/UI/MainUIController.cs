using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainUIController : MonoBehaviour
{
    public GameObject MainCanvas;
    public GameObject InGameCanvas;
    public GameObject GameOverCanvas;
    public GameObject RankPopupCanvas;
    public Image MuteButton;

    [Header("Detailed UI components")] 
    public Text scoreText;
    public Text gameOverText;

    public Sprite muteOn;
    public Sprite muteOff;
    
    private void Start()
    {
        UIManager.Instance.mainUIController = this;
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

    public void LoadBuildings()
    {
        SceneManager.LoadScene(1);
    }

    public void SetScoreText(int score)
    {
        scoreText.text = score.ToString();
        gameOverText.text = score.ToString();
    }

    public void MuteToggle()
    {
        AudioListener.pause = !AudioListener.pause;
        if (AudioListener.pause)
        {
            MuteButton.sprite = muteOn;
            AudioListener.volume = 1f;
        }
        else
        {
            MuteButton.sprite = muteOff;
            AudioListener.volume = 0f;
        }
    }
    public void OpenGithub()
    {
        Application.OpenURL("https://github.com/Gachon-Winter-Programming-1/Stack_Game");
    }
    
    public void DisableRankPopup(){ RankPopupCanvas.SetActive(false);}
    
    public void EnableRankPopup(){ RankPopupCanvas.SetActive(true);}

}
