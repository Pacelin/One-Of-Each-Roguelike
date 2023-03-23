using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ColliderEnterTrigger : Trigger
{
    [SerializeField] private Collider2D _triggeredCollider;
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other == _triggeredCollider)
            Notify();
    }
}