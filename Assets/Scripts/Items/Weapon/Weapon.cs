using Amalgun2D.Core;
using Amalgun2D.Player;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Weapon : MonoBehaviour
{
    // References
    protected GameManager gm;
    public readonly WeaponData weaponData;
    private InputAction playerActions;

    // Flags
    [SerializeField] protected bool bTryAttacking = false;
    [SerializeField] protected bool bCanAttack = true;

    // Timers
    [SerializeField] protected float inputBufferTimer;
    [SerializeField] protected float attackCDTimer = 0; //set in attack

    protected virtual void Start()
    {
        gm = GameManager.Instance;
    }
    protected virtual void Equip(PlayerCharacter player)
    {
        if (player == null)
            return;
        playerActions = player.GetComponent<PlayerInput>().actions["Attack"];
        playerActions.performed += PerformedAttack;
        playerActions.canceled += StopAttack;
    }
    protected virtual void Unequip()
    {
        playerActions.performed -= PerformedAttack;
        playerActions.canceled -= StopAttack;
    }
    protected virtual void FixedUpdate()
    {
        if (attackCDTimer > 0)
        {
            attackCDTimer -= Time.fixedDeltaTime;
        }
        if (inputBufferTimer > 0)
        {
            inputBufferTimer -= Time.fixedDeltaTime;
        }
    }
    protected virtual void PerformedAttack(InputAction.CallbackContext context)
    {
        inputBufferTimer = GlobalValuesData.globalInputBuffer;
        bTryAttacking = true;
    }
    protected virtual void StopAttack(InputAction.CallbackContext context)
    {
        bTryAttacking = false;
    }
    protected virtual void Tick()
    {
        // if attack pressed
        if (bTryAttacking)
        {
            // attack when off CD
            if (attackCDTimer <= 0)
            {
                Attack();
            }
        }
        // if attacked no longer pressed but within the buffer window
        else if (!bTryAttacking && inputBufferTimer > 0)
        {
            // attack when off CD
            if (attackCDTimer <= 0)
            {
                Attack();
            }
        }
    }
    protected virtual void Attack()
    {
        inputBufferTimer = 0;
    }
}
