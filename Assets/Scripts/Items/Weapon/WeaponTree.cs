using Amalgun2D.Player;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponTree : MonoBehaviour
{
    protected WeaponNode rootWeapon;
    protected List<WeaponNode> childWeapons;

    public PlayerCharacter owningPlayer = null;
    private InputAction playerActions;
    // Flags
    [SerializeField] protected bool bTryAttacking = false;
    [SerializeField] protected bool bCanAttack = true;

    // Timers
    [SerializeField] protected float inputBufferTimer;

    public virtual void AssignPlayer(PlayerCharacter player)
    {
        Debug.Log("Equip function" + gameObject.name);
        if (player == null)
            return;
        //Debug.Log("Player is not null");
        owningPlayer = player;
        playerActions = player.GetComponent<PlayerInput>().actions["Attack"];
        playerActions.performed += PerformedAttack;
        playerActions.canceled += StopAttack;
        //player.GetComponent<PlayerEventManager>().WeaponEquip(weaponData);
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
}
