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

    //Method to move square to determine a player moved something and it persisted to other players
    void BudgeRight()
    {
        transform.position = new Vector3(transform.position.x + 0.307f, transform.position.y);
        movePoints -= 1;
        if(movePoints < 1){canMove = false;}
    }

    //Method to move square to determine a player moved something and it persisted to other players
    void BudgeLeft()
    {
        if(!Physics2D.OverlapCircle(transform.position, 5))//number is purely chosen arbitrary
        {
        transform.position = new Vector3(transform.position.x - 0.307f, transform.position.y);
        movePoints -= 1;
        if(movePoints < 1){canMove = false;}
        }
    }

    //Method to move square to determine a player moved something and it persisted to other players
    void BudgeUp()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + 0.307f);
        movePoints -= 1;
        if(movePoints < 1){canMove = false;}
    }

    //Method to move square to determine a player moved something and it persisted to other players
    void BudgeDown()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y - 0.307f);
        movePoints -= 1;
        if(movePoints < 1){canMove = false;}
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
            if (Input.GetKeyDown("right"))
            {
                BudgeRight();
            }
            if (Input.GetKeyDown("left"))
            {
                BudgeLeft();
            }
            if (Input.GetKeyDown("up"))
            {
                BudgeUp();
            }
            if (Input.GetKeyDown("down"))
            {
                BudgeDown();
            }

            //Check Player end turn conditions
            if(canMove == false && canAttack == false)
            {
                EndTurn();
            }
        }
        else{callToTurn = TurnController.Turn;}
    }
}
