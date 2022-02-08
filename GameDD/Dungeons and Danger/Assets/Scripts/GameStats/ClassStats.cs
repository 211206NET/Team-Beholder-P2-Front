using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ClassStats : MonoBehaviour
{

    public void SetClassBarbarian(int classID, float maxHp, float hp, int str, int con, int AC)
        {
            classID = 1;
            maxHp = 41.0f;
            hp = maxHp;
            str = 3;
            con = 2;
            AC = 15;
    }

    public void SetClassCleric(int classID, float maxHp, float hp, int str, int con, int AC)
        {
            classID = 2;
            maxHp = 35.0f;
            hp = maxHp;
            str = 2;
            con = 3;
            AC = 15;
    }

    public void SetClassPaladin(int classID, float maxHp, float hp, int str, int con, int AC)
        {
            classID = 3;
            maxHp = 40.0f;
            hp = maxHp;
            str = 3;
            con = 3;
            AC = 18;
    }

    public void SetClassRanger(int classID, float maxHp, float hp, int str, int con, int AC)
        {
            classID = 4;
            maxHp = 36.0f;
            hp = maxHp;
            str = -1;
            con = 2;
            AC = 14;
    }

    public void SetClassWizard(int classID, float maxHp, float hp, int str, int con, int AC)
        {
            classID = 5;
            maxHp = 22.0f;
            hp = maxHp;
            str = -1;
            con = 1;
            AC = 13;
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
