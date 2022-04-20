using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private Transform rightHandTargetPosition;
    private Vector3 moveAwayTargetPosition;

    private GameObject rightHand, leftHand;

    public GameObject grabPoint;


    private void Awake()
    {
        outline = GetComponentInChildren<Outline>();
        rightHand = GameObject.FindWithTag("PlayerRightHand");
        leftHand = GameObject.FindWithTag("PlayerLeftHand");

        rightHandPointer = rightHand.GetComponent<RightHandPointer>();
        leftHandPointer = leftHand.GetComponent<LeftHandPointer>();
        grabPoint = transform.gameObject;

    }


    // Start is called before the first frame update
    void Start()
    {

        ActivateOutline();
    }

    // Update is called once per frame
    void Update()
    {
        buttonTwoPressed = OVRInput.Get(OVRInput.Button.Two);
        thumbStickDown = OVRInput.Get(OVRInput.Button.SecondaryThumbstickDown);

        //rightHandTargetPosition = rightHand.transform;
        //moveAwayTargetPosition = new Vector3(rightHand.transform.position.x, rightHand.transform.position.y, rightHand.transform.position.z);

        if (leftHandRay || rightHandRay)
        {
            ActivateOutline();

            if (rightHandRay)
            {
                var hitPoint = rightHandPointer.highlightedObjectPostition;

                grabPoint.transform.position = hitPoint;

                if (OVRInput.Get(OVRInput.Button.SecondaryThumbstickDown))
                {
                    rightHandPointer.flexibleLineLength = rightHandPointer.flexibleLineLength - 0.05f;

                    //var step = speed * Time.deltaTime; // calculate distance to move
                    //grabPoint.transform.position = Vector3.MoveTowards(grabPoint.transform.position, rightHandTargetPosition.position, step);
                    ////Debug.LogError("Thumb down");
                }

                if (OVRInput.Get(OVRInput.Button.SecondaryThumbstickUp))
                {
                    rightHandPointer.flexibleLineLength = rightHandPointer.flexibleLineLength + 0.05f;


                    //var step = speed * Time.deltaTime; // calculate distance to move
                    //var inverse = 
                    //grabPoint.transform.position = Vector3.MoveTowards(grabPoint.transform.position, moveAwayTargetPosition, step);
                    ////Debug.LogError("Thumb up");
                }
            }

            if (leftHandRay)
            {
                grabPoint.transform.position = leftHandPointer.endPosition;

            }

            if (OVRInput.Get(OVRInput.Button.SecondaryThumbstickDown))
            {


                //var step = speed * Time.deltaTime; // calculate distance to move
                //grabPoint.transform.position = Vector3.MoveTowards(grabPoint.transform.position, rightHandTargetPosition.position, step);
                ////Debug.LogError("Thumb down");
            }

            if (OVRInput.Get(OVRInput.Button.SecondaryThumbstickUp))
            {
                //var step = speed * Time.deltaTime; // calculate distance to move
                //var inverse = 
                //grabPoint.transform.position = Vector3.MoveTowards(grabPoint.transform.position, moveAwayTargetPosition, step);
                ////Debug.LogError("Thumb up");
            }

        }
        else
        {
            DeactivateOutline();
        }

        if (buttonTwoPressed)
        {
            leftHandRay = false;
            rightHandRay = false;
        }
    }

    public void ActivateOutline()
    {
        outline.OutlineWidth = 5;
    }

    public void DeactivateOutline()
    {
        outline.OutlineWidth = 0;
    }

    public void MoveTowardsHand()
    {

    }
}
