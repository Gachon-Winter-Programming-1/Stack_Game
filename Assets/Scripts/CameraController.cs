using System.Collections;
using System.Collections.Generic;
using Commons;
using UnityEngine;
using Singleton;
public class CameraController : Singleton<CameraController>
{
    public float distance;
    public GameObject CamTarget
    {
        get => CamTarget;
        set => CamMove(value);
    }

    private void CamMove(GameObject cube)
    {
        Vector3 target = cube.transform.position;
        StartCoroutine(Collection.MoveToPosition(transform, target, Constants.CAMSPEED));
    }
}
