using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {
    GameObject cube;
    Vector3 position;
    float size;
    float height;
    int row;
    int colum;
    public Tile(GameObject cube,float size, float height, int row, int colum) {
        this.cube = cube;
        this.size = size;
        this.height = height;
        this.row = row;
        this.colum = colum;
        GenerateTile();
    }

    void GenerateTile(/*int newRow, int newColum*/) {
        cube.transform.localScale = new Vector3(size, height, size);
        cube.transform.localPosition = new Vector3(row * size + size/2, -height/2, colum * size + size/2);
        cube.GetComponent<MeshRenderer>().sharedMaterial = new Material(Shader.Find("Standard"));
    }

    public void Destroy() {
        DestroyImmediate(cube);
    }
}
