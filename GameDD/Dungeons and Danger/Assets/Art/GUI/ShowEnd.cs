using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowEnd : MonoBehaviour
{

    public static string GetRes = "";
    public Text _Text;
    // Update is called once per frame

    
    public void BeVis()
    {
        Debug.Log("I fired");
        Renderer rend;
        rend = GetComponent<Renderer>();
        rend.enabled = true;
    }

    public void BeInVis()
    {
        Renderer rend;
        rend = GetComponent<Renderer>();
        rend.enabled = false;
    }
    
    void Update()
    {
        _Text.text = GetRes;
    }
}

