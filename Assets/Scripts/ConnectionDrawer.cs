using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class ConnectionDrawer : MonoBehaviour
{
    protected LineRenderer lineRenderer;

    protected Object leftConnection;
    protected Object rightConnection;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
        lineRenderer.positionCount = 2;
    }

    // Update is called once per frame
    void Update()
    {
        if(leftConnection != null)
        {
            Vector3 mouse2World = Camera.main.ScreenToWorldPoint(Input.mousePosition);

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
                rightConnection.Link(leftConnection);
                rightConnection.DisableHighlight();
                Reset();
            }
            else
            {
                Reset();
            }
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
