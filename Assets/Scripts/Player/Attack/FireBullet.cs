using UnityEngine;
using Cinemachine;
using Amalgun2D.Player;
using Amalgun2D.Settings;

namespace Amalgun2D.Attacks
{
    public class FireBullet : MonoBehaviour
    {
        public BulletData bulletData;
        private CinemachineImpulseSource impulseSource;
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

        public void SpawnBullet(GameObject owningPlayer, Transform direction)
        {
            PlayerMovement playerMovementScript = owningPlayer.GetComponent<PlayerMovement>();
            if (playerMovementScript == null)
            {
                Debug.LogWarning("Player does not have PlayerAttack script.");
                return;
            }
            if (bulletData == null || bulletData.bulletPrefab == null)
            {
                Debug.LogError("BulletData or BulletPrefab is null.");
                return;
            }
            GameObject bullet = Instantiate(bulletData.bulletPrefab, transform.position, Quaternion.identity);
            BulletBehavior bulletBehavior = bullet.AddComponent<BulletBehavior>();

            bulletBehavior.Initialize(bulletData, direction.right);

            // Recoil force to camera
            impulseSource.m_ImpulseDefinition.m_ImpulseDuration = bulletData.bulletForce;  // Always make recoil duration same as force?
            impulseSource.GenerateImpulse(direction.right * bulletData.bulletForce * .1f * SettingsManager.Instance.cameraShakeIntensity);

            // Recoil force to player
            playerMovementScript.AddRecoilForce(direction.right * -bulletData.bulletForce);

        }
    } 
}
