using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField]
    GameObject enemy;

    //List<GameObject> targets;
    [SerializeField]
    GameObject target;

    float numberSpawned;
    public float maxNumberToSpawn;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnEnemy", 0, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (numberSpawned >= maxNumberToSpawn)
            CancelInvoke("SpawnEnemy");
    }


    void SpawnEnemy()
    {
        GameObject enemyGO = Instantiate(enemy, transform.position, Quaternion.identity, this.transform);
        enemyGO.GetComponent<EnemyUnit>().target = target;
        numberSpawned++;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 5);
        Gizmos.DrawLine(transform.position, target.transform.position);
    }
}
