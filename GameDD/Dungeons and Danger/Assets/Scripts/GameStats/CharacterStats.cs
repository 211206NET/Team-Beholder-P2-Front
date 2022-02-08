using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//using static ClassStats;


public class CharacterStats : MonoBehaviour
{
    

    //Stats like this must be floating point internally but rounded up for the UI
    public int sendRoll = 6;//Assume one six sided dice for now

    public int classID;
<<<<<<< HEAD
    public float maxHp;
    public float hp;
    public int str;
    public int AC;
    public int dmg;
=======
    public float maxHp; //Final Value Stat
    public float hp; //Final Value Stat
    public int str; //Final Value Stat
    public int AC;
    public float dmg; //Calculated in take damage method
>>>>>>> 191d71d3ae59e95bc38a703be84d0b50e2b6c414

    //SetClassBarbarian(maxHp, hp, strength, constitution, armorClass); 
    //Core stats


    //GUI/Effects
    public HealthBar healthBar;
    public GameObject bloodpf;

    // Start is called before the first frame update
    void Start()
    {
        ClassStats classStatsScript = GetComponent<ClassStats>();
        System.Random rand = new System.Random();
        //int number = rand.Next(1, 6);
        int number = 3;
        switch (number) 
        {
            case 1:
<<<<<<< HEAD
                classStatsScript.SetClassBarbarian(classID, maxHp, hp, str, AC, dmg);
                //int maxHpInt = maxHp as int;
                maxHpInt = Convert.ToInt32(maxHp);
                //This will need to be placed anywhere max health gets altered
                healthBar.SetMaxHealth(maxHpInt);
                break;
            case 2:
                classStatsScript.SetClassCleric(classID, maxHp, hp, str, AC, dmg);
                //int maxHpInt = maxHp as int;
                maxHpInt = Convert.ToInt32(maxHp);
                //This will need to be placed anywhere max health gets altered
                healthBar.SetMaxHealth(maxHpInt);
                break;
            case 3:
                classStatsScript.SetClassPaladin(classID, maxHp, hp, str, AC, dmg);
                //int maxHpInt = maxHp as int;
                maxHpInt = Convert.ToInt32(maxHp);
                //This will need to be placed anywhere max health gets altered
                healthBar.SetMaxHealth(maxHpInt);
                break;
            case 4:
                classStatsScript.SetClassRanger(classID, maxHp, hp, str, AC, dmg);
                //int maxHpInt = maxHp as int;
                maxHpInt = Convert.ToInt32(maxHp);
                //This will need to be placed anywhere max health gets altered
                healthBar.SetMaxHealth(maxHpInt);
                break;
            case 5:
                classStatsScript.SetClassWizard(classID, maxHp, hp, str, AC, dmg);
                //int maxHpInt = maxHp as int;
                maxHpInt = Convert.ToInt32(maxHp);
                //This will need to be placed anywhere max health gets altered
                healthBar.SetMaxHealth(maxHpInt);
=======
                classStatsScript.SetClassBarbarian(classID, maxHp, hp, str, AC);
                break;
            case 2:
                classStatsScript.SetClassCleric(classID, maxHp, hp, str, AC);
                break;
            case 3:
                classStatsScript.SetClassPaladin(classID, maxHp, hp, str, AC);
                break;
            case 4:
                classStatsScript.SetClassRanger(classID, maxHp, hp, str, AC);
                break;
            case 5:
                classStatsScript.SetClassWizard(classID, maxHp, hp, str, AC);
>>>>>>> 191d71d3ae59e95bc38a703be84d0b50e2b6c414
                break;
            default:
                //More classes in the future!
                break;
        }

        HPBar();
        //healthBar.doonceish=true;
    }

    //what to do when character takes damage
<<<<<<< HEAD
    public void TakeDamage(int str, int roll, int turn, string name, bool local)
    {
        System.Random rand = new System.Random();
        int damage = str + rand.Next(1, roll);
        Debug.Log("Ow! My name is "+name);
=======
    public void TakeDamage(float getStr, int turn, string name, bool local, int roll)
    {
        System.Random rand = new System.Random();
        //Debug.Log("Ow! My name is "+name);
>>>>>>> 191d71d3ae59e95bc38a703be84d0b50e2b6c414
        //Make Blood
        Vector3 objectPOS = transform.position;
        GameObject newBlood = Instantiate(bloodpf, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
        //Effect stats
<<<<<<< HEAD
        hp -= damage;
        //GUI
        int hpInt = Convert.ToInt32(hp);
        healthBar.SetHealth(hpInt);//Update health bar current value
=======
        hp -= getStr + rand.Next(1, roll);
        HPBar();
>>>>>>> 191d71d3ae59e95bc38a703be84d0b50e2b6c414
        //Send to server
        if(local == true){
        GameObject findGOD; findGOD = GameObject.Find("GOD");
        findGOD.GetComponent<ServerTalker>().tDAction = 1; //What kind of attack was used on me; 1 = Melee, 2 = Spell, 3 = Self Skill, 4 = Self Spell 
        findGOD.GetComponent<ServerTalker>().tDActionID = 1; //The Id for the action in a list
        findGOD.GetComponent<ServerTalker>().tDTargetName = name; //My name (target of attack)
        UpdateServer();
        }
        //Debug.Log("Server Update Sent!");
    }

    //Update HPBar
    public void HPBar()
    {
        //GUI
        //This will need to be placed anywhere max health gets altered
        int hpInt = Convert.ToInt32(hp);
        healthBar.SetHealth(hpInt);//Update health bar current value
        int maxHpInt = Convert.ToInt32(maxHp);
        healthBar.SetMaxHealth(maxHpInt);

        Debug.Log("My HP is: "+hp+"/"+maxHp+"!");
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
    
}
