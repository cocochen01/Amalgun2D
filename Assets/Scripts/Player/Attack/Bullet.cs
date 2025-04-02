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
    public void Initialize(BulletData _bulletData, Vector2 _position, Vector2 _direction, int bounce)
    {
        Debug.Log("Initalize");
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

        //Destroy(gameObject, bulletData.bulletLifetime);

        elapsedTime = 0f;

        rb.linearVelocity = direction * bulletData.speedCurve.Evaluate(0);
        transform.localScale = new Vector3(bulletData.bulletSize, bulletData.bulletSize, 1f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<IBulletInteraction>() != null)
        {
            bulletPool.Release(this);
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

