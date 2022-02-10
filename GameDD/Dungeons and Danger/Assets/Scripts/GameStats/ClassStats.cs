using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ClassStats : MonoBehaviour
{
    CharacterStats sendBackStats;

    // Start is called before the first frame update
    void Start()
    {
        sendBackStats = GetComponent<CharacterStats>();
    }

    /*We just need to send back values, so these methods don't even need parameters.
    However, in the interest of future scalability (our side hobby), we might want to do 
    calculations in these methods, and they will need vars to use in those, so let's keep 
    them. After setting the vars I just added code to send back the values to CharacterStats. 
    Note: this will only send back stats for one class for one player object each.*/
    public void SetClassBarbarian(int classID, float maxHp, float hp, int str, int AC, int dmg, int wepT)
    {
        classID = 1;
        maxHp = 41.0f;
        hp = maxHp;
        str = 3;
        AC = 10;//15
        dmg = 12;
        wepT = 1;

        sendBackStats.classID = classID;
        sendBackStats.maxHp = maxHp;
        sendBackStats.hp = maxHp;
        sendBackStats.str = str;
        sendBackStats.AC = AC;
        sendBackStats.dmg = dmg;
        sendBackStats.wepType = wepT; //1 = Sword, 2 = Sword and Shield, 3 = Staff, 4 = Bow
        sendBackStats.HPBar();
    }


    public void SetClassCleric(int classID, float maxHp, float hp, int str, int AC, int dmg, int wepT)
    {
        classID = 2;
        maxHp = 35.0f;
        hp = maxHp;
        str = 2;
        AC = 10; //15
        dmg = 6;
        wepT = 5;

        sendBackStats.classID = classID;
        sendBackStats.maxHp = maxHp;
        sendBackStats.hp = maxHp;
        sendBackStats.str = str;
        sendBackStats.AC = AC;
        sendBackStats.dmg = dmg;
        sendBackStats.wepType = wepT; //1 = Sword, 2 = Sword and Shield, 3 = Staff, 4 = Bow
        sendBackStats.HPBar();
    }

    public void SetClassPaladin(int classID, float maxHp, float hp, int str, int AC, int dmg, int wepT)
    {
        classID = 3;
        maxHp = 40.0f;
        hp = maxHp;
        str = 3;
        AC = 13; //18
        dmg = 8;
        wepT = 2;

        sendBackStats.classID = classID;
        sendBackStats.maxHp = maxHp;
        sendBackStats.hp = maxHp;
        sendBackStats.str = str;
        sendBackStats.AC = AC;
        sendBackStats.dmg = dmg;
        sendBackStats.wepType = wepT; //1 = Sword, 2 = Sword and Shield, 3 = Staff, 4 = Bow
        sendBackStats.HPBar();
    }

    public void SetClassRanger(int classID, float maxHp, float hp, int str, int AC, int dmg, int wepT)
    {
        classID = 4;
        maxHp = 36.0f;
        hp = maxHp;
        str = -1;
        AC = 9; //14
        dmg = 8;
        wepT = 4;

        sendBackStats.classID = classID;
        sendBackStats.maxHp = maxHp;
        sendBackStats.hp = maxHp;
        sendBackStats.str = str;
        sendBackStats.AC = AC;
        sendBackStats.dmg = dmg;
        sendBackStats.wepType = wepT; //1 = Sword, 2 = Sword and Shield, 3 = Staff, 4 = Bow
        sendBackStats.HPBar();
    }

    public void SetClassWizard(int classID, float maxHp, float hp, int str, int AC, int dmg, int wepT)
    {
        classID = 5;
        maxHp = 22.0f;
        hp = maxHp;
        str = -1;
        AC = 8; //13
        dmg = 6;
        wepT = 3;

        sendBackStats.classID = classID;
        sendBackStats.maxHp = maxHp;
        sendBackStats.hp = maxHp;
        sendBackStats.str = str;
        sendBackStats.AC = AC;
        sendBackStats.dmg = dmg;
        sendBackStats.wepType = wepT; //1 = Sword, 2 = Sword and Shield, 3 = Staff, 4 = Bow
        sendBackStats.HPBar();
    }




    // // Update is called once per frame
    // void Update()
    // {
        
    // }
}
