using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassStats : MonoBehaviour
{

    public void SetClassBarbarian(float maxHp, float hp, int str, int con, int AC)
        {
            maxHp = 41.0f;
            hp = maxHp;
            //int maxHpInt = maxHp as int;
            int maxHpInt = Convert.ToInt32(maxHp);
            healthBar.SetMaxHealth(maxHpInt);//This will need to be placed anywhere max health gets altered
            str = 3;
            con = 2;
            AC = 15;
        }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
