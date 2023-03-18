using UnityEngine;

public abstract class StateMachine<T> : MonoBehaviour where T : StateMachine<T>
{
    protected State<T> _currentState;

    protected abstract void Start();

    protected virtual void Update()
    {
        _currentState.Update();
        _currentState.ChangeState();
    }

    protected virtual void FixedUpdate()
    {
        _currentState.FixedUpdate();
    }

    public void SwitchState(State<T> state) 
    {
        _currentState.Exit();
        _currentState = state;
        _currentState.Init((T) this);
    }
}