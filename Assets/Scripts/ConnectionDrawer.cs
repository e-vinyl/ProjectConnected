using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class ConnectionDrawer : MonoBehaviour
{
    protected LineRenderer lineRenderer;

    protected Object leftConnection;

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
        }

        //Right Click
        if (Input.GetMouseButtonDown(1))
        {
            Collider2D hitCollider = GetColliderUnderMouse();

            if (hitCollider != null)
            {
                lineRenderer.enabled = true;
                leftConnection = hitCollider.GetComponent<Object>();
            }

        }
        else if(Input.GetMouseButtonUp(1))
        {
            Collider2D hitCollider = GetColliderUnderMouse();

            if(hitCollider != null)
            {
                Object rightConnection = hitCollider.GetComponent<Object>();
                leftConnection.Link(rightConnection);
                
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
        leftConnection = null;
        lineRenderer.enabled = false;
    }

    Collider2D GetColliderUnderMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

        return hit.collider;
    }
}
