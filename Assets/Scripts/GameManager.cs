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

    [SerializeField]
    private bool isEnd;

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
        if (Input.GetButtonDown("Fire1"))
        {
            if (isEnd)
            {
                // TODO : Need to Change Component Reset Func
                LoadScene();
            }

            if (MovingCube.CurrentCube != null)
            {
                MovingCube.CurrentCube.Stop();
            }

            if (!isEnd)
            {
                GameStart();
            }
        }
    }

    public void GameStart()
    {
        cam.CameraMoveUp();

        spawnerIndex = spawnerIndex == 0 ? 1 : 0;
        currentSpawner = spawners[spawnerIndex];

        currentSpawner.SpawnCube();
        uiManager.ShowInGameUI();
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

        // ! 오류 발견 저장은 되지만 CamYPos가 이상하게 잡힘
        SaveAllCubes();
        if (currentCube != null)
            CameraController.Instance.SetCamYPos(currentCube.transform.position.y);
    }

    internal void ScoreUp()
    {
        GameScore++;
        uiManager.SetScoreText(GameScore);
        Debug.Log("Score UP : " + GameScore);
    }

    internal void LoadScene() => SceneManager.LoadScene(0);

    internal void PerfectCountUp() => perfectCount++;

    internal void PerfectCountReset() => perfectCount = 0;

    internal int PerfectCountCheck() => perfectCount;
}
