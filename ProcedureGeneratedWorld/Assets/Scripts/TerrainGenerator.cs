﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    public int sizeX;
    public int sizeY;

    private MeshFilter m_MeshFilter = null;
    private MeshRenderer m_MeshRender = null;

    private void Start()
    {
        m_MeshFilter = GetComponent<MeshFilter>();

        GenerateMesh();
    }

    private void GenerateMesh()
    {
        Mesh mesh = new Mesh();

        Vector3[] vertices = new Vector3[(sizeX + 1) * (sizeY + 1)];
        int[] triangles = new int[sizeX * sizeY * 6];
        Vector2[] uv = new Vector2[vertices.Length];
        Vector4[] tangents = new Vector4[vertices.Length];
        Vector4 tangent = new Vector4(1f, 0f, 0f, -1f);

        for (int i = 0, y = 0; y <= sizeY; y++)
        {
            for (int x = 0; x <= sizeX; x++, i++)
            {
                vertices[i] = new Vector3(x, 0, y);

                uv[i] = new Vector2(x / (float)sizeX, y / (float)sizeY);

                tangents[i] = tangent;
            }
        }

        for (int ti = 0, vi = 0, y = 0; y < sizeY; y++, vi++)
        {
            for (int x = 0; x < sizeX; x++, ti += 6, vi++)
            {
                triangles[ti] = vi;
                triangles[ti + 3] = triangles[ti + 2] = vi + 1;
                triangles[ti + 4] = triangles[ti + 1] = vi + sizeX + 1;
                triangles[ti + 5] = vi + sizeX + 2;
            }
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uv;
        mesh.tangents = tangents;

        mesh.RecalculateNormals();

        m_MeshFilter.mesh = mesh;
    }
}
