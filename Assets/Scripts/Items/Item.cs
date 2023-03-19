using UnityEngine;

public abstract class Item : ScriptableObject
{
    [Header("Item Settings")]
    public string Name;
    public string Description;
    public ItemQuality Quality;
}