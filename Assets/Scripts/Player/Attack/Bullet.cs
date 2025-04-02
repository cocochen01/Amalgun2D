using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    // References
    private BulletData bulletData;
    private Rigidbody2D rb;

    private Vector2 direction;
    private float elapsedTime;
    public int bounceCount;

    private ObjectPool<Bullet> bulletPool;

    private void Awake()
    {
        GetComponent<CircleCollider2D>().radius = bulletData.bulletSize / 2f;
    }
    public void Initialize(BulletData _bulletData, Vector2 _position, Vector2 _direction, int bounce)
    {
        transform.position = _position;
        direction = _direction;
        bounceCount = bounce;
        if(bulletData != _bulletData)
        {
            bulletData = _bulletData;
        }
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

        elapsedTime = 0f;
        rb.linearVelocity = bulletData.enableCustomSpeedCurve? direction * bulletData.speedCurve.Evaluate(0) : direction;
        transform.localScale = new Vector3(bulletData.bulletSize, bulletData.bulletSize, 1f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.GetComponent<IBulletInteraction>() != null)
        {
            if (bounceCount > 0)
            {
                direction = Vector2.Reflect(direction, collision.GetContact(0).normal);
                bounceCount--;
            }
            else
            {
                bulletPool.Release(this);
            }
        }
    }

    private void FixedUpdate()
    {
        if(bulletData == null)
        {
            return;
        }
        if (elapsedTime >= bulletData.bulletLifetime)
        {
            bulletPool.Release(this);
        }
        else
        {
            elapsedTime += Time.fixedDeltaTime;
        }

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

    public void SetPool(ObjectPool<Bullet> pool)
    {
        bulletPool = pool;
    }
} 

