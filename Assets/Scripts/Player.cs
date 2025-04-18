using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    public PlayerStats playerStats = new PlayerStats();
    public HealthBar playerHealthBar;
    public Health playerHealth;
    public StaminaBar playerStaminaBar;
    public Stamina playerStamina;
    public Card currentCard;
    public int currentPosition;

    // Start is called before the first frame update
    void Start()
    {
        playerHealth.maxHealth = playerStats.health;
        playerHealth.curHealth = playerHealth.maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    public void DamagePlayer(int damage)
    {
        playerHealth.TakeDamage(damage);
    }
    public void HealPlayer(int heal)
    {
        playerHealth.Heal(heal);
    }

    public void TirePlayer(int stamina)
    {
        playerStamina.Tire(stamina);
    }

    public void RestPlayer(int rest)
    {
        playerStamina.Rest(rest);
    }
    
}

public class PlayerStats
{
    public int health = 100;
    public int stamina = 6;
    public Status status = Status.Normal;
    public State state = State.Idle;

}

public enum State
{
    Idle,
    PreAttack,
    Attack,
    PostAttack
}

public enum Status
{
    Normal,
    Poisoned,
    Dazed
}