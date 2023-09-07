using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class Sugar : MonoBehaviour
{

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        MessageBroadcaster.Instance.Subscribe("OnCoffeePouredOnSugar", OnCoffeePouredOnSugar);
        MessageBroadcaster.Instance.Subscribe("OnSugarPouredOnCoffee", OnCoffeePouredOnSugar);
        MessageBroadcaster.Instance.Subscribe("OnSugarDissolved", OnSugarDissolved);
        MessageBroadcaster.Instance.Subscribe("OnSugarAdded", OnSugarAdded);
    }

    private void OnDestroy()
    {
        MessageBroadcaster.Instance.Unsubscribe("OnCoffeePouredOnSugar", OnCoffeePouredOnSugar);
        MessageBroadcaster.Instance.Unsubscribe("OnSugarPouredOnCoffee", OnCoffeePouredOnSugar);
        MessageBroadcaster.Instance.Unsubscribe("OnSugarDissolved", OnSugarDissolved);
    }

    private void OnCoffeePouredOnSugar()
    {
        animator.SetInteger("Type", 1);
    }

    private void OnSugarDissolved()
    {
        animator.SetInteger("Type", 2);
    }

    private void OnSugarAdded()
    {
        spriteRenderer.enabled = true;
    }
}
