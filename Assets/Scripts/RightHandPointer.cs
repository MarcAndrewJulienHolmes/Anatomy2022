using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightHandPointer : MonoBehaviour
{
    public LineRenderer lineRenderer;

    public float lineWidth = 0.1f;
    public float lineMaxLength;

    public bool toggled;

    private float HandRight = OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger);

    public bool objectHitRight = false;

    private GameObject pointObject;

    // Start is called before the first frame update
    void Start()
    {
        Vector3[] startLinePositions = new Vector3[2] { Vector3.zero, Vector3.zero };
        lineRenderer.SetPositions(startLinePositions);
        lineRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        HandRight = OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger);

        if (HandRight > 0.9)
        {
            toggled = true;
            lineRenderer.enabled = true;
            Debug.LogError("Right hand trigger detected");
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

            pointObject = hit.collider.gameObject;

            if (pointObject.GetComponent<PointedAt>())
            {
                objectHitRight = true;

                pointObject.GetComponent<PointedAt>().rightHandRay = true;

                Debug.Log("Object hit value is: " + objectHitRight);
            }
            else
            {
                objectHitRight = false;

                Debug.Log("Object hit value is: " + objectHitRight);
            }
        }
        else if (objectHitRight)
        {
            objectHitRight = false;

            Debug.Log("Object hit value is: " + objectHitRight);

        }

        lineRenderer.SetPosition(0, targetPosition);
        lineRenderer.SetPosition(1, endPosition);
    }
}
