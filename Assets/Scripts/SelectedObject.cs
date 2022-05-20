using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class SelectedObject : MonoBehaviour
{
    private RightHandPointer rightHandPointer;
    private LeftHandPointer leftHandPointer;
    private Outline outline;
    private BoneToSkeletonAttach boneToSkeletonAttach;
    private OriginRandomiser originRandomiser;

    private GameObject rightHand, leftHand;
    public GameObject thisGameObject;

    public bool leftHandRay;
    public bool rightHandRay;

    public string thisGameObjectName, thisGameObjectTag;

    public Vector3 rightOffset, originalRightOffset;
    public Transform hitPoint;
    public bool atAttachPoint, atOriginPoint;

    public Vector3 origin;


    private void Awake()
    {
        rightHand = GameObject.FindWithTag("PlayerRightHand");
        //leftHand = GameObject.FindWithTag("PlayerLeftHand");

        rightHandPointer = rightHand.GetComponent<RightHandPointer>();
        //leftHandPointer = leftHand.GetComponent<LeftHandPointer>();

        outline = GetComponentInChildren<Outline>();

        boneToSkeletonAttach = GetComponent<BoneToSkeletonAttach>();
        originRandomiser = GetComponent<OriginRandomiser>();

        thisGameObject = transform.gameObject;
        thisGameObjectName = transform.gameObject.name;
        thisGameObjectTag = transform.gameObject.tag;
        hitPoint = thisGameObject.transform;

        outline = thisGameObject.GetComponentInChildren<Outline>();

        //origin = GameObject.Find(thisGameObjectName + " Origin").transform.position;

    }


    // Start is called before the first frame update
    void Start()
    {
        rightHandPointer.rightHandDeselect.AddListener(DeactivateSelect);

        //thisGameObject.transform.position = origin;

        SetUpOutline();
    }

    // Update is called once per frame
    void Update()
    {
        if (atAttachPoint)
        {
            thisGameObject.transform.position = rightHand.transform.position;
        }
    }

    public void SetUpOutline()
    {
        outline.OutlineMode = Outline.Mode.OutlineAll;
        outline.OutlineColor = new Color(0f, 255f, 250f);
        outline.OutlineWidth = 0f;
    }


    public void ActivateSelect()
    {
        outline.OutlineWidth = 5;
    }

    public void DeactivateSelect()
    {
        outline.OutlineWidth = 0;
        atAttachPoint = false;
        leftHandRay = false;
        rightHandRay = false;
        rightHandPointer.holdingObject = false;
    }


    public void MoveToAttach()
    {
        if (!atAttachPoint && rightHandRay)
        {
            thisGameObject.transform.position = rightHand.transform.position;
        }
        else
        {

        }
    }

    public void ReturnToOrigin()
    {
        if (!atOriginPoint && rightHandRay)
        {
            atAttachPoint = false;

            thisGameObject.transform.position = origin;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerRightHand" && rightHandRay)
        {
            atAttachPoint = true;
        }

       
        if (other.tag == "Origin")
        {
            atOriginPoint = true;
        }
        else
        {
            atOriginPoint = false;
        }
    }


}

