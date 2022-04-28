using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class HighlightedObject : MonoBehaviour
{
    public RightHandPointer rightHandPointer;
    public LeftHandPointer leftHandPointer;
    public Outline outline;
    public BoneConnector boneConnectorScript;

    public bool leftHandRay;
    public bool rightHandRay;
    public bool moving;

    //public bool buttonTwoPressed;
    //public bool thumbStickDown;

    //public float speed;

    public GameObject rightHand, leftHand;

    public GameObject thisGameObject;
    public string thisGameObjectName, thisGameObjectTag;

    public GameObject skeletonAttachObject;

    public Vector3 rightOffset, originalRightOffset;
    public Transform hitPoint;
    public bool offsetSet, atAttachPoint;

    public Vector3 origin;


    private void Awake()
    {
        rightHand = GameObject.FindWithTag("PlayerRightHand");
        leftHand = GameObject.FindWithTag("PlayerLeftHand");

        rightHandPointer = rightHand.GetComponent<RightHandPointer>();
        leftHandPointer = leftHand.GetComponent<LeftHandPointer>();

        outline = GetComponentInChildren<Outline>();

        //skeletonAttachObject = GameObject.FindWithTag("SkeletonAttach");
        boneConnectorScript = GetComponent<BoneConnector>();


        thisGameObject = transform.gameObject;
        thisGameObjectName = transform.gameObject.name;
        thisGameObjectTag = transform.gameObject.tag;
        hitPoint = thisGameObject.transform;

        origin = GameObject.Find(thisGameObjectName + " Origin").transform.position;

    }


    // Start is called before the first frame update
    void Start()
    {
        //rightHandPointer.rightHandSelect.AddListener(ActivateSelect);
        rightHandPointer.rightHandDeselect.AddListener(DeactivateSelect);
        //rightHandPointer.rightHandMove.AddListener(MoveTowardsAttach);

        thisGameObject.transform.position = origin;

        SetUpOutline();
    }

    // Update is called once per frame
    void Update()
    {
        if (atAttachPoint)
        {
            thisGameObject.transform.position = rightHand.transform.position;
        }

        //if (!atAttachPoint && rightHandRay)
        //{
        //    //ResetOffset();
        //    hitPoint.transform.position = rightHandPointer.endPosition;
        //    thisGameObject.transform.position = hitPoint.transform.position + rightOffset;
        //}
    }

    public void SetUpOutline()
    {
        outline.OutlineMode = Outline.Mode.OutlineAll;
        outline.OutlineColor = new Color(0f, 255f, 250f);
        outline.OutlineWidth = 0f;
    }

    public void SetOffset()
    {
        if (!offsetSet)
        {
            if (rightHandRay)
            {
                originalRightOffset = thisGameObject.transform.position - rightHandPointer.endPosition;
                rightOffset = originalRightOffset;
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

    public void DeactivateSelect()
    {
        outline.OutlineWidth = 0;
        offsetSet = false;
        atAttachPoint = false;
        leftHandRay = false;
        rightHandRay = false;
    }


    public void MoveTowardsAttach()
    {
        //Vector3 attachLPos = rightHand.transform.position;
        //Vector3 attachWPos = transform.TransformPoint(attachLPos);


        //Vector3 objectLPos = thisGameObject.transform.position;
        //Vector3 objectWPos = transform.TransformPoint(objectLPos);

        //Vector3 hitLPos = rightHandPointer.endPosition;
        //Vector3 hitWPos = transform.TransformPoint(hitLPos);

        //if (attachWPos == objectWPos)
        //{
        //    ResetOffset();
        //    beenMoved = true;
        //    atAttachPoint = true;
        //    Debug.LogError("At attch point");
        //}
        //else
        //{
        //    rightOffset = new Vector3(0f, 0f, 0f);
        //    hitPoint.transform.position = rightHandPointer.endPosition;
        //    thisGameObject.transform.position = hitPoint.transform.position + rightOffset;
        //    beenMoved = true;
        //}

        if (!atAttachPoint && rightHandRay)
        {
            moving = true;
            rightOffset = new Vector3(0f, 0f, 0f);
            hitPoint.transform.position = rightHandPointer.endPosition;
            thisGameObject.transform.position = hitPoint.transform.position + rightOffset;
        }
        else
        {

        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (!boneConnectorScript.isConnected)
        {
            if (other.tag == "PlayerRightHand")
            {
                atAttachPoint = true;
            }
        }
    }
}
