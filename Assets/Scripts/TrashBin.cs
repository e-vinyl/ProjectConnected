using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Animator))]
public class TrashBin : MonoBehaviour
{
    protected Animator animator;

    protected bool hasTrash = true;
    protected bool hasBody = false;

    public bool HasTrash
    {
        get
        {
            return hasTrash;
        }
    }

    public void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("HasTrash", true);
    }

    public void TakeOutTrash()
    {
        if(hasTrash)
        {
            Debug.Log("doing");
            hasTrash = false;
            animator.SetBool("HasTrash", false);
        }
    }

    public void OnBodyInteract(Object other)
    {
        Body body = other.GetComponent<Body>();

        if(body != null && body.IsSliced)
        {
            hasBody = true;
            animator.SetBool("HasTrash", true);
        }
    }
}
