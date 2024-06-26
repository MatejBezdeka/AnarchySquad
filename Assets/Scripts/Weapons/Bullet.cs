﻿using System;
using UnityEngine;

namespace Units {
    public class Bullet : MonoBehaviour {
        [SerializeField] bool squadBullet;
        Vector3 start;
        float range;
        float maxRange;
        float maxTime = 15;
        float bulletSpeed = 10;
        float currentTime = 0;
        
        int baseDamage;
        float distance;
        public void StartBullet(Unit shotBy) {
            start = shotBy.transform.position;
            range = shotBy.weapon.EffectiveRange;
            maxRange = shotBy.weapon.MaxEffectiveRange;
            baseDamage = shotBy.weapon.Damage;
        }

        void Update() {
            currentTime += Time.deltaTime;
            distance = Vector3.Distance(start, transform.position);
            if (distance > maxRange || currentTime > maxTime) {
                Destroy(gameObject);
            }
        }
        void OnCollisionEnter(Collision other) {
//            Debug.Log(other.transform.tag + other.transform.name);
            if (other.transform.CompareTag("Bullet")) {
                return;
            }
            if (!squadBullet && other.transform.CompareTag("Squader") || squadBullet && other.transform.CompareTag("Anarchist")) {
                other.gameObject.GetComponent<Unit>().GetHit(Damage());
            }
            Destroy(gameObject);
        }

        int Damage() {
            if (distance < range) {
                return baseDamage;
            }
            return (int)(baseDamage * ((100 - ((maxRange - range) - (distance - range)) * 2) * 0.01));
        }
    }
}