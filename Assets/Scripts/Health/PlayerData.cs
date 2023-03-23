using System;
using System.Net.WebSockets;

[Serializable]
public class PlayerData
{
    public float MaxHealth { get; private set; }

    public float BaseMaxHealth;

    private float _cachedMaxHealth;

    public void Reset() =>
        _cachedMaxHealth = BaseMaxHealth;

    public void AddMaxHealth(float value) =>
        _cachedMaxHealth += value;

    public void Update() =>
        MaxHealth = _cachedMaxHealth;
}