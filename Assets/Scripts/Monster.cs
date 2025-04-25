using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{


    public HealthBar healthBar;
    public MonsterStats monsterStats = new();
    public Health monsterHealth;
    public int currentPosition;
    public NextTurn nextTurn;
    public Deck deck;
    public int currentArea = 1;
    public List<Attack> attacks = new();
    public GameObject attackText;
    public Attack attack;
    // Start is called before the first frame update
    void Start()
    {
        currentPosition = 2;
        monsterHealth.maxHealth = monsterStats.maxHealth;
        monsterHealth.curHealth = monsterHealth.maxHealth;
        InitializeMonsterAttackText();
        SetMonsterAttack(0);
        UpdateMonsterAttack(0);
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
    private void InitializeMonsterAttackText()
    {
        attackText = new GameObject("Attack");
        attackText.transform.parent = gameObject.transform;
        attackText.transform.position = new Vector3(transform.position.x + 5, transform.position.y + 6, transform.position.z);
        attackText.AddComponent<MeshRenderer>();
        attackText.AddComponent<TextMesh>();
        attackText.GetComponent<TextMesh>().color = Color.black;
        attackText.GetComponent<TextMesh>().fontSize = 30;
        attackText.transform.localScale *= 0.4f;
    }
    public void SetMonsterAttack(int currentTurn)
    {
        attack = attacks[Random.Range(0, attacks.Count)];
        attack.startTurn = currentTurn;
        
    }

    public void UpdateMonsterAttack(int currentTurn)
    {
        attackText.GetComponent<TextMesh>().text = "Attack: " + attack.attackName + "\nDamage: " + attack.damage + "\nTurns: " + (attack.turns + attack.startTurn - currentTurn) + " out of " + attack.turns;

    }
}

[System.Serializable]
public class Attack
{
    public string attackName;
    public int damage;
    public int turns;
    public int startTurn;
}

public class MonsterStats
{
    public int health = 1000;
    public int maxHealth = 1000;
    public int damage = 10;
} 

