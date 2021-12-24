using System;
using UnityEngine;

public class CameraPanning : MonoBehaviour
{
    private Vector3 touchStart;
    private Camera cam;
    public float startZ = 0;

    private void Awake()
    {
        cam = this.GetComponent<Camera>();
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