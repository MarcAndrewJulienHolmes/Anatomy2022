using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightHandPointer : MonoBehaviour
{
    public LineRenderer lineRenderer;

    public Material normal, highlighted;

    public float lineWidth = 0.1f;
    public float flexibleLineLength;
    public float lineMaxLength;

    public int layerMask = 1;

    public Vector3 highlightedObjectPostition;

    public bool toggled;

    private float handRight = OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger);

    public bool objectHitRight = false;

    private GameObject pointObject;

    // Start is called before the first frame update
    void Start()
    {
        Vector3[] startLinePositions = new Vector3[2] { Vector3.zero, Vector3.zero };
        lineRenderer.SetPositions(startLinePositions);
        lineRenderer.enabled = false;
        lineRenderer.material = normal;
    }

    // Update is called once per frame
    void Update()
    {
        handRight = OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger);

        if (handRight > 0.9)
        {
            toggled = true;
            lineRenderer.enabled = true;
            //Debug.LogError("Right hand trigger detected");
        }
        else
        {
            lineRenderer.enabled = false;
            toggled = false;
            objectHitRight = false;
        }

        if (toggled)
        {
            ActiveLineRenderer(transform.position, transform.forward, lineMaxLength);
        }
    }

    private void ActiveLineRenderer(Vector3 targetPosition, Vector3 direction, float length)
    {
        RaycastHit hit;

        Ray lineRendererOut = new Ray(targetPosition, direction);

        Vector3 endPosition = targetPosition + (length * direction);

        if (Physics.Raycast(lineRendererOut, out hit))
        {
            endPosition = hit.point;

            highlightedObjectPostition = endPosition;

            //flexibleLineLength = 2f;

            pointObject = hit.collider.gameObject;

            if (pointObject.GetComponent<HighlightedObject>())
            {
                objectHitRight = true;

                lineRenderer.material = highlighted; 

                if (OVRInput.Get(OVRInput.Button.One))
                {
                    pointObject.GetComponent<HighlightedObject>().rightHandRay = true;
                }

                //Debug.Log("Object hit value is: " + objectHitRight);
            }
            else
            {
                objectHitRight = false;

                lineRenderer.material = normal;

                //Debug.Log("Object hit value is: " + objectHitRight);
            }
        }
        else if (objectHitRight)
        {
            objectHitRight = false;

            lineRenderer.material = normal;

            //Debug.Log("Object hit value is: " + objectHitRight);

        }

        lineRenderer.SetPosition(0, targetPosition);
        lineRenderer.SetPosition(1, endPosition);
    }
}
