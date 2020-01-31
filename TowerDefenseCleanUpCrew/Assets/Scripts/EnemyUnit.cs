using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyUnit : MonoBehaviour
{
    NavMeshAgent agent;

    public GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        Vector3 pos = target.transform.position;
        pos += new Vector3(Random.Range(-2, 2), 0, Random.Range(-2, 2));
        agent.destination = pos;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
