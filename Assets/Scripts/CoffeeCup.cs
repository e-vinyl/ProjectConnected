using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeCup : MonoBehaviour
{
    
    protected bool hasSugar = false;
    protected bool hasCoffee = false;
    protected bool isSpinning = false;

    public bool IsSpinning
    {
        get
        {
            return isSpinning;
        }
    }

    public void OnKettleInteract(Object other)
    {
        if(!hasCoffee)
        {
            Debug.Log("Cup has coffee");
            hasCoffee = true;
        }
    }

    public void OnSugarInteract(Object other)
    {
        if (!hasSugar)
        {
            Debug.Log("Cup has sugar");
            hasSugar = true;
        }
    }

    public void OnSpoonInteract(Object other)
    {
        if (hasSugar && hasCoffee && !isSpinning)
        {
            Debug.Log("Coffee is spinning");
            isSpinning = true;
        }
    }
}
