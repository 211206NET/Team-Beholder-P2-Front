using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BudgeIt : MonoBehaviour
{

//Mechanics
private float _webglbuffer = 1.0f;
public int myTurn;
public string myName;
public int callToTurn = TurnController.Turn;
public GameObject findGOD;
public bool canMove = true;
bool moveClear = true; //If move is not blocked
public bool canAttack = true; //Deactivated for now
private bool _attackonce = true;
public int movePoints = 3;
public bool dead = false;
private bool _processend = true; //can process
float delayEndTurn = 0.0f;
private float _delayStep = 0.0f;
private float _delaySpeed = 0.2f;
bool endTurnMode = false;
public int kills = 0;

//UI
private Transform selectUI;
private Transform targetUI;
public GameObject bloodSpatter;

//Collision Code
private GameObject topBlock;
private GameObject bottomBlock;
private GameObject leftBlock;
private GameObject rightBlock;

public GameObject[] otherPlayerPref;
public GameObject[] otherPlayer;
private bool _canMakeCollision = true;
public GameObject blockPF;

//Vars for weapon
private float _atkrange = 0.0f;
public int weaponType = 1; //1 = Sword, 2 = Sword and Shield, 3 = Staff, 4 = Bow, 5 = cleric staff

public GameObject firBallProj;
public float fbVelocity = 500f;
public GameObject mmProj;
public float mmVelocity = 700f;

bool amTarget = false; //If can be attacked currently
bool eachTurn = true;
private float _checkCollide = 0.0f;

//External values
private int _getstr = 0; //Get character strength
private int _getdmg = 0; //Get incomming damage
private int _getattackroll; //Get incoming attack roll

//AI vars
private int _nearTargetDir = 0; //1 = to the right, 2 = left, 3 = up, 4 = down 
private float _waitafterturn = 0.0f;
private bool _deadend = false;
float delayStart = 0.0f;
private bool _blockright = false;
private bool _blockleft = false;
private bool _blockup = false;
private bool _blockdown = false;
private bool _inrange = false;
private bool _attackgood = true;

void Awake()
{
    findGOD = GameObject.Find("GOD"); 
    selectUI = transform.Find("Selected"); 
    targetUI = transform.Find("TargetUI"); 
    transform.GetChild(5).gameObject.SetActive(false);
    transform.GetChild(6).gameObject.SetActive(false);
    SetVis();
}

public void SetVis()
{
    //Prefab changes got deleted by github
    // gameObject.transform.GetChild(7).GetChild(1).gameObject.SetActive(false);
    // gameObject.transform.GetChild(7).GetChild(2).gameObject.SetActive(false);
    // gameObject.transform.GetChild(7).GetChild(3).gameObject.SetActive(false);
    // gameObject.transform.GetChild(7).GetChild(4).gameObject.SetActive(false);
    // gameObject.transform.GetChild(7).GetChild(5).gameObject.SetActive(false);
    
    transform.GetChild(0).gameObject.SetActive(false);
    transform.GetChild(1).gameObject.SetActive(false);
    transform.GetChild(2).gameObject.SetActive(false);
    transform.GetChild(3).gameObject.SetActive(false);
    transform.GetChild(4).gameObject.SetActive(false);
}

public void SetWeaponArt()
{
    transform.GetChild(weaponType-1).gameObject.SetActive(true);
    //Prefab changes got deleted by github
    //gameObject.transform.GetChild(7).GetChild(weaponType).gameObject.SetActive(true);
}

//-----------------------------------------------------------------------------------------------------------------------------------\\
//                                                         MOVE                                                                    \\
//-----------------------------------------------------------------------------------------------------------------------------------\\
//Method to move square to determine a player moved something and it persisted to other players
public void BudgeRight()
{   
    _nearTargetDir = 0;
    _blockright = false;
    if(!Input.GetKey("left") && !Input.GetKey("up") && !Input.GetKey("down")){
    otherPlayer = GameObject.FindGameObjectsWithTag("CollideObj");
    rightBlock = GameObject.Find("RightBlocker"); 
    //Debug.Log("How Many Collide Obj: " + otherPlayer.Length);

    foreach(GameObject op in otherPlayer)
    {      
        //Debug.Log("Distance to other player: " + (op.transform.position.x - transform.position.x) + ", Abs x: " + Mathf.Abs(op.transform.position.y - transform.position.y));
        if((transform.position.x < op.transform.position.x && op.transform.position.x - transform.position.x < 0.35f && Mathf.Abs(op.transform.position.y - transform.position.y)<0.24f) ||
        rightBlock.transform.position.x - transform.position.x < 0.35f)

        {moveClear = false; if(myTurn>1){ _blockright = true;}}

    }

    if(moveClear){
        transform.localRotation = Quaternion.Euler(0, 180, 0);    
        transform.position = new Vector3(transform.position.x + 0.307f, transform.position.y);
        movePoints -= 1;
        if(movePoints < 1){canMove = false;}
    }
    
    // if(myTurn == 1 && myTurn == ServerTalker.ThisPlayerIs){findGOD.GetComponent<ServerTalker>().tDP1mv = 1;}
    // if(myTurn == 2 && myTurn == ServerTalker.ThisPlayerIs){findGOD.GetComponent<ServerTalker>().tDP2mv = 1;}
    // if(myTurn == 3 && myTurn == ServerTalker.ThisPlayerIs){findGOD.GetComponent<ServerTalker>().tDP3mv = 1;}
    // if(myTurn == 4 && myTurn == ServerTalker.ThisPlayerIs){findGOD.GetComponent<ServerTalker>().tDP4mv = 1;}
    ClearTargets();
    CheckTarget();
    //UpdateServer();

    _delayStep = _delaySpeed+_webglbuffer;

    moveClear = true;//reset
    }
}

//Method to move square to determine a player moved something and it persisted to other players
public void BudgeLeft()
{
    _nearTargetDir = 0;
    _blockleft = false;
    if(!Input.GetKey("right") && !Input.GetKey("up") && !Input.GetKey("down")){
    otherPlayer = GameObject.FindGameObjectsWithTag("CollideObj");
    leftBlock = GameObject.Find("LeftBlocker"); 

    foreach(GameObject op in otherPlayer)
    {     
        //Test
        // if(myTurn==2 && TurnController.TotalPhases == 2)// && op.GetComponent<PlayerCollision>().myParentId == 1
        // {Debug.Log("op.transform.position.x: " + op.transform.position.x + ", op.transform.position.x: " + transform.position.x + 
        // ", Abs y: " + Mathf.Abs(op.transform.position.y - transform.position.y) + ", target id: "+op.GetComponent<PlayerCollision>().myParentId);}

        if((op.transform.position.x < transform.position.x && transform.position.x - op.transform.position.x < 0.35f && 
        Mathf.Abs(op.transform.position.y - transform.position.y)<0.24f) || transform.position.x - leftBlock.transform.position.x < 0.35f)

        {moveClear = false; if(myTurn>1){ _blockleft = true;}}

    }
    // if(myTurn==2 && TurnController.TotalPhases == 2){
    //     Debug.Log("Player is not left of me, moveClear: "+moveClear+",_deadend: "+_deadend);}
    if(moveClear){
        transform.localRotation = Quaternion.Euler(0, 0, 0);    
        transform.position = new Vector3(transform.position.x - 0.307f, transform.position.y);
        movePoints -= 1;
        if(movePoints < 1){canMove = false;}
    }
    
    // if(myTurn == 1 && myTurn == ServerTalker.ThisPlayerIs){findGOD.GetComponent<ServerTalker>().tDP1mv = 2;}
    // if(myTurn == 2 && myTurn == ServerTalker.ThisPlayerIs){findGOD.GetComponent<ServerTalker>().tDP2mv = 2;}
    // if(myTurn == 3 && myTurn == ServerTalker.ThisPlayerIs){findGOD.GetComponent<ServerTalker>().tDP3mv = 2;}
    // if(myTurn == 4 && myTurn == ServerTalker.ThisPlayerIs){findGOD.GetComponent<ServerTalker>().tDP4mv = 2;}
    ClearTargets();
    CheckTarget();
    //UpdateServer();

    _delayStep = _delaySpeed+_webglbuffer;

    moveClear = true;//reset
    }
}

//Method to move square to determine a player moved something and it persisted to other players
public void BudgeUp()
{
    _nearTargetDir = 0;
    _blockup = false;
    if(!Input.GetKey("left") && !Input.GetKey("right") && !Input.GetKey("down")){
    otherPlayer = GameObject.FindGameObjectsWithTag("CollideObj");
    topBlock = GameObject.Find("TopBlocker"); 

    foreach(GameObject op in otherPlayer)
    {      
        //Debug.Log("Distance to other player: " + (op.transform.position.y - transform.position.y) + ", Abs x: " + Mathf.Abs(op.transform.position.x - transform.position.x));
        if((transform.position.y < op.transform.position.y && op.transform.position.y - transform.position.y < 0.35f && Mathf.Abs(op.transform.position.x - transform.position.x)<0.24f) ||
        topBlock.transform.position.y - transform.position.y < 0.35f)

        {moveClear = false; if(myTurn>1){ _blockup = true;}}

    }

    if(moveClear){
        transform.localRotation = Quaternion.Euler(0, 0, 270);    
        transform.position = new Vector3(transform.position.x, transform.position.y + 0.307f);
        movePoints -= 1;
        if(movePoints < 1){canMove = false;}
    }
    
    // if(myTurn == 1 && myTurn == ServerTalker.ThisPlayerIs){findGOD.GetComponent<ServerTalker>().tDP1mv = 3;}
    // if(myTurn == 2 && myTurn == ServerTalker.ThisPlayerIs){findGOD.GetComponent<ServerTalker>().tDP2mv = 3;}
    // if(myTurn == 3 && myTurn == ServerTalker.ThisPlayerIs){findGOD.GetComponent<ServerTalker>().tDP3mv = 3;}
    // if(myTurn == 4 && myTurn == ServerTalker.ThisPlayerIs){findGOD.GetComponent<ServerTalker>().tDP4mv = 3;}
    ClearTargets();
    CheckTarget();
    //UpdateServer();

    _delayStep = _delaySpeed+_webglbuffer;

    moveClear = true;//reset
    }
}

//Method to move square to determine a player moved something and it persisted to other players
public void BudgeDown()
{
    _nearTargetDir = 0;
    _blockdown = false;
    if(!Input.GetKey("left") && !Input.GetKey("up") && !Input.GetKey("right")){
    otherPlayer = GameObject.FindGameObjectsWithTag("CollideObj");
    bottomBlock = GameObject.Find("BottomBlocker"); 
    //Debug.Log("How Many Collide Obj: " + otherPlayer.Length);

    foreach(GameObject op in otherPlayer)
    {      
        //Debug.Log("Distance to other player: " + (transform.position.y - op.transform.position.y) + ", Abs x: " + Mathf.Abs(op.transform.position.x - transform.position.x));
        if((op.transform.position.y < transform.position.y && transform.position.y - op.transform.position.y < 0.35f && Mathf.Abs(op.transform.position.x - transform.position.x)<0.24f) ||
        transform.position.y - bottomBlock.transform.position.y < 0.35f)

        {moveClear = false; if(myTurn>1){_blockdown = true;}}

    }

    if(moveClear){
        transform.localRotation = Quaternion.Euler(0, 0, 90);    
        transform.position = new Vector3(transform.position.x, transform.position.y - 0.307f);
        movePoints -= 1;
        if(movePoints < 1){canMove = false;}
    }
    
    // if(myTurn == 1 && myTurn == ServerTalker.ThisPlayerIs){findGOD.GetComponent<ServerTalker>().tDP1mv = 4;}
    // if(myTurn == 2 && myTurn == ServerTalker.ThisPlayerIs){findGOD.GetComponent<ServerTalker>().tDP2mv = 4;}
    // if(myTurn == 3 && myTurn == ServerTalker.ThisPlayerIs){findGOD.GetComponent<ServerTalker>().tDP3mv = 4;}
    // if(myTurn == 4 && myTurn == ServerTalker.ThisPlayerIs){findGOD.GetComponent<ServerTalker>().tDP4mv = 4;}
    ClearTargets();
    CheckTarget();
    //UpdateServer();

    _delayStep = _delaySpeed+_webglbuffer;

    moveClear = true;//reset
    }
}

//-----------------------------------------------------------------------------------------------------------------------------------\\
//                                                         FACING                                                                    \\
//-----------------------------------------------------------------------------------------------------------------------------------\\
//Just face direction, no move, used in conjunction with attacking
public void FaceRight()
{transform.localRotation = Quaternion.Euler(0, 180, 0); 
if(myTurn == 1 && myTurn == ServerTalker.ThisPlayerIs){findGOD.GetComponent<ServerTalker>().tDP1fc = 1;}
if(myTurn == 2 && myTurn == ServerTalker.ThisPlayerIs){findGOD.GetComponent<ServerTalker>().tDP2fc = 1;}
if(myTurn == 3 && myTurn == ServerTalker.ThisPlayerIs){findGOD.GetComponent<ServerTalker>().tDP3fc = 1;}
if(myTurn == 4 && myTurn == ServerTalker.ThisPlayerIs){findGOD.GetComponent<ServerTalker>().tDP4fc = 1;}
//UpdateServer();

_delayStep = _delaySpeed+_webglbuffer;}

public void FaceLeft()
{transform.localRotation = Quaternion.Euler(0, 0, 0); 
if(myTurn == 1 && myTurn == ServerTalker.ThisPlayerIs){findGOD.GetComponent<ServerTalker>().tDP1fc = 2;}
if(myTurn == 2 && myTurn == ServerTalker.ThisPlayerIs){findGOD.GetComponent<ServerTalker>().tDP2fc = 2;}
if(myTurn == 3 && myTurn == ServerTalker.ThisPlayerIs){findGOD.GetComponent<ServerTalker>().tDP3fc = 2;}
if(myTurn == 4 && myTurn == ServerTalker.ThisPlayerIs){findGOD.GetComponent<ServerTalker>().tDP4fc = 2;}
//UpdateServer();

_delayStep = _delaySpeed+_webglbuffer;}

public void FaceUp()
{transform.localRotation = Quaternion.Euler(0, 0, 270); 
if(myTurn == 1 && myTurn == ServerTalker.ThisPlayerIs){findGOD.GetComponent<ServerTalker>().tDP1fc = 3;}
if(myTurn == 2 && myTurn == ServerTalker.ThisPlayerIs){findGOD.GetComponent<ServerTalker>().tDP2fc = 3;}
if(myTurn == 3 && myTurn == ServerTalker.ThisPlayerIs){findGOD.GetComponent<ServerTalker>().tDP3fc = 3;}
if(myTurn == 4 && myTurn == ServerTalker.ThisPlayerIs){findGOD.GetComponent<ServerTalker>().tDP4fc = 3;}
//UpdateServer();

_delayStep = _delaySpeed+_webglbuffer;}

public void FaceDown()
{transform.localRotation = Quaternion.Euler(0, 0, 90); 
if(myTurn == 1 && myTurn == ServerTalker.ThisPlayerIs){findGOD.GetComponent<ServerTalker>().tDP1fc = 4;}
if(myTurn == 2 && myTurn == ServerTalker.ThisPlayerIs){findGOD.GetComponent<ServerTalker>().tDP2fc = 4;}
if(myTurn == 3 && myTurn == ServerTalker.ThisPlayerIs){findGOD.GetComponent<ServerTalker>().tDP3fc = 4;}
if(myTurn == 4 && myTurn == ServerTalker.ThisPlayerIs){findGOD.GetComponent<ServerTalker>().tDP4fc = 4;}
//UpdateServer();

_delayStep = _delaySpeed+_webglbuffer;}


//-----------------------------------------------------------------------------------------------------------------------------------\\
//                                                         TARGET                                                                    \\
//-----------------------------------------------------------------------------------------------------------------------------------\\
void ClearTargets()
{
    otherPlayerPref = GameObject.FindGameObjectsWithTag("Player");
    foreach(GameObject op in otherPlayerPref)
    {
        //if(myTurn == 1){Debug.Log("amTarget clear: "+amTarget);}
        op.GetComponent<BudgeIt>().amTarget=false; 
        op.transform.GetChild(6).gameObject.SetActive(false);
    }
}

//Check to see if an enemy is in target range
void CheckTarget()
{
    //Check if any enemy is in range
    otherPlayer = GameObject.FindGameObjectsWithTag("CollideObj");
    if(otherPlayer.Length > 0){

    bool foundTarget = false;
    //Determine range
    if(weaponType < 3 || weaponType == 5){_atkrange = 1.0f;}
    if(weaponType == 3){_atkrange = 3.0f;}
    if(weaponType == 4){_atkrange = 4.0f;}
    _atkrange = _atkrange*0.32f; //Set range to Unity units of pixels distance
    

    //Debug.Log("Do foreach?" + " Number of otherPlayer: " + otherPlayer.Length + ", "+Time.time);
    foreach(GameObject op in otherPlayer) //Cycle through all CollideObjs as they are only on enemy Players
    {
        //Debug.Log("Distance: " + Vector2.Distance(op.transform.position, transform.position) + ", atkRange: " + _atkrange + ", "+Time.time);
        if(Vector2.Distance(op.transform.position, transform.position) <= _atkrange && 
        op.GetComponent<PlayerCollision>().myParentId != myTurn && 
        (op.GetComponent<PlayerCollision>().myParentId == 1 && myTurn > 1 ||
        op.GetComponent<PlayerCollision>().myParentId > 1 && myTurn == 1) )
        {
            var nClosest = GameObject.FindGameObjectsWithTag("Player")//Find the Player object on this CollideObj to access it
                .OrderBy(o => (o.transform.position - op.transform.position).sqrMagnitude)
                .FirstOrDefault();

            //Debug.Log("Me Be Checking targets: "+myTurn + ", "+Time.time);
            if(nClosest != null)
            {
                foundTarget = true;  
                canAttack = true;  
                //Debug.Log("TARGET AQUIRED!, "+Time.time);  

                if(myTurn > 1){nClosest.GetComponent<BudgeIt>().Attack();}//Got deleted at some point
                nClosest.GetComponent<BudgeIt>().amTarget=true;
                nClosest.transform.GetChild(6).gameObject.SetActive(true); //Access enemy player
            }
        }

        if(!foundTarget && !canMove){canAttack = false; Debug.Log("canMove is false");}
        if(!foundTarget && _deadend){canAttack = false; Debug.Log("myturn: "+ myTurn + ", canAttack? "+canAttack + ", foundTarget: "+foundTarget);}
        
        foundTarget = false;
    }//End for each
    }//End other Player length
}//End Check target

//Player moved and Attacked, end their turn
void EndTurn()
{
    //Delete own collider
    otherPlayer = GameObject.FindGameObjectsWithTag("CollideObj");
    foreach(GameObject op in otherPlayer) //Cycle through all CollideObjs as they are only on enemy Players
    {
        if(op.GetComponent<PlayerCollision>().myParentId == myTurn){op.GetComponent<PlayerCollision>().Die();}
    }

    delayEndTurn = 1.0f+_waitafterturn+_webglbuffer; //Time.deltaTime+

    Debug.Log("I am "+myName+" and it is turn: "+TurnController.Turn+"! My turn is: "+myTurn+". delayEndTurn: "+delayEndTurn);
    //Debug.Log("I'm "+myTurn+" and I'm going to end turn after: "+delayEndTurn);
    endTurnMode = true;
}

void UpdateServer()
{
    if(myTurn == TurnController.Turn){
    GameObject sTalk; sTalk = GameObject.Find("GOD");
    sTalk.GetComponent<ServerTalker>().ProcessPost();}
}

//-----------------------------------------------------------------------------------------------------------------------------------\\
//-----------------------------------------------------------------------------------------------------------------------------------\\
//                                                         UPDATE                                                                    \\
//-----------------------------------------------------------------------------------------------------------------------------------\\
//-----------------------------------------------------------------------------------------------------------------------------------\\
void Update()
{
    //Debug.Log("_delayStep: "+_delayStep);
    if(_delayStep < 1)
    {
        //Debug.Log("_deadend: "+_deadend);
        if(!dead && (!_deadend || canAttack))
        {
            //callToTurn = TurnController.Turn;
            //Movement
            //Debug.Log("myTurn: " + myTurn + ", ServerPlayers: " + ServerTalker.playersTotal + ", MyPlayerIs " + ServerTalker.ThisPlayerIs + ", ThisPlayerIs: "+ServerTalker.ThisPlayerIs);
            if(TurnController.Turn == myTurn)// && (myTurn == ServerTalker.ThisPlayerIs || ServerTalker.SinglePlayerMode == true))
            {
                //Debug.Log("TurnController.Turn: "+TurnController.Turn+", myTurn:"+myTurn);
                if(eachTurn == true && delayStart < 1.0f)
                {
                    //Standard stuff to do at the start of turn
                    //Debug.Log("Dead? " + dead); //Test
                    GameObject sTalk; sTalk = GameObject.Find("GOD");
                    //if(sTalk.GetComponent<ServerTalker>().tDAction == 0){
                    //sTalk.GetComponent<ServerTalker>().checkNow = true;
                    transform.GetChild(5).gameObject.SetActive(true); 
                    ClearTargets();
                    CheckTarget(); 
                    eachTurn = false;//}
                    //Debug.Log("MeBeStarting: "+myTurn +  ", "+Time.time);
                }

                if(!eachTurn)
                {
                    //if(myTurn==2){Debug.Log("I was cleared!");}
                    //Player Input
                    if(canMove == true)// && _deadend == false)
                    {
                    //AI start of turn, find what direction player is
                    if(myTurn > 1)
                    {
                        //_waitafterturn = 2.0f;
                        float px=0; float py=0;     //Check if any enemy is in range
                        float mx=0; float my=0;     //Check if any enemy is in range
                        otherPlayer = GameObject.FindGameObjectsWithTag("Player");
                        foreach(GameObject op in otherPlayer) //Cycle through all CollideObjs as they are only on enemy Players
                        {
                            if(op.GetComponent<BudgeIt>().myTurn == 1)
                            {
                                px = op.transform.position.x;
                                py = op.transform.position.y;
                            }
                        }
                        
                        mx = transform.position.x; my = transform.position.y;
                        //Move to Player
                        //Move Right
                        if(Mathf.Abs(px-mx)>0.28f && px >= mx && _blockright == false){
                        if(Mathf.Abs(py-my)<0.36f){_nearTargetDir = 1;}
                        if(py < my && Mathf.Abs(py-my)>=0.36f){_nearTargetDir = 4;}
                        if(py > my && Mathf.Abs(py-my)>=0.36f){_nearTargetDir = 3;}
                        }
                        //Move Left
                        if(Mathf.Abs(px-mx)>0.28f && px < mx && _blockleft == false){
                        if(Mathf.Abs(py-my)<0.36f){_nearTargetDir = 2;}
                        if(py < my && Mathf.Abs(py-my)>=0.36f){_nearTargetDir = 4;}
                        if(py > my && Mathf.Abs(py-my)>=0.36f){_nearTargetDir = 3;}
                        }
                        //Move Up
                        if(Mathf.Abs(py-my)>0.28f && py >= my && _blockup == false){
                        if(Mathf.Abs(px-mx)<0.36f){_nearTargetDir = 3;}
                        if(px < mx && Mathf.Abs(px-mx)>=0.36f){_nearTargetDir = 2;}
                        if(px > mx && Mathf.Abs(px-mx)>=0.36f){_nearTargetDir = 1;}
                        }
                        //Move Down
                        if(Mathf.Abs(py-my)>0.28f && py < my && _blockdown == false){
                        if(Mathf.Abs(px-mx)<0.36f){_nearTargetDir = 4;}
                        if(px < mx && Mathf.Abs(px-mx)>=0.36f){_nearTargetDir = 2;}
                        if(px > mx && Mathf.Abs(px-mx)>=0.36f){_nearTargetDir = 1;}
                        }
                        //Debug.Log("px: "+px+", px: "+py+",,, px: "+mx+", px: "+px);

                        //if(myTurn==3){Debug.Log("Me move " + _nearTargetDir);}
                        if(_deadend == false){
                        if((_blockright && _nearTargetDir == 1 || _blockleft && _nearTargetDir == 2 || _blockup && _nearTargetDir == 3 || 
                        _blockdown && _nearTargetDir == 4) || _nearTargetDir == 0 || TurnController.PlayerDead == true){_deadend = true; CheckTarget();}//Can't move too bad
                        Debug.Log("_blockleft: "+_blockleft+", _nearTargetDir: "+_nearTargetDir+", _deadend: "+_deadend+", TurnController.PlayerDead: "+TurnController.PlayerDead);
                        }
                    }

                    if(_deadend == false){ 
                    if ((_nearTargetDir == 1 || (myTurn==1&&Input.GetKeyDown("right"))) && !Input.GetKey("left") && !Input.GetKey("up") && !Input.GetKey("down"))
                    {
                        BudgeRight();
                    }
                    if ((_nearTargetDir == 2 || (myTurn==1&&Input.GetKeyDown("left"))) && !Input.GetKey("right") && !Input.GetKey("up") && !Input.GetKey("down"))
                    {
                        BudgeLeft();
                    }
                    if ((_nearTargetDir == 3 || (myTurn==1&&Input.GetKeyDown("up"))) && !Input.GetKey("left") && !Input.GetKey("right") && !Input.GetKey("down"))
                    {
                        BudgeUp();
                    }
                    if ((_nearTargetDir == 4 || (myTurn==1&&Input.GetKeyDown("down"))) && !Input.GetKey("left") && !Input.GetKey("up") && !Input.GetKey("right"))
                    {
                        BudgeDown();
                    }
                    }
                }

                
                //Check Player end turn conditions
                if(canMove == false && canAttack == false && delayEndTurn < 1 && _checkCollide < 1 && endTurnMode == false)//
                {
                    Debug.Log("me is: "+ myTurn + ", callToTurn: "+callToTurn);
                    if(TurnController.Turn == myTurn){EndTurn();}
                }

                    _canMakeCollision = true;
                }//End check eachTurn
            }
            else
            {
                eachTurn = true;
                transform.GetChild(5).gameObject.SetActive(false);
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
        }//end check dead
        else
        {
            if(endTurnMode == false)//
            {
                if(TurnController.Turn == myTurn){EndTurn(); Debug.Log("I ended turn because I'm dead");}
            }
        }
                
        // int delayEndTurn = 0;
        // bool endTurnMode = false;
        if(endTurnMode)
        {
            if(dead == false){
            GameObject findGOD; findGOD = GameObject.Find("GOD");
            //if(findGOD.GetComponent<ServerTalker>().tDAction == 0)
            //{
            if(delayEndTurn > 0.0f){delayEndTurn -= Time.deltaTime; 
            //Debug.Log("delayEndTurn: " + delayEndTurn + ", TurnController.Turn: "+TurnController.Turn);
            } //*Time.time
            
            if(delayEndTurn<1){
            if(myTurn < 4){TurnController.Turn += 1;}else{TurnController.Turn = 1;}
            callToTurn = TurnController.Turn;
            canMove = true; movePoints = 3;
            canAttack = true;
            _deadend = false;
            _blockright = false;
            _blockleft = false;
            _blockup = false;
            _blockdown = false;
            _inrange = false;
            _attackonce = true;
            _attackgood = true;

            //End Phase
            if(myTurn==4){TurnController.TotalPhases++;}
            endTurnMode = false; 
            //ServerTalker.TakeTurn = callToTurn;  
            // findGOD.GetComponent<ServerTalker>().tDAction = 0; //What kind of attack was used on me; 1 = Melee, 2 = Spell, 3 = Self Skill, 4 = Self Spell 
            // findGOD.GetComponent<ServerTalker>().tDActionID = 0; //The Id for the action in a list
            // findGOD.GetComponent<ServerTalker>().tDTargetName = "z"; //My name (target of attack)

            GameObject[] allPlay; allPlay = GameObject.FindGameObjectsWithTag("Player");
            foreach(GameObject aP in allPlay)
            {
                if(aP.GetComponent<BudgeIt>().myTurn > 1)
                {
                    delayStart = 1.0f;
                }
            }

            //Debug.Log("I firednow!");}
            }
            }//end dead check
            else
            {    
                if(myTurn < 4){TurnController.Turn += 1;}else{TurnController.Turn = 1;}
                callToTurn = TurnController.Turn;
                endTurnMode = false;
            }
            //UpdateServer();
        }
    }//End _delayStep check


    //Death
    //Debug.Log("A Death! "+dead+", _processend: "+_processend);
    //Tally score
    if(dead && _processend == true)
    {
        //Delete own collider
        otherPlayer = GameObject.FindGameObjectsWithTag("CollideObj");
        foreach(GameObject op in otherPlayer) //Cycle through all CollideObjs as they are only on enemy Players
        {
            if(op.GetComponent<PlayerCollision>().myParentId == myTurn){op.GetComponent<PlayerCollision>().Die();}
        }
        //Disappear
        Renderer rend;
        rend = GetComponent<Renderer>();
        rend.enabled = false;

        //bloodSpatter
        Instantiate(bloodSpatter, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);

        //Move out of way
        transform.position = new Vector3(transform.position.x+10, transform.position.y);

        GameObject findGOD; findGOD = GameObject.Find("GOD");
        if(myTurn > 1)//Enemy died give score to player 1
        {
            //Killing
            findGOD.GetComponent<ServerTalker>().tDTotalKills += 1;
            Debug.Log("I died: "+findGOD.GetComponent<ServerTalker>().tDTotalKills);
            
            //Find player and add a kill
            otherPlayerPref = GameObject.FindGameObjectsWithTag("Player");
            foreach(GameObject op in otherPlayerPref)
            {
                if(op.GetComponent<BudgeIt>().myTurn==1) 
                {
                    op.GetComponent<BudgeIt>().kills+=1;
                    
                    //Winning
                    if(op.GetComponent<BudgeIt>().kills == 3)
                    {
                        findGOD.GetComponent<ServerTalker>().tDGamesPlayed += 1; //Process win
                        findGOD.GetComponent<ServerTalker>().tDGamesWon += 1; //Process win
                        findGOD.GetComponent<ServerTalker>().ExitTheGame("You won! Well done.");
                    }
                }
            }
        }
        else
        {
            //Losing
            findGOD.GetComponent<ServerTalker>().tDGamesPlayed += 1; //Process loss
            findGOD.GetComponent<ServerTalker>().ExitTheGame("You died, too bad.");   
        }
        
        //Winning
        // if(findGOD.GetComponent<ServerTalker>().tDTotalKills == 3)
        // { 
        //     findGOD.GetComponent<ServerTalker>().tDGamesPlayed += 1; //Process win
        //     findGOD.GetComponent<ServerTalker>().tDGamesWon += 1; //Process win
        //     findGOD.GetComponent<ServerTalker>().ExitTheGame();
        // }
        Debug.Log("tDGamesPlayed: "+findGOD.GetComponent<ServerTalker>().tDGamesPlayed +
        ", tDGamesWon: "+findGOD.GetComponent<ServerTalker>().tDGamesWon);
        _processend = false;
    }//End tally

    //Debug.Log("WHAT THE HECK!!!!!!!!!! delayStep: "+_delayStep+", TurnController.Turn: "+TurnController.Turn+", myTurn: "+myTurn);
    if(_delayStep > 0 && myTurn == TurnController.Turn)
    {
        _delayStep -= Time.deltaTime; 
        //if(myTurn ==2 && TurnController.TotalPhases==2){Debug.Log("Count down: "+delayStart);}
    }

    //if(myTurn ==1){Debug.Log("Count down: "+delayStart+", TurnController.Turn: "+TurnController.Turn);}
    if(delayStart > 0 && myTurn == TurnController.Turn)
    {
        delayStart -= Time.deltaTime; 
        //if(myTurn ==2 && TurnController.TotalPhases==2){Debug.Log("Count down: "+delayStart);}
    }

    if(_checkCollide > 0)
    {
        _checkCollide -= Time.deltaTime;
        if(_checkCollide < 1){CheckTarget();}
    }


}//End Update

//-----------------------------------------------------------------------------------------------------------------------------------\\
//                                                         ATTACK                                                                    \\
//-----------------------------------------------------------------------------------------------------------------------------------\\
void OnMouseDown()
{
    if(TurnController.Turn==1)
    {
        otherPlayerPref = GameObject.FindGameObjectsWithTag("Player");
        foreach(GameObject plyr in otherPlayerPref)
        {
            if(plyr.GetComponent<BudgeIt>().myTurn == 1 &&
            plyr.GetComponent<BudgeIt>().canAttack == true) 
            {
                Attack();
            }
        }
    }
}

void Attack()
{
    Debug.Log("----------------------------------------------------------------------\n"+
                    "I'm "+myTurn+" and I'm being attacked by AI");
    if(!dead)
    {
        //canAttack = false;
        //Debug.Log("On Mouse Down Worked");
        // this object was clicked - do something
        //Get Damage
        otherPlayerPref = GameObject.FindGameObjectsWithTag("Player");
        foreach(GameObject plyr in otherPlayerPref)
        {
            //Find active player and record their main attack damage
            //This will need to be overhauled for multiple skills and spells
            if(plyr.GetComponent<BudgeIt>().myTurn == TurnController.Turn &&
            plyr.GetComponent<BudgeIt>().canAttack == true) 
            {
                Debug.Log("I'm "+plyr.GetComponent<BudgeIt>().myName+" and I'm attacking with "+plyr.GetComponent<CharacterStats>().dmg+" damage!");
                _getstr = plyr.GetComponent<CharacterStats>().str;
                _getdmg = plyr.GetComponent<CharacterStats>().dmg;
                _getattackroll = plyr.GetComponent<CharacterStats>().attackRoll;
                plyr.GetComponent<BudgeIt>().canAttack = false;
                plyr.GetComponent<BudgeIt>().canMove = false;
                Debug.Log("Me is: "+plyr.GetComponent<BudgeIt>().myTurn+", canAttack: "+plyr.GetComponent<BudgeIt>().canAttack+", canMove: "+plyr.GetComponent<BudgeIt>().canMove);
                //Have player face attacking direction
                float ax = plyr.transform.position.x; float ay = plyr.transform.position.y; 
                float dx = transform.position.x; float dy = transform.position.y;
                if(ax < dx)
                {
                    if(Mathf.Abs(ax-dx) > Mathf.Abs(ay-dy) && Mathf.Abs(ay-dy)<0.35){plyr.GetComponent<BudgeIt>().FaceRight();}
                    if(Mathf.Abs(ay-dy) > Mathf.Abs(ax-dx) && ay>dy && ay-dy>=0.35){plyr.GetComponent<BudgeIt>().FaceDown();}
                    if(Mathf.Abs(ay-dy) > Mathf.Abs(ax-dx) && ay<dy && dy-ay>=0.35){plyr.GetComponent<BudgeIt>().FaceUp();}
                }
                if(ax > dx)
                {
                    if(Mathf.Abs(ax-dx) > Mathf.Abs(ay-dy) && Mathf.Abs(ay-dy)<0.35){plyr.GetComponent<BudgeIt>().FaceLeft();}
                    if(Mathf.Abs(ay-dy) > Mathf.Abs(ax-dx) && ay>dy && ay-dy>=0.35){plyr.GetComponent<BudgeIt>().FaceDown();}
                    if(Mathf.Abs(ay-dy) > Mathf.Abs(ax-dx) && ay<dy && dy-ay>=0.35){plyr.GetComponent<BudgeIt>().FaceUp();}
                }
                if(ay < dy)
                {
                    if(Mathf.Abs(ay-dy) > Mathf.Abs(ax-dx) && Mathf.Abs(ax-dx)<0.35){plyr.GetComponent<BudgeIt>().FaceUp();}
                    if(Mathf.Abs(ax-dx) > Mathf.Abs(ay-dy) && ax>dx && ax-dx>=0.35){plyr.GetComponent<BudgeIt>().FaceLeft();}
                    if(Mathf.Abs(ax-dx) > Mathf.Abs(ay-dy) && ax<dx && dx-ax>=0.35){plyr.GetComponent<BudgeIt>().FaceRight();}
                }
                if(ay > dy)
                {
                    if(Mathf.Abs(ay-dy) > Mathf.Abs(ax-dx) && Mathf.Abs(ax-dx)<0.35){plyr.GetComponent<BudgeIt>().FaceDown();}
                    if(Mathf.Abs(ax-dx) > Mathf.Abs(ay-dy) && ax>dx && ax-dx>=0.35){plyr.GetComponent<BudgeIt>().FaceLeft();}
                    if(Mathf.Abs(ax-dx) > Mathf.Abs(ay-dy) && ax<dx && dx-ax>=0.35){plyr.GetComponent<BudgeIt>().FaceRight();}
                }
            }//Check for attacking player end
        }//End for each attack block        

        //if(myTurn==1){Debug.Log("amTarget: "+amTarget);}//WRONG!!!!!! Gets turned to false for no reason
        if(TurnController.Turn != 1){amTarget = true;}//Set it back to true FFS
        if(amTarget)
        {
            Debug.Log("I'm running in the BudgeIt!");
            //Instantiate(bloodpf, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
            CharacterStats charStatsScript = GetComponent<CharacterStats>();
            int myTurnAC = charStatsScript.AC;
            System.Random rand = new System.Random();
            if(_getattackroll > 1)
            {
                if ((rand.Next(1, _getattackroll) + 2) >= myTurnAC) 
                {
                    charStatsScript.TakeDamage(_getstr, myTurn, myName, true, _getdmg);
                } 
                else 
                {
                    charStatsScript.Miss();
                }
            } 
            else 
            {
                charStatsScript.Miss();
            }
            //charStatsScript.hp -= _getstr; 
            Debug.Log("My Hp Left: " + charStatsScript.hp);
        }
    }//end dead check

    //OLD
    // //Tally score
    // if(dead && _processend == true)
    // {
    //     if(myTurn > 1)//Enemy died give score to player 1
    //     {
    //         GameObject findGOD; findGOD = GameObject.Find("GOD");
    //         findGOD.GetComponent<ServerTalker>().tDTotalKills += 1;
    //         kills += 1;
    //         if(kills == 3)
    //         { 
    //             findGOD.GetComponent<ServerTalker>().tDGamesPlayed += 1; //Process win
    //             findGOD.GetComponent<ServerTalker>().tDGamesWon += 1; //Process win
    //             findGOD.GetComponent<ServerTalker>().ExitTheGame();
    //         }
    //     }
    //     else
    //     {
    //         findGOD.GetComponent<ServerTalker>().tDGamesPlayed += 1; //Process loss
    //         findGOD.GetComponent<ServerTalker>().ExitTheGame();
    //     }
    //     _processend = false;
    // }
    
} //End Attack

//FireSpell
public void FireSpell(int spell)
{
    //firBallProj
    //mmProj   
    // GameObject ball = Instantiate(firBallProj, transform.position, transform.rotation);
    // ball.GetComponent<Rigidbody>().AddRelativeForce(new Vector3 (0, launchVelocity,0));
    
    // transform.position = new Vector3(transform.position.x, transform.position.y + 0.307f);
    // public float fbVelocity = 500f;
    // public float mmVelocity = 700f;
    GameObject clone = Instantiate (firBallProj, GetComponent<Rigidbody2D>().transform.position, Quaternion.identity);//Doesn't do anything
    clone.GetComponent<Rigidbody2D>().velocity = (clone.transform.forward * 1000);
}



}//End Class