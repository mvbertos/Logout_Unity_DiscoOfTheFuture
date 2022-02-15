using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

[RequireComponent(typeof(MeshFilter))]
public class TilemapVisual : MonoBehaviour
{
    [System.Serializable]
    public struct TilemapSpriteUV
    {
        public Tilemap.TilemapObject.TilemapSprite tilemapSprite;
        public Vector2Int uv00Pixels;
        public Vector2Int uv11Pixels;
    }

    private struct UVCoords
    {
        public Vector2 uv00;
        public Vector2 uv11;
    }

    [SerializeField]
    private TilemapSpriteUV[] tilemapSpriteUVsArray;
    private Grid<Tilemap.TilemapObject> grid;
    private Mesh mesh;
    private bool updateMesh;
    private Dictionary<Tilemap.TilemapObject.TilemapSprite, UVCoords> uvCoordsDictionary;

    private void Awake()
    {
        mesh = new Mesh();
        this.gameObject.GetComponent<MeshFilter>().mesh = mesh;

        Texture texture = GetComponent<MeshRenderer>().material.mainTexture;
        float textureWidth = texture.width;
        float textureHeight = texture.height;

        uvCoordsDictionary = new Dictionary<Tilemap.TilemapObject.TilemapSprite, UVCoords>();
        foreach (TilemapSpriteUV tilemapSpriteUV in tilemapSpriteUVsArray)
        {
            uvCoordsDictionary[tilemapSpriteUV.tilemapSprite] = new UVCoords
            {
                uv00 = new Vector2(tilemapSpriteUV.uv00Pixels.x / textureWidth, tilemapSpriteUV.uv00Pixels.y / textureHeight),
                uv11 = new Vector2(tilemapSpriteUV.uv11Pixels.x / textureWidth, tilemapSpriteUV.uv11Pixels.y / textureHeight),

            };
        }
    }
    public void SetGrid(Grid<Tilemap.TilemapObject> grid)
    {
        this.grid = grid;
        UpdateHeatmapVisual();

        grid.OnGridValueChenged += Grid_OnGridValueChanged;
    }

    private void Grid_OnGridValueChanged(object sender, Grid<Tilemap.TilemapObject>.OnGridValueChangedEventArgs e)
    {
        Debug.Log("Grid_OnGridValueChanged");
        updateMesh = true;
    }
    private void LateUpdate()
    {
        if (updateMesh)
        {
            updateMesh = false;
            UpdateHeatmapVisual();
        }
    }

    private void UpdateHeatmapVisual()
    {
        MeshUtils.CreateEmptyMeshArrays(grid.Width * grid.Height, out Vector3[] v, out Vector2[] u, out int[] t);

        for (var x = 0; x < grid.Width; x++)
        {
            for (var y = 0; y < grid.Height; y++)
            {
                int index = x * grid.Height + y;
                Vector3 quadSize = new Vector3(1, 1) * grid.CellSize;

                Tilemap.TilemapObject gridObject = grid.GetGridObject(x, y);
                Tilemap.TilemapObject.TilemapSprite tilemapSprite = gridObject.GetTilemapSprite();
                Vector2 gridObjectUV;
                Vector2 gridUV00, gridUV11;
                if (tilemapSprite == Tilemap.TilemapObject.TilemapSprite.None)
                {

                    gridUV00 = Vector2.zero;
                    gridUV11 = Vector2.zero;
                    quadSize = Vector3.zero;
                }
                else
                {
                    UVCoords uVCoords = uvCoordsDictionary[tilemapSprite];
                    gridUV00 = uVCoords.uv00;
                    gridUV11 = uVCoords.uv11;
                }

                MeshUtils.AddToMeshArrays(v, u, t, index, grid.GetWorldPosition(x, y) + quadSize * .5f, 0f, quadSize, gridUV00, gridUV11);

            }
        }
        mesh.vertices = v;
        mesh.uv = u;
        mesh.triangles = t;
    }

}