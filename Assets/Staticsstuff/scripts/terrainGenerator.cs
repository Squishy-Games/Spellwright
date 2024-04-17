using UnityEngine;

namespace Staticsstuff.scripts
{
    public class terrainGenerator : MonoBehaviour
    {
        [SerializeField] private Texture2D _noiseTexture;
    
        // Start is called before the first frame update
        void Start()
        {
            Terrain terrain = GetComponent<Terrain>();
            GenerateHeights(terrain.terrainData);
        }

        // Update is called once per frame
        void Update()
        {
        
        }
        void GenerateHeights(TerrainData data) {
            float[,] heights = new float[data.heightmapResolution, data.heightmapResolution];
            for (int x = 0; x < data.heightmapResolution; x++) {
                for (int y = 0; y < data.heightmapResolution; y++)
                {
                    heights[x, y] = _noiseTexture.GetPixel(x, y).grayscale;
                }
            }
            data.SetHeights(0, 0, heights);
        }
    }
}
