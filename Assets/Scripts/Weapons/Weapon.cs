using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;


namespace Units {
    
    public class Weapon : ScriptableObject {
        public event Action<float> needToReload;
        [SerializeField] GameObject bulletPrefab;
        //[SerializeField] GameObject muzzle;
        [SerializeField, Tooltip("Per bullet/pellet")] protected int damage;
        [SerializeField] protected int maxAmmo;
        [SerializeField, Tooltip("time to start shooting again")] protected float timeBetweenShots = 2;
        [SerializeField] protected float reloadTime = 1;
        [SerializeField, Tooltip("How far will bullets without any damage penalty")] protected float effectiveRange;
        [SerializeField, Tooltip("How far will bullets go")] protected float maxEffectiveRange;
        [SerializeField, Range(0.01f,0.15f)] float Spread;
        int currentAmmo;
        float currentCooldown = 0;
        Unit unit;
        NavMeshAgent unitAgent;
        GameObject target;
        NavMeshAgent targetAgent;
        GameObject muzzle;
        

        public int Damage => damage;
        public int MaxAmmo => maxAmmo;
        public int CurrentAmmo => currentAmmo;
        public float EffectiveRange => effectiveRange;
        public float MaxEffectiveRange => maxEffectiveRange;
        
        public void Start() {
            currentAmmo = maxAmmo;
        }
        public void Update() {
            currentCooldown += Time.deltaTime;
            if (currentCooldown > timeBetweenShots) {
                Shoot();
                currentAmmo--;
                currentCooldown = 0;
            }
        }

        public void LockOn(GameObject target, NavMeshAgent targetAgent, Unit unit, NavMeshAgent unitAgent, GameObject muzzle) {
            this.target = target;
            this.targetAgent = targetAgent;
            this.unit = unit;
            this.unitAgent = unitAgent;
            this.muzzle = muzzle;
        }
        public void LockOff() {
            //unsub
            currentCooldown = 0;
        }
        protected virtual void Shoot() {
            Vector3 newPos = target.transform.position + targetAgent.velocity;
            Vector3 offset = new Vector3(0.01f * Random.Range(0,unit.stats.Accuracy) + Random.Range(0,Spread), 0.01f * Random.Range(0,unit.stats.Accuracy) + Random.Range(0,Spread),0.01f * Random.Range(0,unit.stats.Accuracy) + Random.Range(0,Spread));
            newPos += offset;
            GameObject bullet = Instantiate(bulletPrefab, muzzle.transform.position, Quaternion.RotateTowards(new Quaternion(0f,0f,0f,0f), new Quaternion(newPos.x, newPos.y, newPos.z,0), 1080));
            bullet.GetComponent<Bullet>().StartBullet(unit);
            bullet.GetComponent<Rigidbody>().AddForce(newPos - muzzle.transform.position);
        }
    }
}