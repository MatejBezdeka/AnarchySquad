using System;
using System.Collections;
using System.Collections.Generic;
using Units;
using UnityEngine;
using UnityEngine.AI;

public class SquadUnit : Unit {
    public event Action updateUI;
    public event Action<float> reloading;
    public event Action<float> startReloading;
    UnitState currentState;
    GameObject selectionPlane;
    public bool selected { get; private set; } = false;
    [SerializeField] Material trajectoryLine;
    protected override void Start() {
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
        reloading?.Invoke(time);
    }

    public void InvokeStartReloading(float time) {
        startReloading?.Invoke(time);
    }
    public void ToggleSprint() {
        if (currentState is RunUnitState) { currentState.ForceChangeState(new NormalUnitState(this)); }
        currentState.ForceChangeState(new RunUnitState(this));
    }

    public void ReloadNow() {
        if (currentState is ReloadUnitState || weapon.CurrentAmmo == weapon.MaxAmmo) { return; }
        currentState.ForceChangeState(new ReloadUnitState(this, weapon.ReloadTime));
    }

    
}
