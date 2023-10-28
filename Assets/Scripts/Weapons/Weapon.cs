using System;
using TMPro;
using UnityEditor.UIElements;
using UnityEngine;

namespace Units {
    
    public class Weapon : ScriptableObject {
        public event Action<float> needToReload;
        [SerializeField, Tooltip("Per bullet/pellet")] protected int Damage;
        [SerializeField] protected int maxAmmo;
        [SerializeField, Tooltip("time to start shooting again")] protected float timeBetweenShots = 2;
        [SerializeField] protected float reloadTime = 1;
        [SerializeField, Tooltip("How far will bullets without any damage penalty")] protected float effectiveRange;
        [SerializeField, Tooltip("How far will bullets go")] protected float maxEffectiveRange;
        private int currentAmmo;
        private float currentCooldown = 0;

        public int MaxAmmo => maxAmmo;
        public int CurrentAmmo => currentAmmo;

        public void Start() {
            currentAmmo = maxAmmo;
        }
        public void Update() {
            currentCooldown += Time.deltaTime;
            if (currentCooldown > timeBetweenShots) {
                Shoot();
                currentCooldown = 0;
            }
        }

        protected void LockOn(GameObject unitGameObject, Rigidbody unitRigidBody, Unit unit, float accuracy) {
            //subscribe to update
            
        }

        protected void LockOff() {
            //unsub
            currentCooldown = 0;
        }
        protected virtual void Shoot() {
            
        }
        
    }
}