using UnityEngine;

namespace Amalgun2D.Attacks
{
    public class FireBullet : MonoBehaviour
    {
        public BulletData bulletData;
        private void Start()
        {
            if (bulletData == null)
            {
                Debug.LogError("BulletData is null.");
                return;
            }
            if (bulletData.bulletPrefab == null)
            {
                Debug.LogWarning("BulletData prefab is null");
            }
        }

        public void SpawnBullet(Transform direction)
        {
            Debug.Log("SpawnBullet.");
            if (bulletData == null || bulletData.bulletPrefab == null)
            {
                Debug.LogError("BulletData or BulletPrefab is null.");
                return;
            }
            GameObject bullet = Instantiate(bulletData.bulletPrefab, transform.position, Quaternion.identity);
            BulletBehavior bulletBehavior = bullet.AddComponent<BulletBehavior>();

            bulletBehavior.Initialize(bulletData, direction.right);

            Debug.Log("Bullet fired!");
        }
    } 
}
