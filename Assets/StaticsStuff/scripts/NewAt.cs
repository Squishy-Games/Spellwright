using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewAt : MonoBehaviour
{
    [SerializeField] private Vector3 SizeTile = new Vector3(1, 1, 1);

    [SerializeField] private int detailResolution = 1024;
    [SerializeField] private int resolutionPerPatch = 16;
    // Start is called before the first frame update
    void Start ()
    {
        GameObject terrain;
        
        //create a new terrain data
        TerrainData _terrainData = new TerrainData();

        //set terrain width, height, length
        _terrainData.size = SizeTile;
        _terrainData.SetDetailResolution(detailResolution,resolutionPerPatch);
       terrain = Terrain.CreateTerrainGameObject(_terrainData);
       terrain.transform.position = transform.position;
       terrain.transform.parent = transform;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
