using System.Collections.Generic;
using UnityEngine;

public class WeaponTree : MonoBehaviour
{
    [System.Serializable]
    public class WeaponNode
    {
        public Weapon weapon;
        public List<WeaponNode> weaponNodes = new List<WeaponNode>();
    }
}
