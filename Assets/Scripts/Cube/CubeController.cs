using System;
using System.Collections;
using System.Collections.Generic;
using Commons;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    
    public GameObject currentCube;
    public GameObject previousCube;
    private CubeMovement cubeMovement;
    private CubeDeploy cubeDeploy;
    private CubeGenerator cubeGenerator;
    
    

    private void Awake()
    {
        cubeMovement = GetComponent<CubeMovement>();
        cubeGenerator = GetComponent<CubeGenerator>();
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            previousCube = cubeDeploy.Deploy(currentCube.transform, previousCube.transform, cubeMovement.side);
            currentCube = cubeGenerator.SpawnCube(previousCube);
            cubeMovement.targetCube = currentCube.transform;
            cubeMovement.ChangeSide(currentCube.transform);
            
        }


        cubeMovement.Move();

    }





}
