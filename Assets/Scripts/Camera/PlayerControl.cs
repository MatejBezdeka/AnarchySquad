using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;
[RequireComponent(typeof(Camera), typeof(PlayerInput))]
public class PlayerControl : MonoBehaviour {
    [Header("=== Control Settings ===")] 
    [SerializeField, Range(0,5), Tooltip("Rychlost rotace okolo plochy")] float rotationSpeed = 1;
    [SerializeField, Range(0,5), Tooltip("Rychlost přibližování od a k ploše")] float zoomSpeed = 1;
    [SerializeField, Range(0,5), Tooltip("Rychlost pohybování po ploše")] float moveSpeed = 1;
    [SerializeField, Range(0, 200), Tooltip("")] float minCameraDistance = 50;
    [SerializeField, Range(100, 1000), Tooltip("")] float maxCameraDistance = 150;
    [SerializeField, Range(0, 2), Tooltip("")] float zoomSmoothness = 0.25f;
    [SerializeField, Range(0, 2), Tooltip("")] float rotationSmoothness = 0.25f;
    [SerializeField, Range(0, 2), Tooltip("")] float moveSmoothness = 0.25f;

    [SerializeField, Range(1, 9), ] int timeChangeSensitivity = 2;

    [Header("=== Cursor Settings ===")] 
    [SerializeField] Texture2D normalCursor;
    [SerializeField] Texture2D goToCursor;
    [SerializeField] Texture2D attackCursor;
    [SerializeField] Texture2D interactCursor;  
    //===========//===========//===========//===========//===========//
    // Events
    public static Action<float> changedTime;
    Camera camera;
    PlayerInput playerInput;
    bool timeStopped = false;
    RaycastHit currentHit;


    List<Unit> selectedUnits = new List<Unit>();
    Unit selectedUnit;
    #region Inputs

    float currentRotation;
    float smoothRotation;
    float currentZoom;
    float smoothZoom;
    Vector2 currentMove;
    Vector2 smoothMove;

    float currentTimeChange;

    InputAction moveAction;
    InputAction zoomAction;
    InputAction rotationAction;

    InputAction leftClickAction;
    InputAction rightClickAction;

    InputAction timeChangeAction;
    InputAction timeStopStartAction;

    InputAction shiftAction;

    Vector3 selectBoxStartPoint;
    #endregion
    void Start() {
        playerInput = GetComponent<PlayerInput>();
        camera = GetComponent<Camera>();
        // Assign Inputs
        moveAction = playerInput.actions["Move"];
        zoomAction = playerInput.actions["Zoom"];
        rotationAction = playerInput.actions["Rotation"];
        leftClickAction = playerInput.actions["LeftClick"];
        leftClickAction.started += _ => LeftClick();
        shiftAction = playerInput.actions["Shift"];
        rightClickAction = playerInput.actions["RightClick"];
        rightClickAction.started += _ => RightClick();
        timeChangeAction = playerInput.actions["TimeControl"];
        timeStopStartAction = playerInput.actions["TimeStopStart"];
        timeStopStartAction.started += _ => { 
            changedTime?.Invoke(Time.timeScale == 0 ? -2f : -1f);
            timeStopped = !timeStopped;
        };
    }
  
    void Update() {
        Move();
        TimeChange();
        RayHit();
     }

    void Move() {
        //Debug.Log("nrm: " + Time.deltaTime); //cca 0.018~
        //Debug.Log("fxd: " + Time.fixedDeltaTime); // 0.02
        //
        //get input values
        float currentInputRotation = rotationAction.ReadValue<float>();
        float currentInputZoom = -zoomAction.ReadValue<float>();
        Vector2 currentInputMove = moveAction.ReadValue<Vector2>();
        //smooth the values
        currentMove = Vector2.SmoothDamp(currentMove, currentInputMove * moveSpeed, ref smoothMove, moveSmoothness, 20);
        currentRotation = Mathf.SmoothDamp(currentRotation, currentInputRotation * rotationSpeed, ref smoothRotation, rotationSmoothness, 20);
        //check boundaries for camera to not go too far or too close
        if (ZoomDistanceCheck(currentInputZoom)) {
            currentZoom = Mathf.SmoothDamp(currentZoom, currentInputZoom * zoomSpeed, ref smoothZoom, zoomSmoothness, 20);
        }
        else {
            //return camera to respective border if needed
            currentZoom = -currentZoom * 0.5f + (1 - transform.position.y/100);
        }
        
        //move camera
        camera.transform.Rotate(0, currentRotation, 0, Space.World) ;
        Vector3 move = new Vector3(currentMove.x, currentZoom, currentMove.y);
        move = move.x * transform.right + move.y * Vector3.up + move.z * new Vector3(2 * transform.forward.x,0,2 * transform.forward.z);
        transform.position += move;
    }

    void TimeChange() {
        currentTimeChange = timeChangeAction.ReadValue<float>();
        if (currentTimeChange == 0) {
            return;
        }
        changedTime?.Invoke(Time.timeScale + currentTimeChange * Time.deltaTime);
    }

    void RayHit() {
        if (!Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 1000)) {
            Cursor.SetCursor(normalCursor, Vector2.zero, CursorMode.Auto);
            return;
        }
        currentHit = hit;
        ChangeCursor(currentHit.transform.tag);
    }
    void LeftClick() {
        if (currentHit.transform == null) {
            return;
        }
        if (currentHit.transform.CompareTag("Squader")) {
            var unit = currentHit.collider.GetComponent<Unit>();
            SelectDeselectUnit(unit);
        }
    }

    void RightClick() {
        Debug.Log("b");

        if (currentHit.transform != null && selectedUnits != null && currentHit.transform.CompareTag("Floor")) {
            Debug.Log("a");
            foreach (var unit in selectedUnits) {
                unit.SetDestination(currentHit.point);
            }
            MakePointWhereUnitIsMoving(currentHit.point);
        }
    }
    
    RaycastHit? CursorRaycastHit() {
        
        return null;
    }
    //Check if camera is too far or too close
    bool ZoomDistanceCheck(float currentInputZoom) {
        if (transform.position.y <= minCameraDistance|| transform.position.y >= maxCameraDistance) {
            return false;
        }
        return true;
    }

    void SelectDeselectUnit(Unit unit) {
        bool value = true;
        //mít více označených jednotek naráz
        for (int i = 0; i < selectedUnits.Count; i++) {
            if (selectedUnits[i] == unit) {
                selectedUnits[i].Deselect();
                selectedUnits.Remove(unit);
                return;
            }
            
        }
        selectedUnits.Add(unit);
        unit.Select();
        /*if (selectedUnit == null || unit != selectedUnit) {
            if (selectedUnit != null) {
                selectedUnit.Deselect();
            }

            selectedUnit = unit;
            value = true;
        }
        else {
            selectedUnit.Deselect();
            selectedUnit = null;
            value = false;
        }
        unit.Deselect();
        return value;*/
    }
    
    void MakePointWhereUnitIsMoving(Vector3 point) {
        
    }

    void ChangeCursor(string tag) {
        switch (tag) {
            case "Floor":
                if (selectedUnits.Count > 0) {
                    Cursor.SetCursor(goToCursor, new Vector2(16,16), CursorMode.Auto);
                }
                else {
                    DefaultCursor();
                }
                break;
            case "Squader":
                    Cursor.SetCursor(interactCursor, new Vector2(8,8), CursorMode.Auto);
                break;
            case "Anarchist":
                if (selectedUnits.Count > 0) {
                    Cursor.SetCursor(attackCursor, new Vector2(16,16), CursorMode.Auto);
                }
                else {
                    Cursor.SetCursor(interactCursor, new Vector2(8,8), CursorMode.Auto);
                }
                break;
            /*case "Building":
                Cursor.SetCursor(normalCursor, new Vector2(32,32), CursorMode.Auto);
                break;*/
            case "Obstacle":
                Cursor.SetCursor(interactCursor, new Vector2(8,8), CursorMode.Auto);
                break;
            default:         
                DefaultCursor();
                return;
        }
    }

    void DefaultCursor() {
        Cursor.SetCursor(normalCursor, new Vector2(8,8), CursorMode.Auto);
    }
}