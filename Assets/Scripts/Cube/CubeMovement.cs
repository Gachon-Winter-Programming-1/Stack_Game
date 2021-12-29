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
        normalDirection = Vector3.right;
    }

    private void Update()
    {
        Move();
    }

    public void Move()
    {
        //todo : fix cube position
        if ((-Constants.CUBEDISTANCE > targetCube.position.z  || targetCube.position.z > Constants.CUBEDISTANCE )
            || (-Constants.CUBEDISTANCE > targetCube.position.x  || targetCube.position.x > Constants.CUBEDISTANCE))
        {normalDirection *= -1;}
        targetCube.position += normalDirection * moveSpeed * Time.deltaTime;

    }
    
    
    public Direction ChangeSide(Transform previousCube)
    {
        side = side == Direction.X ? Direction.Z : Direction.X;
        if (side == Direction.X)
        {
            targetCube.transform.position = new Vector3(Constants.CUBEDISTANCE, previousCube.position.y, previousCube.position.z);
            normalDirection = Vector3.right;
        }
        else if (side == Direction.Z)
        {
            targetCube.transform.position = new Vector3(previousCube.position.x, previousCube.position.y, Constants.CUBEDISTANCE);
            normalDirection = Vector3.forward;
        }
        return side;
    }
}
