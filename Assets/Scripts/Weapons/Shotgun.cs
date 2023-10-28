using UnityEngine;

namespace Units {
    [CreateAssetMenu(menuName = "Units/Weapon/Shotgun")]
    public class Shotgun : Weapon {
        [SerializeField, Tooltip("How many pellets will come from one shell")] protected int pelletCount;

    }
}