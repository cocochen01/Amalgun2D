using Amalgun2D.Attacks;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : Weapon
{
    // References
    protected FireBullet fireBulletScript;

    // Flags
    [SerializeField] protected bool bTryAttacking = false;
    [SerializeField] protected bool bCanAttack = true;

    // Timers
    [SerializeField] protected float inputBufferTimer;
    [SerializeField] protected float attackCDTimer = 0; //set in attack

    protected float chargeTime;
    protected float winddownTime;
    protected float baseDamage;
    protected float fireRate = 1f;
    protected float bulletSpeed;
    protected float bulletRange;

    protected override void Start()
    {
        base.Start();
        fireBulletScript = GetComponent<FireBullet>();
    }
    protected virtual void FixedUpdate()
    {
        Attack();
        if (attackCDTimer > 0)
        {
            attackCDTimer -= Time.fixedDeltaTime;
        }
        if (inputBufferTimer > 0)
        {
            inputBufferTimer -= Time.fixedDeltaTime;
        }
    }
    protected override void PerformedAttack(InputAction.CallbackContext context)
    {
        base.PerformedAttack(context);
        inputBufferTimer = GlobalValuesData.globalInputBuffer;
        bTryAttacking = true;
    }
    protected override void StopAttack(InputAction.CallbackContext context)
    {
        base.StopAttack(context);
        bTryAttacking = false;
    }
    protected void Attack()
    {
        // if attack pressed
        if (bTryAttacking)
        {
            // attack when off CD
            if (attackCDTimer <= 0)
            {
                FireBullet();
            }
        }
        // if attacked no longer pressed but within the buffer window
        else if (!bTryAttacking && inputBufferTimer > 0)
        {
            // attack when off CD
            if (attackCDTimer <= 0)
            {
                FireBullet();
            }
        }

    }

    private void FireBullet()
    {
        // Reset the CD to be the firerate (or GCD)
        attackCDTimer = Mathf.Max(fireRate, GlobalValuesData.globalAttackCD);
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
