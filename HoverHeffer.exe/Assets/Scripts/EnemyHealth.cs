using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public static EnemyHealth instance;
    public GameObject Alien;
    public Slider healthBar;
    public int currentHealth;
    public int maxHealth;
    private void Awake()
    {
        instance = this;
        //Finding the enemy health bar in the UI to apply health reduction when hit
        healthBar = GameObject.FindGameObjectWithTag("healthbar").GetComponent<Slider>();

    }

    private void Start()
    {
        currentHealth = maxHealth;
    }
    /// <summary>
    /// Apply damage if enemy collides with a bullet
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.value = currentHealth;
        if(currentHealth <= 0)
        {
            GameManager.instance.AlienLost();
            Alien.SetActive(false);
        }
    }

    
}
