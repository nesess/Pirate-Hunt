using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text upgradePointsText;
    private int upgradePoints = 0;
    private int maxUpgradePoints = 200;

    [SerializeField]
    private Text maxHealthText;
    [SerializeField]
    private GameObject maxHealthButton;
    private int maxHealthLevel = 0;
    private int maxHealthCost = 10;

    [SerializeField]
    private Text speedText;
    [SerializeField]
    private GameObject speedButton;
    private int speedLevel = 0;
    private int speedCost = 10;

    [SerializeField]
    private Text damageText;
    [SerializeField]
    private GameObject damageButton;
    private int damageLevel = 0;
    private int damageCost = 10 ;

    [SerializeField]
    private Text cannonRangeText;
    [SerializeField]
    private GameObject cannonRangeButton;
    private int cannonRangeLevel = 0;
    private int cannonRangeCost = 10;


    [SerializeField]
    private Image playerHealthBar;
    [SerializeField]
    private Text playerHealthText;


    [SerializeField]
    private GameObject upgradeShipButton;
    private string currentShip = "small";
    [SerializeField]
    private GameObject smallShip;
    [SerializeField]
    private GameObject medShip;
    [SerializeField]
    private GameObject largeShip;

    [SerializeField]
    private GameObject gameScreen;
    [SerializeField]
    private GameObject gameOverScreen;
    [SerializeField]
    private Text gameOverText;
    private bool playerDead = false;

    public static UIManager instance;

    private Player player;

    private void Awake()
    {
        if (UIManager.instance)
        {
            Destroy(base.gameObject);
        }
        else
        {
            UIManager.instance = this;
        }

        player = GameObject.FindObjectOfType<Player>();

    }

    private void Start()
    {
        refreshUpgradeTexts();
        refreshButtons();
        playerDamaged(0);
    }

    public void refreshPoints()
    {
        upgradePointsText.text = "Upgrade Points: " + upgradePoints + "/"+maxUpgradePoints;
        refreshButtons();
    }

    private void refreshButtons()
    {
        if(upgradePoints>=maxHealthCost && maxHealthLevel < 10)
        {
            maxHealthButton.SetActive(true);
        }
        else
        {
            maxHealthButton.SetActive(false);
        }

        if (upgradePoints >= speedCost && speedLevel < 10)
        {
            speedButton.SetActive(true);
        }
        else
        {
            speedButton.SetActive(false);
        }

        if (upgradePoints >= damageCost && damageLevel<10)
        {
            damageButton.SetActive(true);
        }
        else
        {
            damageButton.SetActive(false);
        }

        if (upgradePoints >= cannonRangeCost && cannonRangeLevel<10)
        {
            cannonRangeButton.SetActive(true);
        }
        else
        {
            cannonRangeButton.SetActive(false);
        }

        if (upgradePoints == maxUpgradePoints && currentShip != "large")
        {
            upgradeShipButton.SetActive(true);
        }
        else
        {
            upgradeShipButton.SetActive(false);
        }
        
    }

    private void refreshUpgradeTexts()
    {
        maxHealthText.text = "Max Health: " + maxHealthLevel+"/10\nCost: "+maxHealthCost+" Points";
        speedText.text = "Speed: " + speedLevel + "/10\nCost: " + speedCost + " Points";
        damageText.text = "Damage: " + damageLevel + "/10\nCost: " + damageCost + " Points";
        cannonRangeText.text = "Cannon Range: " + cannonRangeLevel + "/10\nCost: " + cannonRangeCost + " Points";
    }

    public void addPoints(int points)
    {
        if(points + upgradePoints >= maxUpgradePoints)
        {
            upgradePoints = maxUpgradePoints;
            
        }
        else
        {
            upgradePoints += points;
        }
        refreshPoints();
        refreshButtons();
    }

    public void maxHealthUpgrade()
    {
        if(upgradePoints>=maxHealthCost)
        {
            upgradePoints -= maxHealthCost;
            maxHealthCost += maxHealthCost;
            maxHealthLevel++;

            player.health += player.maxHealth;
            player.maxHealth += player.maxHealth;

            playerDamaged(0);
            refreshPoints();
            refreshUpgradeTexts();
        }
    }

    public void speedUpgrade()
    {
        if (upgradePoints >= speedCost )
        {
            upgradePoints -= speedCost;
            speedCost += speedCost;
            speedLevel++;

            player.speed += 3f;
            
            refreshPoints();
            refreshUpgradeTexts();
        }
    }

    public void damageUpgrade()
    {
        if (upgradePoints >= damageCost)
        {
            upgradePoints -= damageCost;
            damageCost += damageCost;
            damageLevel++;

            player.cannonDamage += player.cannonDamage;

            refreshPoints();
            refreshUpgradeTexts();
        }
    }

    public void cannonRangeUpgrade()
    {
        if (upgradePoints >= cannonRangeCost)
        {
            upgradePoints -= cannonRangeCost;
            cannonRangeCost += cannonRangeCost;
            cannonRangeLevel++;

            player.cannonRange += 0.2f;

            refreshPoints();
            refreshUpgradeTexts();
        }
    }


    public void playerDamaged(int damage)
    {
        if(damage < player.health)
        {
            player.health -= damage;
            playerHealthText.text = "Health: "+player.health+"/" + player.maxHealth;
            playerHealthBar.fillAmount = ((float)player.health)/((float)player.maxHealth);
        }
        else if(damage>=player.health && !playerDead )
        {
            playerDead = true;
            playerHealthText.text = "Health: 0/" + player.maxHealth;
            playerHealthBar.fillAmount = 0;
            smallShip.SetActive(false);
            medShip.SetActive(false);
            largeShip.SetActive(false);
            gameScreen.SetActive(false);
            gameOverScreen.SetActive(true);
            gameOverText.text = "SURVIVED "+Time.realtimeSinceStartup+" SECONDS";
        }
    }

    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void upgradeShip()
    {
        if(upgradePoints == maxUpgradePoints)
        {
            if(currentShip == "small")
            {
                maxHealthLevel = 0;
                maxHealthCost = 200;
                speedLevel = 0;
                speedCost = 200;
                damageLevel = 0;
                damageCost = 200;
                cannonRangeLevel = 0;
                cannonRangeCost = 200;

                player.maxHealth = 200;
                player.health = player.maxHealth;
                player.speed = 12;
                player.rotationSpeed = 1.1f;
                player.cannonDamage = 20;
                player.cannonRange = 2f;

                maxUpgradePoints = 1000;

                upgradePoints = 0;

                refreshUpgradeTexts();
                refreshButtons();
                playerDamaged(0);

                smallShip.SetActive(false);
                medShip.SetActive(true);
                player.GetComponent<CapsuleCollider>().radius = 4;

                currentShip = "med";

            }
            else if (currentShip == "med")
            {
                maxHealthLevel = 0;
                maxHealthCost = 1000;
                speedLevel = 0;
                speedCost = 1000;
                damageLevel = 0;
                damageCost = 1000;
                cannonRangeLevel = 0;
                cannonRangeCost = 1000;

                player.speed = 10;
                player.rotationSpeed = 1f;
                player.maxHealth = 1000;
                player.health = player.maxHealth;
                player.cannonDamage = 100;
                player.cannonRange = 3f;

                maxUpgradePoints = 999999;

                upgradePoints = 0;

                refreshUpgradeTexts();
                refreshButtons();
                playerDamaged(0);

                
                medShip.SetActive(false);
                largeShip.SetActive(true);
                player.GetComponent<CapsuleCollider>().radius = 5;

                currentShip = "large";
            }
            refreshPoints();
        }
    }

}
