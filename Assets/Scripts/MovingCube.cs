using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovingCube : MonoBehaviour
{
    public static MovingCube CurrentCube { get; private set; }
    public static MovingCube LastCube { get; private set; }
    public MoveDirection MoveDirection { get; set; }

    [SerializeField]
    private float moveSpeed = 1f;
    [SerializeField]
    private float correction_value;

    private float boxDirection;

    [SerializeField]
    private GameObject DropCube;
    [SerializeField]
    private GameObject windowPrefab1;
    [SerializeField]
    private GameObject windowPrefab2;
    [SerializeField]
    private GameObject pulsePrefab;
    [SerializeField]
    private float betweenWindows;

    [SerializeField]
    private bool isStartCube;

    [SerializeField]
    private float objectScaleUpPoint = 0.6f;
    [SerializeField]
    private float objectScaleUpOver = 1.1f;
    [SerializeField]
    private float objectScaleUpUnder = 1.3f;

    private void OnEnable()
    {
        if (LastCube == null)
        {
            LastCube = GameObject.Find("@Start").GetComponent<MovingCube>();
        }
        if (this != LastCube)
            CurrentCube = this;

        boxDirection = 1f;
        transform.localScale = new Vector3(LastCube.transform.localScale.x, transform.localScale.y, LastCube.transform.localScale.z);

        if (!isStartCube)
            moveSpeed = -1.3f * ((transform.localScale.x + transform.localScale.z) / 2) + 2.6f;
    }
    void Update()
    {
        if (MoveDirection == MoveDirection.Z)
        {
            if (transform.position.z < -1.4)
                boxDirection = 1;
            else if (transform.position.z > 1.4)
                boxDirection = -1;
            transform.position += transform.forward * Time.deltaTime * moveSpeed * boxDirection;
        }
        else
        {
            if (transform.position.x < -1.4)
                boxDirection = 1;
            else if (transform.position.x > 1.4)
                boxDirection = -1;
            transform.position += transform.right * Time.deltaTime * moveSpeed * boxDirection;
        }
    }

    internal void Stop()
    {
        moveSpeed = 0;
        float hangover = GetHangover();

        float max = MoveDirection == MoveDirection.Z ? LastCube.transform.localScale.z : LastCube.transform.localScale.x;
        if (Mathf.Abs(hangover) >= max) // ??????
        {
            // LastCube = null;
            // CurrentCube = null;
            Destroy(LastCube.GetComponent<MovingCube>());
            Destroy(CurrentCube.GetComponent<MovingCube>());

            this.gameObject.AddComponent<Rigidbody>();

            GameManager.Instance.EndGame();
        }
        else if (Mathf.Abs(hangover) < correction_value) // ???????????? ????????? ??????
        {
            transform.position = new Vector3(LastCube.transform.position.x, transform.position.y, LastCube.transform.position.z);
            GameManager.Instance.ScoreUp();
            GameManager.Instance.PerfectCountUp();
            AttachWindow();
            StartCoroutine("executePulse");
            GameManager.Instance.AddCubeToBeSaved(CurrentCube.gameObject);
            GameManager.Instance.currentCube = CurrentCube.gameObject;
            SoundController.Instance.UpKeyPlay();
        }
        else
        {   // ?????????
            float direction = hangover > 0 ? 1 : -1;

            if (MoveDirection == MoveDirection.Z)
            {
                SplitCubeOnZ(hangover, direction);
            }
            else if (MoveDirection == MoveDirection.X)
            {
                SplitCubeOnX(hangover, direction);
            }
            GameManager.Instance.ScoreUp();
            GameManager.Instance.PerfectCountReset();
            AttachWindow();
            GameManager.Instance.AddCubeToBeSaved(CurrentCube.gameObject);
            GameManager.Instance.currentCube = CurrentCube.gameObject;
            SoundController.Instance.ResetKeyPlay();
        }
        if (GameManager.Instance.PerfectCountCheck() == 8)
        {
            if (MoveDirection == MoveDirection.X)
            { //* x ???????????? ?????????, x Scale??? ??????
                if (transform.localScale.x != 1)
                {
                    if (transform.localScale.x >= objectScaleUpPoint) //* objectScaleUpPoint ????????? ???????????? ??????, objectScaleUpOver?????? ??????.
                        transform.localScale = new Vector3(transform.localScale.x * objectScaleUpOver, transform.localScale.y, transform.localScale.z);
                    else //* objectScaleUpPoint ????????? ???????????? ??????, objectScaleUpUnder?????? ??????.
                        transform.localScale = new Vector3(transform.localScale.x * objectScaleUpUnder, transform.localScale.y, transform.localScale.z);
                }
            }
            else if (MoveDirection == MoveDirection.Z)
            { //* z ???????????? ?????????, z Scale??? ??????
                if (transform.localScale.z != 1)
                {
                    if (transform.localScale.z >= objectScaleUpPoint)
                        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z * objectScaleUpOver);
                    else
                        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z * objectScaleUpUnder);
                }
            }
            GameManager.Instance.PerfectCountReset();
        }

        Destroy(LastCube.GetComponent<MovingCube>());
        LastCube = this;
        Debug.Log(Mathf.Abs(hangover));
    }
    private void AttachWindow()
    {
        float HowManyWindowZ = CurrentCube.transform.localScale.x / ((windowPrefab1.transform.localScale.x + betweenWindows) * 10);
        float HowManyWindowX = CurrentCube.transform.localScale.z / ((windowPrefab1.transform.localScale.x + betweenWindows) * 10);

        for (int i = 1; i < HowManyWindowZ; i++)
        {
            int random = UnityEngine.Random.Range(0, 10);
            GameObject window;
            if (random > 6)
            {
                window = Instantiate(windowPrefab1);
            }
            else
            {
                window = Instantiate(windowPrefab2);
            }
            window.transform.position = new Vector3(
            CurrentCube.transform.position.x + CurrentCube.transform.localScale.x / 2 - ((windowPrefab1.transform.localScale.x + betweenWindows) * 10) * i + betweenWindows * 10,
            CurrentCube.transform.position.y,
            CurrentCube.transform.position.z + CurrentCube.transform.localScale.z / 2 + 0.001f);

            window.transform.localRotation = Quaternion.Euler(90f, 0, 0);
            window.transform.parent = CurrentCube.transform;
        }

        for (int i = 1; i < HowManyWindowX; i++)
        {
            int random = UnityEngine.Random.Range(0, 10);
            GameObject window;
            if (random > 6)
            {
                window = Instantiate(windowPrefab1);
            }
            else
            {
                window = Instantiate(windowPrefab2);
            }
            window.transform.position = new Vector3(CurrentCube.transform.position.x + CurrentCube.transform.localScale.x / 2 + 0.001f,
            CurrentCube.transform.position.y,
            CurrentCube.transform.position.z - CurrentCube.transform.localScale.z / 2 + ((windowPrefab1.transform.localScale.x + betweenWindows) * 10) * i - betweenWindows * 10);

            window.transform.parent = CurrentCube.transform;
        }
        Debug.Log("HowManyWindowZ : " + HowManyWindowZ);
        Debug.Log(CurrentCube.transform.localScale.x / HowManyWindowX);

        Debug.Log("HowManyWindowX : " + HowManyWindowX);
        Debug.Log(CurrentCube.transform.localScale.x / HowManyWindowX);
    }

    private float GetHangover()
    {
        if (MoveDirection == MoveDirection.Z)
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

        float newXposition = LastCube.transform.position.x + (hangover / 2f);
        transform.localScale = new Vector3(newXSize, transform.localScale.y, transform.localScale.z);
        transform.position = new Vector3(newXposition, transform.position.y, transform.position.z);

        float cubeEdge = transform.position.x + (newXSize / 2f * direction);
        float fallingBlockXPosition = cubeEdge + (fallingBlockSize / 2f * direction);

        SpawnDropCube(fallingBlockXPosition, fallingBlockSize);
    }

    private void SplitCubeOnZ(float hangover, float direction)
    {
        float newZSize = LastCube.transform.localScale.z - Mathf.Abs(hangover);
        float fallingBlockSize = transform.localScale.z - newZSize;

        float newZposition = LastCube.transform.position.z + (hangover / 2f);
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, newZSize);
        transform.position = new Vector3(transform.position.x, transform.position.y, newZposition);

        float cubeEdge = transform.position.z + (newZSize / 2f * direction);
        float fallingBlockZPosition = cubeEdge + (fallingBlockSize / 2f * direction);

        SpawnDropCube(fallingBlockZPosition, fallingBlockSize);
    }
    private void SpawnDropCube(float fallingBlockPosition, float fallingBlockSize)
    {
        var cube = Instantiate(DropCube);

        if (MoveDirection == MoveDirection.Z)
        {
            cube.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, fallingBlockSize);
            cube.transform.position = new Vector3(transform.position.x, transform.position.y, fallingBlockPosition);
        }
        else if (MoveDirection == MoveDirection.X)
        {
            cube.transform.localScale = new Vector3(fallingBlockSize, transform.localScale.y, transform.localScale.z);
            cube.transform.position = new Vector3(fallingBlockPosition, transform.position.y, transform.position.z);
        }

        cube.AddComponent<Rigidbody>();
    }
    IEnumerator executePulse()
    {
        var pulse = Instantiate(pulsePrefab);
        var pulseVector = new Vector3(CurrentCube.transform.position.x, CurrentCube.transform.position.y - CurrentCube.transform.localScale.y / 2, CurrentCube.transform.position.z);
        var pulseScale = new Vector3(CurrentCube.transform.localScale.x * 0.1f, 1, CurrentCube.transform.localScale.z * 0.1f);
        pulse.transform.position = pulseVector;
        pulse.transform.localScale = pulseScale;
        yield return new WaitForSeconds(pulsePrefab.GetComponent<PulseEffect>().secondPulseStart);
        var pulse2 = Instantiate(pulsePrefab);
        pulse2.transform.position = pulseVector;
        pulse2.transform.localScale = pulseScale;
    }
}