using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;
[RequireComponent(typeof(Camera), typeof(PlayerInput))]
public class CameraRatate : MonoBehaviour {
    
    
    //Kopejsko prdí a smrdí!!!
    
    
    [Header("=== Camera Settings ===")] 
    [SerializeField, Range(0,5), Tooltip("Rychlost rotace okolo plochy")] float rotationSpeed = 1;
    [SerializeField, Range(0,5), Tooltip("Rychlost přibližování od a k ploše")] float zoomSpeed = 1;
    [SerializeField, Range(0,5), Tooltip("Rychlost pohybování po ploše")] float moveSpeed = 1;
    [SerializeField, Range(0, 200), Tooltip("")] float minCameraDistance = 50;
    [SerializeField, Range(100, 1000), Tooltip("")] float maxCameraDistance = 150;
    [SerializeField, Range(0, 2), Tooltip("")] float zoomSmoothness = 0.25f;
    [SerializeField, Range(0, 2), Tooltip("")] float rotationSmoothness = 0.25f;
    [SerializeField, Range(0, 2), Tooltip("")] float moveSmoothness = 0.25f;
    //===========//===========//===========//===========//===========//
    float timeSpeed = 1f;
    bool pausedTime = false;
    Camera camera;
    PlayerInput playerInput;

    [SerializeField, Tooltip("Text that will show your current time multiplayer or if you are paused")] TextMeshProUGUI timeText;

    //List<Unit> selectedUnits;
    Unit selectedUnit;
    #region Inputs

    float currentRotation;
    float smoothRotation;
    float currentZoom;
    float smoothZoom;
    Vector2 currentMove;
    Vector2 smoothMove;

    float currentTimeChange;
    bool currentTimeStopStart = true;

    InputAction moveAction;
    InputAction zoomAction;
    InputAction rotationAction;

    InputAction leftClickAction;
    InputAction rightClickAction;

    InputAction timeChangeAction;
    InputAction timeStopStartAction;
    #endregion
    void Start() {
        playerInput = GetComponent<PlayerInput>();
        camera = GetComponent<Camera>();
        // Assign Inputs
        moveAction = playerInput.actions["Move"];
        zoomAction = playerInput.actions["Zoom"];
        rotationAction= playerInput.actions["Rotation"];
        leftClickAction = playerInput.actions["LeftClick"];
        leftClickAction.started += _ => LeftClick();
        rightClickAction = playerInput.actions["RightClick"];
        rightClickAction.started += _ => RightClick();
        timeChangeAction = playerInput.actions["TimeControl"];
    }
    void Update() {
        Move();
    }

    void Move() {
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
        camera.transform.Rotate(0, currentRotation, 0, Space.World);
        Vector3 move = new Vector3(currentMove.x, currentZoom, currentMove.y);
        move = move.x * transform.right + move.y * Vector3.up + move.z * new Vector3(2 * transform.forward.x,0,2 * transform.forward.z);
        transform.position += move;
        
    }

    void LeftClick() {
        RaycastHit hit = CursorRaycastHit();
        if (hit.transform == null) {
            return;
        }
        if (hit.transform.CompareTag("Squader")) {
            var unit = hit.collider.GetComponent<Unit>();
            
            if (SelectDeselectUnit(unit)) {
                Debug.Log(selectedUnit.GetName());
            }
        }
        else {
            
        }
    }

    void RightClick() {
        RaycastHit hit = CursorRaycastHit();
        if (hit.transform != null && selectedUnit != null && hit.transform.CompareTag("Floor")) {
            selectedUnit.SetDestination(hit.point);
            MakePointWhereUnitIsMoving(hit.point);
        }
    }
    
    RaycastHit CursorRaycastHit() {
        Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 1000);
        return hit;
    }
    //Check if camera is too far or too close
    bool ZoomDistanceCheck(float currentInputZoom) {
        if (transform.position.y <= minCameraDistance|| transform.position.y >= maxCameraDistance) {
            return false;
        }
        return true;
    }

    bool SelectDeselectUnit(Unit unit) {
        // value vraci jestli byla jednotka právě odebrána nebo přidána
        // false = odebrána a naopak
        bool value = true;
        //mít více označených jednotek naráz
        /*for (int i = 0; i < selectedUnits.Count; i++) {
            if (selectedUnits[i] == unit) {
                selectedUnits.Remove(unit);
                return;
            }
        }
        selectedUnits.Add(unit);*/
        
        if (selectedUnit == null || unit != selectedUnit) {
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
        return value;
    }
    // make a animation when unit starts moving
    
    void MakePointWhereUnitIsMoving(Vector3 point) {
        
    }
}
