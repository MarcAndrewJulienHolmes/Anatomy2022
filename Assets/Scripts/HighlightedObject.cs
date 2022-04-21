using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class HighlightedObject : MonoBehaviour
{
    private RightHandPointer rightHandPointer;
    private LeftHandPointer leftHandPointer;
    public Outline outline;

    public bool leftHandRay;
    public bool rightHandRay;

    private bool buttonTwoPressed;
    private bool thumbStickDown;

    public float speed;
   
    private GameObject rightHand, leftHand;

    public GameObject selectedObject;
    public Vector3 rightOffset, leftOffset, originalRightOffset, originalLeftOffset;
    public Transform hitPoint;
    public bool offsetSet, beenMoved;


    private void Awake()
    {
        outline = GetComponentInChildren<Outline>();
        rightHand = GameObject.FindWithTag("PlayerRightHand");
        leftHand = GameObject.FindWithTag("PlayerLeftHand");

        rightHandPointer = rightHand.GetComponent<RightHandPointer>();
        leftHandPointer = leftHand.GetComponent<LeftHandPointer>();

        selectedObject = transform.gameObject;
        hitPoint = selectedObject.transform;


    }


    // Start is called before the first frame update
    void Start()
    {
        //ActivateSelect();
        rightHandPointer.rightHandSelect.AddListener(RightHandMove);
        //rightHandPointer.rightHandMove.AddListener(RightHandMove);
        rightHandPointer.rightHandDeselect.AddListener(DeactivateSelect);
    }

    // Update is called once per frame
    void Update()
    {
        buttonTwoPressed = OVRInput.Get(OVRInput.Button.Two);
        thumbStickDown = OVRInput.Get(OVRInput.Button.SecondaryThumbstickDown);

        if (OVRInput.Get(OVRInput.Button.SecondaryThumbstickDown))
        {
            rightOffset = new Vector3 (0f, 0f, 0f);

            beenMoved = true;

            //rightHandPointer.ActiveLineRenderer(rightHandPointer.transform.position, rightHandPointer.transform.forward, rightHandPointer.flexibleLineLength - 0.05f);

            //rightHandPointer.flexibleLineLength = rightHandPointer.flexibleLineLength - 0.005f;


            //Debug.LogError("Thumb down");
        }


        if (OVRInput.Get(OVRInput.Button.SecondaryThumbstickUp))
        {
            rightOffset = rightOffset * 1;

            beenMoved = true;


            //rightHandPointer.ActiveLineRenderer(rightHandPointer.transform.position, rightHandPointer.transform.forward, rightHandPointer.flexibleLineLength + 0.05f);


            //rightHandPointer.flexibleLineLength = rightHandPointer.flexibleLineLength + 0.005f;


            //var step = speed * Time.deltaTime; // calculate distance to move
            //var inverse = 
            //grabPoint.transform.position = Vector3.MoveTowards(grabPoint.transform.position, moveAwayTargetPosition, step);
            ////Debug.LogError("Thumb up");
        }

        if(!OVRInput.Get(OVRInput.Button.SecondaryThumbstickDown) && !OVRInput.Get(OVRInput.Button.SecondaryThumbstickUp) && beenMoved)
        {
            ResetOffset();
        }


        //rightHandTargetPosition = rightHand.transform;
        //moveAwayTargetPosition = new Vector3(rightHand.transform.position.x, rightHand.transform.position.y, rightHand.transform.position.z);

        //if (leftHandRay || rightHandRay)
        //{
        //    ActivateSelect();

        //    if (rightHandRay)
        //    {
        //        SetOffset();

        //        hitPoint.transform.position = rightHandPointer.endPosition;

        //        selectedObject.transform.position = hitPoint.transform.position + rightOffset;
        //    }

        //if (leftHandRay)
        //{
        //    grabPoint.transform.position = leftHandPointer.endPosition;

        //}



        //}
        //else
        //{
        //    DeactivateSelect();
        //}

        if (buttonTwoPressed)
        {
            DeactivateSelect();
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

    public void RightHandMove()
    {
        ActivateSelect();

        hitPoint.transform.position = rightHandPointer.endPosition;

        selectedObject.transform.position = hitPoint.transform.position + rightOffset;
        
    }

    public void DeactivateSelect()
    {
        outline.OutlineWidth = 0;
        rightHandPointer.flexibleLineLength = 2f;
        //offsetSet = false;

        leftHandRay = false;
        rightHandRay = false;
    }

    public void MoveTowardsHand()
    {

    }
}
