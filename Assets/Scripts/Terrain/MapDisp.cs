using UnityEngine;


    public class MapDisp : MonoBehaviour
    {
        public Renderer textureRender;
        public MeshFilter MeshFilter;
        public MeshRenderer MeshRenderer;
        
        public void DrawTexture(Texture2D texture)
        {
            textureRender.sharedMaterial.mainTexture = texture;
            textureRender.transform.localScale = new Vector3(texture.width, 1, texture.height);
        }

        public void DrawMesh(MeshData meshData, Texture2D texture)
        {
            MeshFilter.sharedMesh = meshData.CreateMesh();
            MeshRenderer.sharedMaterial.mainTexture = texture;
        }
    }

