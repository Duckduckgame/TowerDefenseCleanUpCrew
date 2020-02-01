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

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnEnemy", 0, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*IEnumerator SpawnEnemy()
    {
        Instantiate(enemy, transform.position, Quaternion.identity);
        yield return null;
    }*/

    void SpawnEnemy()
    {
        GameObject enemyGO = Instantiate(enemy, transform.position, Quaternion.identity, this.transform);
        enemyGO.GetComponent<EnemyUnit>().target = target;
    }
}
