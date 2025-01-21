using Unity.VisualScripting;
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
            if (bulletData.bulletPrefab == null)
            {
                Debug.LogWarning("Bullet prefab is null.");
                Destroy(gameObject);
                return;
            }
            if (rb == null)
            {
                Debug.LogWarning("Bullet prefab missing Rigidbody2D component.");
                Destroy(gameObject);
                return;
            }

            Destroy(gameObject, bulletData.bulletLifetime);
            
            elapsedTime = 0f;
            rb.linearVelocity = direction * bulletData.speedCurve.Evaluate(0);
            transform.localScale = new Vector3(bulletData.bulletSize, bulletData.bulletSize, 1f);
        }

        private void FixedUpdate()
        {
            elapsedTime += Time.fixedDeltaTime;

            // Bullet Size
            float currentSize = bulletData.bulletSize;
            if (bulletData.enableCustomSizeCurve && bulletData.sizeCurve != null)
            {
                currentSize = bulletData.bulletSize * bulletData.sizeCurve.Evaluate(elapsedTime);
            }
            transform.localScale = new Vector3(currentSize, currentSize, 1f);

            // Bullet Speed
            float currentSpeed = bulletData.bulletSpeed;
            if (bulletData.enableCustomSpeedCurve && bulletData.speedCurve != null)
            {
                currentSpeed = bulletData.bulletSpeed * bulletData.speedCurve.Evaluate(elapsedTime);
            }

            Vector2 oscillation = Vector2.zero;
            if (bulletData.enableOscillation)
            {
                float oscillationOffset = Mathf.Sin(elapsedTime * bulletData.oscillationFrequency) * bulletData.oscillationAmplitude;
                oscillation = Vector2.Perpendicular(direction) * oscillationOffset;
            }

            rb.linearVelocity = (direction * currentSpeed) + oscillation;
        }
    } 
}
