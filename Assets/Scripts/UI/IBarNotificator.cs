using System;

public interface IBarNotificator
{
    event Action<float> OnValueChanged;

    float GetMin();
    float GetMax();
    float GetCurrent();
}