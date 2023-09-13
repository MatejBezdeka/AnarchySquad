using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "battlefield/size")]

public class Size : ScriptableObject {
    [Range(1,100)] public int width = 10;   
    [Range(1,100)] public int height = 10;
    [Range(1, 50)] public float tileSize = 1;
    [Range(0.2f, 25)] public float thicknes = 2;
}
