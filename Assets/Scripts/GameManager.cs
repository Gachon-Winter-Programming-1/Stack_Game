using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Singleton;

public class GameManager : Singleton<GameManager>
{
    private CubeSpawner[] spawners;
    private int spawnerIndex;
    private CubeSpawner currentSpawner;

    internal bool isEnd;

    [SerializeField]
    private int gameScore;

    [SerializeField]
    private int PerfectCount;

    private void Awake()
    {
        spawners = FindObjectsOfType<CubeSpawner>();
    }

    private void Start()
    {
        isEnd = false;
        gameScore = 0;
        PerfectCount = 0;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (isEnd)
            {
                LoadScene();
            }

            if (MovingCube.CurrentCube != null)
                MovingCube.CurrentCube.Stop();

            if (!isEnd)
            {
                GameObject.Find("@Main Camera").GetComponent<CameraController>().CameraMoveUp();

                spawnerIndex = spawnerIndex == 0 ? 1 : 0;
                currentSpawner = spawners[spawnerIndex];

                currentSpawner.SpawnCube();
            }
        }
    }

    internal void EndGame()
    {
        isEnd = true;
    }

    internal void ScoreUp()
    {
        gameScore++;
    }

    internal void LoadScene()
    {
        SceneManager.LoadScene(0);
    }

    internal void PerfectCountUp()
    {
        PerfectCount++;
    }

    internal void PerfectCountReset()
    {
        PerfectCount = 0;
    }

    internal int PerfectCountCheck()
    {
        return PerfectCount;
    }
}
