using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    [SerializeField]
    private GameObject[] weaponList;
    [SerializeField]
    private GameObject cannonBallPrefab;

    private GameObject cannonBallContainer;

    public bool weaponTrigerInrange = false;
    public float fireRate = 0.22f;
    private float canFire = 0;

    private Enemy enemyScript;

    [SerializeField]
    private AudioClip fireClip;


    private void Awake()
    {
        enemyScript = GetComponentInParent<Enemy>();
    }

    private void Start()
    {
        cannonBallContainer = GameObject.Find("CannonBallContainer");
    }

    private void FixedUpdate()
    {
        if(weaponTrigerInrange == true && Time.time > canFire)
        {
            fireCannons(enemyScript.cannonDamage, enemyScript.cannonSpeed);
            canFire = Time.time + fireRate;
        }
    }

    public void fireCannons(int damage, float cannonSpeed)
    {

        for (int i = 0; i < weaponList.Length; i++)
        {
            GameObject cannonBall = Instantiate(cannonBallPrefab, weaponList[i].transform.position, weaponList[i].transform.rotation, cannonBallContainer.transform);
            AudioSource.PlayClipAtPoint(fireClip, 0.2f * Camera.main.transform.position + 0.8f * cannonBall.transform.position, 1f);
            cannonBall.GetComponent<CannonBall>().damage = damage;
            cannonBall.GetComponent<Rigidbody>().AddRelativeForce(0, 200, 1500 * cannonSpeed);

        }

    }
}
