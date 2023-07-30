using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Opening : MonoBehaviour
{
    public void OnTrashBinInteract(Object other)
    {
        TrashBin trash = other.GetComponent<TrashBin>();

        if(trash != null)
        {
            trash.TakeOutTrash();
        }
    }
}
