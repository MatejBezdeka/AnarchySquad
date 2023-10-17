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
    private List<Unit> Squaders = new List<Unit>();
    private List<Unit> Enemies = new List<Unit>();
    #endregion
    void Awake() {
        instance = this;
        PlayerControl.changedTime += TimeChanged;
    }

    void Start() {
        canvasManager = GetComponent<CanvasManager>();
        
    }

    void FixedUpdate() {
        
    }

    void Update() {
        //Debug.Log("Updating");
    }
    void TimeChanged(float newTime) {
        if (newTime == -2) {
            //Unpause
            TimeChanged(time);
        }else if (newTime == -1f) {
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
