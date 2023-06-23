using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    private int[] triangles;
    private Vector3[] verticies;

    
    private Vector3[] verticiesCopy;


    public Vector2 Offset;
    public Vector2Int GridXZ;

    private Vector2 offset;
    private Vector2Int gridXZ;

    public float GridScale;
    private float gridScale;


    public float NoiseHeight;
    public float NoiseScaleMultiplier;

    private float noiseHeight;
    private float noiseScaleMultiplier;

    private Vector2 noiseOffset;
    public Vector2 NoiseOffset;

    private Mesh mesh;

    private Color[] colours;

    private Color brown = new Color32(75, 25, 0, 1);

    public float radius;

    private void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        GetComponent<MeshCollider>().sharedMesh = mesh;

        if (gridXZ != GridXZ || offset != Offset || noiseHeight != NoiseHeight || noiseScaleMultiplier != NoiseScaleMultiplier || gridScale != GridScale || noiseOffset != NoiseOffset)
        {
            offset = Offset;
            gridXZ = GridXZ;

            gridScale = GridScale;

            noiseHeight = NoiseHeight;
            noiseScaleMultiplier = NoiseScaleMultiplier;

            noiseOffset = NoiseOffset;

            GenerateMesh();
            colours = new Color[verticies.Length];

            for (int i = 0; i < colours.Length; i++)
            {
                colours[i] = Color.green;
            }
            UpdateMesh();

            verticiesCopy = verticies;
        }
        
    }

    private void Update()
    {
        //if(gridXZ != GridXZ || offset != Offset || noiseHeight != NoiseHeight || noiseScaleMultiplier != NoiseScaleMultiplier || gridScale != GridScale || noiseOffset != NoiseOffset)
        //{
        //    offset = Offset;
        //    gridXZ = GridXZ;

        //    gridScale = GridScale;

        //    noiseHeight = NoiseHeight;
        //    noiseScaleMultiplier = NoiseScaleMultiplier;

        //    noiseOffset = NoiseOffset;

        //    GenerateMesh();
        //    UpdateMesh();
        //}


        //if (posToChange != null)
        //{
        //    for (int i = 0, x = 0; x <= gridXZ.x; x++)
        //    {
        //        for (int z = 0; z <= gridXZ.y; z++)
        //        {
        //            float y = verticies[i].y;

        //            if (posToChange.position.x == verticies[i].x && posToChange.position.z == verticies[i].z)
        //            {
        //                Debug.Log("Inside if statement to do deformation");
        //                verticies[i] = verticies[i] - new Vector3(0, 1, 0);

        //                //return;
        //            }
        //            else
        //            {
        //                verticies[i] = new Vector3((z * gridScale) + offset.x, y, (x * gridScale) + offset.y);

        //            }
        //            i++;
        //        }
        //    }
        //}


        //DeformMesh();

        Debug.Log(posToChange);
        //UpdateMesh();
    }

    public LayerMask projectile;

    public Transform posToChange;

    //just need this to work


    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Projectile"))
        {
            posToChange = col.gameObject.transform;

            DeformMesh();

            posToChange = null;
        }
    }

    private void DeformMesh()
    {
        for (int i = 0; i < verticiesCopy.Length; i++)
        {
            Vector3 distanceFromProjectile = verticiesCopy[i] - posToChange.position;

            Debug.Log(distanceFromProjectile.sqrMagnitude);
            if (distanceFromProjectile.sqrMagnitude < (radius * radius))
            {
                verticiesCopy[i] = verticiesCopy[i] + (Vector3.down * 0.5f);

                //Debug.Log("Inside if statement to do deformation");
                colours[i] = brown;
            }
        }


        mesh.vertices = verticiesCopy;
        mesh.colors = colours;
        GetComponent<MeshCollider>().sharedMesh = mesh;
        mesh.RecalculateNormals();
    }


    private void GenerateMesh()
    {
        verticies = new Vector3[(gridXZ.x + 1) * (gridXZ.y + 1)];

        for (int i = 0, x = 0; x <= gridXZ.x; x++)
        {
            for (int z = 0; z <= gridXZ.y; z++)
            {
                float y = Mathf.PerlinNoise(x * noiseScaleMultiplier + noiseOffset.y, z * noiseScaleMultiplier + noiseOffset.x) * noiseHeight;
                float Y = Mathf.PerlinNoise(x * noiseScaleMultiplier + noiseOffset.y, z * noiseScaleMultiplier + noiseOffset.x) * noiseHeight * 0.5f;

                verticies[i] = new Vector3((z * gridScale) + offset.x, y*Y, (x * gridScale) + offset.y);
                i++;
            }
        }


        triangles = new int[gridXZ.x * gridXZ.y * 6];//* 6 as each quad of the mesh needs 6 vetexs for the 2 tris

        int vert = 0;
        int tri = 0;

        for (int x = 0; x < gridXZ.x; x++)
        {
            for (int z = 0; z < gridXZ.y; z++)
            {
                triangles[tri + 0] = vert + 0;
                triangles[tri + 1] = vert + gridXZ.y + 1;
                triangles[tri + 2] = vert + 1;
                triangles[tri + 3] = vert + 1;
                triangles[tri + 4] = vert + gridXZ.y + 1;
                triangles[tri + 5] = vert + gridXZ.y + 2;

                vert++;
                tri += 6;
            }
            vert++;
        }



    }

    private void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = verticies;
        mesh.triangles = triangles;
        
        mesh.colors = colours;
        
        mesh.RecalculateNormals();
        
        GetComponent<MeshCollider>().sharedMesh = mesh;
    }

    //private void OnDrawGizmos()
    //{
    //    if (verticies == null)
    //        return;
    //    Gizmos.color = Color.red;
    //    for (int i = 0; i < verticies.Length; i++)
    //    {
    //        //Gizmos.DrawCube(Verticies[i], new Vector3(0.1f,0.1f,0.1f));
    //        Gizmos.DrawSphere(verticies[i], 1f);
    //    }
    //}
}