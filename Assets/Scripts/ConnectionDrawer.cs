using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum CursorState
{
    None,
    Pickup,
    Link
}

[RequireComponent(typeof(LineRenderer))]
public class ConnectionDrawer : MonoBehaviour
{
    protected LineRenderer lineRenderer;

    protected Object leftConnection;
    protected Object rightConnection;

    [SerializeField]
    protected Animator cursor;

    [SerializeField]
    protected AudioClip magic;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
        lineRenderer.positionCount = 2;
        lineRenderer.startColor = lineRenderer.endColor = Object.LinkHighlightColor;

        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mouse2World = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        cursor.transform.position = new Vector3(mouse2World.x, mouse2World.y, 0f);

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
                    rightConnection.EnableHighlight(true);
                }
            }
            else if(rightConnection != null)
            {
                rightConnection.DisableHighlight();
                rightConnection = null;
            }
        }

        //Right Click Down
        if (Input.GetMouseButtonDown(1))
        {
            cursor.SetInteger("Type", (int)CursorState.Link);
            leftConnection = GetObjectUnderMouse();
            if (leftConnection != null)
            {
                lineRenderer.enabled = true;
                leftConnection.EnableHighlight(true);
            }
        }
        //Right Click Up
        else if(Input.GetMouseButtonUp(1))
        {
            if(rightConnection != null)
            {
                UIAudio.Instance.PlayAudio(magic);
                rightConnection.Link(leftConnection);
                rightConnection.DisableHighlight();
                Reset();
            }
            else
            {
                Reset();
            }

            cursor.SetInteger("Type", (int)CursorState.None);
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

    Object GetObjectUnderMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

        return hit.collider ? hit.collider.GetComponent<Object>() : null;
    }
}
