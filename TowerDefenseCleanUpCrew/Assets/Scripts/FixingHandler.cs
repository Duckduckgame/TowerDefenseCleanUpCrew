using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixingHandler : MonoBehaviour
{
    [SerializeField]
    GameObject health;
    [SerializeField]
    float damageMultiplier;
    public float life = 1000;

    public float maxAllowedEnemies = 10;
    public float crntEnemyCount;

    [SerializeField]
    float damageTimerInSeconds;
    [SerializeField]
    bool showRadius;
    public List<EnemyUnit> attackingUnits;
    // Start is called before the first frame update
    void Start()
    {
        attackingUnits = new List<EnemyUnit>();
        InvokeRepeating("DamageSelf", Random.Range(0f,1f), 1);
        InvokeRepeating("DamageEnemies", 0, damageTimerInSeconds);

    }

    // Update is called once per frame
    void Update()
    {
        life = Mathf.Clamp(life, 0, 1000);
        float healthNorm = Mathf.InverseLerp(0, 1000, life);
        Vector3 scale = health.transform.localScale;
        scale.x = healthNorm;
        health.transform.localScale = scale;
        crntEnemyCount = attackingUnits.Count;
        if(life <= 0)
        {
            Die();
        }
        
    }

    void DamageSelf()
    {
        life -= crntEnemyCount * damageMultiplier;
    }

    void DamageEnemies()
    {
        if(crntEnemyCount != 0)
        {
            int index = Random.Range(0, attackingUnits.Count);
            EnemyUnit unit = attackingUnits[index];
            attackingUnits.RemoveAt(index);
            unit.Die();

            crntEnemyCount--;
            CleanList();
        }
    }

    void Die()
    {
        maxAllowedEnemies = 0;
        foreach(EnemyUnit enemy in attackingUnits)
        {
            try
            {
                enemy.crntState = EnemyUnit.enemyState.Moving;
                if (enemy.GetComponent<UnityEngine.AI.NavMeshAgent>() != null)
                    enemy.ResetDestination();
            }
            catch { }
            Destroy(this);
        }

    }

    void CleanList()
    {
        for (int i = attackingUnits.Count - 1; i > -1; i--)
        {
            if (attackingUnits[i] == null)
                attackingUnits.RemoveAt(i);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        try
        {
            foreach (EnemyUnit enemy in attackingUnits)
            {
                Gizmos.DrawLine(transform.position, enemy.transform.position);
            }
        }
        catch { }

        if (showRadius)
        {
            SphereCollider col = GetComponent<SphereCollider>();
            Gizmos.DrawWireSphere(transform.position + col.center, col.radius);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<PlayerControler>().fixableObject = this.gameObject;
            FindObjectOfType<LevelManager>().FlashFixText();
        }
    }
}
