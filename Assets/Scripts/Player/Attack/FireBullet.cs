using UnityEngine;
using Cinemachine;

namespace Amalgun2D.Attacks
{
    public class FireBullet : MonoBehaviour
    {
        public BulletData bulletData;
        private CinemachineImpulseSource impulseSource;

        private float recoilForce = .1f;
        private float recoilDuration = .4f;
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
            impulseSource = GetComponent<CinemachineImpulseSource>();
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

            impulseSource.m_ImpulseDefinition.m_ImpulseDuration = recoilDuration;
            impulseSource.GenerateImpulse(direction.right * recoilForce);

            Debug.Log("Bullet fired!");
        }
    } 
}
