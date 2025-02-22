using UnityEngine;
using Amalgun2D.Core;

namespace Amalgun2D.Player
{
	public class PlayerMovement : MonoBehaviour
	{
		private Vector2 movementInput;
		private Rigidbody2D rigidBody;

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
        }

        private void Start()
        {
            InputManager input = InputManager.Instance;
            PlayerInputActions.PlayerActions playerActions = input.playerActions;
            playerActions.Move.performed += context => movementInput = context.ReadValue<Vector2>();
            playerActions.Move.canceled += context => movementInput = context.ReadValue<Vector2>();
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
