using UnityEngine;

[CreateAssetMenu(fileName = "GunData", menuName = "Scriptable Objects/GunData")]
public class GunData : WeaponData
{
    public BulletData bulletData;
    public float fireRate = 1f;
}
