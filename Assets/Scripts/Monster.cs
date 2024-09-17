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
    // Start is called before the first frame update
    void Start()
    {
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

    public void MoveMonster(int move)
    {
        int intendedposition = currentPosition + move;
        if (intendedposition >= 0 && intendedposition < 5)
        {
            currentPosition = intendedposition;
        }
        else if (intendedposition < 0)
        {
            currentPosition = 0;
        }
        else
        {
            currentPosition = 4;
        }
        transform.position = new Vector3(currentPosition * -6.5f, 6, -20);
    }
}

public enum Slot
{
    Slot1 = 1,
    Slot2 = 2,
    Slot3 = 3,
    Slot4 = 4,
    Slot5 = 5
}

public class MonsterStats
{
    public int health = 1000;
    public int maxHealth = 1000;
    public int damage = 10;
} 

