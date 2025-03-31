using UnityEngine;

public class PickupObject : MonoBehaviour
{
    protected virtual void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            collider.GetComponent<PickUpInteraction>().AddPickupObject(gameObject);
        }
    }
    protected virtual void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            collider.GetComponent<PickUpInteraction>().RemovePickupObject(gameObject);
        }
    }

    public virtual void ShowPrompt()
    {
        Debug.Log("Showing prompt");
    }
    public virtual void HidePrompt()
    {
        Debug.Log("Hiding prompt");
    }
}
