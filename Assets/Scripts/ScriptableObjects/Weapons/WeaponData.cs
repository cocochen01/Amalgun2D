using UnityEngine;

public abstract class WeaponData : ScriptableObject
{
    public WeaponType weaponType;
    public string weaponName;
    public int damage;
    public float chargeTime;
    public float winddownTime;
    public float turnSpeed = 10f;
    public float recoil = 1f;
    public float fireRate = 1f;
    public abstract float visionRange { get; }
}

public enum WeaponType { Gun, Melee }