using Amalgun2D.Attacks;
using UnityEngine;

public class Gun : Weapon
{
    private FireBullet fireBulletScript;

    private void Start()
    {
        fireBulletScript = GetComponent<FireBullet>();
    }
}
