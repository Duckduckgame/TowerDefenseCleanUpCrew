using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyUnit : MonoBehaviour
{
    public enum enemyState {Moving, Attacking, Nothing }
    enemyState crntState;
    NavMeshAgent agent;

    public GameObject target;
    public Vector3 destination;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.destination = target.transform.position;
        InvokeRepeating("WigglePath", .5f, 1);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Die()
    {
        agent.enabled = false;
        transform.rotation = Quaternion.Euler(0, 0, 90);
        Destroy(this, 2f);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<FixingHandler>() != null)
        {
            if (Random.Range(1, 3) == 1)
            {
                FixingHandler handler = other.GetComponent<FixingHandler>();
                Debug.Log("found handler");
                if (handler.crntEnemyCount < handler.maxAllowedEnemies)
                {
                    crntState = enemyState.Attacking;
                    agent.destination = handler.transform.position + new Vector3(Random.Range(-5,5),0,Random.Range(-5,5));
                    handler.crntEnemyCount++;
                    handler.attackingUnits.Add(this);
                }
            }
        }
    }

    public void ResetDestination()
    {
        agent.destination = target.transform.position;
    }

    void WigglePath()
    {
        agent.Move( new Vector3(Random.Range(-0.5f, 0.5f),0, Random.Range( - 0.5f, 0.5f)));
    }
}
