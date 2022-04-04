using System.Collections;
using System.Collections.Generic;
using CodeMonkey.Utils;
using UnityEngine;

public class TilemapTesting : MonoBehaviour
{
    private Tilemap tilemap;
    private Tilemap.TilemapObject.TilemapSprite tilemapSprite;
    [SerializeField] private TilemapVisual tilemapVisual;
    // Start is called before the first frame update
    void Start()
    {
        tilemap = new Tilemap(20, 20, 10f, Vector3.zero);
        tilemap.SetTilemapVisual(tilemapVisual);
    }

    void Update()
    {

        if (Input.GetMouseButton(0))
        {
            Vector3 mouseWorldPosition = UtilsClass.GetMouseWorldPosition();
            tilemap.SetTilemapSprite(mouseWorldPosition, tilemapSprite);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            tilemapSprite = Tilemap.TilemapObject.TilemapSprite.None;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            tilemapSprite = Tilemap.TilemapObject.TilemapSprite.Ground;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            tilemapSprite = Tilemap.TilemapObject.TilemapSprite.Path;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            tilemapSprite = Tilemap.TilemapObject.TilemapSprite.Dirt;
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            tilemap.Save();
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            tilemap.Load();
        }
    }
}
