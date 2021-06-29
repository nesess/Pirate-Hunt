using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxTriger : MonoBehaviour
{

    private Enemy enemy;

    private void Awake()
    {
        enemy = GetComponentInParent<Enemy>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(enemy.boxFound == false && enemy.playerFound == false)
        {
            
            if (other.tag == "Box")
            {
                enemy.boxFound = true;
                enemy.boxTransform = other.transform;
            }
        }
    }
}
