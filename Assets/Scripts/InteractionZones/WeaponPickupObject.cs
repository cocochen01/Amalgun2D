using UnityEngine;

public class WeaponPickupObject : PickupObject
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }
    protected override void OnTriggerExit2D(Collider2D collision)
    {
        base.OnTriggerExit2D(collision);
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
