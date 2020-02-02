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

    [SerializeField]
    Color[] colours;

    // Start is called before the first frame update
    void Start()
    {

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
        //enemyGO.GetComponentInChildren<SpriteRenderer>().color *= c1[Random.Range(0, c1.Length)];
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 5);
        try
        {
            Gizmos.DrawLine(transform.position, target.transform.position);
        }
        catch { }
    }

    public void StartSiegeSpawn()
    {
        for (int i = 0; i < maxNumberToSpawn; i++)
        {
            SpawnEnemy();
        }
    }
}
