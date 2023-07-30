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
    protected AudioClip sugarPourInCoffee;

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
            UI.Instance.PlayAudio(coffeePour);
            coffeeRenderer.enabled = true;
            hasCoffee = true;
        }
    }

    public void OnSugarInteract(Object other)
    {
        if (!hasSugar)
        {
            UI.Instance.PlayAudio(hasCoffee ? sugarPourInCoffee : sugarPour);
            hasSugar = true;
        }
    }

    public void OnSpoonInteract(Object other)
    {
        UI.Instance.PlayAudio(coffeeStir);

        if (hasSugar && hasCoffee && !isSpinning)
        {    
            isSpinning = true;
        }
    }
}
