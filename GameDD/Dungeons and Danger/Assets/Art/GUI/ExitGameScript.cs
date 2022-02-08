using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGameScript : MonoBehaviour
{
    void OnMouseDown()
    {
        GameObject findG; findG = GameObject.Find("GOD");
        findG.GetComponent<ServerTalker>().ExitTheGame();
    }
}
