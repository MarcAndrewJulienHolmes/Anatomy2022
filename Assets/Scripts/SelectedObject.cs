using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class SelectedObject : MonoBehaviour
{
    [Header("Scene Specific")]
    public string sceneName;

    [Header("Scripts")]
    private RightHandPointer rightHandPointer;
    private Outline outline;
    public OnboardingManager onboardingManager;
    public GameObject onboardingHolder;

    [Header("This Game Object")]
    public GameObject thisGameObject;
    public string thisGameObjectName;

    [Header("Attach Specific")]
    public GameObject rightHand; 
    public GameObject rightHandAttach;
    public Quaternion originalRotation;
    public Vector3 origin;


    public bool leftHandRay;
    public bool rightHandRay;
    public bool atAttachPoint, atOriginPoint;
    public bool selected;

        


    private void Awake()
    {
        rightHand = GameObject.FindWithTag("PlayerRightHand");
        rightHandAttach = GameObject.FindWithTag("PlayerRightHandAttach");

        rightHandPointer = rightHand.GetComponent<RightHandPointer>();

        onboardingHolder = GameObject.Find("---ONBOARDING ---");
        onboardingManager = onboardingHolder.GetComponent<OnboardingManager>();

        outline = GetComponentInChildren<Outline>();

        thisGameObject = transform.gameObject;
        thisGameObjectName = transform.gameObject.name;


        outline = thisGameObject.GetComponentInChildren<Outline>();

        originalRotation = thisGameObject.transform.rotation;

    }


    // Start is called before the first frame update
    void Start()
    {
        rightHandPointer.rightHandDeselect.AddListener(DeactivateSelect);

        rightHandPointer.rightHandReturnOrigin.AddListener(ReturnToOrigin);

        rightHandPointer.rightHandMoveTowards.AddListener(MoveToAttach);

        SetUpOutline();
    }

    // Update is called once per frame
    void Update()
    {
        if (atAttachPoint)
        {
            thisGameObject.transform.position = rightHand.transform.position;

            thisGameObject.transform.rotation = rightHandAttach.transform.rotation;
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
        if (!onboardingManager.selectBone)
        {
            onboardingManager.selectBone = true;
            onboardingManager.UpdateChecklist();
        }
        outline.OutlineWidth = 5;
        selected = true;
        rightHandPointer.linePointerOn = false;
    }

    public void DeactivateSelect()
    {
        onboardingManager.deselectBone = true;
        onboardingManager.UpdateChecklist();
        outline.OutlineWidth = 0;
        atAttachPoint = false;
        leftHandRay = false;
        rightHandRay = false;
        rightHandPointer.holdingObject = false;
        selected = false;
        rightHandPointer.linePointerOn = true;

    }


    public void MoveToAttach()
    {
        if (!atAttachPoint && selected)
        {
            thisGameObject.transform.position = rightHand.transform.position;
            onboardingManager.moveBone = true;
            onboardingManager.UpdateChecklist();

        }
        else
        {

        }
    }

    public void ReturnToOrigin()
    {
        if (!atOriginPoint && selected)
        {
            atAttachPoint = false;
            thisGameObject.transform.position = origin;
            thisGameObject.transform.rotation = originalRotation;
            onboardingManager.returnBoneToOrigin = true;
            onboardingManager.UpdateChecklist();
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

