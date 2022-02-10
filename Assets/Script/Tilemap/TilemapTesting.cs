using System.Collections;
using System.Collections.Generic;
using CodeMonkey.Utils;
using UnityEngine;

public class TilemapTesting : MonoBehaviour
{
    private Tilemap tilemap;

    // Start is called before the first frame update
    void Start()
    {
        tilemap = new Tilemap(20, 10, 10f, Vector3.zero);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouseWorldPosition = UtilsClass.GetMouseWorldPosition();
            tilemap.SetTilemapSprite(mouseWorldPosition, Tilemap.TilemapObject.TilemapSprite.Ground);
        }
    }
}
