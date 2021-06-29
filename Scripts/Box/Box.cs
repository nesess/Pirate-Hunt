using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    private bool highest = false;
    
    private int score;
    private int maxHealth;
    private int health;

    

    // Update is called once per frame
    private void FixedUpdate()
    {
        
    
        if(highest)
        {
            transform.position += new Vector3(0, -0.1f, 0);
            if (transform.position.y <= 0.5f)
            {
                highest = false;
            }
        }
        else if(!highest)
        {
            transform.position += new Vector3(0, 0.1f, 0);
            if (transform.position.y >= 2.5f)
            {
                highest = true;
            }
        }
        
    }

    private void Start()
    {
        boxLevelArrange(Random.Range(0,3));
    }

    public void boxLevelArrange(int level)
    {
        if(level == 0)
        {
            gameObject.transform.localScale = new Vector3(6, 6, 6);
            maxHealth = 5;
            health = maxHealth;
            score = 10;
        }
        else if (level == 1)
        {
            gameObject.transform.localScale = new Vector3(8, 8, 8);
            maxHealth = 10;
            health = maxHealth;
            score = 20;
        }
        else if (level == 2)
        {
            gameObject.transform.localScale = new Vector3(10, 10, 10);
            maxHealth = 30;
            health = maxHealth;
            score = 50;
        }
        else if (level == 3)
        {
            gameObject.transform.localScale = new Vector3(13, 13, 13);
            maxHealth = 50;
            health = maxHealth;
            score = 75;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "PlayerCannonBall")
        {
            health -= other.gameObject.GetComponent<CannonBall>().damage;
            Destroy(other.gameObject);
            if(health <= 0)
            {
                UIManager.instance.addPoints(score);
                GameManager.instance.startBoxEnum();
                Destroy(this.gameObject);
            }
            
        }
        if (other.gameObject.tag == "EnemyCannonBall")
        {
            health -= other.gameObject.GetComponent<CannonBall>().damage;
            Destroy(other.gameObject);
            if (health <= 0)
            {
                GameManager.instance.startBoxEnum();
                Destroy(this.gameObject);
            }

        }
    }


}
