using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;
    private CubeSpawner[] spawners;
    private int spawnerIndex;
    private CubeSpawner currentSpawner;

    internal bool isEnd;
    public int gameScore;
    private void Awake()
    {
        if(null == instance)
        {
            instance = this;
        }

        spawners = FindObjectsOfType<CubeSpawner>();
    }
    public static GameManager Instance
    {
        get
        {
            if(null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    private void Start() {
        isEnd = false;
        gameScore = 0;
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {   
            if(isEnd)
            {
                LoadScene();
            }

            if(MovingCube.CurrentCube != null)
                MovingCube.CurrentCube.Stop();
            
            if(!isEnd)
            {
                GameObject.Find("Main Camera").GetComponent<CameraController>().CameraMoveUp();

                spawnerIndex = spawnerIndex == 0 ? 1 : 0;
                currentSpawner = spawners[spawnerIndex];

                currentSpawner.SpawnCube();
            }
        }
    }
    
    internal void EndGame()
    {
        isEnd=true;
    }

    internal void ScoreUp()
    {
        gameScore++;
    }

    internal void LoadScene()
    {
        SceneManager.LoadScene(0);
    }
}
