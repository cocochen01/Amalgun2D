using UnityEngine;

[CreateAssetMenu(fileName = "GunData", menuName = "Scriptable Objects/GunData")]
public class GunData : WeaponData
{
    public BulletData bulletData;
    public int magazineSize = 10;
    public float spreadAngle = 0;
    public int bounceNum = 0;
}
