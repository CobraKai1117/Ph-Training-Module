using System;
using UnityEngine;

[System.Serializable]

public class BeakerPositions
{
    public Vector3 Ph10BottleTransform
    {
        get { return ph10BottleTransform;  }
        set { ph10BottleTransform = value; }
    }

    public Vector3 Ph7BottleTransform
    {
        get { return ph7BottleTransform; }
        set { ph7BottleTransform = value; }
    }

    public Vector3 Ph4BottleTransform
    {
        get { return ph4BottleTransform; }
        set { ph4BottleTransform = value; }
    }

    Vector3 ph10BottleTransform;
    Vector3 ph7BottleTransform;
    Vector3 ph4BottleTransform;
}
