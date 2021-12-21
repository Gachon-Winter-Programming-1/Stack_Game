using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;
    private CubeSpawner[] spawners;
    private int spawnerIndex;
    private CubeSpawner currentSpawner;

    public bool isEnd;
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
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {   
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
}
