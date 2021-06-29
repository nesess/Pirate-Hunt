using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponTriger : MonoBehaviour
{
    private EnemyWeapon enemyWeapon;

    private void Awake()
    {
        enemyWeapon = GetComponentInParent<EnemyWeapon>();
    }

    private void OnTriggerStay(Collider other)
    {
        
        if (other.GetComponentInParent<Player>() != null)
        {
            enemyWeapon.weaponTrigerInrange = true;
        }
        else if(other.GetComponentInParent<Box>() != null)
        {
            enemyWeapon.weaponTrigerInrange = true;
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponentInParent<Player>() != null)
        {
            enemyWeapon.weaponTrigerInrange = false;
        }
        else if(other.GetComponentInParent<Box>() != null)
        {
            enemyWeapon.weaponTrigerInrange = false;
        }

    }
}
