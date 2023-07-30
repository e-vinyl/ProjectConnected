using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Animator))]
public class TrashBin : MonoBehaviour
{
    protected Animator animator;

    protected bool hasTrash = true;
    protected bool hasBody = false;

    [SerializeField]
    protected AudioClip trash;

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
            hasTrash = false;
            animator.SetBool("HasTrash", false);
            UI.Instance.PlayAudio(trash);
        }
    }

    public void OnBodyInteract(Object other)
    {
        Body body = other.GetComponent<Body>();

        if(body != null && body.IsSliced)
        {
            hasBody = true;
            animator.SetBool("HasTrash", true);
            StartCoroutine(BodyDisposed());
        }
    }

    IEnumerator BodyDisposed()
    {
        yield return new WaitForSeconds(1f);

        UI.Instance.GameEnded();
    }
}
