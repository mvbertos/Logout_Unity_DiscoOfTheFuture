using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class HeatmapTesting : MonoBehaviour
{
    private Grid<HeatmapGridObject> grid;

    // Start is called before the first frame update
    void Start()
    {
        grid = new Grid<HeatmapGridObject>(20, 20, 10f, Vector3.zero, (Grid<HeatmapGridObject> g, int x, int y) => new HeatmapGridObject(g, x, y));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouseWorldPosition = UtilsClass.GetMouseWorldPosition();
            HeatmapGridObject heatmapGridObject = grid.GetGridObject(mouseWorldPosition);
            if (heatmapGridObject != null)
            {
                heatmapGridObject.SetHeatmapValue(heatmapGridObject.GetHeatmapValue() + 5);
            }
        }
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
    public int GetHeatmapValue()
    {
        return value;
    }

    public override string ToString()
    {
        return value.ToString();
    }
}
