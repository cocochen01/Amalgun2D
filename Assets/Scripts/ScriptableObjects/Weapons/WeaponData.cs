using UnityEngine;

public enum WeaponType { Gun, Melee }
public abstract class WeaponData : ScriptableObject
{
    public WeaponType weaponType = WeaponType.Melee;
    public string weaponName = "default";
    public int damage = 1;
    public float chargeTime = 0;
    public float winddownTime = 0;
    public float turnSpeed = 10f;
    public float recoil = 1f;
    public float fireRate = 1f;
    public abstract float visionRange { get; }
    public abstract int magazineSize { get; }
    public abstract float spreadAngle { get; }
}
