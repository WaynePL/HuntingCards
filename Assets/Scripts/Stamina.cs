using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Stamina : MonoBehaviour
{
    public int curStamina = 100;
    public int maxStamina = 100;
    public StaminaBar staminaBar;
    void Start()
    {

    }
    void Update()
    {
        
    }
    public void Tire(int stamina)
    {
        curStamina -= stamina;
        staminaBar.SetStamina(curStamina);
    }
    public void Rest(int rest)
    {
        curStamina = (curStamina + rest > maxStamina) ? maxStamina : curStamina + rest;
        staminaBar.SetStamina(curStamina);
    }
}