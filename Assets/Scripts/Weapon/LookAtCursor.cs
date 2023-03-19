using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCursor : MonoBehaviour
{
    [SerializeField] Camera _lookIn;

    private void Update()
    {
        var mousePos = _lookIn.ScreenToWorldPoint(Input.mousePosition);
        var myPos = transform.position;
        mousePos.z = 0;
        myPos.z = 0;

        var direction = mousePos - myPos;
        transform.right = direction;
        transform.rotation = Quaternion.Euler(
            direction.x < 0 ? 180 : 0,
            0,
            direction.x < 0 ? -transform.rotation.eulerAngles.z : transform.rotation.eulerAngles.z);
    }
}
