using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms;
using Quaternion = UnityEngine.Quaternion;
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

    InputAction clickAction;
    #endregion
    void Start() {
        agent = squader.GetComponent<NavMeshAgent>();
        
        playerInput = GetComponent<PlayerInput>();
        camera = GetComponent<Camera>();
        // Assign Inputs
        moveAction = playerInput.actions["Move"];
        zoomAction = playerInput.actions["Zoom"];
        rotationAction= playerInput.actions["Rotation"];
        clickAction = playerInput.actions["Click"];
        clickAction.started += _ => Clicked();
        
    }
    void Update() {
        Move();
    }

    void Move() {
        float currentInputRotation = rotationAction.ReadValue<float>();
        float currentInputZoom = zoomAction.ReadValue<float>();
        Vector2 currentInputMove = moveAction.ReadValue<Vector2>();
        currentMove = Vector2.SmoothDamp(currentMove, currentInputMove * moveSpeed, ref smoothMove, moveSmoothness, 20);
        currentRotation = Mathf.SmoothDamp(currentRotation, currentInputRotation * rotationSpeed, ref smoothRotation, rotationSmoothness, 20);
        currentZoom = Mathf.SmoothDamp(currentZoom, currentInputZoom, ref smoothZoom, zoomSmoothness, 20);
        camera.transform.Rotate(0, currentRotation, 0, Space.World);
        transform.localPosition = new Vector3(transform.position.x + currentMove.x, transform.position.y + currentZoom , transform.position.z + currentMove.y);
        //move cam
        //zoom
    }

    void Clicked() {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        //vyber co jsi hitl
        agent.SetDestination(hit.point);
    }
    
}
