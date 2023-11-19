using System.Collections;
using UnityEditor.Timeline.Actions;
using UnityEngine;
namespace Units {
    [CreateAssetMenu(menuName = "Units/Weapon/Automatic Gun")]
    public class Automatic : Weapon {
        [SerializeField, Range(1,50), Tooltip("How many bullets will gun shoot in one burst")] protected int burstSize = 1;
        //[SerializeField, Range(0.1f,5), Tooltip("How long will it take to shoot another bullet in a burst")] 
        float timeBetweenBullets = 0.2f;
        int currentBurst = 0;
        bool attacking = false;
        
        public override void Update() {
            currentCooldown += Time.deltaTime;
            if (attacking) {
                if (currentCooldown > timeBetweenBullets) {
                    Shoot();
                    currentBurst++;
                    currentCooldown = 0;
                    if (currentBurst >= burstSize) {
                        attacking = false;
                        currentBurst = 0;
                    }
                }
            }
            else {
                if (currentCooldown > timeBetweenShots) {
                    attacking = true;
                    currentCooldown = 0;
                }
            }
            
        }
    }
}