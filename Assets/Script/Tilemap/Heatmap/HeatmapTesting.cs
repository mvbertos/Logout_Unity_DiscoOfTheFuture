using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class HeatmapTesting : MonoBehaviour
{

    [SerializeField] private HeatmapVisual heatmapVisual;
    private Grid<HeatmapGridObject> grid;

    // Start is called before the first frame update
    void Start()
    {
        grid = new Grid<HeatmapGridObject>(100, 100, 10f, Vector3.zero, (Grid<HeatmapGridObject> g, int x, int y) => new HeatmapGridObject(g, x, y));
        heatmapVisual.SetGrid(grid);
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
                heatmapGridObject.AddValue(mouseWorldPosition, 5, 5);
            }
        }
    }
}