using UnityEngine;

public class PickUpWeapon : Pickup
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }
    public override void ShowPrompt()
    {
        base.ShowPrompt();
    }
    public override void HidePrompt()
    {
        base.HidePrompt();
    }
}
