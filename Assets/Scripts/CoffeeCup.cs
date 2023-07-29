using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CoffeeCup : MonoBehaviour
{
    
    protected bool hasSugar = false;
    protected bool hasCoffee = false;
    protected bool isSpinning = false;

    [SerializeField]
    protected AudioClip coffeePour;

    [SerializeField]
    protected AudioClip sugarPour;

    [SerializeField]
    protected AudioClip coffeeStir;

    [SerializeField]
    protected SpriteRenderer coffeeRenderer;

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
            UIAudio.Instance.PlayAudio(coffeePour);
            coffeeRenderer.enabled = true;
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
