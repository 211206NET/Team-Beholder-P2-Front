using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CharacterStats : MonoBehaviour
{
    //Stats like this must be floating point internally but rounded up for the UI
    public float hp = 10.0f;
    public float maxHp = 10.0f;
    public float damage = 1.0f;

    //GUI
    public HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        //int maxHpInt = maxHp as int;
        int maxHpInt = Convert.ToInt32(maxHp);
        healthBar.SetMaxHealth(maxHpInt);//This will need to be placed anywhere max health gets altered
    }

    //what to do when character takes damage
    public void TakeDamage()
    {
        //int hpInt = hp as int;
        int hpInt = Convert.ToInt32(hp);
        healthBar.SetHealth(hpInt);//Update health bar current value
    }

    //This player is defeated
    void Die()
    {
        BudgeIt budgescript = GetComponent<BudgeIt>();
        budgescript.dead = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(hp <= 0)
        {
            Die();
        }
    }
}
