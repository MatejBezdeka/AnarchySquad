
using UnityEngine;
namespace Units {
    [CreateAssetMenu(menuName = "Units/Weapon/Automatic Gun")]
    public class Automatic : Weapon {
        [SerializeField, Range(1,50), Tooltip("How many bullets will gun shoot in one burst")] protected int burstSize = 1;
        //[SerializeField, Range(0.1f,5), Tooltip("How long will it take to shoot another bullet in a burst")] 
        [SerializeField, Tooltip("How long until next bullet in a burst")] float timeBetweenBullets = 0.2f;
        
        public override void UpdateWeapon(Unit unit, Unit target,ref bool attacking, ref int currentBurst, ref float currentCooldown) {
            //unit.currentCooldown += Time.deltaTime;
            if (attacking) {
                if (currentCooldown > timeBetweenBullets) {
                    Shoot(unit, target);
                    unit.DeductAmmo();
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
        public override string GetDescription() {
            return name + "\n" + "Bullet dmg: " + damage + "\nAmmo: " + maxAmmo + "\nCooldown: " + timeBetweenShots + "\nRange: " +
                   effectiveRange + " - " + maxEffectiveRange + "\nSpread: " + Spread+ "\nBurst Size: " + burstSize + "\n";
        }
    }
}