using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour {
    [SerializeField] protected AI behavior;
    GameObject sphere;
    GameObject plane;
    [SerializeField] Material sphereMat;
    [SerializeField] Material selectMaterial;
    NavMeshAgent agent;
    // Start is called before the first frame update
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
    }

    // Update is called once per frame
    void Update()
    {
        //check for enemy
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
