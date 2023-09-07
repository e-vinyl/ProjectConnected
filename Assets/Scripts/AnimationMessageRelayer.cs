using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationMessageRelayer : MonoBehaviour
{
    public GameObject level2;

    private void Start()
    {
        MessageBroadcaster.Instance.Subscribe("OnLevelEnded", OnLevelEnded);
        MessageBroadcaster.Instance.Subscribe("OnGameEnded", OnGameEnded);
    }

    private void OnDestroy()
    {
        MessageBroadcaster.Instance.Unsubscribe("OnLevelEnded", OnLevelEnded);
        MessageBroadcaster.Instance.Unsubscribe("OnGameEnded", OnGameEnded);
    }

    public void OnFadeReady()
    {
        MessageBroadcaster.Instance.BroadcastEvent("OnLevelReady");
    }

    public void OnLevelFaded()
    {
        level2.SetActive(true);
        Camera.main.transform.position = new Vector3(240f, 0f, -10f);
    }

    public void OnLevelEnded()
    {
        GetComponent<Animator>().SetTrigger("Fade");
    }

    public void OnGameEnded()
    {
        GetComponent<Animator>().SetTrigger("End");
    }
}
