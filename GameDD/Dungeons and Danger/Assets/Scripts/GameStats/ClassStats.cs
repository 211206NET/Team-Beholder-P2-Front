using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ClassStats : MonoBehaviour
{
    CharacterStats sendBackStats;

<<<<<<< HEAD
    public void SetClassBarbarian(int classID, float maxHp, float hp, int str, int AC, int dmg)
        {
            classID = 1;
            maxHp = 41.0f;
            hp = maxHp;
            str = 3;
            AC = 15;
            dmg = 12;

            // CharacterStats sendBackStats = GetComponent<CharacterStats>();
            // this.hp = sendBackStats.hp;
            // sendBackStats.maxHp = this.maxHp;
            // sendBackStats.classID = this.classID;
            // sendBackStats.damage = this.damage;
    }


    public void SetClassCleric(int classID, float maxHp, float hp, int str, int AC, int dmg)
        {
            classID = 2;
            maxHp = 35.0f;
            hp = maxHp;
            str = 2;
            AC = 15;
            dmg = 6;
    }

    public void SetClassPaladin(int classID, float maxHp, float hp, int str, int AC, int dmg)
        {
            classID = 3;
            maxHp = 40.0f;
            hp = maxHp;
            str = 3;
            AC = 18;
            dmg = 8;
    }

    public void SetClassRanger(int classID, float maxHp, float hp, int str, int AC, int dmg)
        {
            classID = 4;
            maxHp = 36.0f;
            hp = maxHp;
            str = -1;
            AC = 14;
            dmg = 8;
    }

    public void SetClassWizard(int classID, float maxHp, float hp, int str, int AC, int dmg)
        {
            classID = 5;
            maxHp = 22.0f;
            hp = maxHp;
            str = -1;
            AC = 13;
            dmg = 6;
=======
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
    public void SetClassBarbarian(int classID, float maxHp, float hp, int str, int AC)
    {
        classID = 1;
        maxHp = 41.0f;
        hp = maxHp;
        str = 3;
        AC = 15;

        sendBackStats.classID = classID;
        sendBackStats.maxHp = maxHp;
        sendBackStats.hp = maxHp;
        sendBackStats.str = str;
        sendBackStats.AC = AC;
        sendBackStats.HPBar();
    }


    public void SetClassCleric(int classID, float maxHp, float hp, int str, int AC)
    {
        classID = 2;
        maxHp = 35.0f;
        hp = maxHp;
        str = 2;
        AC = 15;

        sendBackStats.classID = classID;
        sendBackStats.maxHp = maxHp;
        sendBackStats.hp = maxHp;
        sendBackStats.str = str;
        sendBackStats.AC = AC;
        sendBackStats.HPBar();
    }

    public void SetClassPaladin(int classID, float maxHp, float hp, int str, int AC)
    {
        classID = 3;
        maxHp = 40.0f;
        hp = maxHp;
        str = 3;
        AC = 18;

        sendBackStats.classID = classID;
        sendBackStats.maxHp = maxHp;
        sendBackStats.hp = maxHp;
        sendBackStats.str = str;
        sendBackStats.AC = AC;
        sendBackStats.HPBar();
    }

    public void SetClassRanger(int classID, float maxHp, float hp, int str, int AC)
    {
        classID = 4;
        maxHp = 36.0f;
        hp = maxHp;
        str = -1;
        AC = 14;

        sendBackStats.classID = classID;
        sendBackStats.maxHp = maxHp;
        sendBackStats.hp = maxHp;
        sendBackStats.str = str;
        sendBackStats.AC = AC;
        sendBackStats.HPBar();
    }

    public void SetClassWizard(int classID, float maxHp, float hp, int str, int AC)
    {
        classID = 5;
        maxHp = 22.0f;
        hp = maxHp;
        str = -1;
        AC = 13;

        sendBackStats.classID = classID;
        sendBackStats.maxHp = maxHp;
        sendBackStats.hp = maxHp;
        sendBackStats.str = str;
        sendBackStats.AC = AC;
        sendBackStats.HPBar();
>>>>>>> 191d71d3ae59e95bc38a703be84d0b50e2b6c414
    }




    // // Update is called once per frame
    // void Update()
    // {
        
    // }
}
