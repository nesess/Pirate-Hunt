using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField]
    private float speed;
    [SerializeField]
    private float rotationSpeed;
    
    public int cannonDamage = 1;
    public float cannonSpeed;

    [SerializeField]
    private int maxHealth;
    [SerializeField]
    private int health;
    [SerializeField]
    private int score;
    

    private Weapon weapon;

    public bool playerFound;
    public GameObject player;

    public bool boxFound;
    public Transform boxTransform;

    private bool destinationSet = false;
    private Vector3 destination;

    private Rigidbody rigid;

    private void Awake()
    {
        weapon = GetComponentInChildren<Weapon>();
        rigid = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
        int level = Random.Range(0, 20);
        if(level >= 19)
        {
            enemyLevelArrange(4);
        }
        else if (level >= 16)
        {
            enemyLevelArrange(3);
        }
        else if (level >= 12)
        {
            enemyLevelArrange(2);
        }
        else if (level >= 8)
        {
            enemyLevelArrange(1);
        }
        else 
        {
            enemyLevelArrange(0);
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (playerFound == true && player != null)
        {
            boxFound = false;
            boxTransform = null;
            if (Vector3.Distance(transform.position, player.transform.position) > 30f)
            {
                transform.position += transform.forward * Time.deltaTime * speed;
            }
            else if (!GetComponentInChildren<EnemyWeapon>().weaponTrigerInrange)
            {
                transform.position += transform.forward * Time.deltaTime * speed / 2;
            }



            Vector3 direction = player.transform.position - transform.position;
            direction.Normalize();
            float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;


            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(Vector3.up * angle), rotationSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, player.transform.position) > 50f)
            {
                playerFound = false;
            }
        }
        else if (boxFound == true)
        {

            if (boxTransform == null)
            {
                boxFound = false;
                GetComponentInChildren<EnemyWeapon>().weaponTrigerInrange = false;
                return;
            }

            if (Vector3.Distance(transform.position, boxTransform.position) > 30f)
            {
                transform.position += transform.forward * Time.deltaTime * speed;
            }
            else if (!GetComponentInChildren<EnemyWeapon>().weaponTrigerInrange)
            {
                transform.position += transform.forward * Time.deltaTime * speed / 2;
            }



            Vector3 direction = boxTransform.position - transform.position;
            direction.Normalize();
            float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;


            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(Vector3.up * angle), rotationSpeed * Time.deltaTime);
           

        }
        else
        {
            if(destinationSet == false)
            {
               destination = setDestination();
               destinationSet = true;
            }
            else if(Vector3.Distance(transform.position, destination)<10f)
            {
                destinationSet = false;
                return;
            }

            transform.position += transform.forward * Time.deltaTime * speed;
            

            Vector3 direction = destination - transform.position;
            direction.Normalize();
            float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(Vector3.up * angle), rotationSpeed * Time.deltaTime);

        }


        if (rigid.velocity.magnitude > 2f)
        {
            rigid.velocity = rigid.velocity * 0.95f;
        }
        else
        {
            rigid.velocity = Vector3.zero;
        }

    }

    private Vector3 setDestination()
    {
        int randomX = Random.Range(-248, 248);
        int randomZ = Random.Range(-248, 248);
        Vector3 randomPos = new Vector3(randomX, 0.68f, randomZ);
        return randomPos;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "PlayerCannonBall")
        {
            health -= other.gameObject.GetComponent<CannonBall>().damage;
            Destroy(other.gameObject);
            if (health <= 0)
            {
                UIManager.instance.addPoints(score);
                GameManager.instance.startEnemyEnum();
                Destroy(this.gameObject);
            }

        }
        else if (other.gameObject.tag == "Player")
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

    public void enemyLevelArrange(int level)
    {
        if (level == 0)
        {
            gameObject.transform.localScale = new Vector3(1, 1, 1);
            maxHealth = 10;
            health = maxHealth;
            score = 20;
            cannonDamage = 1;
            cannonSpeed = 1f;
        }
        else if (level == 1)
        {
            gameObject.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
            maxHealth = 30;
            health = maxHealth;
            score = 50;
            cannonDamage = 4;
            cannonSpeed = 1.2f;
        }
        else if (level == 2)
        {
            gameObject.transform.localScale = new Vector3(1.4f, 1.4f, 1.4f);
            maxHealth = 75;
            health = maxHealth;
            score = 150;
            cannonDamage = 10;
            cannonSpeed = 1.4f;
        }
        else if (level == 3)
        {
            gameObject.transform.localScale = new Vector3(1.7f, 1.7f, 1.7f);
            maxHealth = 150;
            health = maxHealth;
            score = 300;
            cannonDamage = 20;
            cannonSpeed = 1.7f;
        }
        else if (level == 4)
        {
            gameObject.transform.localScale = new Vector3(2.0f, 2.0f, 2.0f);
            maxHealth = 500;
            health = maxHealth;
            score = 100;
            cannonDamage = 50;
            cannonSpeed = 2.0f;
        }
    }
}
