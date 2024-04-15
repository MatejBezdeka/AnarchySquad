using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class Grenade : MonoBehaviour {
    [SerializeField] ParticleSystem explodeParticle;
    [SerializeField] float range;
    [SerializeField] float lifeTime;
    [SerializeField] bool hitSquaders = false;
    [SerializeField] int damage;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip clip;
    float currentLifeTime = 0;
    bool exploded = false;
    
    void Update() {
        currentLifeTime += Time.deltaTime;
        if (currentLifeTime >= lifeTime && !exploded) {
            audioSource.PlayOneShot(clip);
            exploded = true;
            Explode();
        }
        
    }
    
    void Explode() {
        Collider[] hit = Physics.OverlapSphere(transform.position, range);
        explodeParticle.Play();
        foreach (var collision in hit) {
            if (collision.CompareTag("Anarchist") && !hitSquaders) {
                collision.GetComponent<Unit>().GetHit(damage);
            }

            if (collision.CompareTag("Squader") && hitSquaders) {
                collision.GetComponent<Unit>().GetHit(damage);
            }
        }
        Destroy(gameObject.GetComponent<MeshRenderer>());
        Invoke(nameof(DestroySelf),clip.length);
    }

    void DestroySelf() {
        Destroy(gameObject);
    }
}
