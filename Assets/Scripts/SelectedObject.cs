using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class SelectedObject : MonoBehaviour
{
    public RightHandPointer rightHandPointer;
    public LeftHandPointer leftHandPointer;
    public Outline outline;
    public BoneConnector boneConnectorScript;


    public GameObject rightHand, leftHand;

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
        leftHand = GameObject.FindWithTag("PlayerLeftHand");

        rightHandPointer = rightHand.GetComponent<RightHandPointer>();
        leftHandPointer = leftHand.GetComponent<LeftHandPointer>();

        outline = GetComponentInChildren<Outline>();

        boneConnectorScript = GetComponent<BoneConnector>();


        thisGameObject = transform.gameObject;
        thisGameObjectName = transform.gameObject.name;
        thisGameObjectTag = transform.gameObject.tag;
        hitPoint = thisGameObject.transform;

        outline = thisGameObject.GetComponent<Outline>();

        origin = GameObject.Find(thisGameObjectName + " Origin").transform.position;

    }


    // Start is called before the first frame update
    void Start()
    {
        rightHandPointer.rightHandDeselect.AddListener(DeactivateSelect);

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
        //SetOffset();
    }

    public void DeactivateSelect()
    {
        outline.OutlineWidth = 0;
        //offsetSet = false;
        atAttachPoint = false;
        leftHandRay = false;
        rightHandRay = false;
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
        if (!atOriginPoint)
        {
            thisGameObject.transform.position = origin;
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
            else
            {
                atAttachPoint = false;
            }
        }
        else
        {
            atAttachPoint = false;
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

