using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    public Slider healthBar;
    public Health health;
    private void Start()
    {
        healthBar = GetComponent<Slider>();
        healthBar.maxValue = health.maxHealth;
        healthBar.value = health.maxHealth;
    }
    public void SetHealth(int hp)
    {
        healthBar.value = hp;
    }
}