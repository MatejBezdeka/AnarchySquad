using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Unit : MonoBehaviour {
    [SerializeField] AI behavior;
    
    
    GameObject selectionPlane;
    [SerializeField, Tooltip("Material for debug sphere that indicates range of unit")] Material debugSphereMaterial;
    [SerializeField, Tooltip("Material for plane that will indicate selected unit")] Material selectMaterial;
    [SerializeField, Range(0.1f, 1), Tooltip("How often is an unit gonna update and respond")] float responseTime = 0.5f; 
    NavMeshAgent agent;
    void Start() {
        GameObject debugSphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        debugSphere.transform.localScale = new Vector3(behavior.maxEffectiveRange,behavior.maxEffectiveRange, behavior.maxEffectiveRange);
        debugSphere.transform.parent = transform;
        debugSphere.transform.localPosition = new Vector3(0, 0, 0);
        debugSphere.GetComponent<MeshRenderer>().material = debugSphereMaterial;
        Destroy(debugSphere.GetComponent<SphereCollider>());

        selectionPlane = GameObject.CreatePrimitive(PrimitiveType.Plane);
        selectionPlane.transform.localScale = new Vector3(1, 1, 1);
        selectionPlane.transform.parent = transform;
        selectionPlane.transform.localPosition = new Vector3(0, 0.05f, 0);
        selectionPlane.GetComponent<MeshRenderer>().material = selectMaterial;
        selectionPlane.SetActive(false);

        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(EnemyDetection());
    }
    IEnumerator EnemyDetection() {
        WaitForSeconds waitTime = new WaitForSeconds(responseTime);
        while (true)
        {
            List<Unit> enemies = DetectEnemiesInProximity();
            if (enemies != null) {
                //Debug.Log(transform.name + "fire!");
            }
            else {
                //Debug.Log(transform.name + " 404 enemy not found");
            }
            //detect enemy
            yield return waitTime;
        }
    }

    List<Unit> DetectEnemiesInProximity() {
        //Detect
        return null;
    }

    void Fire(Vector3 enemyPosition) {
            
    }

    void GetHit(int damage) {
    }
    public void Select() {
        selectionPlane.SetActive(true);
    }

    public void Deselect() {
        selectionPlane.SetActive(false);
    }

    public void SetDestination(Vector3 destination) {
        
        agent.SetDestination(destination);

        //možná chytřejší AI?
        //agent.SetAreaCost();
    }

    public string GetName() {
        return behavior.UnitName;
    }
}
