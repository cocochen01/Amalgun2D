#nullable enable
using UnityEngine;

public class WeaponEquipEvent : FWEvent<WeaponEquipEvent>
{
    public readonly GameObject? WeaponObject;
    public WeaponEquipEvent(GameObject? weaponObject)
    {
        WeaponObject = weaponObject;
    }
}

