using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class SelectedObject : MonoBehaviour
{
    private RightHandPointer rightHandPointer;
    //private LeftHandPointer leftHandPointer;
    private Outline outline;
    private SFXManager sfxManager;


    public OnboardingManager onboardingManager;
    public GameObject onboardingHolder;


    private GameObject rightHand, leftHand;
    private GameObject SFXHolder;
    public GameObject thisGameObject;

    public bool leftHandRay;
    public bool rightHandRay;

    public string thisGameObjectName, thisGameObjectTag;

    public Vector3 rightOffset, originalRightOffset;
    public Quaternion originalRotation;

    public Transform hitPoint;
    public bool atAttachPoint, atOriginPoint;

    public bool selected;

    public Vector3 origin;


    private void Awake()
    {
        rightHand = GameObject.FindWithTag("PlayerRightHand");

        rightHandPointer = rightHand.GetComponent<RightHandPointer>();

        SFXHolder = GameObject.Find("--- AUDIO ---");
        sfxManager = SFXHolder.GetComponent<SFXManager>();

        onboardingHolder = GameObject.Find("---ONBOARDING ---");
        onboardingManager = onboardingHolder.GetComponent<OnboardingManager>();

        outline = GetComponentInChildren<Outline>();

        thisGameObject = transform.gameObject;
        thisGameObjectName = transform.gameObject.name;
        thisGameObjectTag = transform.gameObject.tag;
        hitPoint = thisGameObject.transform;

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

            thisGameObject.transform.rotation = rightHand.transform.rotation ;
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
        sfxManager.PlaySelectTone();
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
        sfxManager.PlayReturnTone();
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
            sfxManager.PlayReturnTone();
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

