using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Singleton;

public class CameraController : Singleton<CameraController>
{
    public GameObject cubePrefab;
    internal void CameraMoveUp()
    {
        StartCoroutine(Collection.MoveToPosition(transform, new Vector3(transform.position.x, transform.position.y + cubePrefab.transform.localScale.y, transform.position.z), 0.1f));
    }

    public void SetCamYPos(float distance)
    {
        float hypotenusepow, equilateralsqrt;
        hypotenusepow = Mathf.Pow(distance, 2);
        equilateralsqrt = hypotenusepow / 2;
        StartCoroutine(Collection.MoveToPosition(transform,
            new Vector3(transform.position.x, 5 + distance, transform.position.z), 0.5f));
        //transform.position = new Vector3(transform.position.x, 5 + distance, transform.position.z);
        GetComponent<Camera>().orthographicSize = Mathf.Sqrt(equilateralsqrt) + 2;
    }
}
