using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Computer : MonoBehaviour
{
    protected bool isEmpty = false;
    protected bool isCooledDown = false;

    public void Cooldown()
    {
        if (!isCooledDown)
        {
            Debug.Log("Computer cooled down");
            isCooledDown = true;
        }
    }

    public void OnTrashLinked(Object other)
    {
        TrashBin bin = other.GetComponent<TrashBin>();

        if (bin != null && !bin.HasTrash && !isEmpty)
        {
            Debug.Log("Computer emptied Ram");
            isEmpty = true;
            FixBlender();
        }
    }

    public void OnFridgeLinked(Object other)
    {
        if (!isCooledDown)
        {
            Debug.Log("Computer cooled down");
            isCooledDown = true;
            FixBlender();
        }
    }

    // TODO Temp
    public void FixBlender()
    {
        if(isCooledDown && isEmpty)
        {
            Blender blender = GameObject.FindObjectOfType<Blender>();
            if(blender != null)
            {
                blender.FixWiring();
            }
        }
    }
}
