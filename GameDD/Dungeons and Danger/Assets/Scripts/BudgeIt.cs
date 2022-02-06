using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BudgeIt : MonoBehaviour
{
    public int myTurn;
    public int callToTurn = TurnController.Turn;
    bool canMove = true;
    bool canAttack = false; //Deactivated for now
    public int movePoints = 3;
    int weaponType = 1; //1 = Sword, 2 = Sword and Shield, 3 = Staff, 4 = Bow
    public GameObject topBlock;
    public GameObject bottomBlock;
    public GameObject leftBlock;
    public GameObject rightBlock;
    public GameObject otherPlayer;
    
    private bool _canMakeCollision { get; set; }

    //Method to move square to determine a player moved something and it persisted to other players
    void BudgeRight()
    {
        rightBlock = GameObject.Find("RightBlocker"); //otherPlayer = GameObject.FindGameObjectsWithTag("CollideObj");
        Debug.Log("Distance to right blocker: " + (rightBlock.transform.position.x - transform.position.x));
        //if(!Physics2D.OverlapCircle(transform.position, 5))//number is purely chosen arbitrary
        if(rightBlock.transform.position.x - transform.position.x > 0.35f 
        && !Input.GetKey("left") && !Input.GetKey("up") && !Input.GetKey("down"))
        //&& (otherPlayer.transform.position.x-transform.position.x > 0.35f))
        {
        transform.localRotation = Quaternion.Euler(0, 180, 0);    
        transform.position = new Vector3(transform.position.x + 0.307f, transform.position.y);
        movePoints -= 1;
        if(movePoints < 1){canMove = false;}
        }
    }

    //Method to move square to determine a player moved something and it persisted to other players
    void BudgeLeft()
    {
        leftBlock = GameObject.Find("LeftBlocker"); //otherPlayer = GameObject.FindGameObjectsWithTag("CollideObj");
        Debug.Log("Distance to left blocker: " + (transform.position.x - leftBlock.transform.position.x));
        //if(!Physics2D.OverlapCircle(transform.position, 5))//number is purely chosen arbitrary
        if(transform.position.x - leftBlock.transform.position.x > 0.35f 
        && !Input.GetKey("right") && !Input.GetKey("up") && !Input.GetKey("down"))
       // && (transform.position.x-otherPlayer.transform.position.x > 0.35f))
        {
        transform.localRotation = Quaternion.Euler(0, 0, 0);
        transform.position = new Vector3(transform.position.x - 0.307f, transform.position.y);
        movePoints -= 1;
        if(movePoints < 1){canMove = false;}
        }
    }

    //Method to move square to determine a player moved something and it persisted to other players
    void BudgeUp()
    {
        topBlock = GameObject.Find("TopBlocker"); //otherPlayer = GameObject.FindGameObjectsWithTag("CollideObj");
        Debug.Log("Distance to top blocker: " + (topBlock.transform.position.y - transform.position.y));
        //if(!Physics2D.OverlapCircle(transform.position, 5))//number is purely chosen arbitrary
        if(topBlock.transform.position.y - transform.position.y > 0.35f 
        && !Input.GetKey("left") && !Input.GetKey("right") && !Input.GetKey("down"))
        //&& (otherPlayer.transform.position.y-transform.position.y > 0.35f))
        {
        transform.localRotation = Quaternion.Euler(0, 0, 270);
        transform.position = new Vector3(transform.position.x, transform.position.y + 0.307f);
        movePoints -= 1;
        if(movePoints < 1){canMove = false;}
        }
    }

    //Method to move square to determine a player moved something and it persisted to other players
    void BudgeDown()
    {
        bottomBlock = GameObject.Find("BottomBlocker"); //otherPlayer = GameObject.FindGameObjectsWithTag("CollideObj");
        Debug.Log("Distance to bottom blocker: " + (transform.position.y - bottomBlock.transform.position.y));
        //if(!Physics2D.OverlapCircle(transform.position, 5))//number is purely chosen arbitrary
        if(transform.position.y - bottomBlock.transform.position.y > 0.35f 
        && !Input.GetKey("left") && !Input.GetKey("up") && !Input.GetKey("right"))
       // && (transform.position.y-otherPlayer.transform.position.y > 0.35f))
        {
        transform.localRotation = Quaternion.Euler(0, 0, 90);
        transform.position = new Vector3(transform.position.x, transform.position.y - 0.307f);
        movePoints -= 1;
        if(movePoints < 1){canMove = false;}
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
                GameObject makeCol = new GameObject();
                makeCol.GetComponent<PlayerCollision>().myParentId = myTurn;
            }
        }
    }
}
