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
        GameObject grenade = Instantiate(grenadePrefab, muzzle.transform.position, Quaternion.LookRotation(destination));
        Rigidbody rigidBody = grenade.GetComponent<Rigidbody>();
        float distance = Vector3.Distance(muzzle.transform.position, destination);
        if (distance > stats.MaxEffectiveRange) {
            distance = stats.MaxEffectiveRange;
        }
        //grenadePrefab.transform.forward.x, grenadePrefab.transform.up.y * Physics.gravity.y, grenadePrefab.transform.forward.z
        Vector3 direction = destination - grenade.transform.position;
        
        direction.y += distance * 2;
        rigidBody.AddForce(direction  * -Physics.gravity.y * rigidBody.mass);
    }

    
    
}
