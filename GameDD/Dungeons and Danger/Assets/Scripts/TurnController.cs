using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    
public class TurnController : MonoBehaviour
{

    public static int Turn = 1; //1 to 4 
    public static int TotalPhases = 1;
    // Update is called once per frame
    void Update()
    {
        //Correct Wrong Turn Setting
        if(Turn > 4 || Turn < 1){Turn = 1;}
    }
}
