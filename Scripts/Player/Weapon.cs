using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private GameObject[] weaponList;
    [SerializeField]
    private GameObject cannonBallPrefab;

    private GameObject cannonBallContainer;

    [SerializeField]
    private AudioClip fireClip;

    private Player player;


    private void Awake()
    {
        player = GetComponentInParent<Player>();
    }

    private void Start()
    {
        cannonBallContainer = GameObject.Find("CannonBallContainer");
    }

    public void fireCannons(int damage,float cannonSpeed)
    {

        for(int i = 0;i<weaponList.Length;i++)
        {
            GameObject cannonBall = Instantiate(cannonBallPrefab, weaponList[i].transform.position, weaponList[i].transform.rotation,cannonBallContainer.transform);
            AudioSource.PlayClipAtPoint(fireClip, 0.8f * Camera.main.transform.position + 0.2f * cannonBall.transform.position, 1f);
            cannonBall.GetComponent<CannonBall>().damage = damage;
            cannonBall.GetComponent<Rigidbody>().AddRelativeForce(0, 200, 1500*cannonSpeed);
            
        }

    }


    

    



}
