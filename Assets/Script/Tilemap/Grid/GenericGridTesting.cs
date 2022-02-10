using System.Collections;
using System.Collections.Generic;
using CodeMonkey.Utils;
using UnityEngine;

public class GenericGridTesting : MonoBehaviour
{
    Grid<BoolGrid> grid;
    private void Start()
    {
        //Tilemap tilemap = new Tilemap(20, 60, 10, Vector3.zero);
        grid = new Grid<BoolGrid>(10, 10, 10, Vector3.zero, (Grid<BoolGrid> g, int x, int y) => new BoolGrid(g, x, y));
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector2 position = UtilsClass.GetMouseWorldPosition();
            BoolGrid boolGrid = grid.GetGridObject(position);
            boolGrid.ChangeValue(true);
        }
    }
}

public class BoolGrid
{
    private Grid<BoolGrid> grid;
    private int x;
    private int y;
    private bool value;

    public BoolGrid(Grid<BoolGrid> grid, int x, int y)
    {
        this.grid = grid;
        this.x = x;
        this.y = y;
    }

    public void ChangeValue(bool value)
    {
        this.value = value;
        grid.TriggerGridObjectChanged(x, y);
    }

    public override string ToString()
    {
        return value.ToString();
    }
}