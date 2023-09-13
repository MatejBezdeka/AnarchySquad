using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object; 

[CustomEditor(typeof(Battlefield))]

public class BattlefieldEditor : Editor {
    Editor sizeEditor;
    Editor colourEditor;
    Battlefield battlefield;

    public override void OnInspectorGUI() {
        //using = že var bude na konci zahozena
        //EditorGUI.ChangeCheckScope kontroluje změny
        using (var check = new EditorGUI.ChangeCheckScope()) {
            base.OnInspectorGUI();
            if (check.changed) {
                //battlefield.GenerateBattlefield();
            }
        }
        if (GUILayout.Button("Generate Board")) {
            battlefield.GenerateBattlefield();
        }
        GenerateSettings(battlefield.size, battlefield.OnSizeSettingsUpdated, ref sizeEditor);
    }

    void GenerateSettings(Object settings, Action onSettingsUpdated, ref Editor editor) {
        if (settings != null) {
            using (var check = new EditorGUI.ChangeCheckScope()){
                CreateCachedEditor(settings, null, ref editor);
                editor.OnInspectorGUI();
                if (check.changed) {
                    onSettingsUpdated?.Invoke();
                }
            }    
        } 
    }

    void OnEnable() {
        battlefield = (Battlefield)target;
    }
}
