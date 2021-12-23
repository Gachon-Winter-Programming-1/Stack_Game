using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Singleton;

public class CameraController : Singleton<CameraController>
{
    [SerializeField]
    private MovingCube cubePrefab;
    internal void CameraMoveUp()
    {
        
        transform.position = new Vector3(transform.position.x,transform.position.y + cubePrefab.transform.localScale.y,transform.position.z);
    }
}
