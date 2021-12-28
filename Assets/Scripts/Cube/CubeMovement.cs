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

    public Direction side;
    public Transform targetCube;
    public float moveSpeed;

    private Vector3 normalDirection;
    private void OnEnable()
    {
        side = Direction.X;
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
        side = side == Direction.X ? Direction.Z : Direction.X;
        if (side == Direction.X)
        {
            targetCube.transform.position = new Vector3(Constants.CUBEDISTANCE, 0, 0);
            normalDirection = Vector3.right;
        }
        else if (side == Direction.Z)
        {
            targetCube.transform.position = new Vector3(0, 0, Constants.CUBEDISTANCE);
            normalDirection = Vector3.forward;
        }
        return side;
    }
}
