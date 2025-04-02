using System.Collections.Generic;
using UnityEngine;

namespace Amalgun2D.Player
{
    public class PlayerWeaponSlots : MonoBehaviour
    {
        // References
        public Transform WeaponsAttachPoint;

        [SerializeField] private List<Weapon> weaponsList = new List<Weapon>();
        private Weapon selectedWeapon;

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
            weapon.Equip(GetComponent<PlayerCharacter>());
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
