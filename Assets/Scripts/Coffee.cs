using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class Coffee : MonoBehaviour
{

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        MessageBroadcaster.Instance.Subscribe("OnCoffeePoured", OnCoffeePoured);
        MessageBroadcaster.Instance.Subscribe("OnSugarPouredOnCoffee", OnSugarPouredOnCoffee);
        MessageBroadcaster.Instance.Subscribe("OnCoffeeStirred", OnCoffeeStirred);
    }

    private void OnDestroy()
    {
        MessageBroadcaster.Instance.Unsubscribe("OnCoffeePoured", OnCoffeePoured);
        MessageBroadcaster.Instance.Unsubscribe("OnSugarPouredOnCoffee", OnSugarPouredOnCoffee);
        MessageBroadcaster.Instance.Unsubscribe("OnCoffeeStirred", OnCoffeeStirred);
    }

    private void OnCoffeePoured()
    {
        spriteRenderer.enabled = true;
    }

    private void OnSugarPouredOnCoffee()
    {
        animator.SetInteger("Type", 1);
    }

    private void OnCoffeeStirred()
    {
        animator.SetTrigger("Stir");
    }
}
