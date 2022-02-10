using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissScript : MonoBehaviour
{

    private float _dietimer = 20.0f;
    public Text _Text;
    string msg = "Miss!";
    // Update is called once per frame
    void Update()
    {
        _Text.text = msg;    

        //Time out die
        if(_dietimer > 0){_dietimer -= Time.deltaTime;}
        if(_dietimer < 1){Destroy(gameObject);}
        
    }
}
