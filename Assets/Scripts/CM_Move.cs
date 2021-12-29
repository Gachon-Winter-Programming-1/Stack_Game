using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CM_Move : MonoBehaviour
{
    public CinemachineVirtualCamera vircam = null;
    private int index;

    private BuildingManager buildingManager;
    private void Start()
    {
        index = 0;
        buildingManager = GameObject.Find("@BuildingManager").GetComponent<BuildingManager>();
        vircam.Follow = buildingManager.gameObjects[index].transform;
        vircam.LookAt = buildingManager.gameObjects[index].transform;
    }
    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (buildingManager.count > index + 1) index++;
            else index = 0;
        }
        vircam.Follow = buildingManager.gameObjects[index].transform;
        vircam.LookAt = buildingManager.gameObjects[index].transform;
    }


}
