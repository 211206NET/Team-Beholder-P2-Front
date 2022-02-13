using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGameScript : MonoBehaviour
{

    void OnMouseDown()
    {
        GameObject findG; findG = GameObject.Find("GOD");
        findG.GetComponent<ServerTalker>().ExitTheGame("You quit the game early. You can try to run away, but you'll only die tired.");
    }
}
