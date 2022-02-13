using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendUser : MonoBehaviour
{

    public static string UserName = "";

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

}
