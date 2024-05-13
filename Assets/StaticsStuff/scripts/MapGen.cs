using System;
using UnityEditor.AssetImporters;
using UnityEngine;
using UnityEngine.Serialization;

namespace StaticsStuff.scripts
{
    public class MapGen : MonoBehaviour
    {
        public Terrain piece;
        public enum DrawMode { NoiseMap,ColourMap,Mesh}
        public DrawMode drawMode;


        const int mapChunkSize = 241;
        [Range(0,6)]
        public int levelOfDetail;
        public float noiseScale;

        public int octaves;
        [Range(0,1)]
        public float persistance;
        public float lacunarity;

        public int seed;
        public Vector2 offset;

        public float meshHeightMultiplier;
        public AnimationCurve meshHeightCurve;

        public bool autoUpdate;

        public TerrainType[] regions;
        public void GenerateMap()
        {
            float[,] noisemap = Noise.GenerateNoiseMap(mapChunkSize, mapChunkSize, seed, noiseScale, octaves,persistance ,lacunarity, offset);

            Color[] colourMap = new Color[mapChunkSize * mapChunkSize];
            
            for (int y = 0; y < mapChunkSize; y++)
            {
                for (int x = 0; x < mapChunkSize; x++)
                {
                    float currentHeight = noisemap[x, y];
                    for (int i = 0; i < regions.Length; i++)
                    {
                        if (currentHeight<= regions[i].height)
                        {
                            colourMap[y * mapChunkSize + x] = regions[i].colour;
                            break;
                        }
                    }
                }
            }
            
            MapDisp display = FindObjectOfType<MapDisp>();

            if (drawMode == DrawMode.NoiseMap)
            {
                display.DrawTexture(TextureGen.TextureFromHeightMap(noisemap));    
            }

            else if(drawMode == DrawMode.ColourMap)
            {
                display.DrawTexture(TextureGen.TextureFromColourMap(colourMap,mapChunkSize,mapChunkSize));
            }
            else if(drawMode==DrawMode.Mesh)
            {
                display.DrawMesh(MeshGen.GenerateTerrainMesh(noisemap, meshHeightMultiplier, meshHeightCurve, levelOfDetail), TextureGen.TextureFromColourMap(colourMap, mapChunkSize, mapChunkSize));
                TerrainData terain = piece.terrainData;
                terain.SetHeights(0, 0,  noisemap);
            }
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
