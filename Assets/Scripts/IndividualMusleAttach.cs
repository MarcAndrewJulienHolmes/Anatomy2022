using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndividualMusleAttach : MonoBehaviour
{
    [Header("Scripts")]
    public CustomPointer rightHandPointer, leftHandPointer;
    public GameObject rightHand, leftHand;

    public MuscleLearningScenarioSetup muscleLearningScenarioSetup;

    [Header("This Game Object")]
    public GameObject thisGameObject;
    public string thisGameObjectName;

    [Header("Muscle Attach Object")]
    public string attachObjectName;


    [Header("Audio Feedback")]
    public GameObject audiosourceHolder;
    public AudioSource audioSource;





    private void Awake()
    {
        rightHand = GameObject.FindWithTag("PlayerRightHand");
        rightHandPointer = rightHand.GetComponent<CustomPointer>();
        leftHand = GameObject.FindWithTag("PlayerLeftHand");
        leftHandPointer = leftHand.GetComponent<CustomPointer>();

        muscleLearningScenarioSetup = GameObject.FindGameObjectWithTag("Scriptholder").GetComponent<MuscleLearningScenarioSetup>();

        thisGameObject = transform.gameObject;
        thisGameObjectName = transform.name;

        attachObjectName = thisGameObjectName + " Ref";

        audiosourceHolder = GameObject.FindGameObjectWithTag("AttachSFX");
        audioSource = audiosourceHolder.GetComponent<AudioSource>();
    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.name == attachObjectName)
        {
            ConnectIndividualMuscleToMainModel();
        }
        else
        {
            return;
        }
    }

    public void ConnectIndividualMuscleToMainModel()
    {
        muscleLearningScenarioSetup.LearningMuscleCount();
        rightHandPointer.holdingObject = false;
        rightHandPointer.linePointerOn = true;
        leftHandPointer.holdingObject = false;
        leftHandPointer.linePointerOn = true;
        thisGameObject.SetActive(false);
        audioSource.Play();
    }
}


