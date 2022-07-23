using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrientatedObjectAttacher : MonoBehaviour
{
    [Header("Scripts")]
    public CustomPointer rightCustomPointer;
    public CustomPointer leftCustomPointer;
    public GameObject rightHand;
    public GameObject leftHand;
    public BoneNameQuiz boneNameQuiz;
    public OnboardingManager onboardingManager;
    public GameObject onboardingHolder;

    [Header("Scene Identifier")]
    public bool skeletalScene;
    public bool muscleLearningScene;
    public bool muscleTestingScene;

    [Header("This Game Object")]
    public GameObject thisGameObject;
    public string thisGameObjectName;
    public bool startOfSequence, endOfSequence;

    [Header("Main Skeleton")]
    public GameObject skeletonAttachObject;
    public GameObject skeletonReplaceObject;
    public GameObject skeletonInvalidObject;
    public GameObject[] nextInSequenceSkeletonTurnOn;
    public GameObject[] nextInSequenceSkeletonTurnOff;

    [Header("Audio Feedback")]
    public GameObject audiosourceHolder;
    public AudioSource audioSource;

    [Header("Collider Elements")]
    public MaleToFemaleColliderChecker[] maleToFemaleColliderCheckers;
    public int currentEnteredCollidersInt;
    public int totalNumberOfColliders;





    private void Awake()
    {
        rightHand = GameObject.FindWithTag("PlayerRightHand");
        leftHand = GameObject.FindWithTag("PlayerLeftHand");
        rightCustomPointer = rightHand.GetComponent<CustomPointer>();
        leftCustomPointer = leftHand.GetComponent<CustomPointer>();


        thisGameObject = transform.gameObject;
        thisGameObjectName = transform.name;

        if (skeletalScene)
        {
            onboardingHolder = GameObject.Find("---ONBOARDING ---");
            onboardingManager = onboardingHolder.GetComponent<OnboardingManager>();

            boneNameQuiz = FindObjectOfType<BoneNameQuiz>();
            skeletonAttachObject = GameObject.Find(thisGameObjectName + " Attach");
            skeletonReplaceObject = GameObject.Find(thisGameObjectName + " Replace");
            skeletonInvalidObject = GameObject.Find(thisGameObjectName + " Invalid");

            skeletonReplaceObject.SetActive(false);
        }


        audiosourceHolder = GameObject.FindGameObjectWithTag("AttachSFX");
        audioSource = audiosourceHolder.GetComponent<AudioSource>();

        totalNumberOfColliders = maleToFemaleColliderCheckers.Length;
    }

    public void Start()
    {
        if (skeletalScene)
        {
            if (startOfSequence)
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
    }

    public void CheckColliders()
    {
        //Debug.LogError("Collider check started");

        if (currentEnteredCollidersInt == maleToFemaleColliderCheckers.Length)
        {
            ConnectBoneToSkeleton();
            Debug.LogError(thisGameObjectName + " all colliders detected");
        }
    }


    public void ConnectBoneToSkeleton()
    {
        rightCustomPointer.holdingObject = false;
        rightCustomPointer.linePointerOn = true;

        leftCustomPointer.holdingObject = false;
        leftCustomPointer.linePointerOn = true;

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