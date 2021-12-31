using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BuildingUIController : MonoBehaviour
{
    public Text scoreText;
    public CM_Move cmMove;

    private void Start()
    {
        scoreText.text = cmMove.currentTarget.childCount.ToString();
    }

    public void NextBuilding()
    {
        Transform building = cmMove.NextBuilding();
        scoreText.text = building.childCount.ToString();
    }

    public void PreviousBuilding()
    {
        Transform building = cmMove.PreviousBuilding();
        scoreText.text = building.childCount.ToString();
    }

    public void PreviousMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Capture()
    {
        ScreenCapture.CaptureScreenshot("Capture");
    }

    public void DestroyBuilding()
    {
        StartCoroutine(BuildingManager.Instance.destroyBuilding(cmMove.index));
        StartCoroutine(Collection.WaitThenCallback(1f, () => { NextBuilding(); }));
    }
}
