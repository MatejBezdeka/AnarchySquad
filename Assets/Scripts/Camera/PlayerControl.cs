using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Camera;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using World;
[RequireComponent(typeof(UnityEngine.Camera), typeof(PlayerInput))]
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
    [SerializeField, Range(1, 9), Tooltip("Not used currently")] int timeChangeSensitivity = 2;

    [Header("=== Cursor Settings ===")] 
    [SerializeField, Tooltip("An arrow with animation that will indicate where are your units going")] GameObject arrowPrefab;
    [SerializeField] Texture2D normalCursor;
    [SerializeField] Texture2D goToCursor;
    [SerializeField] Texture2D attackCursor;
    [SerializeField] Texture2D interactCursor;
    //[SerializeField] GameObject grenadeIndicatorRadius;
    [SerializeField] LineRenderer grenadeIndicator;

    float xBorder;
    float zBorder;
    //===========//===========//===========//===========//===========//
    // Events
    public static Action<float> changedTime;
    public static event Action<Unit> selectedNewUnit;
    public static event Action<Unit> deselectUnit;
    public event Action leftMouseButtonClicked;
    public event Action rightMouseButtonClicked;
    public event Action escButtonClicked;
    //variables
    UnityEngine.Camera camera;
    PlayerInput playerInput;
    bool timeStopped = false;
    RaycastHit currentHit;
    public List<SquadUnit> selectedUnits { get; private set; } = new List<SquadUnit>();
    public Unit selectedUnit { get; private set; }
    //states
    PlayerState currentState;
    public PlayerState nextState;
    public bool shiftIsPressed => playerInput.actions["ShiftAction"].ReadValue<bool>();

    public enum cursorTypes {
        normal, goTo, attack, interact
    }

    cursorTypes currentCursor = cursorTypes.normal;
    public bool hitSomething { get; private set; } = false;
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

    InputAction escapeAction;

    Vector3 selectBoxStartPoint;
    #endregion
    void Start() {
        currentState = new NormalState(this);
        playerInput = GetComponent<PlayerInput>();
        camera = GetComponent<UnityEngine.Camera>();
        Portrait.selectedDeselectedUnit += SelectDeselectUnit;
        Profile.grenadeAction += () => { currentState.Exit(new GrenadeState(this, grenadeIndicator));};
        GameManager.instance.MapGenerator.GetCentre();
        xBorder = GameManager.instance.MapGenerator.MapSizeX / 2f + 150;
        zBorder = GameManager.instance.MapGenerator.MapSizeY / 2f + 150;
        MapGenerator map = GameManager.instance.MapGenerator;
        switch (GameManager.instance.MapGenerator.SpawnSide) {
            case 0:
                //down
                transform.position = new Vector3(0, transform.position.y ,-map.MapSizeY/2 - 50);
                break;
            case 1:
                //up
                transform.Rotate(0,180,0, Space.World);
                transform.position = new Vector3(0, transform.position.y ,map.MapSizeY/2 + 50);
                break;
            case 2:
                //left
                transform.Rotate(0,90,0, Space.World);
                transform.position = new Vector3(-map.MapSizeX/2 - 50,transform.position.y,0);
                break;
            case 3:
                //right
                transform.Rotate(0,-90,0, Space.World);
                transform.position = new Vector3(map.MapSizeX/2 + 50,transform.position.y,0);
                break;
        }
        // Assign Inputs
        #region Assign Inputs

        moveAction = playerInput.actions["Move"];
        zoomAction = playerInput.actions["Zoom"];
        rotationAction = playerInput.actions["Rotation"];
        leftClickAction = playerInput.actions["LeftClick"];
        leftClickAction.started += _ => leftMouseButtonClicked?.Invoke();
        rightClickAction = playerInput.actions["RightClick"];
        rightClickAction.started += _ => rightMouseButtonClicked?.Invoke();
        timeChangeAction = playerInput.actions["TimeControl"];
        timeStopStartAction = playerInput.actions["TimeStopStart"];
        timeStopStartAction.started += _ => { 
            changedTime?.Invoke(Time.timeScale == 0 ? -2f : -1f);
            timeStopped = !timeStopped;
        };
        escapeAction = playerInput.actions["Esc"];
        escapeAction.started += _ => escButtonClicked?.Invoke();

        #endregion
    }
  
    void Update() {
        Move();
        TimeChange();
        currentState = Process();
    }

    void Move() {
        //get input values
        float currentInputRotation = rotationAction.ReadValue<float>();
        float currentInputZoom = -zoomAction.ReadValue<float>();
        Vector2 currentInputMove = moveAction.ReadValue<Vector2>();
        //smooth the values
        currentMove = Vector2.SmoothDamp(currentMove, currentInputMove * moveSpeed, ref smoothMove, moveSmoothness, 20, Time.fixedDeltaTime);
        
        currentRotation = Mathf.SmoothDamp(currentRotation, currentInputRotation * rotationSpeed, ref smoothRotation, rotationSmoothness, 20, Time.fixedDeltaTime);
        
        currentZoom = Mathf.SmoothDamp(currentZoom, currentInputZoom * zoomSpeed, ref smoothZoom, zoomSmoothness, 20, Time.fixedDeltaTime);
        //check boundaries for camera to not go too far or too close
        ZoomDistanceCheck(currentZoom < 0, ref currentZoom);
        //move camera
        camera.transform.Rotate(0, currentRotation, 0, Space.World) ;
        Vector3 move = new Vector3(currentMove.x, currentZoom, currentMove.y);
        move = move.x * transform.right + move.y * Vector3.up + move.z * new Vector3(2 * transform.forward.x,0,2 * transform.forward.z);
        if (transform.position.x + move.x > xBorder || transform.position.x + move.x < -xBorder) {
            move.x = 0;
        }

        if (transform.position.z + move.z > zBorder || transform.position.z + move.z < -zBorder) {
            move.z = 0;
        }
        transform.position += move;
        
        //Debug.Log(transform.forward);
    }

    void TimeChange() {
        currentTimeChange = timeChangeAction.ReadValue<float>();
        if (currentTimeChange == 0) {
            return;
        }
        changedTime?.Invoke(Time.timeScale + currentTimeChange * Time.deltaTime);
    }

    public RaycastHit RayHit() {
        if (!Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 1000)) {
            //Debug.Log("nothing");
            UpdateCursor(cursorTypes.normal);
            currentHit = new RaycastHit();
            hitSomething = false;
            return currentHit;
        }

        hitSomething = true;
        return currentHit = hit;
    }
    void ZoomDistanceCheck(bool minus, ref float currentZoom) {
        if (transform.position.y - 10 + currentZoom <= minCameraDistance && minus) {
            currentZoom *= (float)((Math.Pow(transform.position.y - minCameraDistance, 2) / minCameraDistance)/1.5);
        }
        if (transform.position.y + currentZoom >= maxCameraDistance - 20 && !minus) {
            currentZoom *= (float)((Math.Pow(maxCameraDistance - transform.position.y, 2) / maxCameraDistance)/1.5);
        }
    }
    

    public void SelectDeselectUnit(SquadUnit unit) {
        //mít více označených jednotek naráz
        for (int i = 0; i < selectedUnits.Count; i++) {
            if (selectedUnits[i] == unit) {
                unit.Deselect();
                deselectUnit?.Invoke(unit);
                selectedUnits.Remove(unit);
                UpdateProfile();
                return;
            }
            
        }
        selectedUnits.Add(unit);
        unit.Select();
        UpdateProfile();
        #region oneMax
        /*
        bool value = true;
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
        return value;*/
        #endregion
    }

    void UpdateProfile() {
        if (selectedUnits.Count == 1) {
            selectedUnit = selectedUnits[0];
            selectedNewUnit?.Invoke(selectedUnit);
        }
        else {
            selectedUnit = null;
            selectedNewUnit?.Invoke(null);
            currentState.Exit(new NormalState(this));
        }
    }

    public void DeselectAll() {
        foreach (SquadUnit unit in selectedUnits) {
             unit.Deselect();
        }
        selectedUnits.Clear();
        UpdateProfile();
    }
    
    public void MakePointWhereUnitIsMoving(Vector3 hit) {
        //static values tied to the arrow and the animation made with it!
        hit.y += 1.5f;
        Instantiate(arrowPrefab, hit, Quaternion.Euler(90,0,-90));
    }

    public void UpdateCursor(cursorTypes type) {
        if (type == currentCursor) {
            return;
        }
        switch (type) {
            case cursorTypes.normal:
                Cursor.SetCursor(normalCursor, new Vector2(4,8), CursorMode.Auto);
                break;
            case cursorTypes.attack:
                Cursor.SetCursor(attackCursor, new Vector2(16,16), CursorMode.ForceSoftware);
                break;
            case cursorTypes.interact:
                Cursor.SetCursor(interactCursor, new Vector2(8,8), CursorMode.ForceSoftware);
                break;
            case cursorTypes.goTo:
                Cursor.SetCursor(goToCursor, new Vector2(16,16), CursorMode.ForceSoftware);
                break;
            default:         
                Cursor.SetCursor(normalCursor, new Vector2(4,8), CursorMode.Auto);
                return;
        }
        currentCursor = type;
    }
    /*void EscPressed() {
        switch (currentState.currentState) {
            case PlayerState.state.normal:
                currentState.ChangeState(new PauseState(this));
                break;
            case PlayerState.state.grenade:
                currentState.ChangeState(new NormalState(this));
                break;
            case PlayerState.state.pause:
                currentState.ChangeState(new NormalState(this));
                break;
        }
        //currentStage = stateStages.entry;
    }*/

    PlayerState Process() {
        switch (currentState.currentStage) {
            case PlayerState.stateStages.entry:
                currentState.Enter();
                break;
            case PlayerState.stateStages.update:
                currentState.Update();
                break;
            case PlayerState.stateStages.exit:
            default:
                currentState.currentStage = PlayerState.stateStages.exit;
                return nextState;
        }
        return currentState;
    }
}
