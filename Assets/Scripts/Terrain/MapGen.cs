using UnityEditor.AssetImporters;
using UnityEngine;
using UnityEngine.Serialization;
using System;
using System.Threading;
using System.Collections.Generic;

    public class MapGen : MonoBehaviour
    {
        public enum DrawMode { NoiseMap,ColourMap,Mesh, FalloffMap}
        public DrawMode drawMode;

        public Noise.NormalizeMode normalizeMode;

        public const int mapChunkSize = 241;
        [Range(0,6)]
        public int editorLod;
        public float noiseScale;

        public int octaves;
        [Range(0,1)]
        public float persistance;
        public float lacunarity;

        public int seed;
        public Vector2 offset;

        public bool useFalloff;

        public float meshHeightMultiplier;
        public AnimationCurve meshHeightCurve;
        public AnimationCurve falloffCurve;

        public bool autoUpdate;

        public TerrainType[] regions;

        private float[,] falloffMap;
        
        private Queue<MapThreadInfo<MapData>> mapDataThreadInfoQueue = new Queue<MapThreadInfo<MapData>>();
        private Queue<MapThreadInfo<MeshData>> meshDataThreadInfoQueue = new Queue<MapThreadInfo<MeshData>>();

        private void Awake()
        {
            falloffMap = FalloffGen.GenerateFalloffMap(mapChunkSize);
        }

        public void DrawMapInEditor()
        {
            MapData mapData = GenerateMapData(Vector2.zero);
            MapDisp display = FindObjectOfType<MapDisp>();

            if (drawMode == DrawMode.NoiseMap) {
                display.DrawTexture(TextureGen.TextureFromHeightMap(mapData.heightMap));    
            }

            else if(drawMode == DrawMode.ColourMap) {
                display.DrawTexture(TextureGen.TextureFromColourMap(mapData.colorMap,mapChunkSize,mapChunkSize));
            } else if(drawMode==DrawMode.Mesh) {
                display.DrawMesh(MeshGen.GenerateTerrainMesh(mapData.heightMap, meshHeightMultiplier, meshHeightCurve, editorLod), TextureGen.TextureFromColourMap(mapData.colorMap, mapChunkSize, mapChunkSize));
            }
            else if (drawMode == DrawMode.FalloffMap)
            {   
                display.DrawTexture(TextureGen.TextureFromHeightMap(FalloffGen.GenerateFalloffMap(mapChunkSize)));
            }
        }

        public void RequestMapData(Vector2 center, Action<MapData> callback) {
            ThreadStart threadStart = delegate {
                MapDataThread(center, callback);
            };
            new Thread(threadStart).Start();
        }

        void MapDataThread(Vector2 center, Action<MapData> callback) {
            MapData mapData = GenerateMapData(center);
            lock (mapDataThreadInfoQueue) {
                mapDataThreadInfoQueue.Enqueue(new MapThreadInfo<MapData>(callback,mapData));
            }
            
        }

        public void RequestMeshData(MapData mapData, int lod ,Action<MeshData> callback) {
            ThreadStart threadStart = delegate {
                MeshDataThread(mapData, lod ,callback);
            };
            new Thread(threadStart).Start();
        }

        void MeshDataThread(MapData mapData, int lod, Action<MeshData> callback) {
            MeshData meshData = MeshGen.GenerateTerrainMesh(mapData.heightMap, meshHeightMultiplier, meshHeightCurve, lod);
            lock (meshDataThreadInfoQueue) {
                meshDataThreadInfoQueue.Enqueue(new MapThreadInfo<MeshData>(callback,meshData));
            }
        }

        private void Update() {
            if (mapDataThreadInfoQueue.Count > 0) {
                for (int i = 0; i < mapDataThreadInfoQueue.Count; i++) {
                    MapThreadInfo<MapData> threadInfo = mapDataThreadInfoQueue.Dequeue();
                    threadInfo.callback(threadInfo.parameter);
                }
            }

            if (meshDataThreadInfoQueue.Count > 0) {
                for (int i = 0; i < meshDataThreadInfoQueue.Count; i++) {
                    MapThreadInfo<MeshData> threadInfo = meshDataThreadInfoQueue.Dequeue();
                    threadInfo.callback(threadInfo.parameter);
                }
            }
        }

        MapData GenerateMapData(Vector2 center) 
        {
            float[,] noisemap = Noise.GenerateNoiseMap(mapChunkSize, mapChunkSize, seed, noiseScale, octaves,persistance ,lacunarity, center + offset, normalizeMode);

            Color[] colourMap = new Color[mapChunkSize * mapChunkSize];
            
            for (int y = 0; y < mapChunkSize; y++) 
            {
                for (int x = 0; x < mapChunkSize; x++) 
                {
                    if (useFalloff)
                    {
                        noisemap[x, y] =  Mathf.Clamp01(noisemap[x, y] - falloffMap[x, y]);
                    }
                    
                    float currentHeight = noisemap[x, y];
                    for (int i = 0; i < regions.Length; i++) 
                    {
                        if (currentHeight >= regions[i].height) 
                        {
                            colourMap[y * mapChunkSize + x] = regions[i].colour;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }

            return new MapData(noisemap, colourMap);
        }

        private void OnValidate() 
        {
            if (lacunarity < 1) 
            {
                lacunarity = 1;
            }

            if (octaves < 0) 
            {
                octaves = 0;
            }

            falloffMap = FalloffGen.GenerateFalloffMap(mapChunkSize);
        }

        struct MapThreadInfo<T> {
            
            public readonly Action<T> callback;
            public readonly T parameter;

            public MapThreadInfo(Action<T> callback, T parameter) 
            {
                this.callback = callback;
                this.parameter = parameter;
            }
        }
        
    }

[System.Serializable]
public struct TerrainType 
{
    public string name;
    public float height;
    public Color colour;
}

public struct MapData 
{
    public readonly float[,] heightMap;
    public readonly Color[] colorMap;

    public MapData(float[,] heightMap, Color[] colorMap) 
    {
        this.heightMap = heightMap;
        this.colorMap = colorMap;
    }
}
