using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomMeshTesting : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start");

        //Creating vars
        Mesh mesh = new Mesh();

        Vector3[] vertices = new Vector3[4];
        Vector2[] uv = new Vector2[4];
        int[] triangles = new int[6];

        //Vetices
        vertices[0] = new Vector3(0, 0);
        vertices[1] = new Vector3(0, 100);
        vertices[2] = new Vector3(100, 100);
        vertices[3] = new Vector3(100, 0);

        //Triagles
        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;

        triangles[3] = 0;
        triangles[4] = 2;
        triangles[5] = 3;

        //UV
        uv[0] = new Vector2(0, 0);
        uv[0] = new Vector2(0, 1);
        uv[0] = new Vector2(1, 1);
        uv[0] = new Vector2(1, 0);

        //Add values in the mesh
        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;

        //Set mesh
        this.gameObject.GetComponent<MeshFilter>().mesh = mesh;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
