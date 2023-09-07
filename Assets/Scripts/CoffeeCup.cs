using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Object))]
public class CoffeeCup : MonoBehaviour
{
    
    protected bool hasSugar = false;
    protected bool hasCoffee = false;
    protected bool isSpinning = false;

    private Object objectBody;

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

    private void Awake()
    {
        objectBody = GetComponent<Object>();
    }

    public void OnKettleInteract(Object other)
    {
        if(!hasCoffee)
        {
            UI.Instance.PlayAudio(coffeePour);

            MessageBroadcaster.Instance.BroadcastEvent("OnCoffeePoured");

            objectBody.CanPickUp = false;

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

            objectBody.CanPickUp = false;

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
