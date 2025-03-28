using Amalgun2D.Core;
using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{
    // References
    protected GameManager gm;

    protected virtual void Start()
    {
        gm = GameManager.Instance;
        InputManager input = InputManager.Instance;
        PlayerInputActions.PlayerActions playerActions = input.playerActions;
        playerActions.Attack.performed += PerformedAttack;
        playerActions.Attack.canceled += StopAttack;
    }
    protected virtual void PerformedAttack(InputAction.CallbackContext context)
    {
    }
    protected virtual void StopAttack(InputAction.CallbackContext context)
    {
    }
}
