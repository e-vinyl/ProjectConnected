using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationMessageRelayer : MonoBehaviour
{
    public void OnFadeReady()
    {
        UI.Instance.FinishedAnimation();
    }
}
