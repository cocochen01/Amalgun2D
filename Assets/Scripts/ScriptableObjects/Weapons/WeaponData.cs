using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Scriptable Objects/WeaponData")]
public abstract class WeaponData : ScriptableObject
{
    public WeaponType weaponType;
    public string weaponName;
    public int damage;
    public float chargeTime;
    public float winddownTime;
}

public enum WeaponType { Gun, Melee }