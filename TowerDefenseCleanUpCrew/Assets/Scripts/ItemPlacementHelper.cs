using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
[SelectionBase]
public class ItemPlacementHelper : MonoBehaviour
{
    private void OnEnable()
    {
        FloorPosition();
    }

    private void Update()
    {
        if (transform.hasChanged)
        {
            FloorPosition();
            transform.hasChanged = false;
        }
    }

    void FloorPosition()
    {
        Vector3 pos = transform.position;
        pos.x = Mathf.Floor(pos.x);
        pos.y = Mathf.Floor(pos.y);
        pos.z = Mathf.Floor(pos.z);
        transform.position = pos;
    }
}
