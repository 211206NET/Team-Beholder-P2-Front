using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class CharacterStats : MonoBehaviour
{
    //Stats like this must be floating point internally but rounded up for the UI

    public int coolJob = 0;

    public int sendRoll = 6;//Assume one six sided dice for now

    public int classID;
    public float maxHp = 1000; //Final Value Stat
    public float hp = 1000; //Final Value Stat
    public int str; //Final Value Stat
    public int AC;
    public int dmg; //Calculated in take damage method
    public int attackRoll = 20;

    public string name = "";


    //Inventory
    public int hpPotions = 20;
    public int wepType = 1; //1 = Sword, 2 = Sword and Shield, 3 = Staff, 4 = Bow
    //SetClassBarbarian(maxHp, hp, strength, constitution, armorClass); 
    //Core stats

    //Player Blessing
    private bool _canBless = true;

    //GUI/Effects
    public HealthBar healthBar;
    public GameObject bloodpf;
    public GameObject missTextObj;

    private float _counttohp = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        
        _counttohp = 0.4f;
        GetJob();
    }


    void GetJob()
    {
        hpPotions = 20;
        //Set stats
        ClassStats classStatsScript = GetComponent<ClassStats>();
        //System.Random rand = new System.Random();
        int number = UnityEngine.Random.Range(1, 6);//rand.Next(1, 6);
        switch (number) 
        {
            case 1:
                classStatsScript.SetClassBarbarian(classID, maxHp, hp, str, AC, dmg, wepType);
                break;
            case 2:
                classStatsScript.SetClassCleric(classID, maxHp, hp, str, AC, dmg, wepType);
                break;
            case 3:
                classStatsScript.SetClassPaladin(classID, maxHp, hp, str, AC, dmg, wepType);
                break;
            case 4:
                classStatsScript.SetClassRanger(classID, maxHp, hp, str, AC, dmg, wepType);
                break;
            case 5:
                classStatsScript.SetClassWizard(classID, maxHp, hp, str, AC, dmg, wepType);
                break;
            default:
                //More classes in the future!
                Debug.LogError("No class was selected!");
                break;
        }

        //Set Weapon in main script
        SetWeapon(wepType);

        HPBar();
        //Fix for init hp bar not showing full
        HealPotion();
        HealPotion();
        //healthBar.doonceish=true;

    }

    void SetWeapon(int wt)
    {
        BudgeIt budgescript = GetComponent<BudgeIt>();
        budgescript.weaponType = wt;
        budgescript.SetWeaponArt();
    }

    public int GetRoll(int dmg) {
        int roll;
        //System.Random rand = new System.Random();
        roll = UnityEngine.Random.Range(1, dmg+1);//rand.Next(1, dmg + 1);
        Debug.Log("Roll is: " + roll);
        return roll;
    }

    //what to do when character takes damage
    public void TakeDamage(int getStr, int turn, string namer, bool local, int dmg)
    {
        Debug.Log("Ow! My name is "+namer);
        //Make Blood
        Vector3 objectPOS = transform.position;
        GameObject newBlood = Instantiate(bloodpf, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
        //Effect stats
        int storeDmg; //To hold value for us
        storeDmg = getStr + GetRoll(dmg); //Store temp value from random roll
        GameObject findGOD; findGOD = GameObject.Find("GOD"); //Get GOD object
        findGOD.GetComponent<ServerTalker>().tDFinalDamage = storeDmg; //Set God's var for damage
        findGOD.GetComponent<ServerTalker>().ProcessPost(); //Update server to have damage
        hp -= storeDmg;
        Debug.Log("You hit for " + storeDmg);
        HPBar();
        //Send to server
        // if(local == true){
        // findGOD.GetComponent<ServerTalker>().tDAction = 1; //What kind of attack was used on me; 1 = Melee, 2 = Spell, 3 = Self Skill, 4 = Self Spell 
        // findGOD.GetComponent<ServerTalker>().tDActionID = 1; //The Id for the action in a list
        // findGOD.GetComponent<ServerTalker>().tDTargetName = namer; //My name (target of attack)
        // UpdateServer();
        // }
        //Debug.Log("Server Update Sent!");
    }

    public void Miss() {
        Debug.Log("You missed!!!");
        float randpos = -0.20f;
        randpos = UnityEngine.Random.Range(-0.10f, 0.10f);
        Instantiate(missTextObj, new Vector2(transform.position.x+randpos, transform.position.y+0.24f+randpos), Quaternion.identity);
    }

    //Heal with potion
    public void HealPotion()
    {
        //System.Random rand = new System.Random();
        hp += UnityEngine.Random.Range(24, 48);//rand.Next(24, 48);
        if(hp > maxHp){hp = maxHp;}
        HPBar();
    }

    //Heal with potion
    public void Heal(int power)
    {
        //System.Random rand = new System.Random();
        hp += UnityEngine.Random.Range(power, power+10);//rand.Next(24, 48);
        if(hp > maxHp){hp = maxHp;}
        HPBar();
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

        //Debug.Log(name+"'s HP is: "+hp+"/"+maxHp+"!");
    }

    //This player is defeated
    void Die()
    {
        BudgeIt budgescript = GetComponent<BudgeIt>();
        budgescript.dead = true;
        if(budgescript.myTurn == 1){
        TurnController.PlayerDead = true;}
        //UpdateServer();
    }

    //Update the server with information
    void UpdateServer()
    {
        GameObject sTalk; sTalk = GameObject.Find("GOD");
        sTalk.GetComponent<ServerTalker>().ProcessFinalPost();
    }

    // Update is called once per frame
    void Update()
    {

        //Drink potion
        // if(hpPotions > 0)
        // {
        //     if(Input.GetKeyDown("h"))
        //     {
        //         BudgeIt budgescript = GetComponent<BudgeIt>();
        //         if(budgescript.myTurn == TurnController.Turn){
        //         hpPotions -= 1;
        //         HealPotion();}
        //     }
        // }

        
        //Cheat for player, blessing of life
        if(_canBless == true ){
        BudgeIt budgescript = GetComponent<BudgeIt>();
        if(budgescript.myTurn == 1)
        {
            maxHp += 100;
            //Debug.Log("maxHP: " +maxHp);
            hp = maxHp;
            //Debug.Log("hp: " +hp);
            HealPotion();
            HealPotion();
        }_canBless = false;} 

        //All heal at start and set HP bar FFS
        if(_counttohp > 0)
        {
            _counttohp -= Time.deltaTime;
            if(_counttohp < 1)
            {
                BudgeIt budgescript = GetComponent<BudgeIt>();
                budgescript.SetVis();
                HealPotion();
                HealPotion();
                GetJob();
            }
        }

        if(coolJob == 0)
        {
            BudgeIt budgescript = GetComponent<BudgeIt>();
            budgescript.SetVis();
            HealPotion();
            HealPotion();
            GetJob();
        }

        //Die
        if(hp <= 0 || hp > 999)
        {
            Die();
        }
    }
    
}