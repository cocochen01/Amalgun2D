using Amalgun2D.Attacks;
using UnityEngine;

public class Gun : Weapon
{
    private FireBullet fireBulletScript;
    private float baseDamage;
    private float fireRate;
    private float bulletSpeed;
    private float bulletRange;

    private void Start()
    {
        fireBulletScript = GetComponent<FireBullet>();
    }
}
