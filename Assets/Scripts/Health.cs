using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Health : MonoBehaviour
{
    public int curHealth = 0;
    public int maxHealth = 100;
    public HealthBar healthBar;
    void Start()
    {

    }
    void Update()
    {
        
    }
    public void TakeDamage(int damage)
    {
        curHealth -= damage;
        healthBar.SetHealth(curHealth);
    }
    public void Heal(int heal)
    {
        curHealth = (curHealth + heal > maxHealth) ? maxHealth : curHealth + heal;
        healthBar.SetHealth(curHealth);
    }
}