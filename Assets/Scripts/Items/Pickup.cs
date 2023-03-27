using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class Pickup : MonoBehaviour
{
    public abstract void OnCollect();
    public abstract void OnCollectEnter();
    public abstract void OnCollectExit();
}
