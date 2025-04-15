using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Stamina : MonoBehaviour
{
    public int curStamina = 0;
    public int maxStamina = 100;
    public StaminaBar healthBar;
    void Start()
    {

    }
    void Update()
    {
        
    }
    public void Tire(int damage)
    {
        curStamina -= damage;
        healthBar.SetStamina(curStamina);
    }
    public void Rest(int rest)
    {
        curStamina = (curStamina + rest > maxStamina) ? maxStamina : curStamina + rest;
        healthBar.SetStamina(curStamina);
    }
}