using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class savedObject
{
    public string nameOfObject;
    public float[] objPos = new float[3];
    public float[] objRot = new float[4];
    public float[] objScale = new float[3];
    public float[] childScale = new float[3];
    public string nameOfMaterial;
    




    public savedObject(string objectName, float[] ObjectPosition, float[] ObjectRot, float[] ObjectScale) // Used for PH Containers
    {

    }


    public savedObject(string objName, float[] objPos, float[] objRot, float[] objScale, float[] childScale, string ObjMat) //Used for the beakers
    {





    }

    public savedObject()
    {

    }
}



