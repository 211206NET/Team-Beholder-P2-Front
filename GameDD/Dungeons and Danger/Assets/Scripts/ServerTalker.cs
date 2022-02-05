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
        // Id
        // Players 
        // GameTurn 
        // p1Name
        // p2Name
        // p3Name
        // p4Name
        // P1x
        // P1y
        // P2x
        // P2y
        // P3x
        // P3y
        // P4x
        // P4y
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
        StartCoroutine(Upload("https://localhost:7114/api/Game/", "1"));//, "1"
    }

    //public static string Serialize (object? value, Type inputType, System.Text.Json.JsonSerializerOptions? options = default);

    // DELETE FROM "public"."Games" WHERE "Id" > 1

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
        // P1x
        // P1y
        // P2x
        // P2y
        // P3x
        // P3y
        // P4x
        // P4y
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
        form.AddField("Players", 4);
        form.AddField("gameTurn", TakeTurn);
        form.AddField("p1Name", "aaa");
        form.AddField("p2Name", "bbb");
        form.AddField("p3Name", "ccc");
        form.AddField("p4Name", "itworkedmaybe");
        form.AddField("P1x", 1);
        form.AddField("P1y", 1);
        form.AddField("P2x", 1);
        form.AddField("P2y", 1);
        form.AddField("P3x", 1);
        form.AddField("P3y", 1);
        form.AddField("P4x", 1);
        form.AddField("P4y", 1);
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
        //form.AddField("gameTurn", TakeTurn);//Just one change doesn't seem to work


        //Dictionary<string, string> headers = form.headers;
        //byte[] rawData = form.data; //Needed to sent Put, UNSECURE DOESN'T WORK

        string rawData = "{\"id\":1,\"players\":4,\"gameTurn\":4,\"p1Name\":\"a\",\"p2Name\":\"s\",\"p3Name\":\"d\",\"p4Name\":\"g\",\"p1x\":1,\"p1y\":2,\"p2x\":2,\"p2y\":2,\"p3x\":2,\"p3y\":2,\"p4x\":2,\"p4y\":2,\"action\":2,\"actionID\":2,\"targetName\":\"dff\",\"p1MaxHP\":2,\"p2MaxHP\":1,\"p3MaxHP\":1,\"p4MaxHP\":1,\"p1HP\":1,\"p2HP\":1,\"p3HP\":1,\"p4HP\":1}"; 
        //string rawData = "{\"players\":4,\"gameTurn\":4,\"p1Name\":\"a\",\"p2Name\":\"s\",\"p3Name\":\"d\",\"p4Name\":\"g\",\"p1x\":1,\"p1y\":2,\"p2x\":2,\"p2y\":2,\"p3x\":2,\"p3y\":2,\"p4x\":2,\"p4y\":2,\"action\":2,\"actionID\":2,\"targetName\":\"dff\",\"p1MaxHP\":2,\"p2MaxHP\":1,\"p3MaxHP\":1,\"p4MaxHP\":1,\"p1HP\":1,\"p2HP\":1,\"p3HP\":1,\"p4HP\":1}"; 

        //byte[] myData;
        //myData = System.Text.Encoding.UTF8.GetBytes ("?gameTurn=" + TakeTurn);
        //myData = System.Text.Encoding.UTF8.GetBytes ($"{{\"gameTurn\":\"{TakeTurn}\"}}");

        //myData = System.Text.Encoding.UTF8.GetBytes ("{\"id\":1,\"players\":4,\"gameTurn\":4,\"p1Name\":\"a\",\"p2Name\":\"s\",\"p3Name\":\"d\",\"p4Name\":\"g\",\"p1x\":1,\"p1y\":2,\"p2x\":2,\"p2y\":2,\"p3x\":2,\"p3y\":2,\"p4x\":2,\"p4y\":2,\"action\":2,\"actionID\":2,\"targetName\":\"dff\",\"p1MaxHP\":2,\"p2MaxHP\":1,\"p3MaxHP\":1,\"p4MaxHP\":1,\"p1HP\":1,\"p2HP\":1,\"p3HP\":1,\"p4HP\":1}");
        //myData = System.Text.Encoding.UTF8.GetBytes ("{\"players\":4,\"gameTurn\":4,\"p1Name\":\"a\",\"p2Name\":\"s\",\"p3Name\":\"d\",\"p4Name\":\"g\",\"p1x\":1,\"p1y\":2,\"p2x\":2,\"p2y\":2,\"p3x\":2,\"p3y\":2,\"p4x\":2,\"p4y\":2,\"action\":2,\"actionID\":2,\"targetName\":\"dff\",\"p1MaxHP\":2,\"p2MaxHP\":1,\"p3MaxHP\":1,\"p4MaxHP\":1,\"p1HP\":1,\"p2HP\":1,\"p3HP\":1,\"p4HP\":1}");
        //myData = System.Text.Encoding.UTF8.GetBytes ($"{{\"id\":{1},\"players\":{4},\"gameTurn\":{4},\"p1Name\":\"{a}\",\"p2Name\":\"{s}\",\"p3Name\":\"{d}\",\"p4Name\":\"{g}\",\"p1x\":{1},\"p1y\":{2},\"p2x\":{2},\"p2y\":{2},\"p3x\":{2},\"p3y\":{2},\"p4x\":{2},\"p4y\":{2},\"action\":{2},\"actionID\":{2},\"targetName\":\"{dff}\",\"p1MaxHP\":{2},\"p2MaxHP\":{1},\"p3MaxHP\":{1},\"p4MaxHP\":{1},\"p1HP\":{1},\"p2HP\":{1},\"p3HP\":{1},\"p4HP\":{1}}}");
        //myData = System.Text.Encoding.UTF8.GetBytes ($"{{\"players\":{4},\"gameTurn\":{4},\"p1Name\":\"{a}\",\"p2Name\":\"{s}\",\"p3Name\":\"{d}\",\"p4Name\":\"{g}\",\"p1x\":{1},\"p1y\":{2},\"p2x\":{2},\"p2y\":{2},\"p3x\":{2},\"p3y\":{2},\"p4x\":{2},\"p4y\":{2},\"action\":{2},\"actionID\":{2},\"targetName\":\"{dff}\",\"p1MaxHP\":{2},\"p2MaxHP\":{1},\"p3MaxHP\":{1},\"p4MaxHP\":{1},\"p1HP\":{1},\"p2HP\":{1},\"p3HP\":{1},\"p4HP\":{1}}}");
        
        //Post style: Returns = 409 Conflict
        //Without Id added, error goes from 409 conflict to 405 Method Not Allowed
        Debug.Log("No Id: " + address);
        Debug.Log(rawData);
        using (UnityWebRequest www = UnityWebRequest.Put(address, rawData)) // + myId
        {
            //Send the request then wait here until it returns
            yield return www.SendWebRequest(); //uwr
            if (www.result != UnityWebRequest.Result.Success) //www
            {
                Debug.Log("Turn: " + TakeTurn + ", Something went wrong: " + www.error); //uwr
            }
            else
            {
                Debug.Log("Form upload complete!" + TakeTurn);
            }
        }

        Debug.Log("With Id: " + address + myId);
        Debug.Log(rawData);
        using (UnityWebRequest www = UnityWebRequest.Put(address + myId, rawData)) // + myId
        {
            //Send the request then wait here until it returns
            yield return www.SendWebRequest(); //uwr
            if (www.result != UnityWebRequest.Result.Success) //www
            {
                Debug.Log("Turn: " + TakeTurn + ", Something went wrong with myId: " + www.error); //uwr
            }
            else
            {
                Debug.Log("Form upload complete with myId!" + TakeTurn);
            }
        }
        

        //Raw Handler: Returns = 409 Conflict
        /*
        //Without Id added, error goes from 409 conflict to 405 Method Not Allowed
        string url = address;//+myId;
        var uwr = new UnityWebRequest(url, "PUT");
        uwr.uploadHandler = (UploadHandler)new UploadHandlerRaw(rawData);
        uwr.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        uwr.SetRequestHeader("Content-Type", "application/json");
        yield return uwr.SendWebRequest(); 
        if (uwr.result != UnityWebRequest.Result.Success) 
        {
            Debug.Log("Turn: " + TakeTurn + ", Something went wrong: " + uwr.error); 
        }
        else
        {
            Debug.Log("Form upload complete!" + TakeTurn);
        }
        */

        /*
        UnityWebRequest www = UnityWebRequest.Put(address, form); // + myId  Post
        yield return www.SendWebRequest();

        //Debug.Log("address + myId: " + address + myId);
        */
        
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
                Debug.Log("Turn: " + TakeTurn + ", Something went wrong: " + www.error); //uwr
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
            Debug.LogError("Something went wrong dude: " + www.error);
        }
        else
        {
            //Debug.LogError(www.downloadHandler.text);//success

            ProcessServerResponse(www.downloadHandler.text);
        }
    }
}
