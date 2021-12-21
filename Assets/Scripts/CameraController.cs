using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private MovingCube cubePrefab;
    internal void CameraMoveUp()
    {
        transform.position = new Vector3(transform.position.x,transform.position.y + cubePrefab.transform.localScale.y/2,transform.position.z);
    }
}
