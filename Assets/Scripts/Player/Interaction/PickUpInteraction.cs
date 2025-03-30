using Amalgun2D.Core;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class PickUpInteraction : MonoBehaviour
{
    // References
    PlayerInputActions.PlayerActions playerActions;

    [SerializeField] private List<GameObject> nearPickups = new List<GameObject>();
    public GameObject currNearestPickup;

    private void Start()
    {
        playerActions = InputManager.Instance.PlayerActions;
        playerActions.Interact.performed += InteractWithNearestPickupObject;
    }
    private void OnDestroy()
    {
        playerActions.Interact.performed -= InteractWithNearestPickupObject;
    }

    public void AddPickupObject(GameObject pickup)
    {
        if (pickup == null)
            return;

        if (pickup.GetComponent<Pickup>() != null && !nearPickups.Contains(pickup))
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
        UpdatePrompt();
    }

    private void UpdatePrompt()
    {
        if (nearPickups.Count <= 0)
        {
            if (currNearestPickup != null)
            {
                currNearestPickup.GetComponent<Pickup>().HidePrompt();
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
            currNearestPickup.GetComponent<Pickup>().ShowPrompt();
        }
        else if (nearestPickup != currNearestPickup)
        {
            currNearestPickup.GetComponent<Pickup>().HidePrompt();
            nearestPickup.GetComponent<Pickup>().ShowPrompt();
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
