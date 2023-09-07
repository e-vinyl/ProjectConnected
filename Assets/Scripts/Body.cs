using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Body : MonoBehaviour
{
    protected bool isSliced = false;

    protected Animator animator;

    [SerializeField]
    protected AudioClip grind;

    public bool IsSliced
    {
        get => isSliced;
    }

    public void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void OnTrashInteract(Object other)
    {
        if (isSliced)
        {
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Object>().enabled = false;
        }
    }

    public void OnBlenderLinked(Object other)
    {
        Blender blender = other.GetComponent<Blender>();
        if (blender == null || !blender.IsSpinning)
        {
            return;
        }

        if (!isSliced)
        {
            isSliced = true;
            GetComponent<Object>().CanPickUp = true;
            animator.SetTrigger("Bag");
            UI.Instance.PlayAudio(grind);
        }
    }
}
