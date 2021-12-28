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
    

    private void Awake()
    {
        cubeMovement = GetComponent<CubeMovement>();
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            cubeDeploy.Deploy(currentCube, previousCube, cubeMovement.side);
        }
        
    }





}
