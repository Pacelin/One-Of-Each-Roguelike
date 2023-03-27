using System;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    [SerializeField] private bool _disableWhenTriggered;
    public event Action OnTrigger;
    
    private bool _triggered = false;
    protected void Notify() 
    {
        if (_triggered) return;

        OnTrigger?.Invoke();
        
        if (_disableWhenTriggered)
            _triggered = true;
    }
}