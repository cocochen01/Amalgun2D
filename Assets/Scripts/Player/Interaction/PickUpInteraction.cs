using Amalgun2D.Core;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class PickUpInteraction : MonoBehaviour
{
    // References
    private InputAction playerActions;
    [SerializeField] private LayerMask pickupMask;

    // Values
    public float DetectionRadius = 2f;

    [SerializeField] private List<GameObject> nearPickups = new List<GameObject>();
    public GameObject currNearestPickup;

    private void Start()
    {
        playerActions = GetComponent<PlayerInput>().actions["Interact"];
        playerActions.performed += InteractWithNearestPickupObject;
    }
    private void OnDestroy()
    {
        playerActions.performed -= InteractWithNearestPickupObject;
    }

    public void AddPickupObject(GameObject pickup)
    {
        if (pickup == null)
            return;

        if (pickup.GetComponent<PickupObject>() != null && !nearPickups.Contains(pickup))
        {
            nearPickups.Add(pickup);
        }
    }
    public void RemovePickupObject(GameObject pickup)
    {
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
        }
        else if (context.interaction is HoldInteraction)
        {
            Debug.Log("Held");
        }
    }
}
