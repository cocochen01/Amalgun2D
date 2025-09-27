using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Amalgun2D.Player
{
    public class PlayerWeaponSlots : MonoBehaviour
    {
        // Input buffer
        [SerializeField] private float scrollBufferTime = 0.2f;
        [SerializeField] private float swapCooldownTime = 0.5f;
        private float scrollBufferTimer = 0f;
        private float swapCDTimer = 0f;
        private int scrollDirectionBuffered = 0;

        // References
        private InputAction playerActions;
        public Transform WeaponsAttachPoint;

        [SerializeField] private List<Weapon> weaponsList = new List<Weapon>();
        public Weapon selectedWeapon;

        private void Start()
        {
            playerActions = GetComponent<PlayerInput>().actions["Swap"];
            playerActions.performed += OnScroll;
        }
        private void OnDestroy()
        {
            if (playerActions != null)
                playerActions.performed -= OnScroll;
        }
        private void Update()
        {
            if (swapCDTimer > 0)
                swapCDTimer -= Time.deltaTime;

            if (scrollBufferTimer > 0)
            {
                scrollBufferTimer -= Time.deltaTime;

                if (swapCDTimer <= 0 && scrollDirectionBuffered != 0)
                {
                    SwapMainWeapon(scrollDirectionBuffered);

                    scrollBufferTimer = 0f;
                    swapCDTimer = swapCooldownTime;
                    scrollDirectionBuffered = 0;
                }
            }
        }

        public void AddWeapon(Weapon weapon)
        {
            if(weaponsList.Count == 0)
            {
                AddAndEquipWeapon(weapon);
            }
            else
            {
                EquipHelper(weapon);
            }
        }
        public void AddAndEquipWeapon(Weapon weapon)
        {
            EquipHelper(weapon);
            selectedWeapon = weapon;
            selectedWeapon.AssignPlayer(GetComponent<PlayerCharacter>());
        }

        private void OnScroll(InputAction.CallbackContext context)
        {
            Vector2 scroll = context.ReadValue<Vector2>();

            if (scroll.y > 0) // scroll up
            {
                scrollDirectionBuffered = 1;
                scrollBufferTimer = scrollBufferTime;
            }
            else if (scroll.y < 0) // scroll down
            {
                scrollDirectionBuffered = -1;
                scrollBufferTimer = scrollBufferTime;
            }
        }

        public void SwapMainWeapon(int scrollDirection)
        {
            if (weaponsList.Count == 0) return;

            int currentIndex = weaponsList.IndexOf(selectedWeapon);
            int newIndex = (currentIndex + scrollDirection + weaponsList.Count) % weaponsList.Count;

            selectedWeapon?.UnassignPlayer();
            selectedWeapon = weaponsList[newIndex];
            selectedWeapon.AssignPlayer(GetComponent<PlayerCharacter>());
        }

        private void EquipHelper(Weapon weapon)
        {
            if (WeaponsAttachPoint == null)
            {
                Debug.LogWarning("Set weapon attach point first");
                return;
            }
            if (weapon == null)
            {
                Debug.Log("Tried adding null Weapon object");
                return;
            }
            Transform weaponTransform = weapon.transform;
            Transform attachPoint = weaponTransform.GetChild(0);

            weaponTransform.parent = WeaponsAttachPoint;


            Quaternion rotationOffset = Quaternion.Inverse(attachPoint.rotation) * weaponTransform.rotation;
            weaponTransform.rotation = WeaponsAttachPoint.rotation * rotationOffset;

            Vector3 positionOffset = weaponTransform.position - attachPoint.position;
            weaponTransform.position = WeaponsAttachPoint.position + positionOffset;


            if (!weaponsList.Contains(weapon))
                weaponsList.Add(weapon);
        }
    }
}
