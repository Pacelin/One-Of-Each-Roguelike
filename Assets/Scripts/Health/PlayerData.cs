using System;

[Serializable]
public class PlayerData
{
    public float Health { get; set; }

    public float BaseHealth;

    public void Reset()
    {
        Health = BaseHealth;
    }
}