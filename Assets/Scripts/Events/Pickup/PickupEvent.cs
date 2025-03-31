#nullable enable
using UnityEngine;

public class PickupEvent : FWStaticEvent<PickupEvent>
{
    public readonly GameObject? PickupObject;
    public PickupEvent(GameObject? pickupObject)
    {
        PickupObject = pickupObject;
    }
}
