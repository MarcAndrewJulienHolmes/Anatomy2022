using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class RightHandPointer : MonoBehaviour
{
    public LineRenderer lineRenderer;

    public Material normal, highlighted;

    public float lineWidth = 0.1f;
    public float flexibleLineLength;

    public int layerMask = 1;

    public Vector3 endPosition;

    public bool toggled;

    public bool objectHitRight = false;

    public GameObject pointObject, currenHighlightedObject;

    public string currentHighlightedObjectName;

    public UnityEvent rightHandSelect;
    public UnityEvent rightHandMove;
    public UnityEvent rightHandDeselect;



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

        if (OVRInput.Get(OVRInput.Button.Two))
        {
            rightHandDeselect.Invoke();
        }

        if (OVRInput.Get(OVRInput.Button.One))
        {
            if (currentHighlightedObjectName != null)
            {
                currenHighlightedObject.GetComponent<SelectedObject>().ReturnToOrigin();
            }
            else
            {
                return;
            }
        }

        if (OVRInput.Get(OVRInput.Button.SecondaryThumbstickDown))
        {
            if (currentHighlightedObjectName != null)
            {
                currenHighlightedObject.GetComponent<SelectedObject>().MoveToAttach();
                //currenHighlightedObject.GetComponent<HighlightedObject>().MoveTowardsAttach();
            }
            else
            {
                return;
            }
        }

        if (OVRInput.Get(OVRInput.Button.SecondaryHandTrigger))
        {
            //Debug.LogError("trigger");

        }


        if (OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) > 0.9)
        {
            toggled = true;
            lineRenderer.enabled = true;
        }
        else
        {
            lineRenderer.enabled = false;
            toggled = false;
            objectHitRight = false;
        }

        if (toggled)
        {
            ActiveLineRenderer(transform.position, transform.forward, flexibleLineLength);
        }
    }

 

    public void ActiveLineRenderer(Vector3 targetPosition, Vector3 direction, float length)
    {
        RaycastHit hit;

        Ray lineRendererOut = new Ray(targetPosition, direction);

        endPosition = targetPosition + (length * direction);

        if (Physics.Raycast(lineRendererOut, out hit))
        {
            endPosition = hit.point;

            pointObject = hit.collider.gameObject;

            if (pointObject.GetComponent<SelectedObject>())

            //if (pointObject.GetComponent<HighlightedObject>())
            {
                currentHighlightedObjectName = pointObject.GetComponent<SelectedObject>().thisGameObjectName;

                //currentHighlightedObjectName = pointObject.GetComponent<HighlightedObject>().thisGameObjectName;

                currenHighlightedObject = GameObject.Find(currentHighlightedObjectName);

                objectHitRight = true;

                lineRenderer.material = highlighted; 

                if (OVRInput.Get(OVRInput.Button.SecondaryHandTrigger))
                {
                    currenHighlightedObject.GetComponent<SelectedObject>().rightHandRay = true;
                    currenHighlightedObject.GetComponent<SelectedObject>().ActivateSelect();
                    
                    //currenHighlightedObject.GetComponent<HighlightedObject>().rightHandRay = true;
                    //currenHighlightedObject.GetComponent<HighlightedObject>().ActivateSelect();
                }
            }
            else
            {
                objectHitRight = false;

                lineRenderer.material = normal;

                //rightHandDeselect.Invoke();
            }
        }
        else if (objectHitRight)
        {
            objectHitRight = false;

            lineRenderer.material = normal;

            //rightHandDeselect.Invoke();
        }

        lineRenderer.SetPosition(0, targetPosition);
        lineRenderer.SetPosition(1, endPosition);
    }
}