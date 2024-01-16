using System;
using Units;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;
public class Weapon : ShopItem {
    public event Action<float> needToReload;

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

    [SerializeField, Range(0.01f, 1.15f)] float Spread;
    [SerializeField] Sprite icon;
    int currentAmmo;
    protected float currentCooldown = 0;
    Unit unit;
    Unit target;
    GameObject muzzle;
    public Sprite Icon => icon;


    public int Damage => damage;
    public int MaxAmmo => maxAmmo;
    public int CurrentAmmo => currentAmmo;
    public float EffectiveRange => effectiveRange;
    public float MaxEffectiveRange => maxEffectiveRange;
    public float ReloadTime => reloadTime;

    public void Start() {
        currentAmmo = maxAmmo;
    }

    public virtual void Update() {
        currentCooldown += Time.deltaTime;
        if (currentCooldown > timeBetweenShots) {
            Shoot();
            currentCooldown = 0;
        }
    }

    public void LockOn(Unit target, Unit unit, GameObject muzzle) {
        if (currentAmmo <= 0) {
            needToReload?.Invoke(reloadTime);
        }

        this.target = target;
        this.unit = unit;
        this.muzzle = muzzle;
    }

    public void LockOff() {
        currentCooldown = 0;
    }

    protected virtual void Shoot() {
        Vector3 newPos = target.transform.position + target.Agent.velocity;
        Vector3 offset = new Vector3(RandomOffset(), RandomOffset(), RandomOffset());
        newPos += offset;
        GameObject bullet = Instantiate(bulletPrefab, muzzle.transform.position,
            Quaternion.RotateTowards(new Quaternion(0f, 0f, 0f, 0f), new Quaternion(newPos.x, newPos.y, newPos.z, 0),
                1080));
        bullet.GetComponent<Bullet>().StartBullet(unit);
        bullet.GetComponent<Rigidbody>().AddForce(newPos - muzzle.transform.position);
        currentAmmo--;
        if (currentAmmo == 0) {
            needToReload?.Invoke(reloadTime);
        }
    }

    float RandomOffset() {
        return 0.01f * Random.Range(-unit.stats.Accuracy, unit.stats.Accuracy) + Random.Range(-Spread, Spread);
    }

    public void Reloaded() {
        currentAmmo = maxAmmo;
    }

    public override Shop.types GetType() {
        return Shop.types.weapon;
    }

    public override Sprite GetSprite() {
        return icon;
    }
    }