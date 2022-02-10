using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public int myParentId = 0; //Players give PF/FK id to collider obj so it cna be destroyed on that player's turn


    public void Die()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(myParentId == TurnController.Turn){Die();}//Die when turn of owner player
    }
}
