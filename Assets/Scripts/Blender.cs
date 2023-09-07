using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blender : MonoBehaviour
{
    protected bool isSpinning = false;

    public bool IsSpinning
    {
        get => isSpinning;
    }

    public void FixWiring()
    {
        isSpinning = true;
    }

    
}
