using System.Collections;
using System.Collections.Generic;
using Units;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using UnityEngine.UI;
[RequireComponent(typeof(NavMeshAgent))]
public class Unit : MonoBehaviour {
    [SerializeField] public Stats stats;
    [SerializeField] public Weapon weapon;
    [SerializeField] public Weapon secondaryWeapon;
    [SerializeField, Tooltip("Material for debug sphere that indicates range of unit")] protected Material debugSphereMaterial;
    [SerializeField, Tooltip("Material for plane that will indicate selected unit")] protected Material selectMaterial;
    [SerializeField, Range(0.1f, 1), Tooltip("How often is an unit gonna update and respond")] protected float responseTime = 0.5f;
    [SerializeField] public GameObject muzzle;
    [SerializeField] NavMeshAgent agent;
    [Header("Grenade")]
    [SerializeField] GameObject grenadePrefab;
    [SerializeField, Range(1,100)] protected float maxGrenadeDistance;
    [SerializeField, Range(1,89)] float launchAngle = 35f;
    public NavMeshAgent Agent => agent;
    public float CurrentSpeed => agent.velocity.x + agent.velocity.z;
    public float LaunchAngle => launchAngle;
    public float MaxGrenadeDistance => maxGrenadeDistance;
    Unit targetedUnit;
    bool sprinting;
    
    protected virtual void Start() {
        agent = GetComponent<NavMeshAgent>();
    }
    

    List<Unit> DetectEnemiesInProximity() {
        //Detect
        return null;
    }

    protected virtual void GetHit(int damage) {
        if (stats.CalculateDamage(damage)) {
            Destroy(gameObject);
        }
    }
    

    public void SetDestination(Vector3 destination) {
        agent.SetDestination(destination);
        //možná chytřejší AI?
        //agent.SetAreaCost();
    }

    public Vector3 GetMoveVector() {
        return agent.velocity;
    }

    protected virtual void Die() {
        Destroy(this);
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

    
}
