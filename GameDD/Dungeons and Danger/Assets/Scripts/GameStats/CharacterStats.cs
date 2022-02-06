using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    //Stats like this must be floating point internally but rounded up for the UI
    public float hp = 10.0f;
    public float maxHp = 10.0f;
    public float damage = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    //This player is defeated
    void Die()
    {
        BudgeIt budgescript = GetComponent<BudgeIt>();
        budgescript.dead = true;
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
