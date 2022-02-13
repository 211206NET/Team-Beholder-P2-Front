using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotQtyText : MonoBehaviour
{

    private string _getqty = "";
    public Text _Text;
    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Should show: "+_getqty);
        _getqty = HPPot.Qty.ToString();
        _Text.text = _getqty;
    }
}
