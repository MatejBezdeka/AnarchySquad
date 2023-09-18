using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Unit : MonoBehaviour {
    [SerializeField] protected AI behavior;
    GameObject sphere;
    GameObject plane;
    [SerializeField] Material sphereMat;
    [SerializeField] Material selectMaterial;
    [SerializeField, Range(0.1f, 2), Tooltip("How often is an unit gonna update")] float responseTime; 
    NavMeshAgent agent;
    void Start() {
        sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.localScale = new Vector3(behavior.maxEffectiveRange,behavior.maxEffectiveRange, behavior.maxEffectiveRange);
        sphere.transform.parent = transform;
        sphere.transform.localPosition = new Vector3(0, 0, 0);
        sphere.GetComponent<MeshRenderer>().material = sphereMat;
        Destroy(sphere.GetComponent<SphereCollider>());

        plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
        plane.transform.localScale = new Vector3(1, 1, 1);
        plane.transform.parent = transform;
        plane.transform.localPosition = new Vector3(0, 0.05f, 0);
        plane.GetComponent<MeshRenderer>().material = selectMaterial;
        plane.SetActive(false);

        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(EnemyDetection());
    }
    IEnumerator EnemyDetection() {
        WaitForSeconds waitTime = new WaitForSeconds(responseTime);
        while (true)
        {
            Debug.Log("Looking for a enemy");
            List<Unit> enemies = DetectEnemiesInProximity();
            if (enemies != null) {
                Debug.Log(transform.name + "fire!");
            }
            else {
                Debug.Log(transform.name + " 404 enemy not found");
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
    public void Select() {
        plane.SetActive(true);
    }

    public void Deselect() {
        plane.SetActive(false);
    }

    public void SetDestination(Vector3 destination) {
        
        agent.SetDestination(destination);

        //možná chytřejší AI?
        //agent.SetAreaCost();
    }
}
