using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class HexRenderer : MonoBehaviour
{
    private Mesh mesh;
    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;

    public Material Material;

    private List<Face> faces;

    public float Height;
    public float OuterSize; 
    public float InnerSize;

    public bool IsFlatTopped;

    private void Awake()
    {
        this.meshFilter = this.GetComponent<MeshFilter>();
        this.meshRenderer= this.GetComponent<MeshRenderer>();

        this.mesh = new Mesh();
        this.mesh.name = "Hex";

        this.meshFilter.mesh = this.mesh;
        this.meshRenderer.material = this.Material;
    } 

    private void OnEnable()
    {
        DrawMesh();
    }

    private void OnValidate()
    {
        if (Application.isPlaying)
        {
            DrawMesh();
        }
    }

    private void DrawMesh()
    {
        DrawFaces();
        CombineFaces();
    }

    private void CombineFaces()
    {
       List<Vector3> vertices = new List<Vector3>();
       List<int> triangles = new List<int>();
       List<Vector2> uvs = new List<Vector2>();

       for (int i = 0; i < this.faces.Count; i++)
       {
           vertices.AddRange(this.faces[i].Vertices);
           uvs.AddRange(this.faces[i].Uvs);

           // Offset the triangles
           int offset = (4*i);
           foreach (int  triangle in this.faces[i].Triangles)
           {
               triangles.Add(triangle + offset);
           }
       }


       this.mesh.vertices = vertices.ToArray();
       this.mesh.triangles = triangles.ToArray();
       this.mesh.uv = uvs.ToArray();
       this.mesh.RecalculateNormals();
    }

    private void DrawFaces()
    {
        this.faces = new List<Face>();
        // Top faces
        for (int point = 0; point < 6; point++)
        {
            this.faces.Add(CreateFace(this.InnerSize, this.OuterSize, this.Height / 2f, this.Height / 2f, point));
        }

        // Bottom faces
        for (int point = 0; point < 6; point++)
        {
            this.faces.Add(CreateFace(this.InnerSize, this.OuterSize, -this.Height / 2f, -this.Height / 2f, point, true));
        }

        // Outer side faces 
        for (int point = 0; point < 6; point++)
        {
            this.faces.Add(CreateFace(this.OuterSize, this.OuterSize, this.Height / 2f, -this.Height / 2f, point, true));
        }

        // Inner side faces 
        for (int point = 0; point < 6; point++)
        {
            this.faces.Add(CreateFace(this.InnerSize, this.InnerSize, this.Height / 2f, -this.Height / 2f, point));
        }           
    }

    private Face CreateFace(float innerRadius, float outerRadius, float heightA, float heightB, int point, bool reverse = false)
    {
        Vector3 pointA = GetPoint(innerRadius, heightB, point);
        Vector3 pointB = GetPoint(innerRadius, heightB, (point < 5) ? point + 1 : 0);
        Vector3 pointC = GetPoint(outerRadius, heightA, (point < 5) ? point + 1 : 0);
        Vector3 pointD = GetPoint(outerRadius, heightA, point);

        List<Vector3> vertices = new List<Vector3>
        {
            pointA, pointB, pointC, pointD
        };

        List<int> triangles = new List<int>
        {
            0, 1, 2, 2, 3, 0
        };

        List<Vector2> uvs = new List<Vector2>
        {
            new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1)
        };

        if (reverse)
        {
            vertices.Reverse();
        }

        return new Face(vertices, triangles, uvs);
    }

    private Vector3 GetPoint(float size, float height, int index)
    {
        float angleDegrees = this.IsFlatTopped ? 60 * index : 60 * index - 30;
        float angleRadians = Mathf.PI / 180f * angleDegrees;
        return new Vector3(size * Mathf.Cos(angleRadians), height, size * Mathf.Sin(angleRadians));
    }
}
