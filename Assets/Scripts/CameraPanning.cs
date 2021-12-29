using System;
using UnityEngine;

public class CameraPanning : MonoBehaviour
{
    private Vector3 touchStart;
    private Camera cam;
    public float startZ = 0;

    private BuildingManager buildingManager;

    private void Awake()
    {
        cam = this.GetComponent<Camera>();
    }
    private void Start()
    {
        buildingManager = GameObject.Find("@BuildingManager").GetComponent<BuildingManager>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            touchStart = GetWorldPosition(startZ);
        }
        if (Input.GetMouseButton(0))
        {
            Vector3 direction = touchStart - GetWorldPosition(startZ);
            cam.transform.position += new Vector3(direction.x, 0, 0);
        }
        if (buildingManager.count > 1)
            cam.transform.position = new Vector3(Mathf.Clamp(cam.transform.position.x, 0f, (buildingManager.count - 1) * buildingManager.distance), cam.transform.position.y, cam.transform.position.z);
        else
            cam.transform.position = new Vector3(Mathf.Clamp(cam.transform.position.x, 0f, 0f), cam.transform.position.y, cam.transform.position.z);
    }
    private Vector3 GetWorldPosition(float z)
    {
        Ray mousePos = cam.ScreenPointToRay(Input.mousePosition);
        Plane ground = new Plane(Vector3.forward, new Vector3(0, 0, z));
        float distance;
        ground.Raycast(mousePos, out distance);
        return mousePos.GetPoint(distance);
    }
}