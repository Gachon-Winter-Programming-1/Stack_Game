using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Singleton;
using UnityEngine.SocialPlatforms.Impl;

public class BuildingManager : Singleton<BuildingManager>
{
    public List<GameObject> gameObjects;
    internal int count;
    public int distance;
    // Start is called before the first frame update
    private void Awake()
    {
        DisposeBuilding();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    { }


    internal void DisposeBuilding()
    {
#if UNITY_EDITOR
        string[] arr = AssetDatabase.FindAssets("", new string[] { "Assets/BuildingSaved" });
        count = 0;
        foreach (string str in arr)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(str);
            gameObjects.Add(Instantiate(AssetDatabase.LoadAssetAtPath(assetPath, typeof(GameObject)) as GameObject,
            new Vector3(distance * count, -3, 0),
            Quaternion.Euler(0, 135, 0)));
            count++;
        }
#endif
    }

    public IEnumerator destroyBuilding(int index)
    {
        Transform[] cubes = gameObjects[index].GetComponentsInChildren<Transform>();
        foreach (Transform cube in cubes)
        {
            cube.gameObject.AddComponent<MeshDestroy>();
        }
        yield return new WaitForSeconds(1f);
        gameObjects.RemoveAt(index);
        //destroy asset data funcs
        Destroy(gameObjects[index]);

    }
}
