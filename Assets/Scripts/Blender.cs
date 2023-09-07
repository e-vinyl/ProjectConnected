using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blender : MonoBehaviour
{
    protected bool isSpinning = false;

    public bool IsSpinning
    {
        get => isSpinning;
    }

    private void Awake()
    {
        MessageBroadcaster.Instance.Subscribe("OnWireFixed", OnWireFixed);
    }

    private void OnDestroy()
    {
        MessageBroadcaster.Instance.Unsubscribe("OnWireFixed", OnWireFixed);
    }

    public void OnWireFixed()
    {
        isSpinning = true;
    }

    
}
