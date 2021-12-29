using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeGenerator : MonoBehaviour
{
    public GameObject SpawnCube(GameObject previousCube)
    {
        GameObject cube = Instantiate(previousCube);
        cube.transform.position += Vector3.up * cube.transform.localScale.y;
        return cube;
    }
}
