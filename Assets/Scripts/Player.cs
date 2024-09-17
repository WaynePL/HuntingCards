using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerStats playerStats = new PlayerStats();
    public HealthBar playerHealthBar;
    public Card currentCard;
    public Health playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        playerHealth.maxHealth = playerStats.health;
        playerHealth.curHealth = playerHealth.maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
         
    }

    public void DamagePlayer(int damage)
    {
        playerHealth.TakeDamage(damage);
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