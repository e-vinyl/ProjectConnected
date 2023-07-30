using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Animator))]
public class Turntable : MonoBehaviour
{

    protected AudioSource source;
    protected Animator animator;

    private void Awake()
    {
        UI.Instance.OnReady += OnLevelReady;
    }

    private void Start()
    {
        
        source = GetComponent<AudioSource>();
        
        animator = GetComponent<Animator>();
    }

    public void OnLevelReady()
    {
        source.pitch = 0.5f;
        source.Play();

        animator.speed = 0.5f;
        animator.enabled = true;

        UI.Instance.OnReady -= OnLevelReady;
    }

    public void OnCoffeeLinked(Object other)
    {
        CoffeeCup cup = other.GetComponent<CoffeeCup>();

        if(cup != null && cup.IsSpinning)
        {
            StartCoroutine(FixTurntable());
        }
    }

    IEnumerator FixTurntable()
    {
        for (float pitchValue = source.pitch; pitchValue < 1f; pitchValue += 0.05f)
        {
            source.pitch = pitchValue;
            animator.speed = pitchValue;
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(1f);

        UI.Instance.LevelEnded();
    }
}
