using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Netcode;

namespace Amalgun2D.Player
{
	public class PlayerInputManager : NetworkBehaviour
    {
        private PlayerInputActions inputActions;
        private PlayerInputActions.PlayerActions playerActions;
        private PlayerInputActions.UIActions uiActions;
        private PlayerMovement playerMovementScript;

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
            if(playerMovementScript == null ) Debug.LogWarning("MovementScript not set in InputManager.");

            // playerMovement.[action].performed += context => do something
            playerActions.Move.performed += context => movementInput = context.ReadValue<Vector2>();
            playerActions.Move.canceled += context => movementInput = context.ReadValue<Vector2>();

            playerActions.Enable();
        }

        private void OnEnable()
        {
            if (!IsLocalPlayer) return;
            EnablePlayerActionMap();
        }
        private void OnDisable()
        {
            if (!IsLocalPlayer) return;
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