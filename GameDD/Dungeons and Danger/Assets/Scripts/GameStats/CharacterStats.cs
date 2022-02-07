using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



public class CharacterStats : MonoBehaviour
{
    

    //Stats like this must be floating point internally but rounded up for the UI
    public float maxHp;
    public float hp;
    public int strength;
    public int constitution;
    public int armorClass;
    public float damage;

    //SetClassBarbarian(maxHp, hp, strength, constitution, armorClass); 
    //Core stats


    //GUI/Effects
    public HealthBar healthBar;
    public GameObject bloodpf;

    // Start is called before the first frame update
    void Start()
    {
        ClassStats classStatsScript = GetComponent<ClassStats>();
        Random rand = new Random();
        int number = rand.Next(1, 100);
        classStatsScript.SetClassBarbarian(maxHp, hp, str, con, AC);
    }

    //what to do when character takes damage
    public void TakeDamage(float dmg)
    {
        //Make Blood
        Vector3 objectPOS = transform.position;
        GameObject newBlood = Instantiate(bloodpf, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
        //Effect stats
        hp -= dmg;
        //GUI
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
