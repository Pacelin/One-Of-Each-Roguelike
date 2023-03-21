using System.Threading.Tasks;
using UnityEngine;

public abstract class State<T> : ScriptableObject where T : MonoBehaviour
{
    protected T _machine;
    public virtual void Init(T machine, State<T> from)
    {
        _machine = machine;
    }
    public virtual void Update() { }
    public virtual void FixedUpdate() { }
    public virtual void ChangeState() { }
    public virtual void Exit() { }
}