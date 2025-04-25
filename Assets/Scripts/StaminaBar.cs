using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StaminaBar : MonoBehaviour
{
    public Slider staminaBar;
    public Stamina stamina;
    public GameObject staminaText;
    private void Start()
    {
        staminaBar = GetComponent<Slider>();
        staminaBar.maxValue = stamina.maxStamina;
        staminaBar.value = stamina.maxStamina;
        staminaText = new GameObject("StaminaText");
        staminaText.transform.parent = gameObject.transform;
        staminaText.AddComponent<MeshRenderer>();
        staminaText.AddComponent<TextMesh>();
        staminaText.GetComponent<TextMesh>().color = Color.black;
        staminaText.GetComponent<TextMesh>().fontSize = 30;
        staminaText.transform.localScale *= 0.4f;
        staminaText.transform.position = new Vector3(transform.position.x + 3.5f, transform.position.y + 0.5f, transform.position.z);
    }
    public void SetStamina(int sp)
    {
        staminaBar.value = sp;
    }
    public void OnMouseEnter()
    {
        staminaText.GetComponent<TextMesh>().text = "Stamina: " + staminaBar.value.ToString();
    }

    public void OnMouseExit()
    {
        staminaText.GetComponent<TextMesh>().text = "";
    }
}