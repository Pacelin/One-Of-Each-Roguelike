using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ColliderExitTrigger : Trigger
{
    [SerializeField] private Collider2D _triggeredCollider;
    private void OnTriggerExit2D(Collider2D other) 
    {
        if (other == _triggeredCollider)
            Notify();
    }
}