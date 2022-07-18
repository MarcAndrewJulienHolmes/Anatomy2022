//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class LeftHandPointer : MonoBehaviour
//{
//    public LineRenderer lineRenderer;

//    public float lineWidth = 0.1f;
//    public float lineMaxLength;

//    public Vector3 endPosition;

//    public bool toggled;

//    private float handLeft = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger);

//    public bool objectHitLeft = false;

//    private GameObject pointObject;

//    // Start is called before the first frame update
//    void Start()
//    {
//        Vector3[] startLinePositions = new Vector3[2] { Vector3.zero, Vector3.zero };
//        lineRenderer.SetPositions(startLinePositions);
//        lineRenderer.enabled = false;
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        handLeft = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger);

//        if (handLeft > 0.9)
//        {
//            toggled = true;
//            lineRenderer.enabled = true;
//            Debug.LogError("Left hand trigger detected");

//        }
//        else
//        {
//            lineRenderer.enabled = false;
//            toggled = false;
//            objectHitLeft = false;
//        }

//        if (toggled)
//        {
//            ActiveLineRenderer(transform.position, transform.forward, lineMaxLength);
//        }
//    }

//    private void ActiveLineRenderer(Vector3 targetPosition, Vector3 direction, float length)
//    {
//        RaycastHit hit;

//        Ray lineRendererOut = new Ray(targetPosition, direction);

//        endPosition = targetPosition + (length * direction);

//        if (Physics.Raycast(lineRendererOut, out hit))
//        {
//            endPosition = hit.point;

//            pointObject = hit.collider.gameObject;

//            if (pointObject.GetComponent<SelectedObject>())
//            {
//                objectHitLeft = true;

//                pointObject.GetComponent<SelectedObject>().leftHandRay = true;

//                Debug.Log("Object hit value is: " + objectHitLeft);
//            }
//            else
//            {
//                objectHitLeft = false;

//                Debug.Log("Object hit value is: " + objectHitLeft);
//            }
//        }
//        else if (objectHitLeft)
//        {
//            objectHitLeft = false;

//            Debug.Log("Object hit value is: " + objectHitLeft);

//        }

//        lineRenderer.SetPosition(0, targetPosition);
//        lineRenderer.SetPosition(1, endPosition);
//    }
//}
