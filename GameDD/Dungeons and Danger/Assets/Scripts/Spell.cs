using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{

    public int spell = 0; //1 = fireball, 2 = magic missile
    // Start is called before the first frame update
    private float _countdown = 4.0f;

    void Start()
    {
        
    }

    void OnMouseDown()
    {
        Debug.Log("Spell Attempt");
        GameObject[] findPlayer = GameObject.FindGameObjectsWithTag("Player"); 
        foreach(GameObject fp in findPlayer)
        {
            if(fp.GetComponent<BudgeIt>().myTurn == 1 && TurnController.Turn == 1)
            {
                if(fp.GetComponent<CharacterStats>().coolJob == 5)
                {
                    fp.GetComponent<BudgeIt>().canMove = false;
                    fp.GetComponent<BudgeIt>().canAttack = false;
                    fp.GetComponent<BudgeIt>().FireSpell(spell);
                    if(spell == 1){}//fireball
                    if(spell == 2){}//magic missile
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(_countdown>0){
            _countdown -= Time.deltaTime;
        if(_countdown  < 1){
        GameObject[] findPlayer = GameObject.FindGameObjectsWithTag("Player"); 
        foreach(GameObject fp in findPlayer)
        {
            if(fp.GetComponent<BudgeIt>().myTurn == 1)
            {
                if(fp.GetComponent<CharacterStats>().coolJob == 5)
                {
                    Renderer rend;
                    rend = GetComponent<Renderer>();
                    rend.enabled = true;
                }
            }
        }
        }}
    }
}
