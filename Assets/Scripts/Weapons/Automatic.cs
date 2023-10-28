using UnityEngine;

namespace Units {
    [CreateAssetMenu(menuName = "Units/Weapon/Automatic Gun")]
    public class Automatic : Weapon {
        [SerializeField, Tooltip("How many bullets will gun shoot in one burst")] protected int burstSize = 1;
        [SerializeField, Tooltip("How long will it take to shoot another bullet in a burst")] protected float timeBetweenBullets = 0;

    }
}