using UnityEngine;
using Cinemachine;
using Amalgun2D.Player;
using Amalgun2D.Settings;
using UnityEngine.Pool;
using Amalgun2D.Pools;

namespace Amalgun2D.Attacks
{
    public class FireBullet : MonoBehaviour
    {
        public GunData gunData;
        private CinemachineImpulseSource impulseSource;

        private ObjectPool<Bullet> bulletPool;

        //public ObjectPool<Bullet> playerBulletPool;
        private void Start()
        {
            gunData = GetComponent<Gun>().gunData;
            if (gunData.bulletData == null)
            {
                Debug.LogWarning("BulletData is null.");
                return;
            }
            if (gunData.bulletData.bulletPrefab == null)
            {
                Debug.LogWarning("BulletData prefab is null");
            }
            if (BulletObjectPoolManager.Instance.playerBulletPool == null)
            {
                BulletObjectPoolManager.Instance.playerBulletPool = new ObjectPool<Bullet>(CreateBullet, OnTakeBulletFromPool, OnReturnBulletToPool, OnDestroyBullet, true, 500, 2000);
            }
            bulletPool = BulletObjectPoolManager.Instance.playerBulletPool;
            
            impulseSource = GetComponent<CinemachineImpulseSource>();
        }

        public void SpawnBullet(PlayerCharacter owningPlayer, Transform direction, GunData newGunData)
        {
            gunData = newGunData;
            if (gunData == null)
            {
                Debug.LogWarning("BulletData is null.");
                return;
            }
            PlayerMovement playerMovementScript = owningPlayer.GetComponent<PlayerMovement>();
            if (playerMovementScript == null)
            {
                Debug.LogWarning("Player does not have PlayerAttack script.");
                return;
            }
            if (gunData == null || gunData.bulletData.bulletPrefab == null)
            {
                Debug.LogError("BulletData or BulletPrefab is null.");
                return;
            }
            //Bullet bullet = Instantiate(bulletData.bulletPrefab, transform.position, Quaternion.identity);
            //bullet.GetComponent<Bullet>().Initialize(bulletData, direction.right);
            bulletPool.Get();

            // Recoil force to camera
            impulseSource.m_ImpulseDefinition.m_ImpulseDuration = gunData.bulletData.bulletForce * gunData.recoilMultiplier * SettingsManager.Instance.cameraShakeIntensity * .1f;  // Recoil duration = impulse?
            impulseSource.GenerateImpulse(transform.right * gunData.bulletData.bulletForce * gunData.recoilMultiplier * SettingsManager.Instance.cameraShakeIntensity * .1f);

            // Recoil force to player
            playerMovementScript.AddRecoilForce(-transform.right * gunData.bulletData.bulletForce * gunData.recoilMultiplier);

        }

        private Bullet CreateBullet()
        {
            Bullet bullet = Instantiate(gunData.bulletData.bulletPrefab, transform.position, Quaternion.identity);
            bullet.SetPool(bulletPool);
            bullet.transform.SetParent(BulletObjectPoolManager.Instance.transform);
            return bullet;
        }
        private void OnTakeBulletFromPool(Bullet bullet)
        {
            bullet.gameObject.SetActive(true);
            bullet.Initialize(gunData.bulletData, transform.position, transform.right, gunData.bounceNum);
        }

        private void OnReturnBulletToPool(Bullet bullet)
        {
            bullet.gameObject.SetActive(false);
        }
        private void OnDestroyBullet(Bullet bullet)
        {
            Destroy(bullet.gameObject);
        }
    } 
}
