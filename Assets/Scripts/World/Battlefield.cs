using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Battlefield : MonoBehaviour {
    public Size size;
    [SerializeField] List<Tile> tiles = new();
    public bool autoUpdate = false;

    void Initialize() {
           
    }
    
    public void GenerateBattlefield() {
        Initialize();
        Generate();
    }
    
    void Generate() {
        foreach (var tile in tiles) {
            tile.Destroy();
        }
        tiles = new List<Tile>();
        for (int row = 0; row < size.width; row++) {
            for (int colum = 0; colum < size.height; colum++) {
                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.transform.parent = transform;
                
                tiles.Add(new Tile(cube, size.tileSize, size.height, row, colum));
            }
        }
    }

    public void OnSizeSettingsUpdated() {
        if (autoUpdate) {
            Initialize();
            Generate();   
        }
    }

}
