using Amalgun2D.Attacks;
using System.Collections.Generic;
using UnityEngine;

namespace Amalgun2D.Player
{
	public class PlayerAttack : MonoBehaviour
	{
        public List<Weapon> weapons = new List<Weapon>();
        private void Start()
        {
        }
        public void AttackWithWeapon()
		{
            if (weapons[0] is Gun gun)
            {
                // Make sure the first Child in Player is the Direction!!
                gun.GetComponent<FireBullet>().SpawnBullet(gameObject, transform.GetChild(0));
            }
            Debug.Log("Attack.");
		}
	} 
}
