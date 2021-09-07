using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPiece : MonoBehaviour
{
    //Properties...
    public bool isColored;

    //Function handling ground piece change of color...
    public void ChangeColor(Color color)
    {
        //if instance of GroundPiece isColored is set to false then proceed...
        if(!isColored)
        {
            GetComponent<MeshRenderer>().material.color = color;
            isColored = true;
        }
        //If GameManager isFinished is set to false then proceed....
        if(!(GameManager.singleton.isFinished))
        {
            GameManager.singleton.CheckComplete();
        }
    }
}
