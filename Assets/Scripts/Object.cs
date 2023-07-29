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

public interface IInteractive : IEventSystemHandler
{
    void OnOverlap(IInteractive other);
    void OnExitOverlap(IInteractive other);
    void Interact(IInteractive other);
    void Link(IInteractive other);

    string getTag();
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
public class Object : MonoBehaviour, IInteractive
{
    protected SpriteRenderer sprite;

    protected Vector3 slot;
    protected float originalZ;

    protected ObjectState state = ObjectState.None;

    protected IInteractive overlappingObject;

    [SerializeField]
    protected string interactiveTag;

    [SerializeField]
    protected List<TaggableEvents> interactionEvents;

    [SerializeField]
    protected List<TaggableEvents> linkEvents;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();

        slot = transform.position;
        originalZ = slot.z;
    }

    private void ResetObject()
    {
        state = ObjectState.None;
        transform.position = slot;
        overlappingObject = null;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (state == ObjectState.None)
        {
            ExecuteEvents.Execute<IInteractive>(collision.gameObject, null, (x, y) => x.OnOverlap(this));
            sprite.color = Color.blue;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (state == ObjectState.None)
        {
            ExecuteEvents.Execute<IInteractive>(collision.gameObject, null, (x, y) => x.OnExitOverlap(this));
            sprite.color = Color.white;
        }
    }

    public void OnOverlap(IInteractive other)
    {
        if(overlappingObject != null)
        {
            overlappingObject = other;
        }
    }

    public void OnExitOverlap(IInteractive other)
    {
        if (overlappingObject == other)
        {
            overlappingObject = null;
        }
    }

    public void Interact(IInteractive overlappingObject)
    {
        TaggableEvents foundEvent = interactionEvents.Find((x) => x.tag == overlappingObject.getTag());

        if(foundEvent != null)
        {
            foundEvent.interactiveEvent?.Invoke();
        }
    }

    public string getTag()
    {
        return interactiveTag;
    }

    public void Link(IInteractive other)
    {
        Debug.Log("Linkin " + this + " with " + other);
    }
}
