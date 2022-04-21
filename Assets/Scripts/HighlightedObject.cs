using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class HighlightedObject : MonoBehaviour
{
    public RightHandPointer rightHandPointer;
    public LeftHandPointer leftHandPointer;
    public Outline outline;

    public bool leftHandRay;
    public bool rightHandRay;

    //public bool buttonTwoPressed;
    public bool thumbStickDown;

    public float speed;

    public GameObject rightHand, leftHand, rightHandAttach;

    public GameObject selectedObject;
    public Vector3 rightOffset, leftOffset, originalRightOffset, originalLeftOffset;
    public Transform hitPoint;
    public bool offsetSet, beenMoved, atAttachPoint;


    private void Awake()
    {
        outline = GetComponentInChildren<Outline>();
        rightHand = GameObject.FindWithTag("PlayerRightHand");
        leftHand = GameObject.FindWithTag("PlayerLeftHand");
        //rightHandAttach = GameObject.FindWithTag("PlayerRightHandAttach");

        rightHandPointer = rightHand.GetComponent<RightHandPointer>();
        leftHandPointer = leftHand.GetComponent<LeftHandPointer>();

        selectedObject = transform.gameObject;
        hitPoint = selectedObject.transform;


    }


    // Start is called before the first frame update
    void Start()
    {
        rightHandPointer.rightHandSelect.AddListener(ActivateSelect);
        rightHandPointer.rightHandDeselect.AddListener(DeactivateSelect);
        rightHandPointer.rightHandMove.AddListener(MoveTowardsAttach);
    }

    // Update is called once per frame
    void Update()
    {

        if (OVRInput.Get(OVRInput.Button.SecondaryThumbstickUp))
        {
            rightOffset = rightOffset * 0.1f;

            beenMoved = true;

        }

        if(!OVRInput.Get(OVRInput.Button.SecondaryThumbstickDown) && !OVRInput.Get(OVRInput.Button.SecondaryThumbstickUp) && beenMoved)
        {
            ResetOffset();
        }

        if (atAttachPoint)
        {
            selectedObject.transform.position = rightHand.transform.position;

        }
    }

    public void SetOffset()
    {
        if (!offsetSet)
        {
            if (rightHandRay)
            {
                originalRightOffset = selectedObject.transform.position - rightHandPointer.endPosition;
                rightOffset = originalRightOffset;
            }

            if (leftHandRay)
            {
                originalLeftOffset = selectedObject.transform.position - leftHandPointer.endPosition;
                leftOffset = originalLeftOffset;
            }

            offsetSet = true;
        }        
    }

    public void ResetOffset()
    {
        rightOffset = originalRightOffset;
    }

    public void ActivateSelect()
    {
        outline.OutlineWidth = 5;
        SetOffset();
    }

    //public void RightHandMove()
    //{
    //    ActivateSelect();

    //    hitPoint.transform.position = rightHandPointer.endPosition;

    //    selectedObject.transform.position = hitPoint.transform.position + rightOffset;
        
    //}

    public void DeactivateSelect()
    {
        outline.OutlineWidth = 0;
        rightHandPointer.flexibleLineLength = 3f;
        offsetSet = false;

        atAttachPoint = false;

        leftHandRay = false;
        rightHandRay = false;
    }

    public void MoveTowardsAttach()
    {
        Vector3 attachLPos = rightHand.transform.position;
        Vector3 attachWPos = transform.TransformPoint(attachLPos);


        Vector3 objectLPos = selectedObject.transform.position;
        Vector3 objectWPos = transform.TransformPoint(objectLPos);

        if (attachWPos == objectWPos)
        {
            ResetOffset();
            beenMoved = true;
            atAttachPoint = true;
            Debug.LogError("At attch point");
        }
        else
        {
            rightOffset = new Vector3(0f, 0f, 0f);
            hitPoint.transform.position = rightHandPointer.endPosition;
            selectedObject.transform.position = hitPoint.transform.position + rightOffset;
            beenMoved = true;
        }
    }
}
