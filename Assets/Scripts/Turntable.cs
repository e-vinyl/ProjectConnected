using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Turntable : MonoBehaviour
{

    protected AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
        source.pitch = 0.5f;
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
        for (float pitchValue = source.pitch; pitchValue < 1f; pitchValue += 0.001f)
        {
            source.pitch = pitchValue;
            yield return null;
        }
    }
}
