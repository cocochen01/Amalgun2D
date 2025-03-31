using UnityEngine;
using Amalgun2D.Core;
using Cinemachine;
#nullable enable
namespace Amalgun2D.Player
{
    public class PlayerAiming : MonoBehaviour
    {
        // References
        private PlayerCharacter playerCharacter = null!;
        private Camera playerCam = null!;
        private CinemachineFramingTransposer framingTransposer = null!;
        private Transform direction = null!;

        // Weapon Data Values
        private float turnSpeed;
        private float cameraStretchFactor;

        // Default Values
        private float defaultTurnSpeed = 10f;
        private float defaultCameraStretchFactor = .3f;
        private float maxOffsetX = 2f;
        private float maxOffsetY = 2f;

        public Vector3 mousePosition;

        private void Awake()
        {
            playerCharacter = GetComponent<PlayerCharacter>();
            playerCam = playerCharacter.playerCamera;
            framingTransposer = playerCharacter.virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
            direction = transform.GetChild(1);
        }
        private void OnEnable()
        {
            //gameObject.GetComponent<PlayerEventManager>().PlayerEvents.onWeaponEquip += UpdateWeaponData;
        }
        private void OnDisable()
        {
            //WeaponEquipEvent.Handler -= UpdateWeaponData;
        }

        private void Start()
        {
            turnSpeed = defaultTurnSpeed;
            cameraStretchFactor = defaultCameraStretchFactor;
        }
        private void Update()
        {
            mousePosition = playerCam.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;

            Vector3 newDirection = mousePosition - direction.position;
            newDirection.z = 0;

            float angle = Mathf.Atan2(newDirection.y, newDirection.x) * Mathf.Rad2Deg;

            float targetAngle = Mathf.Atan2(newDirection.y, newDirection.x) * Mathf.Rad2Deg;
            float smoothAngle = Mathf.LerpAngle(direction.eulerAngles.z, targetAngle, turnSpeed * Time.deltaTime);

            direction.rotation = Quaternion.Euler(0f, 0f, smoothAngle);
        }

        private void LateUpdate()
        {
            Vector3 offset = (mousePosition - direction.position) * cameraStretchFactor;
            offset.x = Mathf.Clamp(offset.x, -maxOffsetX, maxOffsetX);
            offset.y = Mathf.Clamp(offset.y, -maxOffsetY, maxOffsetY);
            framingTransposer.m_TrackedObjectOffset = new Vector3(offset.x, offset.y, 0);
        }

        public void UpdateWeaponData(WeaponData? weaponData)
        {
            turnSpeed = weaponData?.turnSpeed ?? defaultTurnSpeed;
            cameraStretchFactor = weaponData?.visionRange ?? defaultCameraStretchFactor;
        }
    }
}
