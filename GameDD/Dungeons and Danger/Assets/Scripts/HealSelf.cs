using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealSelf : MonoBehaviour
{

    public int healtype = 1; //1 = cleric heal, 2 = paladin heal
    private int _heal = 10;
    private int _class = 0; //1 = Sword, 2 = Sword and Shield, 3 = Staff, 4 = Bow, 5 = cleric staff

    private float _countdown = 2.0f;
    // Start is called before the first frame update
    void GetPower()
    {
        GameObject[] findPlayer = GameObject.FindGameObjectsWithTag("Player"); 
        foreach(GameObject fp in findPlayer)
        {
            if(fp.GetComponent<BudgeIt>().myTurn == 1)
            {
                //Player 
                _class = fp.GetComponent<BudgeIt>().weaponType;

                if(_class == 2){_heal = 20; healtype = 1;}//pally
                if(_class == 5){_heal = 50; healtype = 2;}//cleric
                if(_class != 2 && _class != 5){Destroy(gameObject);}//hard coded
                else
                {
                    Renderer rend;
                    rend = GetComponent<Renderer>();
                    rend.enabled = true;
                }
            }
        }
    }

    void OnMouseDown()
    {
        Debug.Log("Heal Attempt");
        GameObject[] findPlayer = GameObject.FindGameObjectsWithTag("Player"); 
        foreach(GameObject fp in findPlayer)
        {
            if(fp.GetComponent<BudgeIt>().myTurn == 1 && TurnController.Turn == 1)
            {
                fp.GetComponent<BudgeIt>().canMove = false;
                fp.GetComponent<BudgeIt>().canAttack = false;
                fp.GetComponent<CharacterStats>().Heal(_heal);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(_countdown > 0.0f)
        {
            _countdown -= Time.deltaTime;
            if(_countdown < 1)
            {
                GetPower();
            }
        }
    }
}
