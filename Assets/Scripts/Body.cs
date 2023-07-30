using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour
{
    protected bool isSliced = false;

    public bool IsSliced
    {
        get
        {
            return isSliced;
        }
    }

    public void OnTrashInteract(Object other)
    {
        Debug.Log("Got rid of body");
        //GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Object>().enabled = false;
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
            Debug.Log("Chopped body");
            isSliced = true;
            GetComponent<Object>().CanPickUp = true;
        }
    }
}
