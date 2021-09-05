using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPiece : MonoBehaviour
{
    //Properties...
    public bool isColored;
    public void ChangeColor(Color color)
    {
        if(!isColored)
        {
            GetComponent<MeshRenderer>().material.color = color;
            isColored = true;
        }
        if(!(GameManager.singleton.isFinished))
        {
            GameManager.singleton.CheckComplete();
        }
        
    }
}
