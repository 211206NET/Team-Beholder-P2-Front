using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ClassStats : MonoBehaviour
{

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
    }



    // // Start is called before the first frame update
    // void Start()
    // {
        
    // }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }
}
