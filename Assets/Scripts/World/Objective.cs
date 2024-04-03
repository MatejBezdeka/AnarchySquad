using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu] [Serializable]
public class Objective : ScriptableObject
{
    public enum ObjectiveType {
        Eliminate,// survive, capture,
    }
    /*
     * Eliminate - kill all enemies/enemy
     * Survive some time
     * 
     */
    [SerializeField] private ObjectiveType type;
    [SerializeField] private string name;
    public bool Completed() {
        switch (type) {
            case ObjectiveType.Eliminate:
                if (GameManager.instance.Enemies.Count == 0) {
                    return true;
                }
                break;
        }
        return false;
    }
}
