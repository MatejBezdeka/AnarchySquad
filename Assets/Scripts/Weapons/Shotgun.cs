using UnityEngine;

namespace Units {
    [CreateAssetMenu(menuName = "Units/Weapon/Shotgun")]
    public class Shotgun : Weapon {
        [SerializeField, Range(1,50),Tooltip("How many pellets will come from one shell")] protected int pelletCount;
        protected override void Shoot(Unit unit, Unit target) {
            for (int i = 0; i < pelletCount; i++) {
                base.Shoot(unit, target);
            }
        }
        public override string GetDescription() {
            return name + "\n" + "Bullet dmg: " + damage + "\nAmmo: " + maxAmmo + "\nCooldown: " + timeBetweenShots + "\nRange: 0 - " +
                   effectiveRange + " - " + maxEffectiveRange + "\nSpread: " + Spread+ "\nPellet count: " + pelletCount + "\n";
        }
    }
}