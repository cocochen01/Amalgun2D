using Amalgun2D.Attacks;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : Weapon
{
    // References
    protected FireBullet fireBulletScript;

    public GunData gunData;

    protected override void Start()
    {
        base.Start();
        fireBulletScript = GetComponent<FireBullet>();
    }
    protected override void FixedUpdate()
    {
        Tick();
        base.FixedUpdate();
    }
    protected override void Attack()
    {
        base.Attack();
        // Reset the CD to be the firerate (or GCD)
        attackCDTimer = Mathf.Max(gunData.fireRate, GlobalValuesData.globalAttackCD);
        if (fireBulletScript)
        {
            fireBulletScript.SpawnBullet(gm.player, transform.GetChild(0));
        }
        else
        {
            Debug.LogError("Gun is missing fire bullet script");
        }
    }
}
