using UnityEngine;

[CreateAssetMenu(fileName = "GunData", menuName = "Scriptable Objects/GunData")]
public class GunData : WeaponData
{
    public BulletData bulletData;
    public int gunMagazineSize = 10;
    public float gunSpreadAngle = 0;
    public override int magazineSize => gunMagazineSize;
    public override float spreadAngle => gunSpreadAngle;
}
