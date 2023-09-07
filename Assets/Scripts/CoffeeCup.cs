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
    protected AudioClip sugarStir;

    [SerializeField]
    protected AudioClip sugarPourInCoffee;

    [SerializeField]
    protected AudioClip coffeeStir;

    [SerializeField]
    protected GameObject coffee;

    [SerializeField]
    protected GameObject sugar;

    [SerializeField]
    protected GameObject spoon;

    public bool IsSpinning
    {
        get => isSpinning;
    }

    public void OnKettleInteract(Object other)
    {
        if(!hasCoffee)
        {
            UI.Instance.PlayAudio(coffeePour);
            coffee.GetComponent<SpriteRenderer>().enabled = true;
            hasCoffee = true;

            if(hasSugar)
            {
                sugar.GetComponent<Animator>().SetInteger("Type", 1);
            }
        }
    }

    public void OnSugarInteract(Object other)
    {
        if (!hasSugar)
        {
            sugar.GetComponent<SpriteRenderer>().enabled = true;

            coffee.GetComponent<Animator>().SetInteger("Type", 1);

            if (hasCoffee)
            {
                sugar.GetComponent<Animator>().SetInteger("Type", 1);
                UI.Instance.PlayAudio(sugarPourInCoffee);
            }
            else
            {
                UI.Instance.PlayAudio(sugarPour);
            }
            
            hasSugar = true;
        }
    }

    public void OnSpoonInteract(Object other)
    {
        UI.Instance.PlayAudio(coffeeStir);

        spoon.GetComponent<Animator>().SetTrigger("Stir");

        if(hasCoffee)
        {
            coffee.GetComponent<Animator>().SetTrigger("Stir");
        }
        else if(hasSugar)
        {
            UI.Instance.PlayAudio(sugarStir);
        }

        if (hasSugar && hasCoffee && !isSpinning)
        {
            sugar.GetComponent<Animator>().SetInteger("Type", 2);
            isSpinning = true;
        }
    }
}
