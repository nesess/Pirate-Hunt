using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTriger : MonoBehaviour
{

    private Enemy enemy;

    private void Awake()
    {
        enemy = GetComponentInParent<Enemy>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (enemy.playerFound == false)
        {
            if (other.GetComponentInParent<Player>() != null)
            {
                enemy.playerFound = true;
                enemy.player = other.GetComponentInParent<Player>().gameObject;
            }
        }
    }
}
