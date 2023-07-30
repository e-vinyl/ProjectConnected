using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blender : MonoBehaviour
{
    protected bool isSpinning = false;

    public bool IsSpinning
    {
        get
        {
            return isSpinning;
        }
    }

    public void FixWiring()
    {
        Debug.Log("fixed blender");
        isSpinning = true;
    }

    
}
