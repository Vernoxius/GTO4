using System;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{

    public int rows = 8;
    public int collums = 8;
    public GameObject tile;
    public GameObject tileIns;
    public GameObject[,] tileList;
    public bool spawning = false;

    // Use this for initialization
    void Start()
    {
        tileList = new GameObject[8,8];
        for (int z = 0; z < rows; z++)
        {
            for (int x = 0; x < rows; x++)
            {
                tileIns = Instantiate(tile, new Vector3(x, 0, z), new Quaternion(0, 0, 0, 0));
                tileIns.name = "Tile_" + x + "_" + z;
                tileIns.transform.SetParent(this.transform);
                tileIns.GetComponent<Tile>().x = x;
                tileIns.GetComponent<Tile>().y = z;
                tileList[x,z] = tileIns;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
