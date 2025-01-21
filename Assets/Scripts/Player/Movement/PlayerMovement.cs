using UnityEngine;

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

        private void FixedUpdate()
        {
            HandleMovement();
        }
        public void SetMovementInput(Vector2 _movementInput)
		{
			movementInput = _movementInput;
		}

        private void HandleMovement()
        {
            Vector2 velocity = movementInput.normalized * moveSpeed;
            rigidBody.linearVelocity = velocity + externalForce;
            if (externalForce.sqrMagnitude > 0.01f)
            {
                externalForce = Vector2.Lerp(externalForce, Vector2.zero, externalForceDecayRate * Time.fixedDeltaTime);
            }
            else
            {
                externalForce = Vector2.zero;
            }
        }

        public void AddRecoilForce(Vector2 force)
        {
            externalForce += force;

        }
	}
}
