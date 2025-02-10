using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InventoryData", menuName = "Scriptable Objects/InventoryData")]
public class InventoryData : ScriptableObject
{
    [Header("Weapons")]
    public List<Weapon> weapons = new List<Weapon>();

    [Header("Items")]
    public List<Weapon> items = new List<Weapon>();
}
