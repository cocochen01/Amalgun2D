using UnityEngine;
#nullable enable
namespace Amalgun2D.Player
{
    public class PlayerEventManager : MonoBehaviour
    {
        public delegate void OnPickupEnterRange(GameObject? pickupObject);
        public event OnPickupEnterRange? onPickupEnterRange;
        public void PickupEnterRange(GameObject? pickupObject)
        {
            onPickupEnterRange?.Invoke(pickupObject);
        }

        public delegate void OnPickupExitRange(GameObject? pickupObject);
        public event OnPickupExitRange? onPickupExitRange;
        public void PickupExitRange(GameObject? pickupObject)
        {
            onPickupExitRange?.Invoke(pickupObject);
        }

        public delegate void OnWeaponEquip(GameObject? weaponObject);
        public event OnWeaponEquip? onWeaponEquip;
        public void WeaponEquip(GameObject? weaponObject)
        {
            onWeaponEquip?.Invoke(weaponObject);
        }
    }
}
