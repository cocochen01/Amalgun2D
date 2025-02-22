using Amalgun2D.Attacks;
using Amalgun2D.Core;
using System.Collections.Generic;
using UnityEngine;

namespace Amalgun2D.Player
{
	public class PlayerAttack : MonoBehaviour
	{
        public List<Weapon> weapons = new List<Weapon>();
        private void Start()
        {
            InputManager input = InputManager.Instance;
            PlayerInputActions.PlayerActions playerActions = input.playerActions;
            playerActions.Attack.performed += context => AttackWithWeapon();
        }
        public void AttackWithWeapon()
		{
            if (weapons[0] is Gun gun)
            {
                // Make sure the first Child in Player is the Direction!!
                gun.GetComponent<FireBullet>().SpawnBullet(gameObject, transform.GetChild(0));
            }
		}
	} 
}
