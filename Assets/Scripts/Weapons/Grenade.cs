using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour {
    [SerializeField] ParticleSystem explodeParticle;
    [SerializeField] float range;
    [SerializeField] float lifeTime;
    [SerializeField] bool hitSquaders = false;
    [SerializeField] int damage;
    float currentLifeTime = 0;
    void Update() {
        currentLifeTime += Time.deltaTime;
        if (currentLifeTime >= lifeTime) {
            Explode();
        }
    }

    void Explode() {
        Collider[] hit = Physics.OverlapSphere(transform.position, range);
        Debug.Log(hit.Length);
        foreach (var collision in hit) {
            if (collision.tag == "Anarchist" && !hitSquaders) {
                collision.GetComponent<Unit>().GetHit(damage);
            }

            if (collision.tag == "Squader" && hitSquaders) {
                collision.GetComponent<Unit>().GetHit(damage);
            }
            Debug.Log(collision.name);
        }
        Debug.Log("ded");
        Destroy(gameObject);
    }
}
