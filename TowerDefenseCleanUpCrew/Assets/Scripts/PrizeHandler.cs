using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrizeHandler : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            FindObjectOfType<LevelManager>().prizesTaken++;

            GetComponentInChildren<SpriteRenderer>().color = Color.red;
            Destroy(this);
        }
    }
}
