using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MovingCube : MonoBehaviour
{
    public static MovingCube CurrentCube {get; private set;}
    public static MovingCube LastCube {get; private set;}
    public MoveDirection MoveDirection {get; set;}

    [SerializeField]
    private float moveSpeed = 1f;
    [SerializeField]
    private float correction_value;

    private float boxDirection;

    [SerializeField]
    private GameObject DropCube;

    private void OnEnable() {
        if(LastCube == null)
        {
            LastCube = GameObject.Find("Start").GetComponent<MovingCube>();
        }
        if(this != LastCube)
        CurrentCube = this;

        boxDirection = 1f;
        transform.localScale = new Vector3(LastCube.transform.localScale.x, transform.localScale.y, LastCube.transform.localScale.z);
    }
    void Update()
    {
        if(MoveDirection == MoveDirection.Z)
        {
            if(transform.position.z < -1.4)
                boxDirection = 1;
            else if(transform.position.z >1.4)
                boxDirection = -1;
            transform.position += transform.forward * Time.deltaTime * moveSpeed * boxDirection;
        } 
        else
        {
            if(transform.position.x < -1.4)
                boxDirection = 1;
            else if(transform.position.x >1.4)
                boxDirection = -1;
            transform.position += transform.right * Time.deltaTime * moveSpeed * boxDirection;
        }
    }

    internal void Stop()
    {
        moveSpeed = 0;
        float hangover = GetHangover();

        float max = MoveDirection == MoveDirection.Z ? LastCube.transform.localScale.z:LastCube.transform.localScale.x;
        if(Mathf.Abs(hangover) >= max)
        {
            LastCube = null;
            CurrentCube = null;

            this.gameObject.AddComponent<Rigidbody>();

            GameManager.Instance.EndGame();
        }
        else if(Mathf.Abs(hangover) < correction_value)
        {
            transform.position = new Vector3(LastCube.transform.position.x,transform.position.y,LastCube.transform.position.z);
            GameManager.Instance.ScoreUp();
        }
        else{
            float direction = hangover > 0 ? 1 : -1;

            if(MoveDirection == MoveDirection.Z)
            {
                SplitCubeOnZ(hangover,direction);
            }
            else if(MoveDirection == MoveDirection.X)
            {
                SplitCubeOnX(hangover,direction);
            }
            GameManager.Instance.ScoreUp();
        }


        LastCube = this;
        Debug.Log(Mathf.Abs(hangover));
    }

    private float GetHangover()
    {
        if(MoveDirection == MoveDirection.Z)
        {
            return transform.position.z - LastCube.transform.position.z;
        }
        else
            return transform.position.x - LastCube.transform.position.x;
    }

    private void SplitCubeOnX(float hangover, float direction)
    {
        float newXSize = LastCube.transform.localScale.x - Mathf.Abs(hangover);
        float fallingBlockSize = transform.localScale.x - newXSize;

        float newXposition = LastCube.transform.position.x + (hangover/2f);
        transform.localScale = new Vector3(newXSize,transform.localScale.y,transform.localScale.z);
        transform.position = new Vector3(newXposition, transform.position.y, transform.position.z );

        float cubeEdge = transform.position.x + (newXSize / 2f * direction);
        float fallingBlockXPosition = cubeEdge + (fallingBlockSize/2f*direction);

        SpawnDropCube(fallingBlockXPosition, fallingBlockSize);
    }

    private void SplitCubeOnZ(float hangover, float direction)
    {
        float newZSize = LastCube.transform.localScale.z - Mathf.Abs(hangover);
        float fallingBlockSize = transform.localScale.z - newZSize;

        float newZposition = LastCube.transform.position.z + (hangover/2f);
        transform.localScale = new Vector3(transform.localScale.x,transform.localScale.y,newZSize);
        transform.position = new Vector3(transform.position.x, transform.position.y, newZposition);

        float cubeEdge = transform.position.z + (newZSize / 2f * direction);
        float fallingBlockZPosition = cubeEdge + (fallingBlockSize/2f*direction);

        SpawnDropCube(fallingBlockZPosition, fallingBlockSize);
    }
    private void SpawnDropCube(float fallingBlockPosition, float fallingBlockSize)
    {
        var cube = Instantiate(DropCube);

        if(MoveDirection == MoveDirection.Z)
        {
            cube.transform.localScale = new Vector3(transform.localScale.x,transform.localScale.y,fallingBlockSize);
            cube.transform.position = new Vector3(transform.position.x,transform.position.y,fallingBlockPosition);
        }
        else if(MoveDirection == MoveDirection.X)
        {
            cube.transform.localScale = new Vector3(fallingBlockSize, transform.localScale.y,transform.localScale.z);
            cube.transform.position = new Vector3(fallingBlockPosition,transform.position.y,transform.position.z);
        }

        cube.AddComponent<Rigidbody>();
    }


}
