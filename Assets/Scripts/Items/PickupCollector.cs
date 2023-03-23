using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PickupCollector : MonoBehaviour
{
    [SerializeField] private KeyCode _collectKey = KeyCode.E;

    private Pickup _collectable;

    private void OnTriggerEnter2D(Collider2D other)
    {
        var pickup = other.gameObject.GetComponent<Pickup>();
        if (pickup != null)
        {
            if (_collectable != null) _collectable.OnCollectExit();
            _collectable = pickup;
            _collectable.OnCollectEnter();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (_collectable == null) return;
        if (_collectable == other.gameObject.GetComponent<Pickup>())
        {
            _collectable.OnCollectExit();
            _collectable = null;
        }
    }

    private void Update()
    {
        if (_collectable == null) return;

        if (Input.GetKeyDown(_collectKey))
            _collectable.OnCollect();
    }
}
