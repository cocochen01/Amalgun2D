using UnityEngine;

namespace Amalgun2D.Attacks
{
    public class BulletBehavior : MonoBehaviour
    {
        private BulletData bulletData;
        private Vector2 direction;
        private Rigidbody2D rb;
        private float elapsedTime;
        public void Initialize(BulletData _bulletData, Vector2 _direction)
        {
            bulletData = _bulletData;
            direction = _direction;
            rb = GetComponent<Rigidbody2D>();

            if (rb == null)
            {
                Debug.LogError("Bullet prefab missing Rigidbody2D component.");
                Destroy(gameObject);
            }

            Destroy(gameObject, bulletData.bulletLifetime);
            
            elapsedTime = 0f;
            if (bulletData.bulletPrefab != null)
            {
                rb.linearVelocity = direction * bulletData.speedCurve.Evaluate(0);
            }
        }

        private void FixedUpdate()
        {
            if (bulletData == null || rb == null)
            {
                return;
            }
            
            elapsedTime += Time.fixedDeltaTime;

            float currentSpeed = bulletData.bulletSpeed;
            if (bulletData.enableCustomSpeedCurve && bulletData.speedCurve != null)
            {
                currentSpeed = bulletData.bulletSpeed * bulletData.speedCurve.Evaluate(elapsedTime);
            }

            Vector2 oscillation = Vector2.zero;
            if (bulletData.enableOscillation)
            {
                float oscillationOffset = Mathf.Sin(elapsedTime * bulletData.oscillationFrequency) * bulletData.oscillationAmplitude;
                oscillation = bulletData.oscillationDirection.normalized * oscillationOffset;
            }

            rb.linearVelocity = (direction * currentSpeed) + oscillation;
        }
    } 
}
