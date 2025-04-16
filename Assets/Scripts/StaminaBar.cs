using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StaminaBar : MonoBehaviour
{
    public Slider staminaBar;
    public Stamina stamina;
    private void Start()
    {
        staminaBar = GetComponent<Slider>();
        staminaBar.maxValue = stamina.maxStamina;
        staminaBar.value = stamina.maxStamina;
    }
    public void SetStamina(int sp)
    {
        staminaBar.value = sp;
    }
}