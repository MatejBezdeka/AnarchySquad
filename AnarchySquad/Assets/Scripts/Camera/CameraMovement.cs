using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;

public class CameraRatate : MonoBehaviour {
    [Header("temp")] 
    [SerializeField] GameObject squader;
    NavMeshAgent agent;
    [Header("=== Variables ===")]
    [SerializeField] GameObject gameField;
    Camera _camera;
    PlayerInput _playerInput;
    [Header("=== Camera Settings ===")] 
    [SerializeField, Range(0,5), Tooltip("Rychlost rotace okolo plochy")] float rotationSpeed = 1;
    [SerializeField, Range(0,5), Tooltip("Rychlost přibližování od a k ploše")] float zoomSpeed = 1;
    [SerializeField, Range(0, 100), Tooltip("")] float minCameraDistance = 20;
    [SerializeField, Range(100, 500), Tooltip("")] float maxCameraDistance = 150;
    [SerializeField, Range(0, 10), Tooltip("")] float zoomSmoothness = 5f;
    [SerializeField, Range(0, 10), Tooltip("")] float rotationSmoothness = 5f;
    
    
    #region Inputs

    float _currentRotation;
    float _smoothRotation;
    float _currentZoom;
    float _smoothZoom;
    Vector2 _currentMove;
    Vector2 _smoothMove;

    InputAction _moveAction;
    InputAction _zoomAction;
    InputAction _rotationAction;

    InputAction _clickAction;
    #endregion
    void Start() {
        agent = squader.GetComponent<NavMeshAgent>();
        
        _playerInput = GetComponent<PlayerInput>();
        _camera = GetComponent<Camera>();
        // Assign Inputs
        _zoomAction = _playerInput.actions["Zoom"];
        _rotationAction= _playerInput.actions["Rotation"];
        _clickAction = _playerInput.actions["Click"];
        _clickAction.started += _ => Clicked();
    }
    void Update() {
        Move();
    }

    void Move() {
        float currentInputRotation = _rotationAction.ReadValue<float>();
        float currentInputZoom = _zoomAction.ReadValue<float>();
        _currentRotation = Mathf.SmoothDamp(_currentRotation, currentInputRotation * rotationSpeed, ref _smoothRotation, rotationSmoothness, 20);
        _currentZoom = Mathf.SmoothDamp(_currentZoom, currentInputZoom, ref _smoothZoom, zoomSmoothness, 20);
        _camera.transform.Rotate(0, _currentRotation, 0, Space.World);
        //move cam
        //zoom
    }

    void Clicked() {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        //vyber co jsi hitl
        agent.SetDestination(hit.point);
    }
    
}
