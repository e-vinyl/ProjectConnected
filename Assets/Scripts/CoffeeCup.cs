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

    public bool IsSpinning
    {
        get => isSpinning;
    }

    public void OnKettleInteract(Object other)
    {
        if(!hasCoffee)
        {
            UI.Instance.PlayAudio(coffeePour);

            MessageBroadcaster.Instance.BroadcastEvent("OnCoffeePoured");

            hasCoffee = true;

            if(hasSugar)
            {
                MessageBroadcaster.Instance.BroadcastEvent("OnCoffeePouredOnSugar");
            }
        }
    }

    public void OnSugarInteract(Object other)
    {
        if (!hasSugar)
        {
            MessageBroadcaster.Instance.BroadcastEvent("OnSugarAdded");

            if (hasCoffee)
            {
                MessageBroadcaster.Instance.BroadcastEvent("OnSugarPouredOnCoffee");
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

        MessageBroadcaster.Instance.BroadcastEvent("OnStirred");

        if (hasCoffee)
        {
            MessageBroadcaster.Instance.BroadcastEvent("OnCoffeeStirred");
        }
        else if(hasSugar)
        {
            UI.Instance.PlayAudio(sugarStir);
        }

        if (hasSugar && hasCoffee && !isSpinning)
        {
            MessageBroadcaster.Instance.BroadcastEvent("OnSugarDissolved");
            isSpinning = true;
        }
    }
}
