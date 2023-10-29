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
    GameObject selectionPlane;
    [SerializeField, Tooltip("Material for debug sphere that indicates range of unit")] Material debugSphereMaterial;
    [SerializeField, Tooltip("Material for plane that will indicate selected unit")] Material selectMaterial;
    [SerializeField, Range(0.1f, 1), Tooltip("How often is an unit gonna update and respond")] protected float responseTime = 0.5f;
    [SerializeField] GameObject grenadePrefab;
    [SerializeField] public GameObject muzzle;
    protected NavMeshAgent agent;
    Unit targetedUnit;
    bool sprinting;
    
    public bool selected { get; private set; } = false;
    protected virtual void Start() {
        GameObject debugSphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        debugSphere.transform.localScale = new Vector3(stats.MaxEffectiveRange,stats.MaxEffectiveRange, stats.MaxEffectiveRange);
        debugSphere.transform.parent = transform;
        debugSphere.transform.localPosition = new Vector3(0, 0, 0);
        debugSphere.GetComponent<MeshRenderer>().material = debugSphereMaterial;
        Destroy(debugSphere.GetComponent<SphereCollider>());

        selectionPlane = GameObject.CreatePrimitive(PrimitiveType.Plane);
        selectionPlane.transform.localScale = new Vector3(1, 1, 1);
        selectionPlane.transform.parent = transform;
        selectionPlane.transform.localPosition = new Vector3(0, -0.49f, 0);
        selectionPlane.GetComponent<MeshRenderer>().material = selectMaterial;
        selectionPlane.SetActive(false);
        agent = GetComponent<NavMeshAgent>();
        stats.Start();
        weapon.Start();
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
    public void Select() {
        selectionPlane.SetActive(true);
        selected = true;
    }

    public void Deselect() {
        selectionPlane.SetActive(false);
        selected = false;
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
