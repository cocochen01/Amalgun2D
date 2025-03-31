using UnityEngine;
#nullable enable
namespace Amalgun2D.Player
{
    public class PlayerEventManager : MonoBehaviour
    {
        public delegate void OnPickupEnterRange();
        public event OnPickupEnterRange? onPickupEnterRange;
        public void PickupEnterRange()
        {
            onPickupEnterRange?.Invoke();
        }

        public delegate void OnWeaponEquip(GameObject? weaponObject);
        public event OnWeaponEquip? onWeaponEquip;
        public void WeaponEquip(GameObject? weaponObject)
        {
            onWeaponEquip?.Invoke(weaponObject);
        }
    }
}
