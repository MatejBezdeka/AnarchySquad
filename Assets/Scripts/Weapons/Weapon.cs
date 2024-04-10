using System;
using Units;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;
[Serializable]
public class Weapon : ShopItem {

    [Header("Weapon stats")] [SerializeField]
    GameObject bulletPrefab;

    //[SerializeField] GameObject muzzle;
    [SerializeField, Tooltip("Per bullet/pellet")]
    protected int damage;

    [SerializeField] protected int maxAmmo;

    [SerializeField, Tooltip("time to start shooting again")]
    protected float timeBetweenShots = 2;

    [SerializeField] protected float reloadTime = 1;

    [SerializeField, Tooltip("How far will bullets without any damage penalty")]
    protected float effectiveRange;

    [SerializeField, Tooltip("How far will bullets go")]
    protected float maxEffectiveRange;

    [SerializeField, Range(0.01f, 1.15f)] protected float Spread;
    [SerializeField] Sprite icon;

    [Header("Weapon sounds")] 
    [SerializeField] AudioClip shootSound;

    public AudioClip ShootSound => shootSound;
    public Sprite Icon => icon;
    
    public int Damage => damage;
    public int MaxAmmo => maxAmmo;
    public float EffectiveRange => effectiveRange;
    public float MaxEffectiveRange => maxEffectiveRange;
    public float ReloadTime => reloadTime;

    public virtual void UpdateWeapon(Unit unit, Unit target,ref bool attacking, ref int currentBurst, ref float currentCooldown) {
        if (currentCooldown >= timeBetweenShots) {
            Shoot(unit, target);
            unit.DeductAmmo();
            currentCooldown = 0;
        }
    }

    protected virtual void Shoot(Unit unit, Unit target) {
        Vector3 newPos = target.transform.position + target.Agent.velocity;
        Vector3 offset = new Vector3(RandomOffset(unit.stats.Accuracy), RandomOffset(unit.stats.Accuracy), RandomOffset(unit.stats.Accuracy));
        newPos += offset;
        GameObject bullet = Instantiate(bulletPrefab,unit.muzzle.transform.position,
            Quaternion.RotateTowards(new Quaternion(0f, 0f, 0f, 0f), new Quaternion(newPos.x, newPos.y, newPos.z, 0),
                1080));
        bullet.GetComponent<Bullet>().StartBullet(unit);
        bullet.GetComponent<Rigidbody>().AddForce(newPos - unit.muzzle.transform.position);
        unit.PlayAudioClip(Unit.AudioClips.shoot);
    }

    float RandomOffset(float accuracy) {
        return 0.01f * Random.Range(-accuracy, accuracy) + Random.Range(-Spread, Spread);
    }

    

    public override Shop.types GetType() {
        return Shop.types.weapon;
    }

    public override Sprite GetSprite() {
        return icon;
    }

    public virtual string GetDescription() {
        return name + "\n" + "Bullet dmg: " + damage + "\nAmmo: " + maxAmmo + "\nCooldown: " + timeBetweenShots + "\nRange: 0 - " +
               effectiveRange + " - " + maxEffectiveRange + "\nSpread: " + Spread + "\n";
    }
}