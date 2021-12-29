using System;
using System.Collections;
using System.Collections.Generic;
using Commons;
using UnityEngine;

public class CubeMovement : MonoBehaviour
{
    public enum Direction
    {
        X,Z
    }

    public Direction direction;
    public Transform targetCube;
    public float moveSpeed;

    private Vector3 normalDirection;
    private void OnEnable()
    {
        direction = Direction.X;
        normalDirection = new Vector3(1, 0, 0);
    }

    private void Update()
    {
        if ((-Constants.CUBEDISTANCE > targetCube.position.z  || targetCube.position.z > Constants.CUBEDISTANCE )
            || (-Constants.CUBEDISTANCE > targetCube.position.x  || targetCube.position.x > Constants.CUBEDISTANCE))
            normalDirection *= -1;
        targetCube.transform.position += normalDirection * moveSpeed * Time.deltaTime;

    }


    public Direction ChangeSide()
    {
        direction = direction == Direction.X ? Direction.Z : Direction.X;
        if (direction == Direction.X)
        {
            targetCube.transform.position = new Vector3(Constants.CUBEDISTANCE, 0, 0);
            normalDirection = Vector3.right;
        }
        else if (direction == Direction.Z)
        {
            targetCube.transform.position = new Vector3(0, 0, Constants.CUBEDISTANCE);
            normalDirection = Vector3.forward;
        }
        return direction;
    }
}
