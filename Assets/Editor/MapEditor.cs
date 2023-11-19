using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.UI;

[CustomEditor(typeof(MapGenerator))]
public class MapEditor : Editor{
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        MapGenerator map = target as MapGenerator;
        if (GUILayout.Button("generateMap")) {
            map.GenerateMap();

        }
    }
}
