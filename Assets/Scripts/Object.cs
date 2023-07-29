using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;


public enum ObjectState
{
    None,
    PickedUp,
    Interacting
}

[Serializable]
public class TaggableEvents
{
    public string tag;
    public UnityEvent interactiveEvent;
}

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
public class Object : MonoBehaviour
{
    protected SpriteRenderer sprite;

    protected Vector3 slot;
    protected float originalZ;

    protected ObjectState state = ObjectState.None;

    protected Object overlappingObject;

    [SerializeField]
    protected string interactiveTag;

    [SerializeField]
    protected List<TaggableEvents> interactionEvents;

    [SerializeField]
    protected List<TaggableEvents> linkEvents;

    public ObjectState State
    {
        get => state;
    }

    public string Tag
    {
        get => tag;
    }

    public bool CanInteract
    {
        get
        {
            return overlappingObject == null && state == ObjectState.PickedUp;
        }
    }

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();

        slot = transform.position;
        originalZ = slot.z;

        sprite.sortingLayerID = SortingLayer.layers[0].id;
    }

    private void ResetObject()
    {
        state = ObjectState.None;
        transform.position = slot;
        overlappingObject = null;

        sprite.sortingLayerID = SortingLayer.layers[0].id;
    }

    private void OnMouseDrag()
    {
        if(state == ObjectState.PickedUp)
        {
            Vector3 mouse2World = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(mouse2World.x, mouse2World.y, originalZ);
        }
    }

    private void OnMouseDown()
    {
        state = ObjectState.PickedUp;
        sprite.sortingLayerID = SortingLayer.layers[1].id;
    }

    private void OnMouseUp()
    {
        if(overlappingObject != null)
        {
            Interact(overlappingObject);
            overlappingObject.Interact(this);
        }
        else
        {
            ResetObject();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Object triggeredObject = collision.GetComponent<Object>();

        if (triggeredObject != null &&
            triggeredObject.CanInteract &&
            state == ObjectState.None)
        {
            
            triggeredObject.OnOverlap(this);
            EnableHighlight();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Object triggeredObject = collision.GetComponent<Object>();

        if (triggeredObject != null &&
            state == ObjectState.None)
        {
            triggeredObject.OnExitOverlap(this);
            DisableHighlight();
        }
    }

    public void OnOverlap(Object other)
    {
        if(overlappingObject == null)
        {
            overlappingObject = other;
        }
    }

    public void OnExitOverlap(Object other)
    {
        if (overlappingObject == other)
        {
            overlappingObject = null;
        }
    }

    public void Interact(Object overlappingObject)
    {
        TaggableEvents foundEvent = interactionEvents.Find((x) => x.tag == overlappingObject.Tag);

        if(foundEvent != null)
        {
            foundEvent.interactiveEvent?.Invoke();
        }
    }

    public void Link(Object other)
    {
        Debug.Log("Linkin " + this + " with " + other);
    }

    public void EnableHighlight(bool magical = false)
    {
        sprite.color = magical ? Color.red : Color.blue;
    }

    public void DisableHighlight()
    {
        sprite.color = Color.white;
    }
}
