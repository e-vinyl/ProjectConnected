using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Spoon : MonoBehaviour
{

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        MessageBroadcaster.Instance.Subscribe("OnStirred", OnStirred);
    }

    private void OnDestroy()
    {
        MessageBroadcaster.Instance.Unsubscribe("OnStirred", OnStirred);
    }

    private void OnStirred()
    {
        animator.SetTrigger("Stir");
    }
}
