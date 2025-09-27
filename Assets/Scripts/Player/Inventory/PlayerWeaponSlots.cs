using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Amalgun2D.Player
{
    public class PlayerWeaponSlots : MonoBehaviour
    {
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
                Debug.Log("Scroll up");
                SwapMainWeapon(1);
            }
            else if (scroll.y < 0) // scroll down
            {
                Debug.Log("Scroll down");
                SwapMainWeapon(-1);
            }
        }
        public void SwapMainWeapon(int scrollDirection)
        {
            if (weaponsList.Count == 0) return;

            int currentIndex = weaponsList.IndexOf(selectedWeapon);
            int newIndex = (currentIndex + scrollDirection + weaponsList.Count) % weaponsList.Count;

            selectedWeapon.UnassignPlayer();
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
