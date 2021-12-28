using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeDynamicMesh : MonoBehaviour
{
    private MeshFilter meshFilter;
    public Vector3[] vertices;
    public Vector3[] purifiedVertices;
    private void Awake()
    {
        meshFilter = GetComponent<MeshFilter>();
    }

    private void Start()
    {
        vertices = meshFilter.mesh.vertices;
        foreach (Vector3 vertex in vertices)
        {
        }
        
    }

    void Update()
    {
        meshFilter.mesh.vertices = vertices;
    }
}
