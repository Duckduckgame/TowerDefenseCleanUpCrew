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
        pos.x = Mathf.Round(pos.x/8)*8;
        //pos.y = 0;
        pos.z = Mathf.Round(pos.z/8)*8;
        transform.position = pos;
    }
}
