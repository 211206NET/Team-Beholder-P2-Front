using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    private string _ShowInfo = "DUNGEONS AND DANGER";
    public Text _Text;
    // Update is called once per frame
    void Update()
    {
        _Text.text = _ShowInfo;
    }
}

