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