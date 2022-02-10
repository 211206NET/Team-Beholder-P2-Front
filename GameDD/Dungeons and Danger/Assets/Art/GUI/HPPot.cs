using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPPot : MonoBehaviour
{

    public static int Qty = 20;


    void OnMouseDown()
    {
        //Use Potion
        if(Qty>0)
        {
            Qty-=1;
            //Find player and add a kill
            GameObject[] otherPlayerPref;
            otherPlayerPref = GameObject.FindGameObjectsWithTag("Player");
            foreach(GameObject op in otherPlayerPref)
            {
                if(op.GetComponent<BudgeIt>().myTurn==1) 
                {
                    op.GetComponent<CharacterStats>().HealPotion();
                }
            }

        }
    }
}
