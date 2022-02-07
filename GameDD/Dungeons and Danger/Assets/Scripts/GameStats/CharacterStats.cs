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

    //Core stats


    //GUI/Effects
    public HealthBar healthBar;
    public GameObject bloodpf;

    // Start is called before the first frame update
    void Start()
    {
        //int maxHpInt = maxHp as int;
        int maxHpInt = Convert.ToInt32(maxHp);
        healthBar.SetMaxHealth(maxHpInt);//This will need to be placed anywhere max health gets altered
    }

    //what to do when character takes damage
    public void TakeDamage(float dmg, int turn, string name, bool local)
    {
        Debug.Log("Ow! My name is "+name);
        //Make Blood
        Vector3 objectPOS = transform.position;
        GameObject newBlood = Instantiate(bloodpf, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
        //Effect stats
        hp -= dmg;
        //GUI
        int hpInt = Convert.ToInt32(hp);
        healthBar.SetHealth(hpInt);//Update health bar current value
        //Send to server
        if(local == true){
        GameObject findGOD; findGOD = GameObject.Find("GOD");
        findGOD.GetComponent<ServerTalker>().tDAction = 1; //What kind of attack was used on me; 1 = Melee, 2 = Spell, 3 = Self Skill, 4 = Self Spell 
        findGOD.GetComponent<ServerTalker>().tDActionID = 1; //The Id for the action in a list
        findGOD.GetComponent<ServerTalker>().tDTargetName = name; //My name (target of attack)
        UpdateServer();
        Debug.Log("Server Update Sent!");}
    }

    //This player is defeated
    void Die()
    {
        BudgeIt budgescript = GetComponent<BudgeIt>();
        budgescript.dead = true;
        UpdateServer();
    }

    //Update the server with information
    void UpdateServer()
    {
        GameObject sTalk; sTalk = GameObject.Find("GOD");
        sTalk.GetComponent<ServerTalker>().ProcessPost();
    }

    // Update is called once per frame
    void Update()
    {
        if(hp <= 0)
        {
            Die();
        }
    }

    void SetClassWarrior()
    {
        //intelleigence = 1;

    }
}
