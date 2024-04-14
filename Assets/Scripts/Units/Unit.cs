using System;
using UnityEngine;
using UnityEngine.AI;
using Random = System.Random;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(AudioSource))]
public abstract class Unit : MonoBehaviour {
    static Random rn = new Random();
    //TODO simplify profile communication
    public event Action updateUI;
    public event Action<float> reloading;
    public event Action<float> startReloading;
    public event Action<float> needToReload;
    public enum AudioClips { select, roger, reload, shoot, }
    
    [HideInInspector] public string UnitName = "NaN";
    
    public int CurrentHp { get; private set; }
    public float CurrentStamina { get; private set; }
    public int CurrentAmmo { get; protected set; }
    public bool selected { get; protected set; } = false;

    [Header("=== Unit Settings ===")]
    /*[HideInInspector]*/ public Stats stats;
    /*[HideInInspector]*/ public Weapon weapon;
    [SerializeField, Range(0.1f, 1), Tooltip("How often is an unit gonna update and respond")] protected float responseTime = 0.5f;
    [SerializeField] public GameObject muzzle; //TODO gun should have a its own model and muzzle
    //possibility to see range of the unit's gun
    //[SerializeField, Tooltip("Material for debug sphere that indicates range of unit")] protected Material debugSphereMaterial;
    
    [Header("=== Grenade ===")]
    [SerializeField] GameObject grenadePrefab;
    [SerializeField, Range(1,100)] protected float maxGrenadeDistance;
    [SerializeField, Range(1,89)] float launchAngle = 35f;
    
    public float LaunchAngle => launchAngle;
    public float MaxGrenadeDistance => maxGrenadeDistance;
    
    [Header("== Unit Audio === ")]
    [SerializeField] protected AudioSource audioSource;
    [SerializeField] protected AudioSource audioRadioSource;
    [SerializeField] AudioClip[] hurtSound;
    [SerializeField] AudioClip[] dieSound;
    [SerializeField] AudioClip[] rogerSounds;
    [SerializeField] AudioClip[] selectSounds;
    [SerializeField] AudioClip[] reloadSounds;
    
    protected NavMeshAgent agent;
    public NavMeshAgent Agent => agent;
    
    protected UnitState currentState;
    public UnitState CurrentState => currentState;
    
    
    
    protected virtual void Start() {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = stats.Speed;
        agent.angularSpeed *= 2;
        CurrentHp = stats.MaxHp;
        CurrentStamina = stats.MaxStamina;
        CurrentAmmo = weapon.MaxAmmo;
    }
    void Update() {
        currentState = currentState.Process();
        updateUI?.Invoke();
    }

    #region stamina
    public void Sprint() {
        CurrentStamina -= 2f;
        if (CurrentStamina < 0) {
            CurrentStamina = 0;
        }
    }
    public void AddStamina() {
        CurrentStamina += 1.5f;
        if (CurrentStamina >= stats.MaxStamina) {
            CurrentStamina = stats.MaxStamina;
        }
    }
    #endregion
    #region health

    public virtual void GetHit(int damage) {
        Debug.Log("AAAHHH");
        damage = (int)(damage * (1f - (stats.Armor * 0.25f) * 0.04f));
        if ((CurrentHp -= damage) <= 0) {
            Destroy(gameObject);
            
        }
        else {
            // 20% play hurt sound
        }
    }
    
    protected virtual void Die() {
        //TODO play die sound
        Destroy(this);
    }
    
    #endregion
    #region ammo

    public void Reloaded() {
        CurrentAmmo = weapon.MaxAmmo;
    }

    public void DeductAmmo() {
        CurrentAmmo--;
        if (CurrentAmmo == 0) {
            needToReload?.Invoke(weapon.ReloadTime);
        }
    }
    
    public void InvokeReloading(float time) {
        reloading?.Invoke(time);
    }

    public void InvokeStartReloading(float time) {
        startReloading?.Invoke(time);
        PlayAudioClip(AudioClips.reload);
    }
    
    #endregion

    public abstract bool isSquadUnit();

    public void SetDestination(Vector3 destination) {
        agent.SetDestination(destination);
    }

    public void ThrowGrenade(Vector3 target) {
        GameObject grenade = Instantiate(grenadePrefab, muzzle.transform.position, Quaternion.identity);
        Rigidbody rigidBody = grenade.GetComponent<Rigidbody>();
        //Asssited code with ChatGpt ==== ==== ==== ====
        Vector3 direction = target - transform.position;
        float horizontalDistance = Mathf.Min(Mathf.Sqrt(direction.x * direction.x + direction.z * direction.z), maxGrenadeDistance);
        direction.Normalize();
        direction *= horizontalDistance;
        float initialVelocityY = Mathf.Sqrt( Physics.gravity.magnitude * (direction.y + Mathf.Tan(Mathf.Deg2Rad * launchAngle) * horizontalDistance));
        float initialVelocityXZ = initialVelocityY / Mathf.Sin(Mathf.Deg2Rad * launchAngle);
        initialVelocityXZ /= 2;
        Vector3 force = new Vector3(direction.x / horizontalDistance * initialVelocityXZ, initialVelocityY, direction.z / horizontalDistance * initialVelocityXZ);
        // ==== ==== ==== ==== ==== ==== ==== ==== ==== ==== 
        rigidBody.AddForce(force, ForceMode.VelocityChange);
    }

    public void PlayAudioClip(AudioClips clip) {
        switch (clip) {
            case AudioClips.roger:
                if (!audioRadioSource.isPlaying) {
                    audioRadioSource.PlayOneShot(rogerSounds[rn.Next(rogerSounds.Length)]);
                }
                break;
            case AudioClips.reload:
                if (!audioRadioSource.isPlaying) {
                    audioRadioSource.PlayOneShot(reloadSounds[rn.Next(reloadSounds.Length)]);
                }
                break;
            case AudioClips.select:
                if (!audioRadioSource.isPlaying) { 
                    audioRadioSource.PlayOneShot(selectSounds[rn.Next(selectSounds.Length)]);
                }
                break;
            case AudioClips.shoot:
                audioSource.PlayOneShot(weapon.ShootSound);
                break;
        }
        
    }
    
}
