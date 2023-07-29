using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;




public enum ObjectState
{
    None,
    PickedUp
}

[Serializable]
public class TaggableEvents
{
    public string tag;
    public UnityEvent<Object> interactiveEvent;
}

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
public class Object : MonoBehaviour
{
    public static readonly Color PickUpHighlightColor = new Color(0.5f, 0.9f, 0.7f, 1f);
    public static readonly Color LinkHighlightColor = new Color(0.34f, 0.81f, 1f, 1f);

    protected SpriteRenderer sprite;

    protected Vector3 slot;
    protected float originalZ;

    protected ObjectState state = ObjectState.None;

    protected Object overlappingObject;

    [SerializeField]
    protected SpriteRenderer highlightRender;

    [SerializeField]
    protected string interactiveTag;

    [SerializeField]
    protected Vector2 interactPivot;

    [SerializeField]
    protected List<TaggableEvents> interactionEvents;

    [SerializeField]
    protected List<TaggableEvents> linkEvents;

    [SerializeField]
    private bool canPickup = true;

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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Vector3 gizmoLocation = transform.position;
        gizmoLocation.x += interactPivot.x;
        gizmoLocation.y += interactPivot.y;

        Gizmos.DrawWireSphere(gizmoLocation, 5);
    }

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();

        slot = transform.position;
        originalZ = slot.z;

        foreach(SpriteRenderer renderer in GetComponentsInChildren<SpriteRenderer>())
        {
            renderer.sortingLayerID = SortingLayer.layers[0].id;
        }
        
        DisableHighlight();
    }

    private void ResetObject()
    {
        state = ObjectState.None;
        transform.position = slot;
        overlappingObject = null;

        foreach (SpriteRenderer renderer in GetComponentsInChildren<SpriteRenderer>())
        {
            renderer.sortingLayerID = SortingLayer.layers[0].id;
        }
    }

    private void OnMouseDrag()
    {
        if(state == ObjectState.PickedUp)
        {
            Vector3 mouse2World = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(mouse2World.x - interactPivot.x, mouse2World.y - interactPivot.y, originalZ);
        }
    }

    private void OnMouseDown()
    {
        if(canPickup)
        {
            state = ObjectState.PickedUp;

            foreach (SpriteRenderer renderer in GetComponentsInChildren<SpriteRenderer>())
            {
                renderer.sortingLayerID = SortingLayer.layers[1].id;
            }
        }
    }

    private void OnMouseUp()
    {
        if(overlappingObject != null)
        {
            Interact(overlappingObject);
            overlappingObject.Interact(this);
            ResetObject();
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

    public void Interact(Object other)
    {
        TaggableEvents foundEvent = interactionEvents.Find((x) => x.tag == other.interactiveTag);

        if (foundEvent != null)
        {
            foundEvent.interactiveEvent?.Invoke(other);
        }
    }

    public void Link(Object other)
    {
        TaggableEvents foundEvent = linkEvents.Find((x) => x.tag == other.interactiveTag);

        if (foundEvent != null)
        {
            foundEvent.interactiveEvent?.Invoke(other);
        }
    }

    public void EnableHighlight(bool magical = false)
    {
        highlightRender.enabled = true;
        highlightRender.color = magical ? LinkHighlightColor : PickUpHighlightColor;
    }

    public void DisableHighlight()
    {
        highlightRender.enabled = false;
    }
}
