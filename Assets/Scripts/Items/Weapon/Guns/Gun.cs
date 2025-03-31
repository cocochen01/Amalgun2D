using Amalgun2D.Attacks;
using Amalgun2D.Player;
using UnityEngine;

public class Gun : Weapon
{
    // References
    protected FireBullet fireBulletScript;

    [SerializeField] private GunData gunData;

    protected override void Start()
    {
        base.Start();
        weaponData = gunData;
        fireBulletScript = GetComponent<FireBullet>();
    }
    protected override void FixedUpdate()
    {
        Tick();
        base.FixedUpdate();
    }

    public override void Equip(PlayerCharacter player)
    {
        base.Equip(player);
        //player.GetComponent<PlayerEventManager>().WeaponEquip(gunData);
    }
    protected override void Attack()
    {
        base.Attack();
        // Reset the CD to be the firerate (or GCD)
        attackCDTimer = Mathf.Max(gunData.fireRate, GlobalValuesData.globalAttackCD);
        if (owningPlayer && fireBulletScript)
        {
            fireBulletScript.SpawnBullet(owningPlayer.player, transform.GetChild(0), gunData.recoilMultiplier);
        }
        else
        {
            Debug.LogError("Gun is missing fire bullet script");
        }
    }
}
