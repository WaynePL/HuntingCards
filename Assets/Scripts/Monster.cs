using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{


    public HealthBar healthBar;
    public MonsterStats monsterStats = new MonsterStats();
    public Health monsterHealth;
    public Transform[] moveSlots;
    public int currentPosition;
    public NextTurn nextTurn;
    public Deck deck;
    public int currentArea = 1;
    // Start is called before the first frame update
    void Start()
    {
        currentPosition = 2;
        monsterHealth.maxHealth = monsterStats.maxHealth;
        monsterHealth.curHealth = monsterHealth.maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DamageMonster(int damage)
    {
        monsterHealth.TakeDamage(damage);

    }
    public void runAway()
    {
        
        currentArea = Random.Range(1, 5);
        
    }
}

public class MonsterStats
{
    public int health = 1000;
    public int maxHealth = 1000;
    public int damage = 10;
} 

