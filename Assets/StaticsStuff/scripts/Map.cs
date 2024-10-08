using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Map : MonoBehaviour
{
    [Header("General Settings for map")][Space] 
    [SerializeField] Vector2Int _mapSize = new Vector2Int(5, 5);
    [SerializeField] float _cellSize;
    [SerializeField] MapModule[] _mapModules;
    [SerializeField] List<MapModuleContact> _contactTypes = new List<MapModuleContact>();
    public MapCell[,] MapCellsMatrix;
    public int RowsCount => MapCellsMatrix.GetLength(0);
    public int ColumnsCount => MapCellsMatrix.GetLength(1);
    private MapCell[] _mapCellsArray;
    
    [Header("Settings for points and prefabs from ground foliage ")][Space] 
    [SerializeField]private List<GameObject> groundFoliage;

    public float radiusF = 1;
    public int rejectionSamplesF = 30;
   
    
    [Header("Settings for points and prefabs from trees ")][Space] 
    [SerializeField]private List<GameObject> trees;
    
    public float radiusT = 1;
    public int rejectionSamplesT = 30;
    
    private bool once;
    private Vector3 _regionSize;
    private List<GameObject> _objectList;
    
    private void Awake()
    {
        _regionSize = new Vector3(_mapSize.x * _cellSize, 0, _mapSize.y * _cellSize);
       
        
        InizializeMap();
        FillCells();
        CreateMap();
        once = false;
    }

    private void Update()
    {
        /*if (once == false)
        {
            SpawnObjectsWithPointCloud(trees,radiusT,_regionSize,rejectionSamplesT);
            SpawnObjectsWithPointCloud(groundFoliage,radiusF,_regionSize,rejectionSamplesF);
            once = true;
        }*/
    }

    void InizializeMap()
    {
        MapCellsMatrix = new MapCell[_mapSize.x, _mapSize.y];

        var mapModules = GetMapModules();
        for (int i = 0; i < _mapSize.x; i++)
        {
            for (int j = 0; j < _mapSize.y; j++)
                MapCellsMatrix[i, j] = new MapCell(this, new Vector2Int(i, j), new List<MapModuleState>(mapModules));
        }
        _mapCellsArray = MapCellsMatrix.Cast<MapCell>().ToArray();
    }

    void FillCells()
    {
        MapCell cell = null;
        
        do
        {
            var cellsWithUnselectedState = _mapCellsArray.Where(c => c.States.Count > 1).ToArray();

            if (cellsWithUnselectedState.Length == 0)
                return;

            var minStatesCount = cellsWithUnselectedState.Min(c => c.States.Count);

            cell = cellsWithUnselectedState.First(c => c.States.Count == minStatesCount);
        }
        while (cell.TrySelectState(states => states[Random.Range(0, states.Count)]));
    }

    void CreateMap()
    {
        for (int i = 0; i < _mapSize.x; i++)
        {
            for (int j = 0; j < _mapSize.y; j++)
            {
                var localPosition = new Vector3(i * _cellSize, 0, j * _cellSize);
                MapCellsMatrix[i, j].States[0].InstantiatePrefab(this,localPosition);
            }
        }
    }

    List<MapModuleState> GetMapModules()
    {
        List<MapModuleState> mapModules = new List<MapModuleState>();
        foreach (var module in _mapModules)
        {
            mapModules.AddRange(module.GetMapModulesFromPrefab());
        }     
        return mapModules;
    }

    public MapModuleContact GetContact(string contactType)
    {
        return _contactTypes.First(contact => contact.ContactType == contactType);
    }
    
    void SpawnObjectsWithPointCloud(List<GameObject> spawnList,float radius, Vector3 sampleRegionSize, int numSamplesBeforeRejection = 30)
    {
        RaycastHit rayCastFromPoints;
        List<Vector3> points = PoissonDiscSampling.GeneratePoints(radius, sampleRegionSize, numSamplesBeforeRejection);
        
        for (int i = 0; i < points.Count; i++)
        {
              if (Physics.Raycast(new Vector3(points[i].x, 10, points[i].z), Vector3.down, out rayCastFromPoints))
              {
                  int random = Random.Range(0, spawnList.Count);
                  float randomRot = Random.Range(0f, 360f);
                  
                  //var lastPrefab = PrefabUtility.InstantiatePrefab(spawnList[random]) as GameObject;
                  //lastPrefab.transform.position = rayCastFromPoints.point;
                  //lastPrefab.transform.rotation = new Quaternion(0, randomRot, 0, 0);
                
              }
        }
    }

    public void RemoveOverlap()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 1,LayerMask.NameToLayer("Greens"));
        Debug.Log(hitColliders.Length);
        foreach (Collider hitCollider in hitColliders)
        {
            Debug.Log(hitCollider.name);
            Destroy(hitCollider.gameObject);
        }
    }
}