using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

namespace Amalgun2D.Player
{
    public class PlayerPickUpInteraction : MonoBehaviour
    {
        // References
        private InputAction playerActions;
        private PlayerEventManager playerEvents;
        [SerializeField] private LayerMask pickupMask;

        // Values
        public float DetectionRadius = 2f;

        [SerializeField] private List<GameObject> nearPickups = new List<GameObject>();
        public GameObject currNearestPickup;

        private void Awake()
        {
            playerActions = GetComponent<PlayerInput>().actions["Interact"];
            playerEvents = GetComponent<PlayerEventManager>();
        }
        private void OnEnable()
        {
            playerActions.performed += InteractWithNearestPickupObject;
            playerEvents.onPickupEnterRange += AddPickupObject;
            playerEvents.onPickupExitRange += RemovePickupObject;
        }
        private void OnDisable()
        {
            playerActions.performed -= InteractWithNearestPickupObject;
            playerEvents.onPickupEnterRange -= AddPickupObject;
            playerEvents.onPickupExitRange -= RemovePickupObject;
        }

        public void AddPickupObject(GameObject pickup)
        {
            //Debug.Log("Add");
            if (pickup == null)
                return;

            if (pickup.GetComponent<PickupObject>() != null && !nearPickups.Contains(pickup))
            {
                nearPickups.Add(pickup);
            }
        }
        public void RemovePickupObject(GameObject pickup)
        {
            //Debug.Log("Remove");
            if (pickup == null)
                return;

            if (nearPickups.Contains(pickup))
            {
                nearPickups.Remove(pickup);
            }
        }

        private void FixedUpdate()
        {
            //Collider2D[] pickups = Physics2D.OverlapCircleAll(transform.position, detectionRadius, pickupLayer);

            List<GameObject> newPickups = new List<GameObject>();
            UpdatePrompt();
        }

        private void UpdatePrompt()
        {
            if (nearPickups.Count <= 0)
            {
                if (currNearestPickup != null)
                {
                    currNearestPickup.GetComponent<PickupObject>().HidePrompt();
                    currNearestPickup = null;
                }
                return;
            }

            GameObject nearestPickup = null;
            float nearestDistance = float.MaxValue;

            foreach (GameObject currPickup in nearPickups)
            {
                float distance = Mathf.Abs(transform.position.x - currPickup.transform.position.x);

                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    nearestPickup = currPickup;
                }
            }
            if (currNearestPickup == null)
            {
                currNearestPickup = nearestPickup;
                currNearestPickup.GetComponent<PickupObject>().ShowPrompt();
            }
            else if (nearestPickup != currNearestPickup)
            {
                currNearestPickup.GetComponent<PickupObject>().HidePrompt();
                nearestPickup.GetComponent<PickupObject>().ShowPrompt();
                currNearestPickup = nearestPickup;
            }
        }

        public void InteractWithNearestPickupObject(InputAction.CallbackContext context)
        {
            if (currNearestPickup == null)
            {
                print("No interactableObject in range");
                return;
            }
            if (context.interaction is TapInteraction)
            {
                Debug.Log("Tapped");
                if(currNearestPickup.GetComponent<Weapon>() != null)
                {
                    GetComponent<PlayerWeaponSlots>().AddWeapon(currNearestPickup.GetComponent<Weapon>());
                }
            }
            else if (context.interaction is HoldInteraction)
            {
                Debug.Log("Held");
                if (currNearestPickup.GetComponent<Weapon>() != null)
                {
                    GetComponent<PlayerWeaponSlots>().AddAndEquipWeapon(currNearestPickup.GetComponent<Weapon>());
                }
            }
        }
    }
}
