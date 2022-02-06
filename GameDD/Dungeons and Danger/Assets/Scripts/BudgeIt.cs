using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BudgeIt : MonoBehaviour
{
    public int myTurn;
    public int callToTurn = TurnController.Turn;
    bool canMove = true;
    bool moveClear = true; //If move is not blocked
    bool canAttack = false; //Deactivated for now
    public int movePoints = 3;
    //int weaponType = 1; //1 = Sword, 2 = Sword and Shield, 3 = Staff, 4 = Bow
    public GameObject topBlock;
    public GameObject bottomBlock;
    public GameObject leftBlock;
    public GameObject rightBlock;
    
    public GameObject[] otherPlayer;
    private bool _canMakeCollision = true;
    public GameObject blockPF;

    //Method to move square to determine a player moved something and it persisted to other players
    void BudgeRight()
    {
        if(!Input.GetKey("left") && !Input.GetKey("up") && !Input.GetKey("down")){
        otherPlayer = GameObject.FindGameObjectsWithTag("CollideObj");
        rightBlock = GameObject.Find("RightBlocker"); 
        //Debug.Log("How Many Collide Obj: " + otherPlayer.Length);

        foreach(GameObject op in otherPlayer)
        {      
            Debug.Log("Distance to other player: " + (op.transform.position.x - transform.position.x) + ", Abs x: " + Mathf.Abs(op.transform.position.y - transform.position.y));
            if((transform.position.x < op.transform.position.x && op.transform.position.x - transform.position.x < 0.35f && Mathf.Abs(op.transform.position.y - transform.position.y)<0.24f) ||
            rightBlock.transform.position.x - transform.position.x < 0.35f){moveClear = false;}
        }

        if(moveClear){
            transform.localRotation = Quaternion.Euler(0, 180, 0);    
            transform.position = new Vector3(transform.position.x + 0.307f, transform.position.y);
            movePoints -= 1;
            if(movePoints < 1){canMove = false;}
        }
        moveClear = true;//reset
        }
    }

    //Method to move square to determine a player moved something and it persisted to other players
    void BudgeLeft()
    {
        if(!Input.GetKey("right") && !Input.GetKey("up") && !Input.GetKey("down")){
        otherPlayer = GameObject.FindGameObjectsWithTag("CollideObj");
        leftBlock = GameObject.Find("LeftBlocker"); 

        foreach(GameObject op in otherPlayer)
        {      
            Debug.Log("Distance to other player: " + (transform.position.x - op.transform.position.x) + ", Abs x: " + Mathf.Abs(op.transform.position.y - transform.position.y));
            if((op.transform.position.x < transform.position.x && transform.position.x - op.transform.position.x < 0.35f && Mathf.Abs(op.transform.position.y - transform.position.y)<0.24f) ||
            transform.position.x - leftBlock.transform.position.x < 0.35f){moveClear = false;}
        }

        if(moveClear){
            transform.localRotation = Quaternion.Euler(0, 0, 0);    
            transform.position = new Vector3(transform.position.x - 0.307f, transform.position.y);
            movePoints -= 1;
            if(movePoints < 1){canMove = false;}
        }
        moveClear = true;//reset
        }
    }

    //Method to move square to determine a player moved something and it persisted to other players
    void BudgeUp()
    {
        if(!Input.GetKey("left") && !Input.GetKey("right") && !Input.GetKey("down")){
        otherPlayer = GameObject.FindGameObjectsWithTag("CollideObj");
        topBlock = GameObject.Find("TopBlocker"); 

        foreach(GameObject op in otherPlayer)
        {      
            Debug.Log("Distance to other player: " + (op.transform.position.y - transform.position.y) + ", Abs x: " + Mathf.Abs(op.transform.position.x - transform.position.x));
            if((transform.position.y < op.transform.position.y && op.transform.position.y - transform.position.y < 0.35f && Mathf.Abs(op.transform.position.x - transform.position.x)<0.24f) ||
            topBlock.transform.position.y - transform.position.y < 0.35f){moveClear = false;}
        }

        if(moveClear){
            transform.localRotation = Quaternion.Euler(0, 0, 270);    
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.307f);
            movePoints -= 1;
            if(movePoints < 1){canMove = false;}
        }
        moveClear = true;//reset
        }
    }

    //Method to move square to determine a player moved something and it persisted to other players
    void BudgeDown()
    {
        if(!Input.GetKey("left") && !Input.GetKey("up") && !Input.GetKey("right")){
        otherPlayer = GameObject.FindGameObjectsWithTag("CollideObj");
        bottomBlock = GameObject.Find("BottomBlocker"); 
        //Debug.Log("How Many Collide Obj: " + otherPlayer.Length);

        foreach(GameObject op in otherPlayer)
        {      
            Debug.Log("Distance to other player: " + (transform.position.y - op.transform.position.y) + ", Abs x: " + Mathf.Abs(op.transform.position.x - transform.position.x));
            if((op.transform.position.y < transform.position.y && transform.position.y - op.transform.position.y < 0.35f && Mathf.Abs(op.transform.position.x - transform.position.x)<0.24f) ||
            transform.position.y - bottomBlock.transform.position.y < 0.35f){moveClear = false;}
        }

        if(moveClear){
            transform.localRotation = Quaternion.Euler(0, 0, 90);    
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.307f);
            movePoints -= 1;
            if(movePoints < 1){canMove = false;}
        }
        moveClear = true;//reset
        }
    }

    //Player moved and Attacked, end their turn
    void EndTurn()
    {
        if(myTurn < 4){TurnController.Turn += 1;}else{TurnController.Turn = 1;}
        callToTurn = TurnController.Turn;
        canMove = true; movePoints = 3;
        canAttack = false; //This would be set to true here if game had combat
        //Servertalker.RecordGameTurn(callToTurn);
        ServerTalker.TakeTurn = callToTurn;
        GameObject sTalk; sTalk = GameObject.Find("GOD");
        sTalk.GetComponent<ServerTalker>().ProcessPost();
    }

    // Update is called once per frame
    void Update()
    {
        //callToTurn = TurnController.Turn;
        //Movement
        if(callToTurn == myTurn)
        {
            //Player Input
            if (Input.GetKeyDown("right") && !Input.GetKey("left") && !Input.GetKey("up") && !Input.GetKey("down"))
            {
                BudgeRight();
            }
            if (Input.GetKeyDown("left") && !Input.GetKey("right") && !Input.GetKey("up") && !Input.GetKey("down"))
            {
                BudgeLeft();
            }
            if (Input.GetKeyDown("up") && !Input.GetKey("left") && !Input.GetKey("right") && !Input.GetKey("down"))
            {
                BudgeUp();
            }
            if (Input.GetKeyDown("down") && !Input.GetKey("left") && !Input.GetKey("up") && !Input.GetKey("right"))
            {
                BudgeDown();
            }

            //Check Player end turn conditions
            if(canMove == false && canAttack == false)
            {
                EndTurn();
            }

            _canMakeCollision = true;
        }
        else
        {
            callToTurn = TurnController.Turn;
            //Create collider once when not your turn so other players can't collide with you
            if(_canMakeCollision == true)
            {
                _canMakeCollision = false;
                //GameObject makeCol = new GameObject();
                GameObject makeCol = Instantiate(blockPF, transform.position, transform.rotation);
                makeCol.GetComponent<PlayerCollision>().myParentId = myTurn;
            }
        }
    }
}
