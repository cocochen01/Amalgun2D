using UnityEngine;
using Unity.Netcode;

namespace Amalgun2D.Player
{
	public class PlayerMovement : NetworkBehaviour
	{
		private Vector2 movementInput;
		private Rigidbody2D rigidBody;

		[SerializeField]
		private float moveSpeed = 5f;

        private void Awake()
        {
            rigidBody = GetComponent<Rigidbody2D>();
            if (rigidBody == null)
                Debug.LogWarning("Rigidbody component not found.");
        }

        private void FixedUpdate()
        {
            if (!IsOwner)
                return;
            HandleMovement();
        }
        public void SetMovementInput(Vector2 _movementInput)
		{
			movementInput = _movementInput;
		}

		private void HandleMovement()
        {
            Vector2 velocity = movementInput.normalized * moveSpeed;
            rigidBody.linearVelocity = velocity;
        }
	}
}
