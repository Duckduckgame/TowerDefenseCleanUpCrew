using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixingHandler : MonoBehaviour
{
    [SerializeField]
    GameObject health;

    public float life = 1000;

    public float maxAllowedEnemies = 10;
    public float crntEnemyCount;

    [SerializeField]
    float damageTimerInSeconds;

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

        if(life <= 0)
        {
            Die();
        }
    }

    void DamageSelf()
    {
        life -= crntEnemyCount * 2;
    }

    void DamageEnemies()
    {
        if(crntEnemyCount != 0)
        {
            int index = Random.Range(0, attackingUnits.Count);
            crntEnemyCount--;
            attackingUnits[index].Die();
            attackingUnits.RemoveAt(index);

        }
    }

    void Die()
    {
        maxAllowedEnemies = 0;
        foreach(EnemyUnit enemy in attackingUnits)
        {
            enemy.ResetDestination();
        }

    }
}
