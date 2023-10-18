using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CanvasManager))]
public class GameManager : MonoBehaviour {
    //Singleton
    public static GameManager instance;
    
    [Header("=== Game Settings ===")]
    [SerializeField, Range(1.1f, 4)] float maxTimeSpeed = 2;
    [SerializeField, Range(0.1f, 1f)] float minTimeSpeed = 0.2f;

    #region variables
    float time = 1;
    CanvasManager canvasManager;
    public List<SquadUnit> Squaders = new List<SquadUnit>();
    public List<EnemyUnit> Enemies = new List<EnemyUnit>();
    #endregion
    void Awake() {
        instance = this;
        PlayerControl.changedTime += TimeChanged;
    }

    void Start() {
        canvasManager = GetComponent<CanvasManager>();
        //generate
        //spawnpoints
        //spawnUnits
        //startGame
        //wakeup units
    }

    void FixedUpdate() {
        
    }

    void Update() {
        //Debug.Log("Updating");
    }
    void TimeChanged(float newTime) {
        if ((int)newTime == -2) {
            //Unpause
            TimeChanged(time);
        }else if ((int)newTime == -1) {
            //Pause
            if (Time.timeScale != 0) {
                time = Time.timeScale;
            }
            Time.timeScale = 0;
            canvasManager.ChangeTimeLabelText("Paused");
        }else {
            //change time speed
            if (newTime > maxTimeSpeed || newTime < minTimeSpeed) {
                return;
            }
            if (newTime == 0) {
                newTime = time;
            }

            Time.timeScale = newTime;
            canvasManager.ChangeTimeLabelText($"{Time.timeScale:0.00}" + " x");
        }
    }

    void PrepareGameField() {
        
    }
}
