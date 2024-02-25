using System;
using System.Collections;
using System.Collections.Generic;
using Units;
using UnityEngine;
using UnityEngine.AI;
[Serializable]
public class SquadUnit : Unit {
   
    public event Action updateUI;
    public event Action<float> reloading;
    public event Action<float> startReloading;
    public event Action<float> switching;
    public event Action<float> startSwitching;
    UnitState currentState;
    [SerializeField] GameObject selectionIndicator;
    public bool selected { get; private set; } = false;
    [SerializeField] Material trajectoryLine;
    public UnitState CurrentState => currentState;
    //[SerializeField] AudioClip
    
    [SerializeField] AudioClip switchWeaponSoundStart;
    [SerializeField] AudioClip switchWeaponSoundEnd;
    protected override void Start() {
        base.Start();
        /*GameObject debugSphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        debugSphere.transform.localScale = new Vector3(stats.MaxEffectiveRange,stats.MaxEffectiveRange, stats.MaxEffectiveRange);
        debugSphere.transform.parent = transform;
        debugSphere.transform.localPosition = new Vector3(0, 0, 0);
        debugSphere.GetComponent<MeshRenderer>().material = debugSphereMaterial;
        Destroy(debugSphere.GetComponent<SphereCollider>());*/
        
        currentState = new NormalUnitState(this);
    }

    public override void GetHit(int damage) {
        base.GetHit(damage);
    }
    
    public void Select() {
        PlayAudioClip(AudioClips.select);
        selectionIndicator.SetActive(true);
        selected = true;
    }

    public void Deselect() {
        selectionIndicator.SetActive(false);
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
        PlayAudioClip(AudioClips.reload);
    }
    public void InvokeSwitching(float time) {
        switching?.Invoke(time);
    }

    public void InvokeStartSwitching(float time) {
        startSwitching?.Invoke(time);
    }
    public void ToggleSprint() {
        if (currentState is RunUnitState) { currentState.ForceChangeState(new NormalUnitState(this)); }
        else currentState.ForceChangeState(new RunUnitState(this));
        PlayAudioClip(AudioClips.roger);
    }

    public void ReloadNow() {
        if (currentState is ReloadUnitState || CurrentAmmo == weapon.MaxAmmo) { return; }
        currentState.ForceChangeState(new ReloadUnitState(this, weapon.ReloadTime));
        PlayAudioClip(AudioClips.reload);
    }

    public void SetNewState(UnitState newState) {
        currentState.ForceChangeState(newState);
    }

    public void StartSwitchingWeaponsNow() {
        if (currentState is SwitchWeaponState) { return; }
        currentState.ForceChangeState(new SwitchWeaponState(this));
        audioSource.PlayOneShot(switchWeaponSoundStart);
    }
    public void SwapWeapons() {
        Weapon temp = weapon;
        int tempAmmo = CurrentAmmo;
        weapon = secondaryWeapon;
        CurrentAmmo = secondaryAmmo;
        secondaryWeapon = temp;
        secondaryAmmo = tempAmmo;
        audioSource.PlayOneShot(switchWeaponSoundEnd);
    }
}
