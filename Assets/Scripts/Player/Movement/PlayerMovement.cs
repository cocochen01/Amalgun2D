using UnityEngine;
using Amalgun2D.Core;
using UnityEngine.InputSystem;

namespace Amalgun2D.Player
{
	public class PlayerMovement : MonoBehaviour
	{
		private Vector2 movementInput;
		private Rigidbody2D rigidBody;
        private PlayerInput playerInput;

        [SerializeField]
        private Vector2 externalForce;
        private float externalForceDecayRate = 5f;

		[SerializeField]
		private float moveSpeed = 5f;

        private void Awake()
        {
            rigidBody = GetComponent<Rigidbody2D>();
            if (rigidBody == null)
                Debug.LogWarning("Rigidbody component not found.");
            playerInput = GetComponent<PlayerInput>();
        }

        private void Start()
        {
            InputAction playerActions = playerInput.actions["Move"];
            playerActions.performed += context => movementInput = context.ReadValue<Vector2>();
            playerActions.canceled += context => movementInput = context.ReadValue<Vector2>();
        }

        private void FixedUpdate()
        {
            HandleMovement();
        }

        private void HandleMovement()
        {
            Vector2 velocity = movementInput.normalized * moveSpeed;
            if (externalForce.sqrMagnitude > .1f)
            {
                externalForce = Vector2.Lerp(externalForce, Vector2.zero, externalForceDecayRate * Time.fixedDeltaTime);
            }
            else
            {
                externalForce = Vector2.zero;
            }
            rigidBody.linearVelocity = velocity + externalForce;
        }

        public void AddRecoilForce(Vector2 force)
        {
            externalForce += force;
        }
	}
}
