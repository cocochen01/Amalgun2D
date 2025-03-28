using Amalgun2D.Attacks;
using Amalgun2D.Core;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

namespace Amalgun2D.Player
{
	public class PlayerAttack : MonoBehaviour
	{
        // References
        private GameManager gm;
        public List<Weapon> weapons = new List<Weapon>();
        public Weapon currWeapon;

        // Flags
        [SerializeField] private bool bTryAttacking = false;
        [SerializeField] private bool bCanAttack = true;

        // Timers
        [SerializeField] private float inputBufferTimer = 0;
        [SerializeField] private float attackCDTimer = 0; //set in attack

        public float tempAttackRate = .5f;
        public bool tempIsChargeWeapon = false;
        private void Start()
        {
            gm = GameManager.Instance;
            InputManager input = InputManager.Instance;
            PlayerInputActions.PlayerActions playerActions = input.playerActions;
            //Handle inputs in weapon class
            //playerActions.Attack.started += StartedAttack;
            //playerActions.Attack.performed += PerformedAttack;
            //playerActions.Attack.canceled += StopAttack;

        }

        private void FixedUpdate()
        {
            Attack();
            if (attackCDTimer > 0)
                attackCDTimer -= Time.deltaTime;
        }
        private void StartedAttack(InputAction.CallbackContext context)
        {

        }
        public void PerformedAttack(InputAction.CallbackContext context)
		{
            inputBufferTimer = GlobalValuesData.globalInputBuffer;
            bTryAttacking = true;

        }
        public void StopAttack(InputAction.CallbackContext context)
        {
            bTryAttacking = false;
        }
        private void Attack()
        {
            // if trying to attack and not on cooldown
            if (bTryAttacking && attackCDTimer <= 0)
            {
                //attackCDTimer = currWeapon.attackrate
                attackCDTimer = Mathf.Max(tempAttackRate, GlobalValuesData.globalAttackCD);
                TempAttackFunction();
            }
            else if (attackCDTimer > 0)
            {
                attackCDTimer -= Time.fixedDeltaTime;
            }
        }

        // put this code in gun script later
        private void TempAttackFunction()
        {
            if (weapons[0] is Gun gun)
            {
                // Make sure the first Child in Player is the Direction!!
                gun.GetComponent<FireBullet>().SpawnBullet(gameObject, transform.GetChild(0));
            }
        }
	} 
}
