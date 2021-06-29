using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Joystick joystick;
    private bool canMove = true;
    private Rigidbody rigid;



    
    public int maxHealth = 10;
    
    public int health = 10;

    
    public float speed;
    
    public float rotationSpeed;
    
    public int cannonDamage = 1;
    
    public float cannonRange = 1f;

    public float fireRate = 0.22f;
    private float canFire = 0;


    private void Awake()
    {
        joystick = FindObjectOfType<Joystick>();
        rigid = GetComponent<Rigidbody>();
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        
        if (canMove)
        {

            if (joystick.Horizontal > 0.2f || joystick.Horizontal < -0.2f || joystick.Vertical > 0.2f || joystick.Vertical < -0.2f)
            {
                transform.position += transform.forward * Time.deltaTime * speed * (Mathf.Abs(joystick.Vertical)+Mathf.Abs(joystick.Horizontal));
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, rotationCalculator(), 0), rotationSpeed * Time.deltaTime);
            }
        }
        if(rigid.velocity.magnitude > 2f)
        {
            rigid.velocity = rigid.velocity * 0.95f;
        }
        else
        {
            rigid.velocity = Vector3.zero;
        }
        
    }

    private float rotationCalculator()
    {
        float calculation = Mathf.Atan2(joystick.Horizontal,joystick.Vertical) * 180 / Mathf.PI;
        return calculation;
    }
    public void weaponFireButton()
    {
        if(Time.time > canFire)
        {
            Weapon weapon = GetComponentInChildren<Weapon>();
            weapon.fireCannons(cannonDamage, cannonRange);
            canFire = Time.time + fireRate;
        }
        
    }


    private void OnCollisionEnter(Collision other)
    {
        
        if (other.gameObject.tag == "EnemyCannonBall")
        {
            UIManager.instance.playerDamaged(other.gameObject.GetComponent<CannonBall>().damage);
            Destroy(other.gameObject);

        }
        else if(other.gameObject.tag=="Enemy")
        {
            Vector3 dir = other.contacts[0].point - transform.position;
            dir = -dir.normalized;
            rigid.AddForce(dir * 1000f);
        }
        else if (other.gameObject.tag == "Box")
        {
            Vector3 dir = other.contacts[0].point - transform.position;
            dir = -dir.normalized;
            rigid.AddForce(dir * 1000f);
        }
        else if (other.gameObject.tag == "Bounds")
        {
            Vector3 dir = other.contacts[0].point - transform.position;
            dir = -dir.normalized;
            rigid.AddForce(dir * 5000f);
        }

    }

}
