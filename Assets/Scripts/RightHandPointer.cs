using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class RightHandPointer : MonoBehaviour
{
    public LineRenderer lineRenderer;

    public QuizButton[] quizButton;

    public Material normal, highlighted;

    private float lineWidth = 0.1f;
    private float flexibleLineLength;

    public int layerMask = 1;

    public Vector3 endPosition;

    public bool toggled;

    public bool objectHitRight = false;

    public bool holdingObject;

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
        else
        {
            for (int i = 0; i < quizButton.Length; i++)
            {
                quizButton[i].rightHandRay = false;
            }
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
            {
                currentHighlightedObjectName = pointObject.GetComponent<SelectedObject>().thisGameObjectName;
                currenHighlightedObject = GameObject.Find(currentHighlightedObjectName);
                objectHitRight = true;
                lineRenderer.material = highlighted; 

                if (OVRInput.Get(OVRInput.Button.SecondaryHandTrigger))
                {
                    if (!holdingObject)
                    {
                        currenHighlightedObject.GetComponent<SelectedObject>().rightHandRay = true;
                        currenHighlightedObject.GetComponent<SelectedObject>().ActivateSelect();
                        holdingObject = true;
                    }
                }
            }

            else if (pointObject.GetComponent<QuizButton>())
            {
                currentHighlightedObjectName = pointObject.GetComponent<QuizButton>().thisGameObjectName;
                currenHighlightedObject = GameObject.Find(currentHighlightedObjectName);
                lineRenderer.material = highlighted;

                for (int i = 0; i < quizButton.Length; i++)
                {
                    if (quizButton[i].name == currentHighlightedObjectName)
                    {
                        quizButton[i].rightHandRay = true;
                    }
                }
                
                if (OVRInput.Get(OVRInput.Button.SecondaryHandTrigger))
                {
                    pointObject.GetComponent<QuizButton>().ButtonSelect();
                }
            }

            else if (!pointObject.GetComponent<QuizButton>() && !pointObject.GetComponent<SelectedObject>())
            {
                for (int i = 0; i < quizButton.Length; i++)
                {
                    quizButton[i].rightHandRay = false;
                }            

                currentHighlightedObjectName = "";

                objectHitRight = false;

                lineRenderer.material = normal;

                //rightHandDeselect.Invoke();
            }
        }
        else if (objectHitRight)
        {
            objectHitRight = false;

            lineRenderer.material = normal;

            for (int i = 0; i < quizButton.Length; i++)
            {
                quizButton[i].rightHandRay = false;
            }

            //rightHandDeselect.Invoke();
        }

        lineRenderer.SetPosition(0, targetPosition);
        lineRenderer.SetPosition(1, endPosition);
    }
}
