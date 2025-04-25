using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    public Slider healthBar;
    public Health health;
    public GameObject healthText;
    private void Start()
    {
        healthBar = GetComponent<Slider>();
        healthBar.maxValue = health.maxHealth;
        healthBar.value = health.maxHealth;
        healthText = new GameObject("HealthText");
        healthText.transform.parent = gameObject.transform;
        healthText.AddComponent<MeshRenderer>();
        healthText.AddComponent<TextMesh>();
        healthText.GetComponent<TextMesh>().color = Color.black;
        healthText.GetComponent<TextMesh>().fontSize = 30;
        healthText.transform.localScale *= 0.4f;
        healthText.transform.position = new Vector3(transform.position.x + 3.5f, transform.position.y + 0.5f, transform.position.z);
    }
    public void SetHealth(int hp)
    {
        healthBar.value = hp;
    }
    public void OnMouseEnter()
    {
        healthText.GetComponent<TextMesh>().text = "Health: " + healthBar.value.ToString();
    }
    public void OnMouseExit()
    {
        healthText.GetComponent<TextMesh>().text = "";
    }
}