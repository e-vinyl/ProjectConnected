using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class Computer : MonoBehaviour
{
    protected bool isEmpty = false;
    protected bool isCooledDown = false;

    protected AudioSource source;

    [SerializeField]
    protected AudioClip boot;

    public void Cooldown()
    {
        if (!isCooledDown)
        {
            Debug.Log("Computer cooled down");
            isCooledDown = true;
        }
    }

    private void Start()
    {
        source = GetComponent<AudioSource>();
        source.Play();
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

    IEnumerator FixComputer()
    {
        for (float volValue = source.volume; volValue > 0f; volValue -= 0.1f)
        {
            source.volume = volValue;
            yield return new WaitForEndOfFrame();
        }

        UI.Instance.PlayAudio(boot);
        source.Stop();
    }

    // TODO Temp
    public void FixBlender()
    {
        if(isCooledDown && isEmpty)
        {
            StartCoroutine(FixComputer());
            
            Blender blender = GameObject.FindObjectOfType<Blender>();
            if(blender != null)
            {
                blender.FixWiring();
            }
        }
    }
}
