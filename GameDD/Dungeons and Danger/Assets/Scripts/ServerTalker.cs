using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;

public class ServerTalker : MonoBehaviour
{

    static public int TakeTurn = 1;
    //Need something like this
    //public string highscoreURL = "http://localhost/unity_test/display.php";

    // Start is called before the first frame update
    void Start() //http://localhost:8000/user/
    {
        StartCoroutine( GetWebData("https://localhost:7114/api/Game/", "1")); //, "http://"localhost:8000/user.gameTurn  //, "foo"
    }

    void ProcessServerResponse( string rawResponse )
    {
        JSONNode node = JSON.Parse( rawResponse );

        //Debug.Log("Username: " + node["username"]);
        //Debug.Log("Misc Data: " + node["someArray"][1]["name"] + " = " + node["someArray"][1]["value"]);

        //e.g.
        
        //PlayerData.SetBar(node["someArray"][1]["value"]);
        Debug.Log("SQL Turn: " + node["gameTurn"]);
        Debug.Log("P1X: " + node["p1x"]);
        Debug.Log("P1Y: " + node["p1y"]);
        Debug.Log("P2X: " + node["p2x"]);
        Debug.Log("P2Y: " + node["p2y"]);
        Debug.Log("P3X: " + node["p3x"]);
        Debug.Log("P3Y: " + node["p3y"]);
        Debug.Log("P4X: " + node["p4x"]);
        Debug.Log("P4Y: " + node["p4y"]);

    }

    // static void RecordGameTurn(int takeTurn)
    // {
    //     // var dummyData = {
    //     //     "gameTurn" = takeTurn
    //     // }
    //     JSONNode node = JSON.Parse(node["gameTurn"] = takeTurn);
    //     node["gameTurn"] = takeTurn;
    //     Debug.Log("Game Turn: " + node["gameTurn"]);
    // }

    public void ProcessPost()
    {
        //Debug.Log("ProcessPost fired at least");
        StartCoroutine(Upload("https://localhost:7114/api/Game"));//, "1"
    }

    //public static string Serialize (object? value, Type inputType, System.Text.Json.JsonSerializerOptions? options = default);

    public IEnumerator Upload( string address )//, string myId
    {
        WWWForm form = new WWWForm();
        form.AddField("gameTurn", TakeTurn);
        //form.AddField("Id");

        //Debug.Log("form: " + form);

        UnityWebRequest www = UnityWebRequest.Post(address, form); // + myId
        yield return www.SendWebRequest();

        //Debug.Log("address + myId: " + address + myId);
        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Turn: " + TakeTurn + ", Something went wrong: " + www.error);
        }
        else
        {
            //Debug.Log("Form upload complete!" + TakeTurn);
        }
        
    }

    IEnumerator GetWebData( string address, string myId )//, int theTurn 
    {
        UnityWebRequest www = UnityWebRequest.Get(address + myId);
        yield return www.SendWebRequest();

        if(www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Something went wrong dude: " + www.error);
        }
        else
        {
            //Debug.LogError(www.downloadHandler.text);//success

            ProcessServerResponse(www.downloadHandler.text);
        }
    }
}
