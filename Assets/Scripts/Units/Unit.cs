using System.Collections;
using System.Collections.Generic;
using Units;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Unit : MonoBehaviour {
    [SerializeField] public Stats stats;
    [SerializeField] public Weapon weapon;
    [SerializeField] public Weapon secondaryWeapon;
    [SerializeField, Tooltip("Material for debug sphere that indicates range of unit")] protected Material debugSphereMaterial;
    [SerializeField, Tooltip("Material for plane that will indicate selected unit")] protected Material selectMaterial;
    [SerializeField, Range(0.1f, 1), Tooltip("How often is an unit gonna update and respond")] protected float responseTime = 0.5f;
    [SerializeField] GameObject grenadePrefab;
    [SerializeField] protected float maxGrenadeDistance;
    [SerializeField] public GameObject muzzle;
    [SerializeField] NavMeshAgent agent;
    public NavMeshAgent Agent => agent;
    Unit targetedUnit;
    bool sprinting;
    
    protected virtual void Start() {
        Debug.Log("nazdar");
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

    public void ThrowGrenade(Vector3 destination) {
        GameObject grenade = Instantiate(grenadePrefab, muzzle.transform.position, Quaternion.identity);
        Rigidbody rigidBody = grenade.GetComponent<Rigidbody>();
        float launchAngle = 45;
        //float distance = Mathf.Min(Vector3.Distance(muzzle.transform.position, destination), stats.MaxEffectiveRange);
        
        // Calculate direction vector
        Vector3 direction = destination - transform.position;

        // Calculate the horizontal distance
        float horizontalDistance = Mathf.Sqrt(direction.x * direction.x + direction.z * direction.z);

        // Calculate the initial velocity required for the specified launch angle
        float initialVelocity = horizontalDistance / (Mathf.Cos(Mathf.Deg2Rad * launchAngle) * Mathf.Sqrt(2f * Mathf.Abs(Physics.gravity.y)));

        // Calculate the initial velocity components
        float Vx = initialVelocity * Mathf.Cos(Mathf.Deg2Rad * launchAngle);
        float Vy = initialVelocity * Mathf.Sin(Mathf.Deg2Rad * launchAngle);

        // Calculate the time of flight
        float flightTime = 5f * Vy / Mathf.Abs(Physics.gravity.y);

        // Calculate the initial force vector
        Vector3 force = new Vector3(Vx, Vy, direction.z / flightTime);

        // Apply force to the rigidbody
        rigidBody.AddForce(force, ForceMode.VelocityChange);
    }
}
