using System.Collections.Generic;
using UnityEngine;

public class WeaponSlots : MonoBehaviour
{
    //public List<Weapon> WeaponsList = new List<Weapon>();
    [SerializeField] private List<GameObject> weaponsList = new List<GameObject>();
    private GameObject selectedWeapon;

    public Transform WeaponsParent;

    public void AddWeapon(GameObject weapon)
    {
        if (weapon == null)
        {
            Debug.Log("Tried adding null WeaponPrefab");
            return;
        }
        if (weapon.GetComponent<Weapon>() == null)
        {
            Debug.Log("Tried adding a non Weapon");
            return;
        }
        Transform weaponTransform = weapon.transform;
        weaponTransform.parent = WeaponsParent;
        weaponTransform.localPosition = -weaponTransform.GetChild(0).position;

        weaponsList.Add(weapon);
    }
}
