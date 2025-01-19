using UnityEngine;
using UnityEngine.InputSystem;

namespace Amalgun2D.Player
{
	public class PlayerInputManager : MonoBehaviour
    {
        private PlayerInputActions inputActions;
        private PlayerInputActions.PlayerActions playerActions;
        private PlayerInputActions.UIActions uiActions;
        private PlayerMovement playerMovementScript;
        private PlayerAttack playerAttackScript;

        public Vector2 movementInput;

        private void Awake()
        {
            inputActions = new PlayerInputActions();
            playerActions = inputActions.Player;
            uiActions = inputActions.UI;
        }

        private void Start()
        {
            playerMovementScript = GetComponent<PlayerMovement>();
            if(playerMovementScript == null ) Debug.LogWarning("Movement script not set in InputManager.");
            playerAttackScript = GetComponent<PlayerAttack>();
            if (playerAttackScript == null) Debug.LogWarning("Attack script not set in InputManager.");

            // playerMovement.[action].performed += context => do something
            playerActions.Move.performed += context => movementInput = context.ReadValue<Vector2>();
            playerActions.Move.canceled += context => movementInput = context.ReadValue<Vector2>();
            playerActions.Attack.performed += context => playerAttackScript.Attack();

            playerActions.Enable();
        }

        private void OnEnable()
        {
            EnablePlayerActionMap();
        }
        private void OnDisable()
        {
            DisableAllActionMaps();
        }

        private void Update()
        {
            playerMovementScript.SetMovementInput(movementInput);
        }

        private void DisableAllActionMaps()
        {
            playerActions.Disable();
            uiActions.Disable();
        }
        public void EnablePlayerActionMap()
        {
            DisableAllActionMaps();
            playerActions.Enable();
        }
        public void EnableUIActionMap()
        {
            DisableAllActionMaps();
            uiActions.Enable();
        }
    }

}