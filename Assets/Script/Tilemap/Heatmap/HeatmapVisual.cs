using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

[RequireComponent(typeof(MeshFilter))]
public class HeatmapVisual : MonoBehaviour
{
    private Grid<HeatmapGridObject> grid;
    private Mesh mesh;
    private bool updateMesh;

    private void Awake()
    {
        mesh = new Mesh();
        this.gameObject.GetComponent<MeshFilter>().mesh = mesh;
    }
    public void SetGrid(Grid<HeatmapGridObject> grid)
    {
        this.grid = grid;
        UpdateHeatmapVisual();

        grid.OnGridValueChenged += Grid_OnGridValueChanged;
    }

    private void Grid_OnGridValueChanged(object sender, Grid<HeatmapGridObject>.OnGridValueChangedEventArgs e)
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

                HeatmapGridObject gridObject = grid.GetGridObject(x, y);
                int gridValue = int.Parse(gridObject.ToString());
                float gridValueNormalized = (float)gridValue / gridObject.GetHeatMapMax();
                Vector2 gridObjectUV = new Vector2(gridValueNormalized, 0f);

                MeshUtils.AddToMeshArrays(v, u, t, index, grid.GetWorldPosition(x, y) + quadSize * .5f, 0f, quadSize, gridObjectUV, gridObjectUV);

            }
        }
        mesh.vertices = v;
        mesh.uv = u;
        mesh.triangles = t;
    }

}
public class HeatmapGridObject
{
    //Init values
    private Grid<HeatmapGridObject> grid;
    private int x;
    private int y;
    private int value = 0;

    //Heatmap data
    public const int HEAT_MAP_MAX_VALUE = 100;
    public const int HEAT_MAP_MIN_VALUE = 0;
    public HeatmapGridObject(Grid<HeatmapGridObject> grid, int x, int y)
    {
        this.grid = grid;
        this.x = x;
        this.y = y;
    }
    public void SetHeatmapValue(int value)
    {
        this.value = Mathf.Clamp(value, HEAT_MAP_MIN_VALUE, HEAT_MAP_MAX_VALUE);
        grid.TriggerGridObjectChanged(x, y);
    }
    public void AddValue(Vector3 worldPosition, int value, int range)
    {
        grid.GetXY(worldPosition, out int originX, out int originY);
        for (var x = 0; x < range; x++)
        {
            for (var y = 0; y < range - x; y++)
            {
                HeatmapGridObject gridObject = grid.GetGridObject(originX + x, originY + y);
                gridObject?.SetHeatmapValue(gridObject.GetHeatmapValue() + value);

                if (x != 0)
                {
                    gridObject = grid.GetGridObject(originX - x, originY + y);
                    gridObject?.SetHeatmapValue(gridObject.GetHeatmapValue() + value);
                }

                if (y != 0)
                {
                    gridObject = grid.GetGridObject(originX + x, originY - y);
                    gridObject?.SetHeatmapValue(gridObject.GetHeatmapValue() + value);

                    if (x != 0)
                    {
                        gridObject = grid.GetGridObject(originX - x, originY - y);
                        gridObject?.SetHeatmapValue(gridObject.GetHeatmapValue() + value);
                    }
                }


            }
        }
    }
    public int GetHeatmapValue()
    {
        return value;
    }

    public int GetHeatMapMax()
    {
        return HEAT_MAP_MAX_VALUE;
    }
    public int GetHeatMapMin()
    {
        return HEAT_MAP_MIN_VALUE;
    }

    public override string ToString()
    {
        return value.ToString();
    }
}

