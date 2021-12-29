using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using Singleton;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    
    public Canvas MainMenu;
    public Canvas InGame;
    public Text scoreText;
    
    public void HiddenAllUI()
    {
       MainMenu.gameObject.SetActive(false);
       InGame.gameObject.SetActive(false);
    }
    public void ShowAllUI(){
        MainMenu.gameObject.SetActive(true);
        InGame.gameObject.SetActive(true);
    }

    public void ShowInGameUI()
    { 
        MainMenu.gameObject.SetActive(false); 
        InGame.gameObject.SetActive(true);
    }

    public void HiddenInGameUI()
    {
        InGame.gameObject.SetActive(false);
    }
    public void SetScoreText(int score)
    {
        scoreText.text = score.ToString();
    }
}
