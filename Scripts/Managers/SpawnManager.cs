using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    [SerializeField]
    private GameObject enemyPrefab;
    [SerializeField]
    private GameObject enemyContainer;
    [SerializeField]
    private GameObject boxPrefab;
    [SerializeField]
    private GameObject boxContainer;

    // Start is called before the first frame update
    void Start()
    {
        spawnStartBox();
        spawnStartEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void spawnBox()
    {
        bool spawned;
        do
        {
            int randomX = Random.Range(-248, 248);
            int randomZ = Random.Range(-248, 248);
            Vector3 spawnPoint = new Vector3(randomX, 0.68f, randomZ);
            var hitColliders = Physics.OverlapSphere(spawnPoint, 2.5f);

            if (hitColliders.Length > 0)
            {
                spawned = false;
            }
            else
            {
                GameObject box = Instantiate(boxPrefab, spawnPoint, Quaternion.identity, boxContainer.transform);
                spawned = true;
            }

        } while (spawned == false);
    }


    public void spawnEnemy()
    {
        bool spawned;
        do
        {
            int randomX = Random.Range(-248, 248);
            int randomZ = Random.Range(-248, 248);
            Vector3 spawnPoint = new Vector3(randomX, 0.68f, randomZ);
            var hitColliders = Physics.OverlapSphere(spawnPoint, 2.5f);

            if (hitColliders.Length > 0)
            {
                spawned = false;
            }
            else
            {
                GameObject box = Instantiate(boxPrefab, spawnPoint, Quaternion.identity, boxContainer.transform);
                spawned = true;
            }

        } while (spawned == false);
    }

    private void spawnStartEnemy()
    {
        for (int i = 0; i < 15; i++)
        {
            bool spawned;
            do
            {
                int randomX = Random.Range(-248, 248);
                int randomZ = Random.Range(-248, 248);
                Vector3 spawnPoint = new Vector3(randomX, 0.68f, randomZ);
                var hitColliders = Physics.OverlapSphere(spawnPoint, 5.5f);

                if (hitColliders.Length > 0)
                {
                    spawned = false;
                }
                else
                {
                    GameObject enemy = Instantiate(enemyPrefab, spawnPoint, Quaternion.identity, enemyContainer.transform);
                    spawned = true;
                }

            } while (spawned == false);
        }
    }

    private void spawnStartBox()
    {
        for(int i=0; i<100; i++)
        {
            bool spawned;
            do
            {
                int randomX = Random.Range(-248, 248);
                int randomZ = Random.Range(-248, 248);
                Vector3 spawnPoint = new Vector3(randomX, 0.68f, randomZ);
                var hitColliders = Physics.OverlapSphere(spawnPoint, 2.5f);

                if (hitColliders.Length > 0)
                {
                    spawned = false;
                }
                else
                {
                    GameObject box = Instantiate(boxPrefab, spawnPoint, Quaternion.identity, boxContainer.transform);
                    spawned = true;
                }

            } while (spawned == false);
        }
    }

}
