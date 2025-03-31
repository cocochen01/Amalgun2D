using UnityEngine;
using Amalgun2D.Core;
using Cinemachine;
using UnityEngine.InputSystem;
#nullable enable
namespace Amalgun2D.Player
{
    public class PlayerAiming : MonoBehaviour
    {
        // References
        private PlayerEventManager playerEvents = null!;
        private Camera playerCam = null!;
        private CinemachineVirtualCamera virtualCam = null!;
        private CinemachineFramingTransposer framingTransposer = null!;
        private Transform direction = null!;

        // Weapon Data Values
        [SerializeField] private float turnSpeed;
        [SerializeField] private float maxOffset;
        [SerializeField] private float orthoLensSize;

        // Default Values
        private float defaultTurnSpeed = 10f;
        private float defaultMaxOffset = 2f;
        private float defaultOrthoLensSize = 8f;
        private float orthoLensLerpSpeed = 6f;
        private float cameraStretchFactor = .3f;

        public Vector3 mousePosition;

        private void Awake()
        {
            playerEvents = GetComponent<PlayerEventManager>();
            playerCam = GetComponent<PlayerCharacter>().playerCamera;
            virtualCam = GetComponent<PlayerCharacter>().virtualCamera;
            framingTransposer = virtualCam.GetCinemachineComponent<CinemachineFramingTransposer>();
            direction = transform.GetChild(1);
        }
        private void OnEnable()
        {
            playerEvents.onWeaponEquip += UpdateWeaponData;

        }
        private void OnDisable()
        {
            playerEvents.onWeaponEquip -= UpdateWeaponData;
        }

        private void Start()
        {
            turnSpeed = defaultTurnSpeed;
            maxOffset = defaultMaxOffset;
            orthoLensSize = defaultOrthoLensSize;
        }
        private void FixedUpdate()
        {
            mousePosition = playerCam.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;

            Vector3 newDirection = mousePosition - direction.position;
            newDirection.z = 0;

            float angle = Mathf.Atan2(newDirection.y, newDirection.x) * Mathf.Rad2Deg;
            float targetAngle = Mathf.Atan2(newDirection.y, newDirection.x) * Mathf.Rad2Deg;
            float smoothAngle = Mathf.LerpAngle(direction.eulerAngles.z, targetAngle, turnSpeed * Time.fixedDeltaTime);

            direction.rotation = Quaternion.Euler(0f, 0f, smoothAngle);

            virtualCam.m_Lens.OrthographicSize = Mathf.Lerp(virtualCam.m_Lens.OrthographicSize, orthoLensSize, orthoLensLerpSpeed * Time.fixedDeltaTime);
        }

        private void LateUpdate()
        {
            Vector3 offset = (mousePosition - direction.position) * cameraStretchFactor;
            if (offset.magnitude > maxOffset)
            {
                offset = offset.normalized * maxOffset;
            }
            framingTransposer.m_TrackedObjectOffset = new Vector3(offset.x, offset.y, 0);
        }

        public void UpdateWeaponData(WeaponData? weaponData)
        {
            turnSpeed = weaponData?.turnSpeed ?? defaultTurnSpeed;
            maxOffset = weaponData?.visionRange ?? defaultMaxOffset;
            orthoLensSize = weaponData?.visionRange ?? defaultOrthoLensSize;
        }
    }
}
