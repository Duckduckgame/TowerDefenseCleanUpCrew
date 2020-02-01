using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateHandler : MonoBehaviour
{
    [SerializeField]
    GameObject gate;
    FixingHandler fixingHandler;
    // Start is called before the first frame update
    void Start()
    {
        fixingHandler = GetComponent<FixingHandler>();   
    }

    // Update is called once per frame
    void Update()
    {
        if(fixingHandler.life <= 0)
        {
            gate.SetActive(false);
            GetComponent<UnityEngine.AI.NavMeshObstacle>().enabled = false;
        }
    }
}
