using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tilemap
{
    private Grid<int> grid;

    public Tilemap(int width, int height, float cellsize, Vector3 originPosition)
    {
        // grid = new Grid<int>(width, height, cellsize, originPosition, () => 0);
    }

    public class TilemapObject
    {
        public enum TilemapSprite
        {
            None,
            Ground,
        }

        private TilemapSprite tilemapSprite;
    }
}
