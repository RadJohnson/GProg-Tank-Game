using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    //public Vector2 WorldSize;

    public Vector2 terrainGridSize;

    public GameObject player;

    public GameObject terrain;

    public List<GameObject> terrainList = new List<GameObject>();

    private void Start()
    {
        for (int x = 0; x < terrainGridSize.x; x++)
        {
            for (int z = 0; z < terrainGridSize.y; z++)
            {
                var obj = Instantiate(terrain, new Vector3(x * 100 * 5, 0, z * 100 * 5), Quaternion.identity);
                obj.GetComponent<TerrainGenerator>().NoiseOffset = new Vector2(x * 5, z * 5);
                terrainList.Add(obj);
            }
        }
    }

    void Update()
    {
        
    }
}
