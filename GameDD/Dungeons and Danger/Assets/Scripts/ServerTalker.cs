using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;

public class ServerTalker : MonoBehaviour
{
    public static int ThisPlayerIs = 0; //What turn this player is, so each player only controls one character
    bool canInitialize = true; ///Tell server you joined once
    bool showDebug = false;
    static public int TakeTurn = 1;
    //Need something like this
    //public string highscoreURL = "http://localhost/unity_test/display.php";
    
    int playersTotal = 0;
    // Start is called before the first frame update
    void Start() //http://localhost:8000/user/
    {
        StartCoroutine( GetWebData("https://localhost:7114/api/Game/", "1")); //, "http://"localhost:8000/user.gameTurn  //, "foo"

        
        
        // StartCoroutine(checkInternetConnection((isConnected)=>{
        //     // handle connection status here
        //     Debug.Log("Player " + myTurn + " connected.");
        // }));
    }

    void ProcessServerResponse( string rawResponse )
    {
        JSONNode node = JSON.Parse( rawResponse );

        //Debug.Log("Username: " + node["username"]);
        //Debug.Log("Misc Data: " + node["someArray"][1]["name"] + " = " + node["someArray"][1]["value"]);

        //e.g.
        // Id
        // Players 
        // GameTurn 
        // p1Name
        // p2Name
        // p3Name
        // p4Name
        // P1mv
        // P2mv
        // P3mv
        // P4mv
        // P1fc
        // P2fc
        // P3fc
        // P4fc
        // Action //0 = No Action Yet, 1 = Melee, 2 = Spell, 3 = Self Skill, 4 = Self Spell
        // ActionID //the Id for the action in a list
        // TargetName//Who is being targeted this turn
        // P1MaxHP 
        // P2MaxHP 
        // P3MaxHP 
        // P4MaxHP 
        // P1HP 
        // P2HP 
        // P3HP 
        // P4HP 

        /*
        {"id":1,"players":4,"gameTurn":1,"p1Name":"aaa","p2Name":"bbb","p3Name":"ccc","p4Name":"itworkedmaybe",
        "p1x":1,"p1y":1,"p2x":1,"p2y":1,"p3x":1,"p3y":1,"p4x":1,"p4y":1,"action":1,"actionID":1,"targetName":"aaa",
        "p1MaxHP":1,"p2MaxHP":1,"p3MaxHP":1,"p4MaxHP":1,"p1HP":1,"p2HP":1,"p3HP":1,"p4HP":1}
        */

        if(showDebug){
        Debug.Log("Players: " + node["players"]);
        Debug.Log("SQL Turn: " + node["gameTurn"]);
        Debug.Log("P1MV: " + node["p1mv"]);
        Debug.Log("P2MV: " + node["p2mv"]);
        Debug.Log("P3MV: " + node["p3mv"]);
        Debug.Log("P4MV: " + node["p4mv"]);
        Debug.Log("P1FC: " + node["p1mv"]);
        Debug.Log("P2FC: " + node["p2mv"]);
        Debug.Log("P3FC: " + node["p3mv"]);
        Debug.Log("P4FC: " + node["p4mv"]);}


        //Initialize Once
        if(canInitialize)
        {
            //yield return new WaitForSeconds(Random.Range(1, 10));
            if(node["players"] < 4)
            {
                playersTotal = node["players"]+1;//Set local record of how many players there are and what player this is
                ThisPlayerIs = playersTotal;
                canInitialize = false;
            }
            else
            {
                //Error, this should never run starting with 4 players already, this must be a test
                playersTotal = 1;
                ThisPlayerIs = playersTotal;
                canInitialize = false;
            }
        }

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
        StartCoroutine(Upload("https://localhost:7114/api/Game/", "1"));//, "1"
        //StartCoroutine(DeleteData("https://localhost:7114/api/Game/", "2"));
    }

    //public static string Serialize (object? value, Type inputType, System.Text.Json.JsonSerializerOptions? options = default);

    // DELETE FROM "public"."Games" WHERE "Id" > 1


    //byte[] myData = System.Text.Encoding.UTF8.GetBytes("This is some test data");
    //UnityWebRequest put = UnityWebRequest.Put("http://www.my-server.com/upload", myData);

    //Put   
    public IEnumerator Upload( string address, string myId )//, string myId
    {
        //[{"id":1,"players":4,"gameTurn":4,"p1Name":"a","p2Name":"s","p3Name":"d","p4Name":"g","p1x":1,"p1y":2,"p2x":2,"p2y":2,"p3x":2,"p3y":2,"p4x":2,"p4y":2,"action":2,"actionID":2,"targetName":"dff","p1MaxHP":2,"p2MaxHP":1,"p3MaxHP":1,"p4MaxHP":1,"p1HP":1,"p2HP":1,"p3HP":1,"p4HP":1}]
        //UnityWebRequest www = UnityWebRequest.Put(URL_01, "{\"name\":\"user_01\",\"address\":{\"raw\":\"MountFiji\"}}");
        //www.SetRequestHeader ("Content-Type", "application/json");

        WWWForm form = new WWWForm();        
        // Id
        // Players 
        // GameTurn 
        // p1Name
        // p2Name
        // p3Name
        // p4Name
        // P1mv
        // P2mv
        // P3mv
        // P4mv
        // P1fc
        // P2fc
        // P3fc
        // P4fc
        // Action //0 = No Action Yet, 1 = Melee, 2 = Spell, 3 = Self Skill, 4 = Self Spell
        // ActionID //the Id for the action in a list
        // TargetName//Who is being targeted this turn
        // P1MaxHP 
        // P2MaxHP 
        // P3MaxHP 
        // P4MaxHP 
        // P1HP 
        // P2HP 
        // P3HP 
        // P4HP 
        
        form.AddField("Id", 1);
        form.AddField("Players", playersTotal);
        form.AddField("gameTurn", TakeTurn);
        form.AddField("p1Name", "aaa");
        form.AddField("p2Name", "bbb");
        form.AddField("p3Name", "ccc");
        form.AddField("p4Name", "itworkedmaybe");
        form.AddField("P1mv", 1);
        form.AddField("P2mv", 1);
        form.AddField("P3mv", 1);
        form.AddField("P4mv", 1);
        form.AddField("P5fc", 1);
        form.AddField("P6fc", 1);
        form.AddField("P7fc", 1);
        form.AddField("P8fc", 1);
        form.AddField("Action", 1);
        form.AddField("ActionID", 1);
        form.AddField("TargetName", "aaa");
        form.AddField("P1MaxHP", 1);
        form.AddField("P2MaxHP", 1);
        form.AddField("P3MaxHP", 1);
        form.AddField("P4MaxHP", 1);
        form.AddField("P1HP", 1);
        form.AddField("P2HP", 1);
        form.AddField("P3HP", 1);
        form.AddField("P4HP", 1);

        byte[] rawData = form.data; 
        
        //Without Id added, error goes from 409 conflict to 405 Method Not Allowed
        string url = address;//+myId;
        var uwr = new UnityWebRequest(url, "PUT");
        uwr.uploadHandler = (UploadHandler)new UploadHandlerRaw(rawData);
        uwr.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        uwr.SetRequestHeader("Content-Type", "application/x-www-form-urlencoded"); //'application/x-www-form-urlencoded'  ,"application/json"
        yield return uwr.SendWebRequest(); 
        if (uwr.result != UnityWebRequest.Result.Success) 
        {
            if(showDebug){Debug.Log("Turn: " + TakeTurn + ", Something went wrong: " + uwr.error);}
        }
        else
        {
            if(showDebug){Debug.Log("Form upload complete!" + TakeTurn);}
        }

        
    }

    public IEnumerator PostNew( string address )//, string myId
    {
        WWWForm form = new WWWForm();
        form.AddField("gameTurn", TakeTurn);

        using (UnityWebRequest www = UnityWebRequest.Post(address, form))
        {
            //Send the request then wait here until it returns
            yield return www.SendWebRequest(); //uwr

            if (www.result != UnityWebRequest.Result.Success) //www
            {
                if(showDebug){Debug.Log("Turn: " + TakeTurn + ", Something went wrong: " + www.error);} //uwr
            }
            else
            {
                //Debug.Log("Form upload complete!" + TakeTurn);
            }
        }
    }

    IEnumerator GetWebData( string address, string myId )//, int theTurn 
    {
        UnityWebRequest www = UnityWebRequest.Get(address + myId);
        yield return www.SendWebRequest();

        if(www.result != UnityWebRequest.Result.Success)
        {
            if(showDebug){Debug.LogError("Something went wrong dude: " + www.error);}
        }
        else
        {
            //Debug.LogError(www.downloadHandler.text);//success

            ProcessServerResponse(www.downloadHandler.text);
        }
    }

    
    //Delete
    IEnumerator DeleteData( string address, string myId )//, int theTurn 
    {
        UnityWebRequest www = UnityWebRequest.Delete(address+myId);
        yield return www.SendWebRequest();

        if(www.result != UnityWebRequest.Result.Success)
        {
            if(showDebug){Debug.LogError("Can't delete it: " + www.error);}
        }
        else
        {
            if(showDebug){Debug.Log("It's deleted!");}
        }
    }
}



