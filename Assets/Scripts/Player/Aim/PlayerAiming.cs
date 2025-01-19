using UnityEngine;

namespace Amalgun2D.Player
{
    public class PlayerAiming : MonoBehaviour
    {
        private Camera playerCamera;
        private void Start()
        {
            playerCamera = Camera.main;
        }

        private void Update()
        {
            Vector3 mousePosition = playerCamera.ScreenToWorldPoint(Input.mousePosition);

            Vector3 direction = mousePosition - transform.position;
            direction.z = 0;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }
    } 
}
