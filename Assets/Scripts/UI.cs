using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CursorState
{
    None,
    PickUp,
    Link
}

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(AudioSource))]
public class UI : MonoBehaviour
{
    private static UI instance;

    public static UI Instance
    {
        get
        {
            if(instance == null)
            {
                instance = GameObject.FindObjectOfType<UI>();
            }

            return instance;
        }
    }
    
    protected LineRenderer lineRenderer;
    
    protected AudioSource source;

    protected Object leftConnection;
    protected Object rightConnection;

    public delegate void ReadyEventHandler();

    public event ReadyEventHandler OnReady;

    protected CursorState cursorState;

    public CursorState CursorState
    {
        get
        {
            return cursorState;
        }

        set
        {
            cursorState = value;
            cursor.SetInteger("Type", (int)cursorState);
        }
    }

    public Vector3 CursorPosition
    {
        get
        {
            return cursor.transform.position;
        }
    }

    protected SpriteRenderer cursorSpriteRenderer;

    [SerializeField]
    protected Animator cursor;

    [SerializeField]
    protected AudioClip magic;
    
    [SerializeField]
    protected AudioClip magicWrong;

    [SerializeField]
    protected AudioClip selectedSound;

    private void Start()
    {
        source = GetComponent<AudioSource>();
        
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
        lineRenderer.positionCount = 2;
        lineRenderer.startColor = lineRenderer.endColor = Object.LinkHighlightColor;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
        
        cursorSpriteRenderer = cursor.GetComponent<SpriteRenderer>();
    }

    public void FinishedAnimation()
    {
        OnReady?.Invoke();
    }

    public void PlaySelectedSound()
    {
        PlayAudio(selectedSound);
    }

    public void PlayAudio(AudioClip clip)
    {
        source.PlayOneShot(clip, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mouse2World = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //TODO Get a more final way to clamp to screen
        
        //mouse2World.x = Mathf.Clamp(mouse2World.x, -Camera.main.pixelWidth / 2f, Camera.main.pixelWidth / 2f - cursorSpriteRenderer.bounds.size.x);
        //mouse2World.y = Mathf.Clamp(mouse2World.y, -Camera.main.pixelHeight / 2f + cursorSpriteRenderer.bounds.size.y, Camera.main.pixelHeight / 2f);
        
        cursor.transform.position = new Vector3(mouse2World.x, mouse2World.y, 0f);

        //Render magic line
        if (leftConnection != null)
        {
            lineRenderer.SetPosition(0, leftConnection.transform.position);
            lineRenderer.SetPosition(1, new Vector3(mouse2World.x, mouse2World.y, leftConnection.transform.position.z));

            Object hoveredObject = GetObjectUnderMouse();
            if(hoveredObject != null)
            {
                if(rightConnection == null && hoveredObject != leftConnection)
                {
                    rightConnection = hoveredObject;
                    rightConnection.EnableHighlight(HighlightType.Link);
                }
            }
            else if(rightConnection != null)
            {
                rightConnection.DisableHighlight();
                rightConnection = null;
            }
        }

        //Right Click Held 
        if (leftConnection == null && Input.GetMouseButton(1))
        {
            CursorState = CursorState.Link;
            leftConnection = GetObjectUnderMouse();
            if (leftConnection != null)
            {
                lineRenderer.enabled = true;
                leftConnection.EnableHighlight(HighlightType.Link);
            }
        }
        //Right Click Up
        else if(Input.GetMouseButtonUp(1))
        {
            if(rightConnection != null)
            {
                
                bool connectionSuccessful = rightConnection.Link(leftConnection);
                
                source.PlayOneShot(connectionSuccessful ? magic : magicWrong, 1f);
                
                rightConnection.DisableHighlight();
                Reset();
            }
            else
            {
                Reset();
            }

            CursorState = CursorState.None;
        }
    }

    protected void Reset()
    {
        if(leftConnection != null)
        {
            leftConnection.DisableHighlight();
            leftConnection = null;
        }

        if(rightConnection != null)
        {
            rightConnection.DisableHighlight();
            rightConnection = null;
        }

        lineRenderer.enabled = false;
    }

    private Object GetObjectUnderMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

        if(hit.collider != null)
        {
            Object obj = hit.collider.GetComponent<Object>();
            if(obj.enabled)
            {
                return obj;
            }
        }

        return null;
    }
}
