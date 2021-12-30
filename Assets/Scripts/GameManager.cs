using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Singleton;
using UnityEngine.Serialization;
using UnityEditor;

public class GameManager : Singleton<GameManager>
{
    private UIManager uiManager;
    private CubeSpawner[] spawners;
    private int spawnerIndex;
    private CubeSpawner currentSpawner;

    private CameraController cam;

    public List<GameObject> CubesToBeSaved;
    public GameObject currentCube;

    [SerializeField] private bool isEnd;
    [SerializeField] private bool isStart;

    public int GameScore { get; private set; }

    [SerializeField]
    private int perfectCount;

    private void Awake()
    {
        uiManager = UIManager.Instance;
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
        if (Input.GetButtonDown("Fire1") && isStart)
        {

            if (MovingCube.CurrentCube != null)
            {
                MovingCube.CurrentCube.Stop();
            }

            if (!isEnd)
            {
                
                cam.CameraMoveUp();

                spawnerIndex = spawnerIndex == 0 ? 1 : 0;
                currentSpawner = spawners[spawnerIndex];

                currentSpawner.SpawnCube();
            }
            
        }
    }

    public void GameStart()
    {
        isStart = true;
        
        cam.CameraMoveUp();

        spawnerIndex = spawnerIndex == 0 ? 1 : 0;
        currentSpawner = spawners[spawnerIndex];
        
        currentSpawner.SpawnCube();
        
        uiManager.mainUIController.GameStart(); 
    }

    public void GameRestart()
    {
        LoadScene();
    }
    

    internal void AddCubeToBeSaved(GameObject gameObject) => CubesToBeSaved.Add(gameObject);

    internal void SaveAllCubes()
    {
        if (CubesToBeSaved.Count != 0)
        {
            Debug.Log("Save");
            GameObject Empty = new GameObject("Building");
            foreach (GameObject gameObject in CubesToBeSaved)
            {
                gameObject.transform.parent = Empty.transform;
            }

            string localPath = "Assets/BuildingSaved/" + Empty.name + ".prefab";

            localPath = AssetDatabase.GenerateUniqueAssetPath(localPath);

            PrefabUtility.SaveAsPrefabAssetAndConnect(Empty, localPath, InteractionMode.UserAction);
        }
        else
        {
            Debug.Log("저장할 큐브가 존재하지 않습니다");
        }
    }

    internal void EndGame()
    {
        
        isEnd = true;
        uiManager.mainUIController.GameOver();

        // ! 오류 발견 저장은 되지만 CamYPos가 이상하게 잡힘
        CameraController.Instance.SetCamYPos(currentCube.transform.position.y);
        SaveAllCubes();
    }

    internal void ScoreUp()
    {
        GameScore++;
        uiManager.mainUIController.SetScoreText(GameScore);
        Debug.Log("Score UP : " + GameScore);
    }

    internal void LoadScene() => SceneManager.LoadScene(0);

    internal void PerfectCountUp() => perfectCount++;

    internal void PerfectCountReset() => perfectCount = 0;

    internal int PerfectCountCheck() => perfectCount;
}
