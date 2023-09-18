using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class CameraRatate : MonoBehaviour {
    
    //Kopejsko prdí a smrdí!!!
    
    [Header("temp")] 
    [SerializeField] GameObject squader;
    NavMeshAgent agent;
    [Header("=== Variables ===")]
    [SerializeField] GameObject gameField;
    Camera camera;
    PlayerInput playerInput;
    [Header("=== Camera Settings ===")] 
    [SerializeField, Range(0,5), Tooltip("Rychlost rotace okolo plochy")] float rotationSpeed = 1;
    [SerializeField, Range(0,5), Tooltip("Rychlost přibližování od a k ploše")] float zoomSpeed = 1;
    [SerializeField, Range(0,5), Tooltip("Rychlost přibližování od a k ploše")] float moveSpeed = 1;
    [SerializeField, Range(0, 100), Tooltip("")] float minCameraDistance = 20;
    [SerializeField, Range(100, 500), Tooltip("")] float maxCameraDistance = 150;
    [SerializeField, Range(0, 10), Tooltip("")] float zoomSmoothness = 5f;
    [SerializeField, Range(0, 10), Tooltip("")] float rotationSmoothness = 5f;
    [SerializeField, Range(0, 10), Tooltip("")] float moveSmoothness = 5f;

    //List<Unit> selectedUnits;
    Unit selectedUnit;
    #region Inputs

    float currentRotation;
    float smoothRotation;
    float currentZoom;
    float smoothZoom;
    Vector2 currentMove;
    Vector2 smoothMove;

    InputAction moveAction;
    InputAction zoomAction;
    InputAction rotationAction;

    InputAction leftClickAction;
    InputAction rightClickAction;
    #endregion
    void Start() {
        agent = squader.GetComponent<NavMeshAgent>();
        
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
        
        if (ZoomDistanceCheck(currentInputZoom)) {
            currentZoom = Mathf.SmoothDamp(currentZoom, currentInputZoom * zoomSpeed, ref smoothZoom, zoomSmoothness, 20);
        }
        else {
            currentZoom = 0;
        }
        
        //move camera
        camera.transform.Rotate(0, currentRotation, 0, Space.World);
        Vector3 move = new Vector3(currentMove.x, currentZoom, currentMove.y);
        move = move.x * transform.right + move.y * Vector3.up + move.z * new Vector3(2 * transform.forward.x,0,2 * transform.forward.z);
        transform.position += move;
        
    }

    void LeftClick() {
        RaycastHit hit = CursorRaycastHit();
        if (hit.transform.CompareTag("Squader")) {
            var unit = hit.collider.GetComponent<Unit>();
            SelectDeselectUnit(unit);
        }
        
        //vyber co jsi hitl

    }

    void RightClick() {
        RaycastHit hit = CursorRaycastHit();
        if (selectedUnit != null && hit.transform.CompareTag("Floor")) {
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
        if (transform.position.y <= minCameraDistance && currentInputZoom < 0 || transform.position.y >= maxCameraDistance && currentInputZoom > 0) {
            return false;
        }
        return true;
    }

    void SelectDeselectUnit(Unit unit) {
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
            unit.Select();
            selectedUnit = unit;
        }
        else {
            selectedUnit.Deselect();
            unit.Deselect();
            selectedUnit = null;
        }
    }
    // make a animation when unit starts moving
    
    void MakePointWhereUnitIsMoving(Vector3 point) {
        
    }
}
