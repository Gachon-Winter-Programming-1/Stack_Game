using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Singleton;
using UnityEngine.Serialization;
using UnityEditor;

public class GameManager : Singleton<GameManager>
{
    private CubeSpawner[] spawners;
    private int spawnerIndex;
    private CubeSpawner currentSpawner;

    private CameraController cam;

    public List<GameObject> CubesToBeSaved;

    [SerializeField]
    private bool isEnd;

    public int GameScore { get; private set; }

    [SerializeField]
    private int perfectCount;

    private void Awake()
    {
        isEnd = false;
        GameScore = 0;
        perfectCount = 0;
        spawners = FindObjectsOfType<CubeSpawner>();
    }

    private void Start()
    {
        cam = GameObject.Find("@Main Camera").GetComponent<CameraController>();
        CubesToBeSaved = new List<GameObject>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (isEnd)
            {
                SaveAllCubes();
                // TODO : Need to Change Component Reset Func
                LoadScene();
            }

            if (MovingCube.CurrentCube != null)
            {
                MovingCube.CurrentCube.Stop();
            }

            if (!isEnd)
            {
                // TODO : GameObject.Find() func is decrease to Performance. Change Camera cache variable.
                //cam.CameraMoveUp();

                spawnerIndex = spawnerIndex == 0 ? 1 : 0;
                currentSpawner = spawners[spawnerIndex];

                currentSpawner.SpawnCube();
            }
        }
    }
    internal void AddCubeToBeSaved(GameObject gameObject) => CubesToBeSaved.Add(gameObject);

    internal void SaveAllCubes()
    {
        GameObject Empty = new GameObject("Building");
        foreach (GameObject gameObject in CubesToBeSaved)
        {
            gameObject.transform.parent = Empty.transform;
        }

        string localPath = "Assets/BuildingSaved/" + Empty.name + ".prefab";

        localPath = AssetDatabase.GenerateUniqueAssetPath(localPath);

        PrefabUtility.SaveAsPrefabAssetAndConnect(Empty, localPath, InteractionMode.UserAction);
    }

    public  void GameOver()
    {
    }

    internal void EndGame() => isEnd = true;

    internal void ScoreUp()
    {
        GameScore++;
        Debug.Log("Score UP : " + GameScore);
    }

    internal void LoadScene() => SceneManager.LoadScene(0);

    internal void PerfectCountUp() => perfectCount++;

    internal void PerfectCountReset() => perfectCount = 0;

    internal int PerfectCountCheck() => perfectCount;
}
