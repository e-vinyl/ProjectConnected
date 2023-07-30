using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class TrashBin : MonoBehaviour
{
    protected bool hasTrash = true;
    protected bool hasBody = false;

    public bool HasTrash
    {
        get
        {
            return hasTrash;
        }
    }

    public void TakeOutTrash()
    {
        if(hasTrash)
        {
            Debug.Log("No trash");
            hasTrash = false;
        }
    }

    public void OnBodyInteract(Object other)
    {
        Body body = other.GetComponent<Body>();

        if(body != null && body.IsSliced)
        {
            hasBody = true;
            Debug.Log("Tash has body");
        }
    }
}
