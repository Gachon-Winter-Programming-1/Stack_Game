using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Singleton;
using UnityEngine.Serialization;
using UnityEditor;

public class GameManager : Singleton<GameManager>
{
    public bool isStarted = false;
    public CubeController cubeController;
    public int currentScore;

    public void Awake()
    {
        cubeController.gameObject.SetActive(false);
    }

    private void Start()
    {
        GameStart();
    }

    public void GameOver()
    {
        isStarted = false;
        cubeController.gameObject.SetActive(isStarted);
    }

    public void GameStart()
    {
        isStarted = true;
        cubeController.gameObject.SetActive(isStarted);
        
    }

    public void GameRestart() => SceneManager.LoadScene(0);
}
