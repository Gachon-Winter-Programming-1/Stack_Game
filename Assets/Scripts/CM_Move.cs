using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CM_Move : MonoBehaviour
{
    public CinemachineVirtualCamera vircam = null;
    private int index;
    public Transform currentTarget; 

    private BuildingManager buildingManager;
    private void Start()
    {
        index = 0;
        buildingManager = GameObject.Find("@BuildingManager").GetComponent<BuildingManager>();
        vircam.Follow = buildingManager.gameObjects[index].transform;
        vircam.LookAt = buildingManager.gameObjects[index].transform;
        currentTarget = buildingManager.gameObjects[index].transform;
    }
    private void Update()
    {
    }

    public Transform NextBuilding()
    {
        index = (index + 1) % buildingManager.count;
        currentTarget = buildingManager.gameObjects[index].transform;
        vircam.Follow = buildingManager.gameObjects[index].transform;
        vircam.LookAt = buildingManager.gameObjects[index].transform;
        return buildingManager.gameObjects[index].transform;
    }
    
    public Transform PreviousBuilding()
    {
        index = (index - 1) < 0 ? buildingManager.count-1 : index-1;
        currentTarget = buildingManager.gameObjects[index].transform;
        vircam.Follow = buildingManager.gameObjects[index].transform;
        vircam.LookAt = buildingManager.gameObjects[index].transform;
        return buildingManager.gameObjects[index].transform; 
    }


}
