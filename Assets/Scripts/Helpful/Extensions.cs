using UnityEngine;

public static class TransformExtension
{
    public static void ClearChilds(this Transform transform)
    {
        for (int i = 0; i < transform.childCount; i++)
            Object.Destroy(transform.GetChild(i));
    }
    public static Transform AddChild(this Transform transform, Vector3 localPosition)
    {
        var tr = new GameObject("Point").transform;
        tr.SetParent(transform);
        tr.localPosition = localPosition;
        return tr;
    }
}

public static class Vector2Extension
{
    public static Vector2 Rotate(this Vector2 vector, float angle)
    {
        var sin = Mathf.Sin(angle * Mathf.Deg2Rad);
        var cos = Mathf.Cos(angle * Mathf.Deg2Rad);
        return new Vector2(
            vector.x * cos - vector.y * sin,
            vector.x * sin + vector.y * cos
        );
    }
}