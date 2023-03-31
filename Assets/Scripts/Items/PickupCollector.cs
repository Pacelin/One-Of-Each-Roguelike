using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PickupCollector : MonoBehaviour
{
    public Pickup CurrentPickup => _collectables.LastOrDefault();

    [SerializeField] private KeyCode _collectKey = KeyCode.E;

    private List<Pickup> _collectables = new();

    private void OnTriggerEnter2D(Collider2D other)
    {
        var pickup = other.gameObject.GetComponent<Pickup>();
        if (pickup != null)
            AddPickup(pickup);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (CurrentPickup == null) return;

        var pickup = other.gameObject.GetComponent<Pickup>();
        RemovePickup(pickup);
    }

    private void Update()
    {
        if (CurrentPickup == null) return;

        if (Input.GetKeyDown(_collectKey))
            CurrentPickup.OnCollect();
    }

    private void AddPickup(Pickup pickup)
    {
        CurrentPickup?.OnCollectExit();
        _collectables.Add(pickup);
        CurrentPickup.OnCollectEnter();
    }

    private void RemovePickup(Pickup pickup)
    {
        if (CurrentPickup == pickup)
        {
            CurrentPickup.OnCollectExit();
            _collectables.Remove(pickup);
            CurrentPickup?.OnCollectEnter();
        }
        else
        {
            _collectables.Remove(pickup);
        }
    }
}
