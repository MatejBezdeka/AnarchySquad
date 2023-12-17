using System;
using System.Collections;
using System.Collections.Generic;
using Units;
using UnityEngine;
using UnityEngine.AI;

public class SquadUnit : Unit {
    public event Action updateUI;
    public event Action<float> startReloading;
    UnitState currentState;
    GameObject selectionPlane;
    public bool selected { get; private set; } = false;
    [SerializeField] Material trajectoryLine;
    protected override void Start() {
        Debug.Log("ahoj");
        base.Start();
        
        /*GameObject debugSphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        debugSphere.transform.localScale = new Vector3(stats.MaxEffectiveRange,stats.MaxEffectiveRange, stats.MaxEffectiveRange);
        debugSphere.transform.parent = transform;
        debugSphere.transform.localPosition = new Vector3(0, 0, 0);
        debugSphere.GetComponent<MeshRenderer>().material = debugSphereMaterial;
        Destroy(debugSphere.GetComponent<SphereCollider>());*/

        selectionPlane = GameObject.CreatePrimitive(PrimitiveType.Plane);
        selectionPlane.transform.localScale = new Vector3(1, 1, 1);
        selectionPlane.transform.parent = transform;
        selectionPlane.transform.localPosition = new Vector3(0, -0.49f, 0);
        selectionPlane.GetComponent<MeshRenderer>().material = selectMaterial;
        selectionPlane.SetActive(false);
        currentState = new NormalUnitState(this);
    }

    /*public void SetAttributes(Stats newStats, Weapon mainWeapon, Weapon secondaryWeapon) {
        stats = newStats;
        weapon = mainWeapon;
        this.secondaryWeapon = secondaryWeapon;
        stats.Start();
        weapon.Start();
    }*/
    public void SetTarget(Unit target) {
        currentState.ForceChangeState(new AttackUnitState(this,target));
        //weapon.LockOn(target.gameObject, , this, agent);
        //checkVisibilty
        //checkDistance
        //shoot
    }

    protected override void GetHit(int damage) {
        base.GetHit(damage);
        
    }
    public void Select() {
        //selectionPlane.SetActive(true);
        selected = true;
    }

    public void Deselect() {
        //selectionPlane.SetActive(false);
        selected = false;
    }
    void Update() {
        currentState = currentState.Process();
        updateUI?.Invoke();
    }

    public void InvokeReloading(float time) {
        startReloading?.Invoke(time);
    }
    public void ToggleSprint() {
        if (currentState is RunUnitState) { currentState.ForceChangeState(new NormalUnitState(this)); }
        currentState.ForceChangeState(new RunUnitState(this));
    }

    public void ReloadNow() {
        if (currentState is ReloadUnitState) { return; }
        currentState.ForceChangeState(new ReloadUnitState(this, weapon.ReloadTime));
    }

    void ShowTrajectory(Vector3 target) {
        Vector3 grenadeVelocity = Vector3.RotateTowards(transform.position, target, 360, 360).normalized *
                                  Mathf.Min(Vector3.Distance(transform.position, target), maxGrenadeDistance);
        List<Vector3> points = new List<Vector3>();
        //trajectoryLine.
    }
}
