using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    

    public static GameManager instance;

    private SpawnManager spawnManager;

    private void Awake()
    {
        if (GameManager.instance)
        {
            Destroy(base.gameObject);
        }
        else
        {
            GameManager.instance = this;
        }

        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();

    }

   

    


    public void startBoxEnum()
    {
        StartCoroutine(boxDestroyed());
    }

    public void startEnemyEnum()
    {
        StartCoroutine(enemyDestroyed());
    }

    private IEnumerator boxDestroyed()
    {
        yield return new WaitForSeconds(Random.Range(5f, 10f));
        spawnManager.spawnBox();
    }

    private IEnumerator enemyDestroyed()
    {
        yield return new WaitForSeconds(Random.Range(10f, 15f));
        spawnManager.spawnEnemy();
    }
}
