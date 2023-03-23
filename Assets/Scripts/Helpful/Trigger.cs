using System;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    [SerializeField] private bool _disableWhenTriggered;
    public event Action OnTrigger;
    
    protected void Notify() 
    {
        OnTrigger?.Invoke();
        if (_disableWhenTriggered)
            Destroy(gameObject);
    }
}