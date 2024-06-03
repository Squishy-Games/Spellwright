using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

    public class terrainGenerator : MonoBehaviour
    {
        [SerializeField] private Texture2D _noiseTexture;
        [SerializeField] private bool isCool;
        
        public int width = 1025; //x-axis of the terrain
        public int height = 1025; //z-axis

        public int depth = 20; //y-axis

        public float scale = 20f;

        public float offsetX = 100f;
        public float offsetY = 100f;
        
        void Update() {
            Terrain terrain = GetComponent<Terrain>();
            
            if (isCool == true) {
                GenerateMap(terrain.terrainData);
            } else {
                terrain.terrainData = GenerateTerrain(terrain.terrainData);
            }
        }
        
        void GenerateMap(TerrainData data) {
            float[,] heights = new float[data.heightmapResolution, data.heightmapResolution];
            for (int x = 0; x < data.heightmapResolution; x++) {
                for (int y = 0; y < data.heightmapResolution; y++) {
                    heights[x, y] = _noiseTexture.GetPixel(x, y).grayscale;
                }
            }
            data.SetHeights(0, 0, heights);
        }
        
        TerrainData GenerateTerrain (TerrainData terrainData) {
            terrainData.heightmapResolution = width + 1;
            terrainData.size = new Vector3(width, depth, height);

            terrainData.SetHeights(0, 0, GenerateHeights());
            return terrainData;
        }

        float[,] GenerateHeights() {
            float[,] heights = new float[width, height];
            for(int x = 0; x < width; x++) {
                for (int y = 0; y < height; y++) {
                    heights[x, y] = CalculateHeight(x, y);
                }
            }

            return heights;
        }

        float CalculateHeight (int x, int y) {
            float xCoord = (float)x / width * scale + offsetX;
            float yCoord = (float)y / height * scale + offsetY;

            return Mathf.PerlinNoise(xCoord, yCoord);
        }
    }

