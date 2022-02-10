using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour
{
    private float _countdown = 0.0f;
    public string input = "Enter Name";
    //public Text _Text;

    public GameObject inputField;
    public GameObject textDisplay;

    public void StoreName()
    {
        input = inputField.GetComponent<Text>().text;
        textDisplay.GetComponent<Text>().text = "Welcome " + input + "!";
        _countdown = 3.0f;
    }

    void Update()
    {
        //Login started
        if(_countdown > 0){
            _countdown -= Time.deltaTime;
            if(_countdown < 1)
            {
                SendUser.UserName = input;
                SceneManager.LoadScene(sceneName:"GameBoard");
            }
        }
    }
}
