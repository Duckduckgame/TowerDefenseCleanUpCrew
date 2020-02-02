using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyUnit : MonoBehaviour
{
    public enum enemyState {Moving, Attacking, Nothing }
    public enemyState crntState;
    NavMeshAgent agent;

    
    SpriteRenderer spriteChild;

    public GameObject target;
    public Vector3 destination;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.destination = target.transform.position;
        spriteChild = GetComponentInChildren<SpriteRenderer>();
        Invoke("Die", 20);
    }

    // Update is called once per frame
    void Update()
    {
        if(agent.velocity.x > 0)
        {
            spriteChild.flipX = false;
        }
        if (agent.velocity.x < 0)
        {
            spriteChild.flipX = true;
        }
    }

    public void Die()
    {
        try
        {
            agent.isStopped = true;
        }
        catch { }
        try
        {
            GetComponentInChildren<Animator>().SetBool("isDead", true);
        }
        catch { }
        GetComponent<CapsuleCollider>().isTrigger = true;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<FixingHandler>() != null && crntState != enemyState.Attacking)
        {
                FixingHandler handler = other.GetComponent<FixingHandler>();

                if (handler.crntEnemyCount < handler.maxAllowedEnemies)
                {
                    crntState = enemyState.Attacking;
                    agent.destination = handler.transform.position + new Vector3(Random.Range(-5,5),0,Random.Range(-5,5));
                    handler.crntEnemyCount++;
                    handler.attackingUnits.Add(this);
                }
            
        }
    }


    public void ResetDestination()
    {
        try
        {
            if (agent.enabled == true)
                agent.destination = target.transform.position;
        }
        catch { }
    }

    private void OnDrawGizmos()
    {
        //Gizmos.DrawLine(transform.position, agent.destination);
    }

}
