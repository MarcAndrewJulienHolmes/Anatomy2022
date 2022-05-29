using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneToSkeletonAttach : MonoBehaviour
{
    public RightHandPointer rightHandPointer;

    public BoneNameQuiz boneNameQuiz;

    public GameObject rightHand;

    public GameObject thisGameObject;

    public string thisGameObjectName;

    public GameObject skeletonAttachObject;

    public GameObject skeletonReplaceObject;

    public GameObject skeletonInvalidObject;

    public GameObject[] nextInSequenceSkeletonTurnOn;

    public GameObject[] nextInSequenceSkeletonTurnOff;

    public string skeletonAttachObjectName;

    public AudioSource audioSource;

    public bool startOfSequence, endOfSequence;

    public OnboardingManager onboardingManager;
    public GameObject onboardingHolder;


    private void Awake()
    {
        rightHand = GameObject.FindWithTag("PlayerRightHand");

        rightHandPointer = rightHand.GetComponent<RightHandPointer>();

        boneNameQuiz = FindObjectOfType<BoneNameQuiz>();

        thisGameObject = transform.gameObject;

        thisGameObjectName = transform.name;

        skeletonAttachObject = GameObject.Find(thisGameObjectName + " Attach");

        skeletonReplaceObject = GameObject.Find(thisGameObjectName + " Replace");

        skeletonInvalidObject = GameObject.Find(thisGameObjectName + " Invalid");

        skeletonAttachObjectName = skeletonAttachObject.name;

        audioSource = skeletonReplaceObject.GetComponent<AudioSource>();

        skeletonReplaceObject.SetActive(false);

        onboardingHolder = GameObject.Find("---ONBOARDING ---");
        onboardingManager = onboardingHolder.GetComponent<OnboardingManager>();
    }

    public void Start()
    {
        if(startOfSequence)
        {
            skeletonAttachObject.SetActive(true);
            skeletonReplaceObject.SetActive(false);
            skeletonInvalidObject.SetActive(false);
        }
        else
        {
            skeletonAttachObject.SetActive(false);
            skeletonReplaceObject.SetActive(false);
            skeletonInvalidObject.SetActive(true);
        }
    }



    public void OnTriggerEnter(Collider other)
    {        
        if (other.name == skeletonAttachObject.name)
        {
            ConnectBoneToSkeleton();
        }
        else        
        {        
            return;          
        }
    }

    public void ConnectBoneToSkeleton()
    {
        rightHandPointer.holdingObject = false;

        thisGameObject.SetActive(false);

        skeletonAttachObject.SetActive(false);

        skeletonReplaceObject.SetActive(true);

        audioSource.Play();

        boneNameQuiz.lastBoneConnected = thisGameObjectName;

        boneNameQuiz.GenerateQuiz();

        onboardingManager.attachBone = true;
        onboardingManager.UpdateChecklist();

        if (!endOfSequence)
        {
            for (int i = 0; i < nextInSequenceSkeletonTurnOff.Length; i++)
            {
                nextInSequenceSkeletonTurnOff[i].SetActive(false);
            }

            for (int i = 0; i < nextInSequenceSkeletonTurnOn.Length; i++)
            {
                nextInSequenceSkeletonTurnOn[i].SetActive(true);
            }
        }
    }
}
