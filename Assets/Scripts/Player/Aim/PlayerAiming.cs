using UnityEngine;
using Amalgun2D.Core;
using Cinemachine;

namespace Amalgun2D.Player
{
    public class PlayerAiming : MonoBehaviour
    {
        public Vector3 mousePosition;

        private float cameraStretchFactor = .3f;

        private float maxOffsetX = 2f;
        private float maxOffsetY = 2f;

        private CinemachineFramingTransposer framingTransposer;
        private void Start()
        {
            framingTransposer = GameManager.Instance.virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
        }
        private void Update()
        {
            mousePosition = GameManager.Instance.playerCamera.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;

            Vector3 direction = mousePosition - transform.position;
            direction.z = 0;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0f, 0f, angle);

        }

        private void LateUpdate()
        {
            Vector3 offset = (mousePosition - transform.position) * cameraStretchFactor;
            offset.x = Mathf.Clamp(offset.x, -maxOffsetX, maxOffsetX);
            offset.y = Mathf.Clamp(offset.y, -maxOffsetY, maxOffsetY);
            framingTransposer.m_TrackedObjectOffset = new Vector3(offset.x, offset.y, 0);
        }
    } 
}
