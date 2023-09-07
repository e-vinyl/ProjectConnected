using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Body : MonoBehaviour
{
    protected bool isSliced = false;

    protected Animator animator;
    protected SpriteRenderer spriteRenderer;
    protected Object objectBody;

    [SerializeField]
    protected AudioClip grind;

    public bool IsSliced
    {
        get => isSliced;
    }

    public void Start()
    {
        animator = GetComponent<Animator>();

        spriteRenderer = GetComponent<SpriteRenderer>();

        objectBody = GetComponent<Object>();
    }

    public void OnTrashInteract(Object other)
    {
        if (isSliced)
        {
            spriteRenderer.enabled = false;
            objectBody.enabled = false;
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
            objectBody.CanPickUp = true;
            animator.SetTrigger("Bag");
            UI.Instance.PlayAudio(grind);
        }
    }
}
