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
        healthBar = GameObject.FindGameObjectWithTag("healthbar").GetComponent<Slider>();

    }

    private void Start()
    {
        currentHealth = maxHealth;
    }

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
