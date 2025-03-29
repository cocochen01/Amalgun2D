using UnityEngine;

[CreateAssetMenu(fileName = "GunData", menuName = "Scriptable Objects/GunData")]
public class GunData : WeaponData
{
    public BulletData bulletData;
    public float gunVisionRange = .3f;
    public override float visionRange => gunVisionRange;
}
