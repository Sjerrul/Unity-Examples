using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshGenerator : MonoBehaviour
{
    Mesh mesh;
    Vector3[] vertices;
    int[] triangles;

    public int xSize = 20;
    public int zSize = 20;

    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        StartCoroutine(CreateShape());
    }
    

    private IEnumerator CreateShape()
    {
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];

        int i = 0;
        for (int z = 0; z < zSize + 1; z++)
        {
            for (int x = 0; x < xSize + 1; x++)
            {
                float y = Mathf.PerlinNoise(x * 0.3f, z * 0.3f) * 2f;

                vertices[i] = new Vector3(x, y, z);
                i++;
            }
        }

        triangles = new int[6 * zSize * xSize];
        int vertex = 0;
        int triangle = 0;

        for (int z = 0; z < zSize; z++)
        {
             for (int x = 0; x < xSize; x++)
            {

                triangles[triangle + 0] = vertex + 0;
                triangles[triangle + 1] = vertex + xSize + 1;
                triangles[triangle + 2] = vertex + 1;
                triangles[triangle + 3] = vertex + 1;
                triangles[triangle + 3] = vertex + 1;
                triangles[triangle + 4] = vertex + xSize + 1;
                triangles[triangle + 5] = vertex + xSize + 2;;

                vertex++;
                triangle += 6;

                yield return new WaitForSeconds(0.01f);
            }

            vertex++;
        }
       
        
    }

    private void UpdateMesh()
    {
        this.mesh.Clear();
        this.mesh.vertices = vertices;
        this.mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }



    // Update is called once per frame
    void Update()
    {
         UpdateMesh();
    }

    void OnDrawGizmos()
    {
        if (vertices == null)
        {
            return;

        }
        for (int i = 0; i < vertices.Length; i++)
        {
            Gizmos.DrawSphere(vertices[i], 0.1f);   
        }
    }
}
