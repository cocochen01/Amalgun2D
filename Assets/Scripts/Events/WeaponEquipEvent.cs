#nullable enable
public class WeaponEquipEvent : FWEvent<WeaponEquipEvent>
{
    public readonly WeaponData? WeaponData;
    

    public WeaponEquipEvent(WeaponData? weaponData)
    {
        WeaponData = weaponData;
    }
}

